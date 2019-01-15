package chuhan.gsp.buff;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

import chuhan.gsp.attr.SBuffChangeResult;
import chuhan.gsp.buff.ContinualBuff;
import chuhan.gsp.buff.ContinualBuffConfig;

public abstract class BuffAgent
{
	public static final org.apache.log4j.Logger logger = org.apache.log4j.Logger.getLogger(BuffAgent.class);
	protected xbean.BuffAgent agent;

	protected boolean readonly;
	
	
	public BuffAgent(xbean.BuffAgent agent, boolean readonly)
	{
		this.agent = agent;
		this.readonly = readonly;
	}
	
	/***
	 * 检查buff冲突，是否能添加该buff
	 * 
	 * @param buffId buff类型Id
	 * @return false 不能添加该类型buff
	 */
	public boolean canAddBuff(int buffId)
	{
		return checkCanAddBuff(buffId) == 0;
	}
	
	/***
	 * 检查buff冲突，并返回冲突的ID，0为不冲突
	 * 
	 * @param buffId buff类型Id
	 * @return 0  不冲突
	 */
	public int checkCanAddBuff(int buffId)
	{
		int conflictId = checkBuffConflict(buffId);
		if(conflictId != 0)
			return conflictId;
		
		return checkBuffValid(buffId);
	}
	
	/***
	 * 检查buff冲突，是否能添加该buff或执行动作。
	 * 注意：对于非实体buff（state），只有角色身上会存在。
	 * BuffRoleImpl中覆盖了此方法实现对state冲突的检测，而默认的（针对pet，monster）则没有检测。
	 * 
	 * @param buffTypeId buff类型Id
	 * @return 如果返回0，则无冲突；大于0，为冲突的buffID
	 */
	public int checkBuffConflict(int buffOpId)
	{
		BuffConflicts buffconflicts = Module.getInstance().getBuffConflicts(buffOpId);
		if(buffconflicts == null)
			return 0;
		Set<Integer> existIds = getAllBuffBeans().keySet();
		int conflict = buffconflicts.checkBuffConflict(existIds);
		if(conflict > 0)
			return conflict;
		int bufftype = 0;
		if(Module.isContinualBuff(buffOpId))
		{
			ContinualBuffConfig cfg = Module.getInstance().getDefaultCBuffConfig(buffOpId);
			if(cfg == null) return 0;
			bufftype = cfg.getBuffType();
			
		}
		else
		{
			return 0;
		}
		
		buffconflicts = Module.getInstance().getBuffConflicts(bufftype);
		if(buffconflicts == null)
			return 0;
		conflict = buffconflicts.checkBuffConflict(existIds);
		return conflict;
	}
	
	
	/**
	 * 战斗内检查此buff是否会生效
	 * @return
	 */
	public boolean canEffectBuff(int buffId)
	{
		
		return checkBuffValid(buffId) == 0;
	}
	
	/**
	 * 战斗内检查此buff是否会生效
	 * @return
	 */
	public int checkBuffValid(int buffId)
	{
		BuffConflicts buffconflicts = Module.getInstance().getBuffConflicts(buffId);
		if(buffconflicts == null)
			return 0;
		Set<Integer> existIds = getAllBuffBeans().keySet();
		int invalid = buffconflicts.checkBuffInvalid(existIds);
		if(invalid > 0)
			return invalid;
		int bufftype = 0;
		if(Module.isContinualBuff(buffId))
		{
			ContinualBuffConfig cfg = Module.getInstance().getDefaultCBuffConfig(buffId);
			if(cfg == null) return 0;
			bufftype = cfg.getBuffType();
			
		}
		else
		{
			return 0;
		}
		
		buffconflicts = Module.getInstance().getBuffConflicts(bufftype);
		if(buffconflicts == null)
			return 0;
		invalid = buffconflicts.checkBuffInvalid(existIds);
		return invalid;
	}
	/***
	 * 获取被覆盖的buff
	 * 
	 * @param buffTypeId buff类型Id
	 * @return false 不能添加该类型buff
	 */
	public List<Integer> getOverridedBuffIds(int buffId)
	{
		BuffConflicts buffConflicts = Module.getInstance().getBuffConflicts(buffId);
		if(buffConflicts == null)
			return new ArrayList<Integer>();
		return buffConflicts.getOverridedBuffIds(getAllBuffBeans().keySet());
	}

	/***
	 * 检查该类型的buff是否存在
	 * 
	 * @param buffTypeId buff类型ID
	 * @return true 存在该类型的buff
	 */
	public boolean existBuff(int buffId)
	{
		ContinualBuffConfig conf = Module.getInstance().getDefaultCBuffConfig(buffId);
		//NullBuff表示没有buff类，只是个状态的检测
		if(conf == null || conf.getClassname() == null) return false;
		if(conf.getClassname().equals("chuhan.gsp.buff.continual.NullBuff"))
		{
			return existState(buffId);
		}
		boolean exist = existBuffBean(buffId);
		if(!exist)
			return exist;
		return exist;
	}
	
	/**
	 * 判断某一类型的buff是否存在（注意，只能判断实体buff）
	 * @param type [1,1000)
	 * @return
	 */
	public boolean existBuffByType(int type)
	{
		if(type<1 || type >= 1000)
			return false;
		for(Integer buffId : agent.getBuffs().keySet())
		{
			ContinualBuffConfig cfg = Module.getInstance().getDefaultCBuffConfig(buffId);
			if(type >= 100 )//三级类型符合
			{
				if(type == cfg.getBuffType())
					return true;
			}
			else if(type >= 10)//二级类型符合
			{
				if(type == cfg.getBuffType() / 10)
					return true;
			}
			else if(type >= 1)//一级类型符合
			{
				if(type == cfg.getBuffType() / 100)
					return true;
			}
		}
		return false;
	}

	public abstract boolean existState(int buffTypeId);
	
	/***
	 * 根据buffId获取agent身上的buff
	 * @param buffTypeId
	 * @return null表示该buff在此agent上不存在
	 */
	public ContinualBuff getBuff(int buffId)
	{
		xbean.Buff buffbean = getBuffBean(buffId);
		if(buffbean == null)
			return null;
		ContinualBuff cbuff = Module.getInstance().createContinualBuff(buffbean);
		return cbuff;
	}
	
	/***********************对xbean.Buff的操作包装 BEGIN****************************/
	/**
	 * 获取xbean.buff
	 * 角色的xbean.buff保存在不同的表中
	 * @param buffId
	 * @return xbean.Buff
	 */
	public xbean.Buff getBuffBean(int buffId)
	{
		return agent.getBuffs().get(buffId);
	}
	
	public xbean.Buff addBuffBean(xbean.Buff buffbean)
	{
		return agent.getBuffs().put(buffbean.getId(), buffbean);
	}
	
	public xbean.Buff removeBuffBean(int buffId)
	{
		return agent.getBuffs().remove(buffId);
	}
	
	public boolean existBuffBean(int buffId)
	{
		return agent.getBuffs().containsKey(buffId);
	}
	
	/**
	 * 只读
	 * 对Map的操作不影响原Map，但是对其中元素的修改会影响xdb中的数据
	 * @return
	 */
	public Map<Integer,xbean.Buff> getAllBuffBeans()
	{
		Map<Integer,xbean.Buff> buffbeans = new HashMap<Integer, xbean.Buff>();
		buffbeans.putAll(agent.getBuffs());
		return buffbeans;
	}
	/***********************对xbean.Buff的操作包装 END****************************/
	
	/**
	 * 获取需要回合末结算的buff
	 */
	public List<ContinualBuff> getRoundBuffs()
	{
		List<ContinualBuff> buffs = new ArrayList<ContinualBuff>();
		for(Map.Entry<Integer, xbean.Buff> buffentry : getAllBuffBeans().entrySet())
		{
			//有回合计数的，都要做回合末结算
			if(buffentry.getValue().getRound()>0)
			{
				ContinualBuff cbuff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(buffentry.getValue());
				buffs.add(cbuff);
			}
			else
			{
				BuffConfig cfg = Module.getInstance().getDefaultCBuffConfig(buffentry.getKey());
				if(cfg == null)
					continue;
				//如果是RoundBuff，即使没有回合计数，也要回合末结算
				try
				{
					if(chuhan.gsp.buff.Module.ROUND_BUFF_CLASS.isAssignableFrom(Class.forName(cfg.getClassname())))
					{
						ContinualBuff cbuff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(buffentry.getValue());
						buffs.add(cbuff);
					}
				} catch (ClassNotFoundException e)
				{
					e.printStackTrace();
				}
			}
		}
		return buffs;
	}
	
	public BuffResult countDownWhileRoundEnd(List<ContinualBuff> buffs)
	{
		BuffResult result = new BuffResult(true);
		
		for(ContinualBuff buff : buffs)
		{
			buff.setRound(buff.getRound() - 1);
			if(buff.getRound() <= 0)
				result.updateResult(removeCBuff(buff.getId()));
			else
				result.addAddedBuff(buff);
		}
		
		return result;
	}
	
	
	/**
	 * 添加持续性Buff，默认用Buff表中填的BuffConfig构造Buff
	 * 对于NullBuff来说，因为不是实体buff，不能使用此方法添加，如果要判断互斥情况，可以调用canAddBuff
	 * @param buffTypeId
	 * @return Result 
	 */
	public BuffResult addCBuff(int buffTypeId)
	{
		ContinualBuff buff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(buffTypeId);
		if(buff == null )
		{
			logger.error("Buff(ID: " + buffTypeId +") is not exist.");
			return new BuffResult(false);
		}
		return addCBuff(buff);
	}
	/**
	 * 因为添加buff时，大多数情况修改的参数是回合和时间，添加此方法
	 * 对于NullBuff来说，因为不是实体buff，不能使用此方法添加，如果要判断互斥情况，可以调用canAddBuff
	 * @param buffId
	 * @param round 如果不计回合，则填0
	 * @param time 如果不计时，则填0
	 * @return Result 添加buff的结果
	 */
	public BuffResult addCBuff(int buffId , int round , long time)
	{
		ContinualBuff cbuff = Module.getInstance().createContinualBuff(buffId);
		cbuff.setRound(round);
		cbuff.setTime(time);
		return addCBuff(cbuff);
	}
	
	public BuffResult addCBuff(ContinualBuff buff)
	{
		//检查能不能添加
		if(!canAddBuff(buff.getId()))
			return new BuffResult(false);
		BuffResult result  = new BuffResult(true);
		
		//检查自己的buff是不是存在存在则移除,移除不需要发送协议，因为移除后要马上添加
		if(existBuff(buff.getId()))
		{
			//检查自己的叠加规则
			result = processOverrideSelf(buff);
			if(!result.isSuccess())
				return result;
		}
		//检查需要覆盖冲掉的buff
		List<Integer> rmbuffIds = getOverridedBuffIds(buff.getId());
		
		for(int rmbuffId : rmbuffIds)
		{
			result.updateResult(removeCBuff(rmbuffId));
		}
		
		result.updateResult(buff.attach(this));
		/*if(result.isSuccess())
			addSceneState(buff.getBuffConfig());*/
		return result;
	}
	
	/**
	 * 根据CBuff自己覆盖自己的规则，处理覆盖
	 * @param newbuff
	 * @return
	 */
	public BuffResult processOverrideSelf(ContinualBuff newbuff)
	{
		
		if(newbuff.getBuffConfig().getOverrideSelfType() == ContinualBuffConfig.OVERRIDE_TYPE_ALL)
		{
			return removeCBuff(newbuff.getId());
		}
		else if(newbuff.getBuffConfig().getOverrideSelfType() == ContinualBuffConfig.OVERRIDE_TYPE_ATTR_ADD && newbuff.getBuffConfig().getOverrideAttr() != 0)
		{
			xbean.Buff existedBuff =  getBuffBean(newbuff.getId());
			Float existedAttr = existedBuff.getEffects().get(newbuff.getBuffConfig().getOverrideAttr());
			if(existedAttr == null)
				existedAttr = 0f;
			Float newAttr = newbuff.getEffects().get(newbuff.getBuffConfig().getOverrideAttr());
			if(newAttr == null)
				newAttr = 0f;
			newAttr = existedAttr + newAttr;
			if(newAttr != 0f)
				newbuff.getEffects().put(newbuff.getBuffConfig().getOverrideAttr(), newAttr);
			return  removeCBuff(newbuff.getId());
		}
		else if(newbuff.getBuffConfig().getOverrideSelfType() == ContinualBuffConfig.OVERRIDE_TYPE_ATTR_HIGHER)
		{
			xbean.Buff existedBuff =  getBuffBean(newbuff.getId());
			Float existedAttr = existedBuff.getEffects().get(newbuff.getBuffConfig().getOverrideAttr());
			existedAttr = (existedAttr == null)? 0f : Math.abs(existedAttr) ;
			Float newAttr = newbuff.getEffects().get(newbuff.getBuffConfig().getOverrideAttr());
			newAttr = (newAttr == null)? 0f : Math.abs(newAttr) ;
			if(existedAttr > newAttr)
				newbuff.getEffects().put(newbuff.getBuffConfig().getOverrideAttr(), existedAttr);
			return removeCBuff(newbuff.getId());
		}
		return new BuffResult(false);
		
	}
	
	/**
	 * 添加持续性Buff，默认用Buff表中填的BuffConfig构造Buff
	 * 发送buff更新协议
	 * 对于NullBuff来说，因为不是实体buff，不能使用此方法添加，如果要判断互斥情况，可以调用canAddBuff
	 * @param buffTypeId
	 * @return Result 
	 */
	public boolean addCBuffWithSP(int buffTypeId)
	{
		
		ContinualBuff buff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(buffTypeId);
		
		//ContinualBuffConfig bufConf = chuhan.gsp.buff.Module.getInstance().getDefaultCBuffConfig(buffTypeId);
		if(buff == null )
		{
			logger.error("Buff(ID: " + buffTypeId +") is not exist.");
			return false;
		}
		return addCBuffWithSP(buff);
	}
	/**
	 * 添加持续性Buff，直接传入ContinualBuff
	 * 发送buff更新协议
	 * 对于NullBuff来说，因为不是实体buff，不能使用此方法添加，如果要判断互斥情况，可以调用canAddBuff
	 * 生成并加载一个buff的步骤：
	 * 1.使用chuhan.gsp.buff.Module.getInstance()的ContinualBuff createContinualBuff(int buffTypeId)构造带默认参数的CBuff
	 * 2.直接对ContinualBuff操作，修改时间回合等参数
	 * 3.调用BuffAgent的Result addCBuffWithSP(ContinualBuff buff)
	 * @param ContinualBuff
	 * @return Result
	 */
	public boolean addCBuffWithSP(ContinualBuff buff)
	{
		BuffResult result = addCBuff(buff);
		if(result == null || !result.isSuccess())
			return false;
		psendSBuffChangeResult(result);
		return true;
	}
	
	/**
	 * 用buffID去除一个持续性Buff 适用于不能叠加Effect的buff类型
	 * 发送buff更新协议，只能在protocol中调用
	 * @param buffId
	 * @return Result
	 */
	public BuffResult removeCBuff(int buffId)
	{
		if(readonly)
			return new BuffResult(false);
		if (existBuff(buffId))
		{
			//TODO 验证是否是不可叠加的buff类型
			ContinualBuff buff = getBuff(buffId);
			BuffResult result = buff.detach(this);
			/*if(result.isSuccess())
				removeSceneState(buff.getBuffConfig());*/
			return result;
		}
		else
			return new BuffResult(false);
	}
	
	/**
	 * 用buffConfig去除一个持续性Buff的部分效果，适用于可以叠加Effect的buff类型
	 * 发送buff更新协议，只能在protocol中调用
	 * @param ContinualBuffConfig
	 * @return
	 */
	public BuffResult removeCBuff(int buffId, Map<Integer,Float> effects)
	{
		if(readonly)
			return new BuffResult(false);
		if (existBuff(buffId))
		{
			ContinualBuff buff = getBuff(buffId);
			
			//计算叠加后的效果
			for(Entry<Integer,Float> effect:effects.entrySet())
			{
				Float oldvalue = buff.getEffects().get(effect.getKey());
				if(oldvalue == null) continue;
				float newvalue = oldvalue + effect.getValue();
				if(newvalue < 0)
				{
					buff.getEffects().remove(effect.getKey());
					continue;
				}
			}
			if(buff.getEffects().size() == 0)
				return buff.detach(this);//buff没有效果了，直接删除还是保留？ FIXME
			else
			{
				BuffResult result = buff.attach(this);
				return result;
			}
			
		} else
			return new BuffResult(false);

	}
	
	/**
	 * 用buffID去除一个持续性Buff 适用于不能叠加Effect的buff类型
	 * 
	 * @param buffId
	 * @return Result
	 */
	public boolean removeCBuffWithSP(int buffId)
	{
		BuffResult result = removeCBuff(buffId);
		if(result == null || !result.isSuccess())
			return false;
		psendSBuffChangeResult(result);
		return true;
	}
		
	public boolean removeCBuffWithSP(int buffId, Map<Integer,Float> effects)
	{
		BuffResult result = removeCBuff(buffId, effects);
		if(result == null || !result.isSuccess())
			return false;
		psendSBuffChangeResult(result);
		return true;
	}
	
	/**
	 * 删除所有只能在战斗中存在的buff和战斗结时应该被清除束的buff，离开战斗时使用
	 * @return Result
	 */
	public BuffResult removeBuffsWhileBattleEnd()
	{
		if(readonly)
			return new BuffResult(false);
		BuffResult result = new BuffResult(true);
		List<Integer> rmBuffIds = new ArrayList<Integer>();
		for(int buffId : getAllBuffBeans().keySet())
		{
			try{	
				ContinualBuffConfig cbuffconf = Module.getInstance().getDefaultCBuffConfig(buffId);
				if(cbuffconf.getClearType() == BuffConstant.CLEAR_TYPE_OUT_BATTLE
						|| cbuffconf.getClearType() == BuffConstant.CLEAR_TYPE_IN_BATTLE_DEATH
						|| cbuffconf.getClearType() == BuffConstant.CLEAR_TYPE_IN_BATTLE_HURT)
					rmBuffIds.add(buffId);
			}catch(Exception e){
				e.printStackTrace();
			}
		}
		for(int rmBuffId : rmBuffIds)
		{
			try{
				result.updateResult(removeCBuff(rmBuffId));
			}catch(Exception e){
				e.printStackTrace();
			}
		}

		return result;
	}
	
	
	/**
	 * 删除所有只能在战斗中存在的buff和战斗结束时应该被清除的buff，离开战斗时使用
	 * @return Result
	 */
	public boolean removeBuffsWhileBattleEndWithSP()
	{
		try{
			if (readonly)
				return false;
			BuffResult result = removeBuffsWhileBattleEnd();
			if (result.isSuccess())
			{
				psendSBuffChangeResult(result);
				return true;
			}
		}catch(Exception e){
			e.printStackTrace();
		}
		return false;
	}
	
	/**
	 * 调用buff的处理，用于PeriodBuff的周期性处理
	 * @param buffId
	 * @return
	 */
	public BuffResult processCBuff(int buffId)
	{
		ContinualBuff buff = this.getBuff(buffId);
		if(buff == null)
			return new BuffResult(false);
		return buff.process(this);
	}
		
	public abstract boolean psendSBuffChangeResult(BuffResult result);
	
/*	public abstract void addSceneState(ContinualBuffConfig buffcfg);
	
	public abstract void removeSceneState(ContinualBuffConfig buffcfg);*/

	protected abstract SBuffChangeResult getSBuffChangeResult(BuffResult result);
	
	public xbean.BuffAgent getAgent()
	{
		return agent;
	}

	public void setAgent(xbean.BuffAgent agent)
	{
		this.agent = agent;
	}

	public boolean isReadonly()
	{
		return readonly;
	}

	public void setReadonly(boolean readonly)
	{
		this.readonly = readonly;
	}

	public abstract chuhan.gsp.attr.AttrAgent getAttrAgent();
		
}
