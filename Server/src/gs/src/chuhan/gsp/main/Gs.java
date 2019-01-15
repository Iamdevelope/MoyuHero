package chuhan.gsp.main;

import gnet.link.Role;

import java.io.IOException;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.atomic.AtomicBoolean;

import org.apache.log4j.Logger;

import chuhan.gsp.LangueVersion;
import chuhan.gsp.log.OpLogManager;

import com.pwrd.op.LogOp;

import xdb.util.JMXServer;

public class Gs {
	private static Logger logger = Logger.getLogger(Gs.class);
	private static GsInfo info = new GsInfo();
	public static AtomicBoolean isShutDown = new AtomicBoolean(false);
	
	private  static javax.management.MBeanServer mbs=java.lang.management.ManagementFactory.getPlatformMBeanServer();
	
	private static JMXServer jmxserver;
	private static SocketServerThread socketServerThread;
	
	static void registerMbean(Object obj,String name) throws javax.management.NotCompliantMBeanException,javax.management.MBeanRegistrationException,javax.management.InstanceAlreadyExistsException,javax.management.MalformedObjectNameException{
		mbs.registerMBean(obj, new javax.management.ObjectName("bean:name="+name));
	}
	
	public static long getStartTime() {
		return info.getStartTime();
	}
	
	public static void init() throws Exception{		
		
		mainThreadid=Thread.currentThread().getId();
		org.apache.log4j.xml.DOMConfigurator.configure("log4j.xml");
		ConfigManager.init("gamedata/xml/auto",new XStreamUnmarshaller(),"properties");
		LangueVersion.init();
		ModuleManager.init();
		ServerStartInit.init();
		
		info.onServerStart();
		jmxserver = new JMXServer(ConfigManager.getRmiPort()+ConfigManager.getGsZoneId(), ConfigManager.getRmiPort() +1000+ ConfigManager.getGsZoneId(), null, null, null);
		jmxserver.start();
		
		//LogOp初始化
		if(OpLogManager.isOn()) {
			LogOp.init("operlog.properties");
		}
		
		//GM工具
		socketServerThread = new SocketServerThread(ConfigManager.getRmiPort() + 2000 +ConfigManager.getGsZoneId());
		socketServerThread.start();
		
		//new PChangeUserID().submit();
	}
	
	public static void docleanup(){
		ModuleManager.getInstance().exit();
	}
	
	private static void shutDownIWebSocket() {
		try {
			socketServerThread.interrupt();
			socketServerThread.serverSocket.close();
		} catch (IOException e) {
			logger.info("关闭iweb socket");	
		}
	}
	
	/**
	 * 注意：在main函数执行之前或返回之后不得调用！
	 * @return
	 */
	public static boolean isInMainThread(){
		return Thread.currentThread().getId()==mainThreadid;
	}
	
	//public static RandomAccessFile enterlog;
	private static long mainThreadid=-1;
	
	public static String[] args;
	/**
	 * @param args
	 */
	public static void main(String[] args) throws Exception {
		logger.info("SERVER STARTING...");
		Gs.args = args;
		init();
		try {
			
			//必须在所有模块都初始化完成之后，再开启网络
			xio.Engine.getInstance().open();
			logger.info("SERVER OPENED");
			
			
			final Stopper stopper=new Stopper();
			registerMbean(stopper,"stopper");
			stopper.doWait();
			logger.info("SERVER SHUTDOWN...");
			shutDownIWebSocket();
            //不再接受任何的协议
            isShutDown.set(true);
            
            onShutdown();
			
			logger.info("BYE");
		} finally {
		}
	}
	
	private static void onShutdown() throws Exception
	{
		//关服时，需要有顺序的处理：
		
		//1.踢所有人下线
        List<Role> roles = new LinkedList<Role>();
        roles.addAll(gnet.link.Onlines.getInstance().getRoles());
        for(Role r : roles)
        {
        	try
        	{
        		//new knight.gsp.state.PRoleOffline(r.getRoleid(),PRoleOffline.TYPE_LINK_BROKEN).submit().get();
        	}
        	catch(Exception e)
        	{
        		e.printStackTrace();
        	}
        	gnet.link.Onlines.getInstance().kick(r.getRoleid(), chuhan.gsp.KickErrConst.ERR_SERVER_SHUTDOWN);
		}
		
		//2.停止战斗
		//knight.gsp.battle.Module.serverShutdown();
		
		//3.转换状态为UnEntryState（结束所有人的下线保护状态）
		//knight.gsp.state.StateManager.serverShutdown();
		
		//4.让link停止监听。不接受新的登录请求，现有的依然处理,之所以挪到这里来是因为如果放在前面的话,因为要踢所有人下线,link又会被trigger listen
//        final gnet.link.LinkServerControl linkcontrol = new gnet.link.LinkServerControl();
//        linkcontrol.ptype = gnet.link.LinkServerControl.E_STOP_LISTEN;
//        for (final gnet.link.Link link : gnet.link.Onlines.getInstance().links()) {
//                linkcontrol.send(link.getXio());
//        }

		docleanup();//执行不用按顺序处理的模块退出
		
		stopJMXServer();
	}
	
	private static void stopJMXServer()
	{
		try {
			Stopper.shutdownCompletedLock.lockInterruptibly();
		}catch(final InterruptedException ex){
			return;
		}
		Stopper.shutdownCompleted.signalAll();
		Stopper.shutdownCompletedLock.unlock();
		jmxserver.stop();
		logger.info("JMX SERVER STOPED");
	}

}
