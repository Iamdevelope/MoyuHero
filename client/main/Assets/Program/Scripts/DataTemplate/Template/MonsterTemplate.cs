//
// autor : OgreZCD
// date : 2015-10-22
//

using UnityEngine;
using System.Collections;
using System.IO;


public class MonsterTemplate : IExcelBean
{
	#region attribute
	//怪物id
	private	int m_id;
	//怪物名称
	private	string m_monstername;
	//名称ID
	private	string m_NameID;
	//描述ID
	private	string m_DescriptionID;
	//资源id
	private	int m_artresources;
	//怪物类型
	private	int m_monstertype;
	//普通攻击
	private	int m_normalattack;
	//技能
	private	int[] m_monsterskill;
	//初始生命点
	private	int m_MaxHP;
	//初始物理攻击点
	private	int m_PhysicalAttack;
	//初始物理防御点
	private	int m_PhysicalDefence;
	//初始法术攻击点
	private	int m_MagicAttack;
	//初始法术防御点
	private	int m_MagicDefence;
	//初始命中点
	private	int m_Hit;
	//初始闪避点
	private	int m_Dodge;
	//初始暴击点
	private	int m_Critical;
	//初始韧性点
	private	int m_Tenacity;
	//初始速度点
	private	int m_Speed;
	//命中几率千分比
	private	int m_BaseHit;
	//闪避几率千分比
	private	int m_BaseDodge;
	//暴击几率千分比
	private	int m_BaseCritical;
	//韧性几率千分比
	private	int m_BaseTenacity;
	//物伤加成千分比
	private	int m_BasePhyDamageIncrease;
	//物伤减免千分比
	private	int m_BasePhyDamageDecrease;
	//法伤加成千分比
	private	int m_BaseMagDamageIncrease;
	//法伤减免千分比
	private	int m_BaseMagDamageDecrease;
	//暴击伤害加成千分比
	private	int m_BaseCriticalDamage;
	//伤害附加点
	private	int m_DamageIncrease;
	//生命恢复点
	private	int m_lifeRestoringForce;
	//伤害减免点
	private	int m_DamageDecrease;
	//角色类型标记（客户端）
	private	int[] m_clientSignType;
	//血条长度放大倍数
	private	int m_HPLengthMultiple;
	//血条宽度放大倍数
	private	int m_HPWidthMultiple;
	//伤害字冒出位置上下偏移量
	private	float m_damageTextShow;
	//等级框和字放大倍数
	private	int m_levelBorderMultiple;
	//每损失1%生命奖励怒气值模板ID
	private	int m_HPTransformFury;
	//所属阵营
	private	int m_camp;
	//伤害达到生命上限%受击后仰
	private	int m_fadeCondition;
	//死亡慢动作时间（秒）
	private	float[] m_deathSlowTime;
	//怪物等级
	private	int m_monsterlevel;
	//怪物星级
	private	int m_monsterstar;
	//怪物放大系数
	private	float m_monsterEnlarge;
	//怪物百分比计算时生命值
	private	int m_monsterPercentMaxHp;
	//死亡掉落SKILL类型
	private	int[] m_deathSkillType;
	//死亡掉落SKILL权重
	private	int[] m_deathSkillProb;
	//怪物最大星级
	private	int m_monstermaxstar;
	//格挡几率千分比
	private	int m_BlockHit;
	//破击几率千分比
	private	int m_SabotageHit;
	//伤害加成千分比
	private	int m_DamageBonusHit;
	//伤害减免千分比
	private	int m_DamageReductionHit;
	//吸血率千分比
	private	int m_VampireRate;
	//怒气序列
	private	int m_FuryId;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_monstername = ReadToString(data);
		m_NameID = ReadToString(data);
		m_DescriptionID = ReadToString(data);
		m_artresources = data.ReadInt32();
		m_monstertype = data.ReadInt32();
		m_normalattack = data.ReadInt32();
		m_monsterskill = parserXMLIntArray(ReadToString(data));
		m_MaxHP = data.ReadInt32();
		m_PhysicalAttack = data.ReadInt32();
		m_PhysicalDefence = data.ReadInt32();
		m_MagicAttack = data.ReadInt32();
		m_MagicDefence = data.ReadInt32();
		m_Hit = data.ReadInt32();
		m_Dodge = data.ReadInt32();
		m_Critical = data.ReadInt32();
		m_Tenacity = data.ReadInt32();
		m_Speed = data.ReadInt32();
		m_BaseHit = data.ReadInt32();
		m_BaseDodge = data.ReadInt32();
		m_BaseCritical = data.ReadInt32();
		m_BaseTenacity = data.ReadInt32();
		m_BasePhyDamageIncrease = data.ReadInt32();
		m_BasePhyDamageDecrease = data.ReadInt32();
		m_BaseMagDamageIncrease = data.ReadInt32();
		m_BaseMagDamageDecrease = data.ReadInt32();
		m_BaseCriticalDamage = data.ReadInt32();
		m_DamageIncrease = data.ReadInt32();
		m_lifeRestoringForce = data.ReadInt32();
		m_DamageDecrease = data.ReadInt32();
		m_clientSignType = parserXMLIntArray(ReadToString(data));
		m_HPLengthMultiple = data.ReadInt32();
		m_HPWidthMultiple = data.ReadInt32();
		m_damageTextShow = ReadToSingle(data);
		m_levelBorderMultiple = data.ReadInt32();
		m_HPTransformFury = data.ReadInt32();
		m_camp = data.ReadInt32();
		m_fadeCondition = data.ReadInt32();
		m_deathSlowTime = parserXMLFloatArray(ReadToString(data));
		m_monsterlevel = data.ReadInt32();
		m_monsterstar = data.ReadInt32();
		m_monsterEnlarge = ReadToSingle(data);
		m_monsterPercentMaxHp = data.ReadInt32();
		m_deathSkillType = parserXMLIntArray(ReadToString(data));
		m_deathSkillProb = parserXMLIntArray(ReadToString(data));
		m_monstermaxstar = data.ReadInt32();
		m_BlockHit = data.ReadInt32();
		m_SabotageHit = data.ReadInt32();
		m_DamageBonusHit = data.ReadInt32();
		m_DamageReductionHit = data.ReadInt32();
		m_VampireRate = data.ReadInt32();
		m_FuryId = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getMonstername()
	{
		return this.m_monstername;
	}

	public	string	getNameID()
	{
		return this.m_NameID;
	}

	public	string	getDescriptionID()
	{
		return this.m_DescriptionID;
	}

	public	int	getArtresources()
	{
		return this.m_artresources;
	}

	public	int	getMonstertype()
	{
		return this.m_monstertype;
	}

	public	int	getNormalattack()
	{
		return this.m_normalattack;
	}

	public	int[]	getMonsterskill()
	{
		return this.m_monsterskill;
	}

	public	int	getMaxHP()
	{
		return this.m_MaxHP;
	}

	public	int	getPhysicalAttack()
	{
		return this.m_PhysicalAttack;
	}

	public	int	getPhysicalDefence()
	{
		return this.m_PhysicalDefence;
	}

	public	int	getMagicAttack()
	{
		return this.m_MagicAttack;
	}

	public	int	getMagicDefence()
	{
		return this.m_MagicDefence;
	}

	public	int	getHit()
	{
		return this.m_Hit;
	}

	public	int	getDodge()
	{
		return this.m_Dodge;
	}

	public	int	getCritical()
	{
		return this.m_Critical;
	}

	public	int	getTenacity()
	{
		return this.m_Tenacity;
	}

	public	int	getSpeed()
	{
		return this.m_Speed;
	}

	public	int	getBaseHit()
	{
		return this.m_BaseHit;
	}

	public	int	getBaseDodge()
	{
		return this.m_BaseDodge;
	}

	public	int	getBaseCritical()
	{
		return this.m_BaseCritical;
	}

	public	int	getBaseTenacity()
	{
		return this.m_BaseTenacity;
	}

	public	int	getBasePhyDamageIncrease()
	{
		return this.m_BasePhyDamageIncrease;
	}

	public	int	getBasePhyDamageDecrease()
	{
		return this.m_BasePhyDamageDecrease;
	}

	public	int	getBaseMagDamageIncrease()
	{
		return this.m_BaseMagDamageIncrease;
	}

	public	int	getBaseMagDamageDecrease()
	{
		return this.m_BaseMagDamageDecrease;
	}

	public	int	getBaseCriticalDamage()
	{
		return this.m_BaseCriticalDamage;
	}

	public	int	getDamageIncrease()
	{
		return this.m_DamageIncrease;
	}

	public	int	getLifeRestoringForce()
	{
		return this.m_lifeRestoringForce;
	}

	public	int	getDamageDecrease()
	{
		return this.m_DamageDecrease;
	}

	public	int[]	getClientSignType()
	{
		return this.m_clientSignType;
	}

	public	int	getHPLengthMultiple()
	{
		return this.m_HPLengthMultiple;
	}

	public	int	getHPWidthMultiple()
	{
		return this.m_HPWidthMultiple;
	}

	public	float	getDamageTextShow()
	{
		return this.m_damageTextShow;
	}

	public	int	getLevelBorderMultiple()
	{
		return this.m_levelBorderMultiple;
	}

	public	int	getHPTransformFury()
	{
		return this.m_HPTransformFury;
	}

	public	int	getCamp()
	{
		return this.m_camp;
	}

	public	int	getFadeCondition()
	{
		return this.m_fadeCondition;
	}

	public	float[]	getDeathSlowTime()
	{
		return this.m_deathSlowTime;
	}

	public	int	getMonsterlevel()
	{
		return this.m_monsterlevel;
	}

	public	int	getMonsterstar()
	{
		return this.m_monsterstar;
	}

	public	float	getMonsterEnlarge()
	{
		return this.m_monsterEnlarge;
	}

	public	int	getMonsterPercentMaxHp()
	{
		return this.m_monsterPercentMaxHp;
	}

	public	int[]	getDeathSkillType()
	{
		return this.m_deathSkillType;
	}

	public	int[]	getDeathSkillProb()
	{
		return this.m_deathSkillProb;
	}

	public	int	getMonstermaxstar()
	{
		return this.m_monstermaxstar;
	}

	public	int	getBlockHit()
	{
		return this.m_BlockHit;
	}

	public	int	getSabotageHit()
	{
		return this.m_SabotageHit;
	}

	public	int	getDamageBonusHit()
	{
		return this.m_DamageBonusHit;
	}

	public	int	getDamageReductionHit()
	{
		return this.m_DamageReductionHit;
	}

	public	int	getVampireRate()
	{
		return this.m_VampireRate;
	}

	public	int	getFuryId()
	{
		return this.m_FuryId;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}