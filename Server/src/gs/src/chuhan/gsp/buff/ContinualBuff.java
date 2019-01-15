package chuhan.gsp.buff;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 注意：除了刚new完一个buff后对buff的属性进行修改，其他情况下不要直接对buff进行操作 
 * 对buff的操作，例如添加/删除/执行要通过BuffAgent来完成
 *
 */
public class ContinualBuff extends AbstractBuff
{
	protected static final org.apache.log4j.Logger logger = org.apache.log4j.Logger.getLogger(ContinualBuff.class);
	protected xbean.Buff buffBean;
	final protected ContinualBuffConfig buffConfig;
	protected int buffId;
	
	
	/**
	 * 用于已经存在的Buff,和buffcopy时
	 * @param buffRole
	 * @param buffBean
	 */
	public ContinualBuff(xbean.Buff buffBean)
	{
		this.buffId = buffBean.getId();
		this.buffBean = buffBean;
		buffConfig = chuhan.gsp.buff.Module.getInstance().getDefaultCBuffConfig(buffId);
	}

	/**
	 * 用于新建一个Buff时
	 * @param buffConfig
	 */
	public ContinualBuff(ContinualBuffConfig buffConfig)
	{
		//this.buffRole = buffRole;
		this.buffConfig = buffConfig;
		this.buffId = buffConfig.getBuffTypeId();
		initialBuffBean();
	}
	
	
	protected void initialBuffBean()
	{
		buffBean = xbean.Pod.newBuff();
		buffBean.setId(buffConfig.getBuffTypeId());
		buffBean.setAmount(buffConfig.getInitAmount());
		buffBean.setRound(buffConfig.getInitRound());
		buffBean.setTime(buffConfig.getInitTime());
		for(int effectTypeId : buffConfig.getEffects().keySet())
		{//初始化buff config中的效果值
			buffBean.getEffects().put(effectTypeId, buffConfig.getEffects().get(effectTypeId));
		}
	}
	

	/**
	 * 附加buff，由BuffAgent调用add类方法，不要直接使用
	 */
	public BuffResult attach(BuffAgent buffAgent)
	{
		//过滤无用属性
		List<Integer> rmids = new ArrayList<Integer>(); 
		for(Map.Entry<Integer, Float> entry :buffBean.getEffects().entrySet())
		{
			if(entry.getValue() == 0)
				rmids.add(entry.getKey());
		}
		for(Integer effectid :rmids)
			buffBean.getEffects().remove(effectid);
		chuhan.gsp.attr.AttrAgent erole = buffAgent.getAttrAgent();
		buffAgent.addBuffBean(buffBean);
		erole.attachEffects(buffBean.getEffects());
		//启动定时器
		BuffResult result = new BuffResult(true);
		result.addAddedBuff(this);
		// 更新属性
		result.updateChangedAttrs(erole.updateAllFinalAttrs());
		return result;
	}
	
	
	/**
	 * 周期执行由BuffAgent调用process类方法，不要直接使用
	 * 一般来说PeriodBuff有效，ContinualBuff不用
	 * 注意：不要直接使用此方法，调用BuffAgent.processCBuff()来执行
	 * 所有对Buff的操作都通过BuffAgent来执行
	 * @return
	 */
	public BuffResult process(BuffAgent buffAgent)
	{
		return new BuffResult(false);
	}
	
	/**
	 * 默认使用的，非到时detach
	 * 直接移除buff，由BuffAgent调用remove类方法，不要直接使用
	 */
	public BuffResult detach(BuffAgent buffAgent)
	{
		buffAgent.removeBuffBean(buffBean.getId());
		chuhan.gsp.attr.AttrAgent erole = buffAgent.getAttrAgent();
		erole.detachEffects(buffBean.getEffects());
		BuffResult result = new BuffResult(true); 
		result.addDeletedBuff(this);
		result.setChangedAttrs(erole.updateAllFinalAttrs());
		return result;
	}
	
	/**
	 * 到时后调用的detach
	 * @param buffAgent
	 * @param timeout
	 * @return
	 */
	public BuffResult detach(BuffAgent buffAgent,boolean timeout)
	{		
		return detach(buffAgent);
		
	}
	

	/**
	 * 战斗中使用的attach
	 */
	/*@Override
	public DemoResult attach(xbean.BattleInfo battleInfo,Fighter opfighter, Fighter aimfighter, BattleSkill battleskill, Map<Integer, JavaScript> effects)
	{
		DemoResult demoResult = new DemoResult();
		demoResult.targetid = aimfighter.getFighterId();
		if(effects!=null)
		{
			for(Map.Entry<Integer, JavaScript> entry: effects.entrySet())
			{
				buffBean.getEffects().put(entry.getKey(), entry.getValue().eval(battleInfo.getEngine()).floatValue());
			}
		}
		buffBean.setFighterkey(opfighter.getFighterBean().getFighterkey());
		BuffResult result = aimfighter.getBuffAgent().addCBuff(this);
		chuhan.gsp.buff.Module.updateDemoResultFromResult(demoResult, result,aimfighter);
		return demoResult;
	}*/

	

	/**
	 * 战斗中调用
	 * 回合结束时结算，返回战斗脚本
	 * 如果没有回合结算（包括回合数变化等），返回null
	 * @param battleInfo
	 * @param fighter
	 * @return DemoResult 返回null则无脚本
	 */
	/*public DemoResult onRoundEnd(xbean.BattleInfo battleInfo,Fighter fighter)
	{
		boolean changed = false;
		DemoResult demo = processRoundEndIBuff(battleInfo, fighter);
		//如果有回合结束时需要结算的一次性BUF
		if(demo == null)
		{
			demo = new DemoResult();
			demo.targetid = fighter.getFighterId();
		}
		else
			changed = true;
		//减少回合数
		BuffResult result = roundCountdown(fighter.getBuffAgent());
		if(result.isSuccess())
		{
			chuhan.gsp.buff.Module.updateDemoResultFromResult(demo, result, fighter);
			changed = true;
		}
		if(changed)
			return demo;
		else
			return null;
	}
	
	protected DemoResult processRoundEndIBuff(xbean.BattleInfo battleInfo,Fighter fighter)
	{
		if(buffConfig.getRoundIBuffId() > 0)
		{
			Map<Integer,JavaScript> effects = new HashMap<Integer, JavaScript>();
			for (Map.Entry<Integer, Float> entry : buffBean.getEffects().entrySet())
			{
				JavaScript script = new JavaScript(entry.getValue().toString());
				effects.put(entry.getKey(), script);
			}
			IBuff instantbuff = chuhan.gsp.buff.Module.getInstance().createBuff(buffConfig.getRoundIBuffId());
			if (fighter.getBuffAgent().canAddBuff(instantbuff.getId()))
			{
				DemoResult tmpdemo = instantbuff.attach(battleInfo, fighter, fighter, null, effects);
				if (tmpdemo != null)
				{
					return tmpdemo;
				}
			}
		}
		return null;
	}*/
	
	protected BuffResult roundCountdown(BuffAgent agent)
	{
		if(buffBean.getRound() <= 0)//count小于等于0，说明该buff不是个计回合的buff，会一直保持
			return new BuffResult(false);
		
		int count = buffBean.getRound();
		count--;
		buffBean.setRound(count);
		if(count == 0)//计数buff计数为0，移除该buff
			return agent.removeCBuff(buffId);
		else
		{
			BuffResult result = new BuffResult(true);
			result.addAddedBuff(this);
			return result;
		}
			
	}

	
	public ContinualBuff copy()
	{
		return Module.getInstance().createContinualBuff(buffBean.copy());
	}
	
	public ContinualBuff copyToData()
	{
		return Module.getInstance().createContinualBuff(buffBean.toData());
	}
	
	public ContinualBuffConfig getBuffConfig()
	{
		return buffConfig;
	}
	public xbean.Buff getBuffBean()
	{
		return buffBean;
	}

	/**
	 * 毫秒
	 * @return
	 */
	public long getTime()
	{
		return buffBean.getTime();
	}

	/**
	 * 毫秒
	 * @return
	 */
	public void setTime(long time)
	{
		buffBean.setTime(time);
	}
	/**
	 * 回合
	 * @return
	 */
	public int getRound()
	{
		return buffBean.getRound();
	}
	/**
	 * 回合
	 * @return
	 */
	public void setRound(int count)
	{
		buffBean.setRound(count);
	}
	/**
	 * 量
	 * @return
	 */
	public long getAmount()
	{
		return buffBean.getAmount();
	}
	/**
	 * 量
	 * @return
	 */
	public void setAmount(long amount)
	{
		buffBean.setAmount(amount);
	}
	/**
	 * 效果
	 * @return
	 */
	public Map<Integer,Float> getEffects()
	{
		return buffBean.getEffects();
	}
	
	/**
	 * 把默认的效果清除，只留下刚传入的效果
	 * @param effects
	 */
	public void setEffects(Map<Integer,Float> effects)
	{
		buffBean.getEffects().clear();
		buffBean.getEffects().putAll(effects);
	}

	@Override
	public int getId()
	{
		return buffId;
	}
	
	public void setAttachTime(long attachTime)
	{
		buffBean.setAttachtime(attachTime);
	}
	
	public long getAttachTime()
	{
		return buffBean.getAttachtime();
	}
	
	@Override
	public String toString()
	{
		return buffBean.toString();
	}
}
