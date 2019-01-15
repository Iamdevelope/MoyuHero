
package xbean;

public interface FighterInfo extends xdb.Bean {
	public FighterInfo copy(); // deep clone
	public FighterInfo toData(); // a Data instance
	public FighterInfo toBean(); // a Bean instance
	public FighterInfo toDataIf(); // a Data instance If need. else return this
	public FighterInfo toBeanIf(); // a Bean instance If need. else return this

	public final static int HERO = 1; // 
	public final static int MONSTER = 2; // 

	public int getFighterid(); // 
	public int getFightertype(); // 
	public int getPos(); // 
	public int getHeroid(); // 
	public int getGrouptype(); // 阵营
	public int getLevel(); // 等级
	public int getColor(); // 颜色
	public int getGrade(); // 阶
	public int getWeaponinfo(); // 武器信息
	public int getArmorinfo(); // 铠甲信息
	public int getHorseinfo(); // 战马信息
	public int getSpeed(); // 速
	public int getHp(); // 兵力
	public xbean.BasicFightProperties getBfp(); // 基础战斗属性
	public java.util.Map<Integer, Float> getEffects(); // 效果 key = effect type id
	public java.util.Map<Integer, Float> getEffectsAsData(); // 效果 key = effect type id
	public java.util.Map<Integer, Float> getFinalattrs(); // 最终属性 key = attr type
	public java.util.Map<Integer, Float> getFinalattrsAsData(); // 最终属性 key = attr type
	public xbean.BuffAgent getBuffagent(); // buff代理
	public java.util.List<xbean.BattleSkill> getSkills(); // 技能
	public java.util.List<xbean.BattleSkill> getSkillsAsData(); // 技能
	public int getShape(); // 造型ID

	public void setFighterid(int _v_); // 
	public void setFightertype(int _v_); // 
	public void setPos(int _v_); // 
	public void setHeroid(int _v_); // 
	public void setGrouptype(int _v_); // 阵营
	public void setLevel(int _v_); // 等级
	public void setColor(int _v_); // 颜色
	public void setGrade(int _v_); // 阶
	public void setWeaponinfo(int _v_); // 武器信息
	public void setArmorinfo(int _v_); // 铠甲信息
	public void setHorseinfo(int _v_); // 战马信息
	public void setSpeed(int _v_); // 速
	public void setHp(int _v_); // 兵力
	public void setShape(int _v_); // 造型ID
}
