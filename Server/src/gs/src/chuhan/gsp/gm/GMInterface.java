package chuhan.gsp.gm;

import java.io.File;
import java.lang.reflect.Method;
import java.util.Properties;
import java.util.concurrent.atomic.AtomicBoolean;

import org.apache.log4j.Logger;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.GameProp;

public class GMInterface implements GMInterfaceMXBean {
	private static Logger logger = Logger.getLogger(GMInterface.class);
	
	static Properties prop = chuhan.gsp.main.ConfigManager.getInstance().getPropConf("sys");
	
	private static AtomicBoolean isGMOn = new AtomicBoolean(GameProp.getIntValue(prop, "sys.gm.initalOn") == 1);
	
	public static boolean TARGET_BY_ROLEID = true;
	
	public static void setGMOn(boolean isOn){
		isGMOn.getAndSet(isOn);
	}
	
	public static boolean getGMOn(){
		return isGMOn.get();
	}
	
	/**
	 * 判断玩家是否有gm权限
	 * @param userid
	 * @param roleid
	 * @return
	 */
	public static boolean gmPriv(int userid){
		Integer isgm = xtable.Auuserinfo.selectBlisgm(userid);
		if(isgm == null || isgm!=1){
			return false;
		}
		return true;
	}
	
	public static long getTargetRoleId(String arg)
	{
		//if(TARGET_BY_ROLEID)
		return Long.valueOf(arg);
		//else
		//	return AccountUtil.getRoleIdByUsername(arg);
	}
	
	private static void doGmOperateLog(int gmuserid, long gmroleid, String command,String[] paras){
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < paras.length; i++)
			sb.append(paras[i]).append(" ");
		logger.info("GmCommand,gmuserid="+gmuserid+",gmroleid="+gmroleid+",command="+command+" "+sb.toString());
		/*
		//final long now = java.util.Calendar.getInstance().getTimeInMillis();
		java.util.Map<String, Object> param = new java.util.HashMap<String, Object>();
		param.put(RemoteLogParam.GMUSERID, gmuserid);
		param.put(RemoteLogParam.GMROLEID, gmroleid);
		param.put(RemoteLogParam.GMORDER, command);
		if (paras == null || paras.length == 0)
			param.put(RemoteLogParam.PARAMETER, 0);
		else {
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < paras.length; i++) {
				sb.append(paras[i]).append(" ");
			}
			param.put(RemoteLogParam.PARAMETER, sb.toString());
		}
		LogManager.getInstance().doLog(RemoteLogID.GMOPERATE, param);
	*/}

	public static void doGmOperateLog(int gmuserid, long gmroleid, String operateStr){
		String[] strs = operateStr.split(" ");
		if (strs.length<2){
			doGmOperateLog(gmuserid, gmroleid, strs[0], null);
		}else {
			doGmOperateLog(gmuserid, gmroleid, strs[0], java.util.Arrays.copyOfRange(strs, 1, strs.length));
		}
	}
	/**
	 * for debug only
	 * 
	 * @param cmd
	 *            要执行的命令
	 */
	@Override
	public int execCommand(String cmd) {
		return execCommand(-1,-1,-1, cmd,false);
	}
	
	
	public static int execCommand(int userid,long gmroleid,int localsid, String cmd) {
		return execCommand(userid, gmroleid, localsid, cmd, true);
	}

	/**
	 * 这里面不要涉及protocol
	 * 
	 * @param gmroleid
	 * @param cmd
	 * @param needgmuser 是否需要gm账号才能操作
	 * @return
	 */
	public static int execCommand(int userid,long gmroleid,int localsid, String cmd, boolean needgmuser) {
		// TODO:检查权限
	
		//检查GM权限
		if(needgmuser && !isGMOn.get()){
			if(xtable.Auuserinfo.selectBlisgm(userid)!=1){
				//Message.sendPopByRoleID(gmroleid, "GM开关已经关闭，请联系GM开启！！！");
				return -2;
			}
		}
		
		final String[] argv = cmd.split("\\s+");
		if (argv.length == 0)
			return -2;
		if(argv[0].length() < 2)
			return -2;
		
		argv[0] = argv[0].substring(2);
		if (!argv[0].matches("[a-zA-Z]+"))
			return -2;
		GMCommand obj;
		try {
			obj = (GMCommand) Class.forName(
					GMInterface.class.getPackage().getName() + ".Cmd_"
							+ argv[0].toLowerCase()).newInstance();
			obj.setGmroleid(gmroleid);
			obj.setGmUserid(userid);
			obj.setGmLocalsid(localsid);
		} catch (final ClassNotFoundException ex) {
			return execExtCommand(userid, gmroleid, localsid, argv);//当在正常的包内找不到gm指令时，去ExtJar找
		} catch (final IllegalAccessException ex) {
			logger.error("err", ex);
			return -3;
		} catch (final InstantiationException ex) {
			logger.error("err", ex);
			return -3;
		}
		
		try{
			String[] parameters = java.util.Arrays.copyOfRange(argv, 1, argv.length);
			doGmOperateLog(userid, gmroleid, argv[0].toLowerCase(),parameters);
			return obj.exec(parameters) ? 0 : -4;
		}catch(final NumberFormatException ex){
			logger.error("err",ex);
			obj.sendToGM("参数格式错误："+obj.usage());
			return -5;
		}
	}
	
	private static int execExtCommand(int userid,long gmroleid,int localsid,String[] argv)
	{
		String jarfilename = ConfigManager.getInstance().getPropConf("sys").getProperty("sys.extcommand.filename");
		File jarFile = new File(jarfilename);
		if(!jarFile.exists())
		{
			if(gmroleid > 0)
				//TODO Message.sendPopByRoleID(gmroleid, argv[0].toLowerCase() + " 不是合法的gm指令。");
			return -3;//不存在ext command
		}
		Class<?> cls;
		GMCommand obj;
		ExtraCommandClassLoader clzloader = new ExtraCommandClassLoader(".", jarfilename);
		try
		{
			cls = clzloader.loadClass(GMInterface.class.getPackage().getName() 
					+ ".Cmd_"+ argv[0].toLowerCase());
			obj = (GMCommand)(cls).newInstance();
			obj.setGmroleid(gmroleid);
			obj.setGmUserid(userid);
			obj.setGmLocalsid(localsid);
		} catch (ClassNotFoundException e)
		{
			/*if(gmroleid > 0)
				Message.sendPopByRoleID(gmroleid, argv[0].toLowerCase() + " 不是合法的gm指令。");*/
			return -3;
		} catch (InstantiationException e)
		{
			logger.error("err", e);
			return -3;
		} catch (IllegalAccessException e)
		{
			logger.error("err", e);
			return -3;
		}
		
		try{
			String[] parameters = java.util.Arrays.copyOfRange(argv, 1, argv.length);
			doGmOperateLog(userid, gmroleid, argv[0].toLowerCase(),parameters);
			Method m =cls.getDeclaredMethod("exec", String[].class);//.getMethod("usage", new Class[]{}); 
			m.setAccessible(true);
			boolean succ = (Boolean)(m.invoke(obj, new Object[]{parameters})); 
			return succ ? 0	: -4;
		}catch(final NumberFormatException ex){
			logger.error("err",ex);
			obj.sendToGM("参数格式错误");
			return -5;
		} catch (Exception e)
		{
			logger.error("err",e);
			obj.sendToGM("其他错误");
			return -5;
		}
	}
}
