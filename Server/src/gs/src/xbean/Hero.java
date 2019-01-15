
package xbean;

public interface Hero extends xdb.Bean {
	public Hero copy(); // deep clone
	public Hero toData(); // a Data instance
	public Hero toBean(); // a Bean instance
	public Hero toDataIf(); // a Data instance If need. else return this
	public Hero toBeanIf(); // a Bean instance If need. else return this

	public int getKey(); // 英雄唯一ID（新系统可能不需要）
	public int getHeroid(); // 英雄配表ID
	public int getHeroexp(); // 英雄本级经验
	public int getHerolevel(); // 英雄等级
	public int getHeroviewid(); // 英雄外观
	public int getHerojinjiestar(); // 进阶星级
	public int getHerojinjiesmall(); // 进阶阶级
	public int getHeropinji(); // 品质（升品换英雄配表ID）
	public String getHeroskill(); // 技能（:分割，根据位置记录技能等级）
	public com.goldhuman.Common.Octets getHeroskillOctets(); // 技能（:分割，根据位置记录技能等级）
	public String getHeromishu(); // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
	public com.goldhuman.Common.Octets getHeromishuOctets(); // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
	public String getHeropeiyang(); // 培养（:分割，根据位置记录培养等级）
	public com.goldhuman.Common.Octets getHeropeiyangOctets(); // 培养（:分割，根据位置记录培养等级）
	public String getHeroequip(); // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
	public com.goldhuman.Common.Octets getHeroequipOctets(); // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）

	public void setKey(int _v_); // 英雄唯一ID（新系统可能不需要）
	public void setHeroid(int _v_); // 英雄配表ID
	public void setHeroexp(int _v_); // 英雄本级经验
	public void setHerolevel(int _v_); // 英雄等级
	public void setHeroviewid(int _v_); // 英雄外观
	public void setHerojinjiestar(int _v_); // 进阶星级
	public void setHerojinjiesmall(int _v_); // 进阶阶级
	public void setHeropinji(int _v_); // 品质（升品换英雄配表ID）
	public void setHeroskill(String _v_); // 技能（:分割，根据位置记录技能等级）
	public void setHeroskillOctets(com.goldhuman.Common.Octets _v_); // 技能（:分割，根据位置记录技能等级）
	public void setHeromishu(String _v_); // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
	public void setHeromishuOctets(com.goldhuman.Common.Octets _v_); // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
	public void setHeropeiyang(String _v_); // 培养（:分割，根据位置记录培养等级）
	public void setHeropeiyangOctets(com.goldhuman.Common.Octets _v_); // 培养（:分割，根据位置记录培养等级）
	public void setHeroequip(String _v_); // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
	public void setHeroequipOctets(com.goldhuman.Common.Octets _v_); // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
}
