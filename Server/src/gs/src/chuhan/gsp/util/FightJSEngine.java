package chuhan.gsp.util;

import chuhan.gsp.attr.AttrType;
import chuhan.gsp.battle.Fighter;

public class FightJSEngine extends AbstractJSEngine
{

	public void setOpFighter(Fighter opfighter)
	{
		this.put("hpa", opfighter.getAttrFighter().getHp());
		this.put("armya", opfighter.getAttrFighter().getAttrById(AttrType.ARMY));
		this.put("attacka", opfighter.getAttrFighter().getAttrById(AttrType.ATTACK));
		this.put("defenda", opfighter.getAttrFighter().getAttrById(AttrType.DEFEND));
		this.put("skilla", opfighter.getAttrFighter().getAttrById(AttrType.SKILL));
		this.put("nodefa", opfighter.getAttrFighter().getAttrById(AttrType.NO_DEF));
		this.put("nohurta", opfighter.getAttrFighter().getAttrById(AttrType.NO_HURT));
		this.put("singledefa", opfighter.getAttrFighter().getAttrById(AttrType.ANTI_SINGLE_SKILL));
		this.put("multidefa", opfighter.getAttrFighter().getAttrById(AttrType.ANTI_MULTI_SKILL));
	}
	
	public void setAimFighter(Fighter aimfighter)
	{
		this.put("hpb", aimfighter.getAttrFighter().getHp());
		this.put("armyb", aimfighter.getAttrFighter().getAttrById(AttrType.ARMY));
		this.put("attackb", aimfighter.getAttrFighter().getAttrById(AttrType.ATTACK));
		this.put("defendb", aimfighter.getAttrFighter().getAttrById(AttrType.DEFEND));
		this.put("skillb", aimfighter.getAttrFighter().getAttrById(AttrType.SKILL));
		this.put("nodefb", aimfighter.getAttrFighter().getAttrById(AttrType.NO_DEF));
		this.put("nohurtb", aimfighter.getAttrFighter().getAttrById(AttrType.NO_HURT));
		this.put("singledefb", aimfighter.getAttrFighter().getAttrById(AttrType.ANTI_SINGLE_SKILL));
		this.put("multidefb", aimfighter.getAttrFighter().getAttrById(AttrType.ANTI_MULTI_SKILL));
	}
	
	public void setSkillPower(float power)
	{
		this.put("powera", power);
	}
	
	public void setSkillReduce(double reduce)
	{
		this.put("reducea", reduce);
	}
	
	
	public void setLevel(int level)
	{
		this.put("level", level);
	}
	
}
