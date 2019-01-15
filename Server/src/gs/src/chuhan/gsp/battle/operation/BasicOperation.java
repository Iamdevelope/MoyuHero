package chuhan.gsp.battle.operation;

import java.util.LinkedList;
import java.util.List;

import xbean.BattleInfo;
import chuhan.gsp.battle.BattleDemo;
import chuhan.gsp.battle.BattleUtil;
import chuhan.gsp.battle.Fighter;
import chuhan.gsp.battle.TargetDemo;
import chuhan.gsp.battle.damage.Damage;

public abstract class BasicOperation {
	protected final xbean.BattleInfo battle;
	protected final Fighter opfighter;
	public BasicOperation(BattleInfo battleInfo, Fighter opfighter) {
		this.battle = battleInfo;
		this.opfighter = opfighter;
	}
	
	
	public BattleDemo process()
	{
		BattleDemo demo = new BattleDemo();
		demo.attacker = (byte)(opfighter.getFighterId());
		demo.skillid = (short)getOperationId();
		List<TargetDemo> demos = getTargetDemos(getAims());
		if(demos == null || demos.isEmpty())
			return null;
		demo.targets.addAll(demos);
		return demo;
	}
	
	protected List<Fighter> getSequenceAims(xbean.BattleInfo battle, Fighter opfighter)
	{
		return BattleUtil.getSequenceAims(battle, opfighter);
	}
	
	protected List<Fighter> getAims()
	{
		List<Fighter> fighters = getSequenceAims(battle, opfighter);
		if(fighters.isEmpty())
			return null;
		List<Fighter> aims = new LinkedList<Fighter>();
		for(Fighter fighter : fighters)
		{
			if(fighter.isDeath())
				continue;
			if(!fighter.inBattle())
				continue;
			aims.add(fighter);
			if(aims.size() >= getTargetNum())
				return aims;
		}
		return aims;
	}
	
	protected abstract int getOperationId();
	
	protected abstract int getTargetNum();
	
	protected abstract Damage getDamage();
	
	protected List<TargetDemo> getTargetDemos(List<Fighter> aims)
	{

		List<TargetDemo> demos = new LinkedList<TargetDemo>();
		for(Fighter aimfighter : aims)
		{
			battle.getEngine().setAimFighter(aimfighter);
			Damage damage = getDamage();
			TargetDemo demo = damage.attach(battle, opfighter, aimfighter, this);
			if(demo != null && demo.targetid > 0)
				demos.add(demo);
		}
		return demos;
	
	}
	
}
