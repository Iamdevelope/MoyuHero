package chuhan.gsp.mbean;

import chuhan.gsp.gm.GMInterface;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.ReloadResult;

import com.goldhuman.service.mhsdinterfaces.ResultBean;

public class Module implements chuhan.gsp.main.ModuleInterface
{
	private javax.management.MBeanServer mbs=java.lang.management.ManagementFactory.getPlatformMBeanServer();
	public static Logger logger = Logger.getLogger(Module.class);
	@Override
	public void init() throws Exception
	{
		logger.info(new StringBuilder("JMX SERVER START, rmiport = ").append(ConfigManager.getRmiPort()));
		
//		com.goldhuman.service.ytinterfaces.GameControl gamecontrol = new GameControlImpl();
//		gamecontrol.registerMBean();
//		com.goldhuman.service.ytinterfaces.RoleControl rolecontrol = new RoleControl();
//		rolecontrol.registerMBean();
//		
		//new GameControl().registerMBean();
//		new com.goldhuman.service.ytinterfaces.RoleControl().registerMBean();
		
		//register all mbeans
		
		registerMbean(new Reload(), "IWEB:type=Reload");
		registerMbean(new GMInterface(), "IWEB:type=GM");
		//new RoleControlImpl().registerMBean();
		//new GameControlImpl().registerMBean();
	}
	
	
	private void registerMbean(Object obj,String name) throws javax.management.NotCompliantMBeanException,javax.management.MBeanRegistrationException,javax.management.InstanceAlreadyExistsException,javax.management.MalformedObjectNameException{
		mbs.registerMBean(obj, new javax.management.ObjectName(name));
	}
	
	@Override
	public void exit()
	{
	}
	
	
	public static ResultBean getIWebCommonSuccResultBean()
	{
		return getIWebResultBean(true, "invoke success.");
	}
	
	public static ResultBean getIWebResultBean(boolean success, String msg)
	{
		if(success)
			return new ResultBean(true, msg, "");
		else
			return new ResultBean(false,"", msg);
	}
	
	public static ResultBean getIWebWrongRoleIdResult(Long roleId)
	{
		if(roleId <= 0)
			return new ResultBean(false,"", "RoleID必须是正数。");
		Integer userid = xtable.Properties.selectUserid(roleId);
		if(userid == null)
			return new ResultBean(false,"", "没有找到该RoleID对应的角色。");
		
		return null;
	}


	@Override
	public ReloadResult reload() throws Exception
	{
		return new ReloadResult(false,"module" + this.getClass().getName() + "not support reload");
	}
}
