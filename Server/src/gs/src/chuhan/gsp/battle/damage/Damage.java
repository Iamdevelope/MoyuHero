package chuhan.gsp.battle.damage;


import chuhan.gsp.battle.Fighter;
import chuhan.gsp.battle.ResultType;
import chuhan.gsp.battle.TargetDemo;
import chuhan.gsp.battle.operation.BasicOperation;
import chuhan.gsp.buff.BuffConstant;
import chuhan.gsp.util.Conv;

public abstract class Damage {
	protected xbean.BattleInfo battleInfo;
	protected Fighter opfighter;
	protected Fighter aimfighter; 
	protected BasicOperation operation;
	
	protected TargetDemo targetdemo = new TargetDemo();
	
	protected int damage;
	
	public TargetDemo attach(xbean.BattleInfo battleInfo,Fighter opfighter, Fighter aimfighter,BasicOperation operation)
	{
		init(battleInfo, opfighter, aimfighter, operation);
		
		if(!processDamage())
			return null;
		
		end();
		return targetdemo;
	}
	
	protected void init(xbean.BattleInfo battleInfo,Fighter opfighter, Fighter aimfighter,BasicOperation operation)
	{
		this.battleInfo = battleInfo;
		this.opfighter = opfighter;
		this.aimfighter = aimfighter;
		this.operation = operation;
		targetdemo.targetid = (byte)aimfighter.getFighterId();
	}
	
	protected void end()
	{
		
	}
	
	protected boolean processDamage()
	{
		if(aimfighter.isDeath())
			return false;
		if(!aimfighter.inBattle())
			return false;
		if(!isHit())
		{
			targetdemo.targetresult |= ResultType.RESULT_DODGE;
			return true;
		}
		
		damage = calcCritAndDamage();
		damage = amendDamage(damage);
		dealDamage(damage);
		return true;
	}
	
	protected int calcCritAndDamage() {
		
		int damage = calcDamage();
		if(isCrit())
		{
			targetdemo.targetresult |= ResultType.RESULT_CRIT;
			damage = (int)(damage * getCritValue());
		}
		
		return damage;
	}
	
	protected int amendDamage(int damage)
	{
		if(battleInfo.getRound() >= 3)
			damage = (int)(damage * (1.0 + 0.2 * battleInfo.getTurn()));
		damage = (int)(damage * opfighter.getGroupAmendByAim(aimfighter.getGroupType()));
		return damage;
	}
	
	protected abstract boolean isHit();
	protected abstract boolean isCrit();
	protected abstract double getCritValue();
	protected abstract int calcDamage();
	
	
	protected void dealDamage(int damage)
	{
		aimfighter.attachHpChange(damage);
		targetdemo.hpchange = damage;
		if (aimfighter.getAttrFighter().getHp() <= 0)
		{
			targetdemo.targetresult |= dealDeath(opfighter,aimfighter);
		}
	}
	
	protected int dealDeath(Fighter opfighter, Fighter aimfighter)
	{
		aimfighter.getBuffFighter().addCBuff(BuffConstant.BUFF_DEATH);
		return ResultType.RESULT_DEATH;
	}
}
