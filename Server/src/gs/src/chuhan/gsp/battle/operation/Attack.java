package chuhan.gsp.battle.operation;

import java.util.List;

import xbean.BattleInfo;
import chuhan.gsp.battle.BattleUtil;
import chuhan.gsp.battle.Fighter;
import chuhan.gsp.battle.SBattleNPCConfig;
import chuhan.gsp.battle.damage.Damage;
import chuhan.gsp.battle.damage.PhysicalDamage;
import chuhan.gsp.util.Misc;

public class Attack extends BasicOperation{

	public Attack(BattleInfo battleInfo, Fighter opfighter) {
		super(battleInfo, opfighter);
	}

	@Override
	protected int getOperationId() {
		return 0;
	}
	@Override
	protected int getTargetNum()
	{
		return 1;
	}

	@Override
	protected Damage getDamage() {
		return new PhysicalDamage();
	}
	
	protected List<Fighter> getSequenceAims(xbean.BattleInfo battle, Fighter opfighter)
	{
		List<Fighter> seq = BattleUtil.getSequenceAims(battle, opfighter);
		SBattleNPCConfig monstercfg = opfighter.getSBattleNPCConfig();
		if(monstercfg != null && monstercfg.battletype == 1)
			Misc.randomlizeList(seq);
		return seq;
	}

	
}
