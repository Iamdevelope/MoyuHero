package chuhan.gsp.battle.damage;

import chuhan.gsp.attr.AttrType;
import chuhan.gsp.battle.BattleUtil;

public class PhysicalDamage extends Damage{

	@Override
	protected boolean isHit() {
		float hita = opfighter.getAttrFighter().getAttrById(AttrType.HIT);
		float dodgeb = aimfighter.getAttrFighter().getAttrById(AttrType.DODGE);
		return Math.random() <= (hita-dodgeb);
	}
	@Override
	protected boolean isCrit()
	{
		float critrate = opfighter.getAttrFighter().getAttrById(AttrType.CRUEL_RATE);
		float anticrit = aimfighter.getAttrFighter().getAttrById(AttrType.ANTI_CRUEL_RATE);
		return Math.random() < (critrate - anticrit);
	}
	
	@Override
	protected double getCritValue() {
		return 1.5;
	}
	
	@Override
	protected int calcDamage()
	{
		float attacka = opfighter.getAttrFighter().getAttrById(AttrType.ATTACK);
		float defendb = aimfighter.getAttrFighter().getAttrById(AttrType.DEFEND);
		float nodefa = opfighter.getAttrFighter().getAttrById(AttrType.NO_DEF);
		float nohurtb = aimfighter.getAttrFighter().getAttrById(AttrType.NO_HURT);
		int distance = BattleUtil.getDistance(opfighter.getPosition(), aimfighter.getPosition());
		float reducea = 0f;
		switch(distance)
		{
		case 1:
			reducea = 0.18f;
			break;
		case 2:
			reducea = 0.42f;
			break;
		case 3:
			reducea = 0.56f;
			break;
		case 4:
			reducea = 0.7f;
			break;
		}
		return (int)Math.min(-1, -Math.max( attacka-defendb*(1-nodefa), attacka*0.1 )*(1-nohurtb)*(1-reducea));
	}

}
