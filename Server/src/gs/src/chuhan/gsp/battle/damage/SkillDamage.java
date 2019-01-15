package chuhan.gsp.battle.damage;

import chuhan.gsp.attr.AttrType;
import chuhan.gsp.battle.BattleUtil;
import chuhan.gsp.battle.operation.UseSkill;

public class SkillDamage extends Damage{
	
	@Override
	protected boolean isHit() {
		return true;
	}
	
	protected boolean isCrit()
	{
		float critrate = opfighter.getAttrFighter().getAttrById(AttrType.POWER_CRUEL_RATE);
		float anticrit = aimfighter.getAttrFighter().getAttrById(AttrType.ANTI_CRUEL_RATE);
		return Math.random() < (critrate - anticrit);
	}
	
	@Override
	protected double getCritValue() {
		return 2.0;
	}
	
	protected int calcDamage()
	{
		UseSkill skilloperation = ((UseSkill)operation);
		/*float attacka = opfighter.getAttrFighter().getAttrById(AttrType.ATTACK);
		float skilla = opfighter.getAttrFighter().getAttrById(AttrType.SKILL);
		float nodefa = opfighter.getAttrFighter().getAttrById(AttrType.NO_DEF);
		
		float skillb = aimfighter.getAttrFighter().getAttrById(AttrType.SKILL);
		float defendb = aimfighter.getAttrFighter().getAttrById(AttrType.DEFEND);
		float nohurtb = aimfighter.getAttrFighter().getAttrById(AttrType.NO_HURT);*/
		
		int distance = BattleUtil.getDistance(opfighter.getPosition(), aimfighter.getPosition());
		float powera = skilloperation.getBattleSkill().getSkillPower();
		double reducea = skilloperation.getBattleSkill().getSkillReduce(distance);
		battleInfo.getEngine().setSkillPower(powera);
		battleInfo.getEngine().setSkillReduce(reducea);
		double v = -battleInfo.getEngine().evalToDouble("with(Math){"+skilloperation.getBattleSkill().getConfig().hurt+"}");
		//double v = -Math.max( (attacka+powera*skilla/100)-(defendb+skillb*0.3)*(1-nodefa), (attacka+powera*skilla/100)*0.1 )*(1-nohurtb)*reducea;
		return (int)Math.min(-1, v);
	}

}
