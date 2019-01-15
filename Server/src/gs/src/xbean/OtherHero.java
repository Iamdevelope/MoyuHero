
package xbean;

public interface OtherHero extends xdb.Bean {
	public OtherHero copy(); // deep clone
	public OtherHero toData(); // a Data instance
	public OtherHero toBean(); // a Bean instance
	public OtherHero toDataIf(); // a Data instance If need. else return this
	public OtherHero toBeanIf(); // a Bean instance If need. else return this

	public int getHeroid(); // 英雄配表ID
	public int getExp(); // 当前经验
	public int getHerolevel(); // 英雄等级
	public int getHp(); // 血量
	public int getPhysicalattack(); // 物理攻击
	public int getPhysicaldefence(); // 物理防御
	public int getMagicattack(); // 魔法攻击
	public int getMagicdefence(); // 魔法防御
	public int getSkill1(); // 技能1编号（未开通为0）
	public int getSkill2(); // 技能2编号（未开通为0）
	public int getSkill3(); // 技能3编号（未开通为0）
	public int getHeroviewid(); // 英雄外观

	public void setHeroid(int _v_); // 英雄配表ID
	public void setExp(int _v_); // 当前经验
	public void setHerolevel(int _v_); // 英雄等级
	public void setHp(int _v_); // 血量
	public void setPhysicalattack(int _v_); // 物理攻击
	public void setPhysicaldefence(int _v_); // 物理防御
	public void setMagicattack(int _v_); // 魔法攻击
	public void setMagicdefence(int _v_); // 魔法防御
	public void setSkill1(int _v_); // 技能1编号（未开通为0）
	public void setSkill2(int _v_); // 技能2编号（未开通为0）
	public void setSkill3(int _v_); // 技能3编号（未开通为0）
	public void setHeroviewid(int _v_); // 英雄外观
}
