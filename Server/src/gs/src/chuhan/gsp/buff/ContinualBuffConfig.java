package chuhan.gsp.buff;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import chuhan.gsp.buff.BuffConfig;

/**
 * 只读
 * 从表中读出的默认的持续性buff配置，不允许修改！
 *
 */
public class ContinualBuffConfig extends BuffConfig
{
	private int buffType;//buff类型，不同于BuffId,是buff比较细的归类类型，例如HOT类，毒类，增益类等等
	
	private long initTime = 0;//时间
	
	private int initRound = 0;//回合数/Periodbuff时的执行次数
	
	private long initAmount = 0;//量（吸收量/攻击次数等）
	
	private long initDelay = 0;//开始延迟时间，用于PeriodBuff（周期性buff）

	private int clearType;//清除类型
	
	private boolean storeToDisk;//保存到硬盘（关机重启也保存）
	
	private boolean send2Client;
	
	//弃用private int showScale;//对自己发送;对队友广播;战斗中对所有人广播，战斗外对9屏内玩家广播

	private boolean inBattleScript;//是否跟随战斗脚本一起发送，当需要在脚本播放时增删buff，并且脚本中的buff会被所有战斗中角色所得到
	
	private int roundIBuffId;//回合结算的buff，记录每回合结束时释放的一次性BuffId
	
	private int sceneStateId = 0;//场景ID，没有则为0
	
	private Map<Integer,Float> effects = new HashMap<Integer, Float>();

	private String[] otherArgs;
	
	private int overrideSelfType = 0;
	
	public final static int OVERRIDE_TYPE_ALL = 0;
	
	public final static int OVERRIDE_TYPE_ATTR_ADD = 1;

	public final static int OVERRIDE_TYPE_ATTR_HIGHER = 2;

	private int overrideAttr = 0; 
	
	private List<Integer> limitedBattleOperations = new ArrayList<Integer>();
	
	private boolean isShowTime;// 在客户端是否现显示计时

	public List<Integer> getLimitedBattleOperations()
	{
		return limitedBattleOperations;
	}
	
	/**
	 * 不要使用，初始化默认配置时使用
	 * @throws Exception 
	 */
	public ContinualBuffConfig(chuhan.gsp.attr.SCBuffConfig scbuffconfig) throws Exception
	{
		
		buffTypeId = scbuffconfig.id;
		buffName = scbuffconfig.name;
		classname = scbuffconfig.classname;
		Class<?> cls = Class.forName(classname);
		if(cls == null)
			throw new IllegalArgumentException("buff:" + buffTypeId + " class not exists.");
		isShowTime = true;
		buffType = scbuffconfig.buffclass;

		clearType = scbuffconfig.clearType;

		send2Client = scbuffconfig.sendtoclient == 1;
	
	}
	
	public long getInitTime(java.util.concurrent.TimeUnit tu)
	{
		if(tu.equals(java.util.concurrent.TimeUnit.SECONDS))
			return initTime/1000;
		else if(tu.equals(java.util.concurrent.TimeUnit.MILLISECONDS))
			return initTime;
		else if(tu.equals(java.util.concurrent.TimeUnit.HOURS))
			return initTime/3600000;
		return initTime;
	}
	
	public long getInitTime()
	{
		return initTime;
	}

	public int getInitCount()
	{
		return initRound;
	}

	public long getInitAmount()
	{
		return initAmount;
	}

	public boolean isStoreToDisk()
	{
		return storeToDisk;
	}

	public Map<Integer,Float> getEffects()
	{
		return effects;
	}

	public String[] getOtherArgs()
	{
		return otherArgs;
	}

	public int getClearType()
	{
		return clearType;
	}
	
	public int getPeriodCount()
	{
		return initRound;
	}

	public long getInitDelay()
	{
		return initDelay;
	}

	public int getRoundIBuffId()
	{
		return roundIBuffId;
	}

	public boolean isInBattleScript()
	{
		return inBattleScript;
	}

	public int getBuffType()
	{
		return buffType;
	}

	public int getInitRound()
	{
		return initRound;
	}
	
	public long getPeriod()
	{
		return initTime;
	}
	
	public int getOverrideSelfType()
	{
		return overrideSelfType;
	}

	public int getOverrideAttr()
	{
		return overrideAttr;
	}

	public int getSceneStateId()
	{
		return sceneStateId;
	}
	
	public boolean isShowTime()
	{
		return isShowTime;
	}
	
	public boolean isSendToClient()
	{
		return send2Client;
	}
	
}
