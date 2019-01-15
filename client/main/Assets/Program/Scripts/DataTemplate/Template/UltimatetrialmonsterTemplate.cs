//
// autor : OgreZCD
// date : 2015-07-22
//

using UnityEngine;
using System.Collections;
using System.IO;


public class UltimatetrialmonsterTemplate : IExcelBean
{
	#region attribute
	//关卡id
	private	int m_id;
	//怪物组权重
	private	int[] m_Probability;
	//生命系数
	private	float m_MaxHPCoefficient;
	//物攻系数
	private	float m_PhysicalAttackCoefficient;
	//物防系数
	private	float m_PhysicalDefenceCoefficient;
	//法攻系数
	private	float m_MagicAttackCoefficient;
	//法防系数
	private	float m_MagicDefenceCoefficient;
	//命中值系数
	private	float m_HitCoefficient;
	//闪避值系数
	private	float m_DodgeCoefficient;
	//暴击值系数
	private	float m_CriticalCoefficient;
	//韧性值系数
	private	float m_TenacityCoefficient;
	//生命值
	private	float m_AdditionalMaxHP;
	//物理攻击力
	private	float m_AdditionalPhysicalAttack;
	//物理防御力
	private	float m_AdditionalPhysicalDefence;
	//法术攻击力
	private	float m_AdditionalMagicAttack;
	//法术防御力
	private	float m_AdditionalMagicDefence;
	//命中值
	private	float m_AdditionalHit;
	//闪避值
	private	float m_AdditionalDodge;
	//暴击值
	private	float m_AdditionalCritical;
	//韧性值
	private	float m_AdditionalTenacity;
	//速度值
	private	float m_AdditionalSpeed;
	//命中率
	private	float m_AdditionalBaseHit;
	//闪避率
	private	float m_AdditionalBaseDodge;
	//暴击率
	private	float m_AdditionalBaseCritical;
	//韧性率
	private	float m_AdditionalBaseTenacity;
	//物伤加成率
	private	float m_AdditionalBasePhyDamageIncrease;
	//物伤减免率
	private	float m_AdditionalBasePhyDamageDecrease;
	//法伤加成率
	private	float m_AdditionalBaseMagDamageIncrease;
	//法伤减免率
	private	float m_AdditionalBaseMagDamageDecrease;
	//暴击伤害加成率
	private	float m_AdditionalBaseCriticalDamage;
	//附加伤害值
	private	float m_AdditionalDamageIncrease;
	//生命恢复力
	private	float m_AdditionallifeRestoringForce;
	//绝对减免值
	private	float m_AdditionalDamageDecrease;
	//怪物等级加成
	private	int m_AdditionalLevel;
	//技能ID附加值
	private	int m_AdditionalSkill;
	//本波奖励掉落
	private	int m_wavereward;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_Probability = parserXMLIntArray(ReadToString(data));
		m_MaxHPCoefficient = ReadToSingle(data);
		m_PhysicalAttackCoefficient = ReadToSingle(data);
		m_PhysicalDefenceCoefficient = ReadToSingle(data);
		m_MagicAttackCoefficient = ReadToSingle(data);
		m_MagicDefenceCoefficient = ReadToSingle(data);
		m_HitCoefficient = ReadToSingle(data);
		m_DodgeCoefficient = ReadToSingle(data);
		m_CriticalCoefficient = ReadToSingle(data);
		m_TenacityCoefficient = ReadToSingle(data);
		m_AdditionalMaxHP = ReadToSingle(data);
		m_AdditionalPhysicalAttack = ReadToSingle(data);
		m_AdditionalPhysicalDefence = ReadToSingle(data);
		m_AdditionalMagicAttack = ReadToSingle(data);
		m_AdditionalMagicDefence = ReadToSingle(data);
		m_AdditionalHit = ReadToSingle(data);
		m_AdditionalDodge = ReadToSingle(data);
		m_AdditionalCritical = ReadToSingle(data);
		m_AdditionalTenacity = ReadToSingle(data);
		m_AdditionalSpeed = ReadToSingle(data);
		m_AdditionalBaseHit = ReadToSingle(data);
		m_AdditionalBaseDodge = ReadToSingle(data);
		m_AdditionalBaseCritical = ReadToSingle(data);
		m_AdditionalBaseTenacity = ReadToSingle(data);
		m_AdditionalBasePhyDamageIncrease = ReadToSingle(data);
		m_AdditionalBasePhyDamageDecrease = ReadToSingle(data);
		m_AdditionalBaseMagDamageIncrease = ReadToSingle(data);
		m_AdditionalBaseMagDamageDecrease = ReadToSingle(data);
		m_AdditionalBaseCriticalDamage = ReadToSingle(data);
		m_AdditionalDamageIncrease = ReadToSingle(data);
		m_AdditionallifeRestoringForce = ReadToSingle(data);
		m_AdditionalDamageDecrease = ReadToSingle(data);
		m_AdditionalLevel = data.ReadInt32();
		m_AdditionalSkill = data.ReadInt32();
		m_wavereward = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int[]	getProbability()
	{
		return this.m_Probability;
	}

	public	float	getMaxHPCoefficient()
	{
		return this.m_MaxHPCoefficient;
	}

	public	float	getPhysicalAttackCoefficient()
	{
		return this.m_PhysicalAttackCoefficient;
	}

	public	float	getPhysicalDefenceCoefficient()
	{
		return this.m_PhysicalDefenceCoefficient;
	}

	public	float	getMagicAttackCoefficient()
	{
		return this.m_MagicAttackCoefficient;
	}

	public	float	getMagicDefenceCoefficient()
	{
		return this.m_MagicDefenceCoefficient;
	}

	public	float	getHitCoefficient()
	{
		return this.m_HitCoefficient;
	}

	public	float	getDodgeCoefficient()
	{
		return this.m_DodgeCoefficient;
	}

	public	float	getCriticalCoefficient()
	{
		return this.m_CriticalCoefficient;
	}

	public	float	getTenacityCoefficient()
	{
		return this.m_TenacityCoefficient;
	}

	public	float	getAdditionalMaxHP()
	{
		return this.m_AdditionalMaxHP;
	}

	public	float	getAdditionalPhysicalAttack()
	{
		return this.m_AdditionalPhysicalAttack;
	}

	public	float	getAdditionalPhysicalDefence()
	{
		return this.m_AdditionalPhysicalDefence;
	}

	public	float	getAdditionalMagicAttack()
	{
		return this.m_AdditionalMagicAttack;
	}

	public	float	getAdditionalMagicDefence()
	{
		return this.m_AdditionalMagicDefence;
	}

	public	float	getAdditionalHit()
	{
		return this.m_AdditionalHit;
	}

	public	float	getAdditionalDodge()
	{
		return this.m_AdditionalDodge;
	}

	public	float	getAdditionalCritical()
	{
		return this.m_AdditionalCritical;
	}

	public	float	getAdditionalTenacity()
	{
		return this.m_AdditionalTenacity;
	}

	public	float	getAdditionalSpeed()
	{
		return this.m_AdditionalSpeed;
	}

	public	float	getAdditionalBaseHit()
	{
		return this.m_AdditionalBaseHit;
	}

	public	float	getAdditionalBaseDodge()
	{
		return this.m_AdditionalBaseDodge;
	}

	public	float	getAdditionalBaseCritical()
	{
		return this.m_AdditionalBaseCritical;
	}

	public	float	getAdditionalBaseTenacity()
	{
		return this.m_AdditionalBaseTenacity;
	}

	public	float	getAdditionalBasePhyDamageIncrease()
	{
		return this.m_AdditionalBasePhyDamageIncrease;
	}

	public	float	getAdditionalBasePhyDamageDecrease()
	{
		return this.m_AdditionalBasePhyDamageDecrease;
	}

	public	float	getAdditionalBaseMagDamageIncrease()
	{
		return this.m_AdditionalBaseMagDamageIncrease;
	}

	public	float	getAdditionalBaseMagDamageDecrease()
	{
		return this.m_AdditionalBaseMagDamageDecrease;
	}

	public	float	getAdditionalBaseCriticalDamage()
	{
		return this.m_AdditionalBaseCriticalDamage;
	}

	public	float	getAdditionalDamageIncrease()
	{
		return this.m_AdditionalDamageIncrease;
	}

	public	float	getAdditionallifeRestoringForce()
	{
		return this.m_AdditionallifeRestoringForce;
	}

	public	float	getAdditionalDamageDecrease()
	{
		return this.m_AdditionalDamageDecrease;
	}

	public	int	getAdditionalLevel()
	{
		return this.m_AdditionalLevel;
	}

	public	int	getAdditionalSkill()
	{
		return this.m_AdditionalSkill;
	}

	public	int	getWavereward()
	{
		return this.m_wavereward;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}