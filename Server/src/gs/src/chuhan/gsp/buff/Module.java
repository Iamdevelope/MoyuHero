package chuhan.gsp.buff;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ModuleManager;
import chuhan.gsp.main.ReloadResult;


/**
 * 
 *
 */
public class Module implements chuhan.gsp.main.ModuleInterface
{

	
//	private static Module instance;
	
	public static final String MODULE_NAME = "buff";
	
	java.util.Map<Integer, Class<? extends ContinualBuff>> cid2class;
	java.util.Map<Integer, ContinualBuffConfig> defaultCBuffConfigs = new HashMap<Integer, ContinualBuffConfig>();
	Map<Integer, BuffConflicts> buffConflictsMap = new HashMap<Integer, BuffConflicts>();
	private Map<Integer,List<Integer>> limitedBattleOperations = new HashMap<Integer, List<Integer>>();
	public static Class<?> ROUND_BUFF_CLASS;
	public static Module getInstance()
	{
		//不再用以上的单例模式，从ModuleManager获得，由其管理
		return ((Module)ModuleManager.getInstance().getModuleByName(MODULE_NAME));
	}

	static public final Logger logger = Logger.getLogger(Module.class);


	@Override
	public void exit()
	{

	}

	@SuppressWarnings("unchecked")
	@Override
	public void init() throws Exception
	{
		ROUND_BUFF_CLASS = RoundBuff.class;
		// 持续性buff Config初始化
		cid2class = new java.util.HashMap<Integer, Class<? extends ContinualBuff>>();
		cid2class.clear();
		defaultCBuffConfigs.clear();
		for (chuhan.gsp.attr.SCBuffConfig buffconfig : chuhan.gsp.main.ConfigManager.getInstance().getConf(chuhan.gsp.attr.SCBuffConfig.class).values())
		{
			String c = buffconfig.classname;
			if (c == null || c.isEmpty())
				throw new IllegalArgumentException("Buff" + buffconfig.id+" classname == null");
			try
			{
				Class<?> cls = Class.forName(c);
				cid2class.put(buffconfig.id, (Class<? extends ContinualBuff>) cls);
			} catch (ClassNotFoundException ex)
			{
				logger.error("载入持续性BUFF " + buffconfig.id + "失败，找不到类" + c);
				continue;
			}

			// 初始化defaultCBuffConfigs
			try
			{
				ContinualBuffConfig cbuffConfig = new ContinualBuffConfig(buffconfig);
				for (int operationtype : cbuffConfig.getLimitedBattleOperations())
				{
					List<Integer> buffIds = limitedBattleOperations.get(operationtype);
					if (buffIds == null)
						continue;
					buffIds.add(cbuffConfig.getBuffTypeId());
				}
				defaultCBuffConfigs.put(cbuffConfig.getBuffTypeId(), cbuffConfig);
			} catch (Exception ex)
			{
				logger.error("初始化ContinualBuffConfig" + buffconfig.id + "失败，持续性buff表错误。", ex);
				continue;
			}
		}

		
		buffConflictsMap.clear();
		// buff冲突的初始化
		for (chuhan.gsp.attr.SBuffConflicts sbuffconflicts : chuhan.gsp.main.ConfigManager.getInstance().getConf(chuhan.gsp.attr.SBuffConflicts.class).values())
		{
			BuffConflicts buffconflicts = new BuffConflicts(sbuffconflicts.getId(), sbuffconflicts.getName());
			try
			{
				if (sbuffconflicts.getConflictbuffs() != null && !sbuffconflicts.getConflictbuffs().equals(""))
				{
					String[] conflicts = sbuffconflicts.getConflictbuffs().split(";");
					for (String conflict : conflicts)
					{
						ContinualBuffConfig cbuffcfg = defaultCBuffConfigs.get(Float.valueOf(conflict).intValue());
						buffconflicts.addConflictBuff(cbuffcfg);
					}
				}
				if (sbuffconflicts.getOverridebuffs() != null && !sbuffconflicts.getOverridebuffs().equals(""))
				{
					String[] overrides = sbuffconflicts.getOverridebuffs().split(";");
					for (String override : overrides)
						buffconflicts.getOverrideBuffs().add(Float.valueOf(override).intValue());
				}
				if (sbuffconflicts.getInvalidbuffs() != null && !sbuffconflicts.getInvalidbuffs().equals(""))
				{
					String[] invalids = sbuffconflicts.getInvalidbuffs().split(";");
					for (String invalid : invalids)
						buffconflicts.getInvalidBuffs().add(Float.valueOf(invalid).intValue());
				}
			} catch (Exception e)
			{
				logger.error("Initial Buff Conflicts Error, Buff Id = " + sbuffconflicts.getId());
				e.printStackTrace();
			}
			buffConflictsMap.put(buffconflicts.getBuffId(), buffconflicts);
		}

	}
	
	/**
	 * 只是用buffId构造，取表内的默认参数
	 * 根据buffID 来决定构造的是一次性还是持续性Buff
	 * @param buffId
	 * @return IBuff
	 */
	public IBuff createBuff(int buffId)
	{
		return createContinualBuff(buffId);
	}
	
	
	public ContinualBuff createContinualBuff(ContinualBuffConfig buffConfig)
	{
		Class<? extends ContinualBuff> buffClass = cid2class.get(buffConfig.getBuffTypeId());
		//ContinualBuffConfig scconf = getContinualBuffConfig(buffConfig.getBuffTypeId());
		//buffConfig.setOriginBuffConfig(scconf);
		if (buffClass == null)
			return null;
		try
		{
			return buffClass.getConstructor(ContinualBuffConfig.class).newInstance(buffConfig);
		} catch (NoSuchMethodException ex)
		{
			logger.error(buffClass.getCanonicalName() + "缺少BuffConfig的构造函数");
			return null;
		} catch (java.lang.reflect.InvocationTargetException ex)
		{
			logger.error(buffClass.getCanonicalName() + "输入参数错误，错误的buff id为" + buffConfig.getBuffTypeId());
			return null;
		} catch (IllegalAccessException ex)
		{
			return null;
		} catch (InstantiationException ex)
		{
			return null;
		}
	}
	
	/**
	 * 根据buffId生成buff，buff配置取buff表里填的默认的持续性buff配置
	 * 
	 * @param buffTypeId
	 * @return ContinualBuffConfig，不存在该buff配置时返回null
	 */
	public ContinualBuff createContinualBuff(int buffTypeId)
	{
		ContinualBuffConfig buffConfig = getInstance().getDefaultCBuffConfig(buffTypeId);
		if(buffConfig == null)
			return null;
		Class<? extends ContinualBuff> buffClass = cid2class.get(buffTypeId);
		if (buffClass == null)
			return null;
		try
		{
			return buffClass.getConstructor(ContinualBuffConfig.class).newInstance(buffConfig);
		} catch (NoSuchMethodException ex)
		{
			logger.error(buffClass.getCanonicalName() + "缺少BuffConfig的构造函数");
			return null;
		} catch (java.lang.reflect.InvocationTargetException ex)
		{
			logger.error(buffClass.getCanonicalName() + "输入参数错误，错误的buff id为" + buffConfig.getBuffTypeId());
			return null;
		} catch (IllegalAccessException ex)
		{
			return null;
		} catch (InstantiationException ex)
		{
			return null;
		}
	}
	
	/**
	 * 根据buffId和buffbean生成buff，用于身上的已有的buff
	 * 
	 * @param buffTypeId
	 * @return ContinualBuffConfig，不存在该buff配置时返回null
	 */
	public ContinualBuff createContinualBuff(xbean.Buff buffbean)
	{
		//ContinualBuffConfig buffConfig = getInstance().getDefaultCBuffConfig(buffId);
		int buffId = buffbean.getId();
		Class<? extends ContinualBuff> buffClass = cid2class.get(buffId);
		if (buffClass == null)
			return null;
		try
		{
			return buffClass.getConstructor(xbean.Buff.class).newInstance(buffbean);
		} catch (NoSuchMethodException ex)
		{
			logger.error(buffClass.getCanonicalName() + "缺少BuffConfig的构造函数");
			return null;
		} catch (java.lang.reflect.InvocationTargetException ex)
		{
			logger.error(buffClass.getCanonicalName() + "输入参数错误，错误的buff id为" + buffId);
			return null;
		} catch (IllegalAccessException ex)
		{
			return null;
		} catch (InstantiationException ex)
		{
			return null;
		}
	}
	
	/**
	 * 根据buffTypeId获取buff表里填的默认的持续性buff配置
	 * 
	 * @param buffTypeId
	 * @return ContinualBuffConfig，不存在该buff配置时返回null
	 */
	public ContinualBuffConfig getDefaultCBuffConfig(int buffId)
	{
		return defaultCBuffConfigs.get(buffId);
//		if(cbuffconfig == null)
//			return null;
//		return cbuffconfig.copy();
	}
	
	/**
	 * 是否是持续性buff
	 * @param buffId
	 * @return true持续性buff；false一次性buff
	 */
	public static boolean isContinualBuff(int buffId)
	{
		if(buffId/10000 == 50)
			return true;
		else
			return false;
	}

	@Override
	public ReloadResult reload() throws Exception
	{
		ReloadResult result = chuhan.gsp.attr.Module.getInstance().reload();
		if(!result.isSuccess())
			return result.appendMsg("buff module reload failed /n");
		try
		{
			Module m = new Module();
			m.init();
			ModuleManager.getInstance().putModuleByName(MODULE_NAME,m);
			return result;
		}
		catch(Exception e)
		{
			logger.error(e);
			return result.appendMsg("buff module reload failed /n");
		}
		
	}
	/**
	 * 获取buff冲突列表
	 * @param buffId
	 * @return
	 */
	public BuffConflicts getBuffConflicts(int buffId)
	{
		return buffConflictsMap.get(buffId);
	}
}
