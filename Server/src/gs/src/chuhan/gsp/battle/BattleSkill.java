package chuhan.gsp.battle;

import chuhan.gsp.item.SSkill;

public class BattleSkill {
	
	public final static int SKILL_TYPE_SINGLE = 1;
	public final static int SKILL_TYPE_MULTI = 2;
	public final static int SKILL_TYPE_BUFF = 3;
	
	public final int skillId;
	public final int level;
	public final SSkill cfg;
	public BattleSkill(int skillId, int level, SSkill cfg) {
		this.skillId = skillId;
		this.level = level;
		this.cfg = cfg;
	}
	public int getSkillId() {
		return skillId;
	}
	public SSkill getConfig() {
		return cfg;
	}
	
	public int getSkillLevel(){
		return this.level;
	}
	
	public int getSkillPower()
	{
		return (int)(cfg.skillstrength + cfg.skillstrength_grow * level);
	}
	
	public double getSkillReduce(int distance)
	{
		if(distance == 0)
			return 1;
		else if(distance == 1)
			return cfg.reduce1;
		else if(distance == 2)
			return cfg.reduce2;
		else if(distance == 3)
			return cfg.reduce3;
		else if(distance == 4)
			return cfg.reduce4;
		else
			return 0;
	}
	
	public int getSkillNum()
	{
		return cfg.effectNum;
	}
	
	
}
