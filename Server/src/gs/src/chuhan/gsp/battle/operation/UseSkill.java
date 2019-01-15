package chuhan.gsp.battle.operation;

import xbean.BattleInfo;
import chuhan.gsp.battle.BattleSkill;
import chuhan.gsp.battle.Fighter;
import chuhan.gsp.battle.damage.Damage;
import chuhan.gsp.battle.damage.SkillDamage;

public class UseSkill extends BasicOperation {
	private final BattleSkill skill;
	
	public UseSkill(BattleInfo battleInfo, Fighter opfighter, BattleSkill skill) {
		super(battleInfo, opfighter);
		this.skill = skill;
	}

	@Override
	protected int getOperationId() {
		return skill.getSkillId();
	}

	@Override
	protected int getTargetNum()
	{
		return skill.getSkillNum();
	}

	@Override
	protected Damage getDamage() {
		return new SkillDamage();
	}
	
	public BattleSkill getBattleSkill()
	{
		return skill;
	}
	
}
