package chuhan.gsp.main;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Properties;
import java.util.concurrent.atomic.AtomicInteger;

import org.apache.log4j.Logger;

import chuhan.gsp.HotfixConfig;
import chuhan.gsp.mbean.Module;
import chuhan.gsp.util.GameProp;

public class ConfigManager implements ModuleInterface{

	public static Logger logger = Logger.getLogger(ConfigManager.class);
	private final static int DEFAULT_RMI_PORT = 6000;
	private static ConfigManager instance = null;
	
	private static int rmiport = DEFAULT_RMI_PORT;
	public static String CONFIG_PATH = "gamedata/xml/auto/";
	public static String PROPERTY_PATH = "properties";
	public static final String OCTETS_CHARSET_ANSI = "ISO-8859-1";
	public static final String OCTETS_CHARSET_UNICODE = "UTF-16LE";
	public static int SERVER_STATE = 0;
	
	public static int gs_zoneid = 0;
	public static int PLATFORM_TYPE = -1;//被ServerIDResponse协议赋值，未定义时不允许充值
	public static int link_zoneid = 0;//被ServerIDResponse协议赋值，未定义时不允许充值
	
	//public static int PLATFORM_TYPE_WANMEI = 0;
	//public static int PLATFORM_TYPE_91 = 1;
	//public static int PLATFORM_TYPE_PP = 2;
	//public static int PLATFORM_TYPE_APPSTORE = 3;
	
	
	public static long FIRST_START_TIME = 0l;
	public static long THIS_START_TIME = 0l;

	private int loginLimitType = 0;
	private int errorType;
	private List<Integer> userIDList = new LinkedList<Integer>();
	
	private final java.util.Map<String,Object> beannames = new java.util.concurrent.ConcurrentHashMap<String,Object>();
	private final java.util.Map<String,Properties> propnames = new java.util.concurrent.ConcurrentHashMap<String,Properties>();
	//private Unmarshaller unmarshaller;
	
	public synchronized static ConfigManager getInstance() {
		return instance;
	}

	/**
	 * 通过类名来获得有隐患，如果将来类名或包名变化，会获得不到
	 * @param name
	 * @return
	 */
	/*@Deprecated
	public Object getConf1(String name){
		return beannames.get(name);
	}*/

    public Properties getPropConf(String name){
    	return propnames.get(name);
    }
    
    /**
     * LC：返回值使用了TreeMap而非接口Map是为了保持原有的类型，
     * 但会关联thoughtworks版本实现的更改，如果要用新的thoughtworks版本，注意其Map是否有新的实现
     * @param <T>
     * @param t
     * @return
     */
	public <T> java.util.TreeMap<Integer,T> getConf(Class<T> t)
    {
    	Object o = beannames.get(t.getName());
    	if(o == null)
    		return null;
    	try
    	{
    		@SuppressWarnings("unchecked")
    		java.util.TreeMap<Integer,T> result = (java.util.TreeMap<Integer,T>)o;
    		return result;
    	}
    	catch(ClassCastException e)
    	{
    		e.printStackTrace();
    		return null;
    	}
    }
    
	static void usage() {
		System.out.println("Usage: java -jar gsxdb.jar [options]");
		System.out.println("    -rmiport    rmi port");
		System.out.println("    -gszoneid      gszone id");
		System.exit(0);
	}

	private static String ARGS(String args[], int i) {
		if (i < args.length) return args[i];
		usage();
		return null;
	}
	
	public int getLoginLimitType() {
		return loginLimitType;
	}

	public void setLoginLimitType(int loginLimitType) {
		this.loginLimitType = loginLimitType;
	}
	
	public List<Integer> getUserIDList() {
		return userIDList;
	}
	
	public int getErrorType() {
		return errorType;
	}

	public void setErrorType(int errorType) {
		this.errorType = errorType;
	}
	
	
	/**
	 * 理论来说，这个init方法是可以调用多次的
	 */
	static void init(String xmldir,XStreamUnmarshaller unmarshaller,String propdir) {
		/*synchronized (ConfigManager.class) {
			instance = null;
		} 注释掉的原因是，本方法执行结束前，还是可以getInstance的，只有在最后才把instance替换为新的*/

		final ConfigManager cm=new ConfigManager();
		//初始化系统参数
		rmiport = DEFAULT_RMI_PORT;
		String[] args = Gs.args;
		for (int i = 0; i < args.length; ++i)
		{
			if (args[i].equals("-rmiport"))
				rmiport = Integer.valueOf(ARGS(args, ++i));
			else if (args[i].equals("-gszoneid"))
				gs_zoneid = Integer.valueOf(ARGS(args, ++i));
			else if (DEFAULT_RMI_PORT == rmiport)
				rmiport = Integer.valueOf(args[i]); // 兼容
			else
				usage();
		}
		//cm.unmarshaller = unmarshaller;
		cm.beannames.clear();

		for (final java.io.File f : new java.io.File(xmldir).listFiles()) {
			if (!f.getName().endsWith(".xml"))
				continue;
			Object o;
			try {
				o = unmarshaller.unmarshal(new FileInputStream(f));
			} catch (final Exception ex) {
				logger.error("载入" + f.getAbsolutePath() + "失败", ex);
				continue;
			}
			final String beanname = f.getName().substring(0,
					f.getName().length() - new String(".xml").length());			
			logger.info("register bean name=" + beanname);
			cm.beannames.put(beanname, o);
			if(!(o instanceof java.util.Map<?,?>)) continue;			
		}
		
		mytools.ConvMain.doCheck(cm.beannames);
		
		Properties prop=null;
		FileInputStream fis=null;
		for (final java.io.File f : new java.io.File(propdir).listFiles()){
			if (!f.getName().endsWith(".properties"))
				continue;
			prop=new Properties();
			try {
				fis=new FileInputStream(f);
				prop.load(fis);
				fis.close();
			} catch (FileNotFoundException e) {
				logger.error(f.getAbsolutePath()+"not found", e);
				continue;
			} catch (IOException e) {
				logger.error(f.getAbsolutePath()+"load error", e);
				continue;
			}
			final String propname=f.getName().substring(0, f.getName().indexOf(".properties"));
			cm.propnames.put(propname, prop);
			logger.info("register properties=" + propname);
		}
		synchronized (ConfigManager.class) {
			instance = cm;
		}
		Properties sysprop = chuhan.gsp.main.ConfigManager.getInstance().getPropConf("sys");
		SERVER_STATE = GameProp.getIntValue(sysprop, "sys.server.state");
	}
	@Override
	public void exit() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void init() throws Exception {
		init(CONFIG_PATH,new XStreamUnmarshaller(),PROPERTY_PATH);
	}

	public static int getGsZoneId()
	{
		return gs_zoneid;
	}
	public static int getRmiPort()
	{
		return rmiport;
	}
	
	
	/**
	 * 以下数据将来要挪到XDB中去，xdb可以更好的支持回滚等操作
	 */
	private static AtomicInteger onlineRoleNum = new AtomicInteger(0);//当前在线人数
	private static AtomicInteger dayMaxRoleNum = new AtomicInteger(0);//今天最大在线人数
	
	
	public static void setOnlineRoleNum(int onlinenum)
	{
		onlineRoleNum.set(onlinenum);
		if(onlinenum > dayMaxRoleNum.get() )
			dayMaxRoleNum.set(onlinenum);
	}
	
	public static int getOnlineRoleNum()
	{
		return onlineRoleNum.get();
	}
	public static int getDayMaxRoleNum()
	{
		return dayMaxRoleNum.get();
	}
	public static void cleanDayMaxRoleNum()
	{
		dayMaxRoleNum.set(0);
	}
	
	public void putConfig(String beanname, Object o)
	{
		beannames.put(beanname, o);
	}

	@Override
	public ReloadResult reload() throws Exception
	{
		String filename = getPropConf("sys").getProperty("sys.hotfix.filename");
		java.io.File hotfixcfg = new java.io.File(ConfigManager.CONFIG_PATH +filename);
		if(!hotfixcfg.exists())
		{
			return new ReloadResult(false,"Hotfix file not exists. \n");
		}
		if (!hotfixcfg.getName().endsWith(".xml"))
		{
			return new ReloadResult(false,"Hotfix file is not valid. \n");
		}
		ReloadResult result = new ReloadResult(true);
		List<File> files = new LinkedList<File>();
		files.add(hotfixcfg);
		result.appendResult(reloadConfigFiles(files));
		if(!result.isSuccess())
			return result;
		
		Map<Integer,HotfixConfig> hotfixs = ConfigManager.getInstance().getConf(HotfixConfig.class);
		
		//reload xml文件
		files.clear();
		for(HotfixConfig hotfix : hotfixs.values())
		{
			if(hotfix.type == 1)//xml文件
				files.add(new java.io.File(ConfigManager.CONFIG_PATH +"/" + hotfix.name));
		}
		
		result.appendResult(reloadConfigFiles(files));
		if(!result.isSuccess())
			return result;
		
		//reload properties文件
		files.clear();
		for(HotfixConfig hotfix : hotfixs.values())
		{
			if(hotfix.type == 3)//properties文件
				files.add(new java.io.File(ConfigManager.PROPERTY_PATH +"/" + hotfix.name));
		}
		
		result.appendResult(reloadPropertyFiles(files));
		if(!result.isSuccess())
			return result;
		
		Module.logger.info("load files success. \n");
		return result;
	}
	
	private ReloadResult reloadConfigFiles(List<File> cfgfiles)
	{
		XStreamUnmarshaller unmarshaller = new XStreamUnmarshaller();
		Map<String,Object> cfgs = new HashMap<String, Object>();
		ReloadResult result = new ReloadResult(true);
		for (final java.io.File f : cfgfiles) {
			if(!f.exists())
			{
				String msg = f.getAbsolutePath() + " not exists. \n";
				Module.logger.error(msg);
				doLog(false, f.getName());
				return new ReloadResult(false,msg);
			}
			if (!f.getName().endsWith(".xml"))
			{
				String msg = f.getAbsolutePath() + " is not end with xml \n";
				Module.logger.error(msg);
				doLog(false, f.getName());
				return new ReloadResult(false);
			}
			Object o;
			try {
				o = unmarshaller.unmarshal(new FileInputStream(f));
			} catch (final Exception ex) {
				String msg = "load " + f.getAbsolutePath() + " fail. \n";
				Module.logger.error(msg, ex);
				doLog(false, f.getName());
				return new ReloadResult(false,msg);
			}
			final String beanname = f.getName().substring(0,
					f.getName().length() - new String(".xml").length());
			String msg = "File: " + f.getName() + " reload success. \n";
			result.appendMsg(msg);
			Module.logger.info(msg);
			doLog(true, f.getName());
			cfgs.put(beanname, o);
		}
		if(!mytools.ConvMain.doCheck(cfgs))
			return new ReloadResult(false,"load success, but check error.");
		//全加载成功后，再替换
		for(Map.Entry<String,Object> entry : cfgs.entrySet())
			beannames.put(entry.getKey(), entry.getValue());
		
		return result;
	}
	private ReloadResult reloadPropertyFiles(List<File> propfiles)
	{
		Map<String,Properties> props= new HashMap<String, Properties>();
		ReloadResult result = new ReloadResult(true);
		for (final java.io.File f : propfiles) {
			if(!f.exists())
			{
				String msg = f.getAbsolutePath() + " not exists. \n";
				Module.logger.error(msg);
				doLog(false, f.getName());
				return new ReloadResult(false,msg);
			}
			if (!f.getName().endsWith(".properties"))
			{
				String msg = f.getAbsolutePath() + " is not end with properties \n";
				Module.logger.error(msg);
				doLog(false, f.getName());
				return new ReloadResult(false);
			}
			
			Properties prop = new Properties();
			try {
				FileInputStream fis=new FileInputStream(f);
				prop.load(fis);
				fis.close();
			} catch (final Exception ex) {
				String msg = "load " + f.getAbsolutePath() + " fail. \n";
				Module.logger.error(msg, ex);
				doLog(false, f.getName());
				return new ReloadResult(false,msg);
			}
			final String propname=f.getName().substring(0, f.getName().indexOf(".properties"));
			String msg = "File: " + f.getAbsolutePath() + " reload success. \n";
			result.appendMsg(msg);
			Module.logger.info(msg);
			doLog(true, f.getName());
			props.put(propname, prop);
		}
		
		//全加载成功后，再替换
		for(Map.Entry<String,Properties> entry : props.entrySet())
			propnames.put(entry.getKey(), entry.getValue());
		
		return result;
	}
	private void doLog(boolean succ, String filename)
	{
		logger.info("Reload: success="+succ+",filename="+filename);
		/*
		Map<String, Object> param = new HashMap<String, Object>();
		param.put(RemoteLogParam.ISSUCC, succ ? "1" : "0");
		param.put(RemoteLogParam.FILENAME, filename );
		LogManager.getInstance().doLog(RemoteLogID.RELOADFILE, param);
	*/}
	public static IdType getIdType(int id)
	{
		if(id >= 100 && id <= 999)
			return IdType.ITEM_GROUP;
		else if(id >= 3000 && id <= 3999)
			return IdType.BAG_ITEM; 
		else if(id >= 4000 && id <= 4999)
			return IdType.EQUIP; 
		else if(id >= 5000 && id <= 5999)
			return IdType.SKILL; 
		else if(id >= 6000 && id <= 6999)
			return IdType.SOUL; 
		else if(id >= 7000 && id <= 7999)
			return IdType.COLLECT; 
		else if(id >= 9000 && id <= 9999)
			return IdType.HERO;
		else if(id >= 10000)
			return IdType.AWARD_ID;
		
		return IdType.NULL;
	}
	public static boolean inReviewState()
	{
		return SERVER_STATE == 2;
	}
	
}
