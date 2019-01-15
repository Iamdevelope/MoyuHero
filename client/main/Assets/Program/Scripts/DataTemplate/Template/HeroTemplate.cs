//
// autor : OgreZCD
// date : 2015-10-27
//

using UnityEngine;
using System.Collections;
using System.IO;


public class HeroTemplate : IExcelBean
{
	#region attribute
	//英雄ID
	private	int m_id;
	//名称ID
	private	string m_NameID;
	//描述ID
	private	string m_DescriptionID;
	//特点
	private	string m_tedian;
	//品质
	private	int m_Quality;
	//英雄类型
	private	int m_herotype;
	//初始生命点
	private	int m_InitMaxHP;
	//初始物理攻击点
	private	int m_InitPhysicalAttack;
	//初始物理防御点
	private	int m_InitPhysicalDefence;
	//初始法术攻击点
	private	int m_InitMagicAttack;
	//初始法术防御点
	private	int m_InitMagicDefence;
	//初始命中点
	private	int m_InitHit;
	//初始闪避点
	private	int m_InitDodge;
	//初始暴击点
	private	int m_InitCritical;
	//初始韧性点
	private	int m_InitTenacity;
	//初始速度点
	private	int m_InitSpeed;
	//生命点成长
	private	float m_HPGrowth;
	//生命点等级修正模板ID
	private	int m_HPGrowthMultiple;
	//物理攻击点成长
	private	float m_PhysicalAttackGrowth;
	//物理攻击点等级修正模板ID
	private	int m_PhysicalAttackGrowthMultiple;
	//物理防御点成长
	private	float m_PhysicalDefenceGrowth;
	//物理防御点等级修正模板ID
	private	int m_PhysicalDefenceGrowthMultiple;
	//法术攻击点成长
	private	float m_MagicAttackGrowth;
	//法术攻击点等级修正模板ID
	private	int m_MagicAttackGrowthMultiple;
	//法术防御点成长
	private	float m_MagicDefenceGrowth;
	//法术防御点等级修正模板ID
	private	int m_MagicDefenceGrowthMultiple;
	//命中点成长
	private	float m_HitGrowth;
	//命中点等级修正模板ID
	private	int m_HitGrowthMultiple;
	//闪避点成长
	private	float m_DodgeGrowth;
	//闪避点等级修正模板ID
	private	int m_DodgeGrowthMultiple;
	//暴击点成长
	private	float m_CriticalGrowth;
	//暴击点等级修正模板ID
	private	int m_CriticalGrowthMultiple;
	//韧性点成长
	private	float m_TenacityGrowth;
	//韧性点等级修正模板ID
	private	int m_TenacityGrowthMultiple;
	//速度点成长
	private	float m_SpeedGrowth;
	//速度点等级修正模板ID
	private	int m_SpeedGrowthMultiple;
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
	//等级上限
	private	int m_MaxLevel;
	//经验值列
	private	int m_ExpNum;
	//普攻技能ID
	private	int m_normalskill;
	//技能1ID
	private	int m_skill1ID;
	//技能2ID
	private	int m_skill2ID;
	//技能3ID
	private	int m_skill3ID;
	//默认资源id
	private	int m_artresources;
	//移动速度
	private	float m_movespeed;
	//每损失1%生命奖励怒气值模板ID
	private	int m_HPTransformFury;
	//入场奖励怒气值模板ID
	private	int m_entranceFury;
	//每波奖励怒气模板ID
	private	int m_waveFury;
	//角色类型标记（客户端）
	private	int[] m_clientSignType;
	//所属阵营
	private	int m_camp;
	//伤害达到生命上限%受击后仰
	private	int m_fadeCondition;
	//称号ID
	private	string m_titleID;
	//英雄搭配
	private	int[] m_skillPair;
	//符文搭配1
	private	int m_runePair1;
	//符文搭配2
	private	int m_runePair2;
	//符文搭配3
	private	int m_runePair3;
	//符文搭配4
	private	int m_runePair4;
	//符文组合效果
	private	int m_runePassive;
	//升阶消耗资源ID1
	private	int m_stageUpCostType1;
	//升阶消耗资源数量1
	private	int m_stageUpCost1;
	//升阶消耗资源ID2
	private	int m_stageUpCostType2;
	//升阶消耗资源数量2
	private	int m_stageUpCost2;
	//配套资源id们
	private	int[] m_useableArtresource;
	//培养使用库1
	private	int m_trainSlot1;
	//培养最大次数1
	private	int m_trainMaximum1;
	//培养使用库2
	private	int m_trainSlot2;
	//培养最大次数2
	private	int m_trainMaximum2;
	//培养使用库3
	private	int m_trainSlot3;
	//培养最大次数3
	private	int m_trainMaximum3;
	//培养使用库4
	private	int m_trainSlot4;
	//培养最大次数4
	private	int m_trainMaximum4;
	//进阶后的ID
	private	int m_stageUpTargetID;
	//最高星级
	private	int m_maxQuality;
	//熔灵返还
	private	int m_returnBack;
	//英雄搭配描述
	private	string m_heroDes;
	//技能最高等级
	private	int m_skillMaxLevel;
	//获得途径
	private	string m_accessMethod;
	//系统广播
	private	int m_systemBroadcasts;
	//资质
	private	int m_Born;
	//定位
	private	int m_Qosition;
	//格挡几率千分比
	private	int m_BlockHit;
	//破甲几率千分比
	private	int m_SabotageHit;
	//伤害加成千分比
	private	int m_DamageBonusHit;
	//伤害减免千分比
	private	int m_DamageReductionHit;
	//怒气序列
	private	int m_FuryId;
	//升品游戏币消耗
	private	int m_Gold;
	//升品材料id
	private	int m_Stuff;
	//材料数量
	private	int m_Numbers;
	//合成排序id
	private	int m_Paxid;
	//合成道具所需id
	private	int m_SyntheticItemid;
	//合成所需数量
	private	int m_SyntheticCount;
	//吸血几率千分比
	private	int m_VampireRate;
	//秘术id
	private	int[] m_msid;
	//装备id
	private	int[] m_equipmentid;
	//所有技能
	private	int[] m_totalskill;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_NameID = ReadToString(data);
		m_DescriptionID = ReadToString(data);
		m_tedian = ReadToString(data);
		m_Quality = data.ReadInt32();
		m_herotype = data.ReadInt32();
		m_InitMaxHP = data.ReadInt32();
		m_InitPhysicalAttack = data.ReadInt32();
		m_InitPhysicalDefence = data.ReadInt32();
		m_InitMagicAttack = data.ReadInt32();
		m_InitMagicDefence = data.ReadInt32();
		m_InitHit = data.ReadInt32();
		m_InitDodge = data.ReadInt32();
		m_InitCritical = data.ReadInt32();
		m_InitTenacity = data.ReadInt32();
		m_InitSpeed = data.ReadInt32();
		m_HPGrowth = ReadToSingle(data);
		m_HPGrowthMultiple = data.ReadInt32();
		m_PhysicalAttackGrowth = ReadToSingle(data);
		m_PhysicalAttackGrowthMultiple = data.ReadInt32();
		m_PhysicalDefenceGrowth = ReadToSingle(data);
		m_PhysicalDefenceGrowthMultiple = data.ReadInt32();
		m_MagicAttackGrowth = ReadToSingle(data);
		m_MagicAttackGrowthMultiple = data.ReadInt32();
		m_MagicDefenceGrowth = ReadToSingle(data);
		m_MagicDefenceGrowthMultiple = data.ReadInt32();
		m_HitGrowth = ReadToSingle(data);
		m_HitGrowthMultiple = data.ReadInt32();
		m_DodgeGrowth = ReadToSingle(data);
		m_DodgeGrowthMultiple = data.ReadInt32();
		m_CriticalGrowth = ReadToSingle(data);
		m_CriticalGrowthMultiple = data.ReadInt32();
		m_TenacityGrowth = ReadToSingle(data);
		m_TenacityGrowthMultiple = data.ReadInt32();
		m_SpeedGrowth = ReadToSingle(data);
		m_SpeedGrowthMultiple = data.ReadInt32();
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
		m_MaxLevel = data.ReadInt32();
		m_ExpNum = data.ReadInt32();
		m_normalskill = data.ReadInt32();
		m_skill1ID = data.ReadInt32();
		m_skill2ID = data.ReadInt32();
		m_skill3ID = data.ReadInt32();
		m_artresources = data.ReadInt32();
		m_movespeed = ReadToSingle(data);
		m_HPTransformFury = data.ReadInt32();
		m_entranceFury = data.ReadInt32();
		m_waveFury = data.ReadInt32();
		m_clientSignType = parserXMLIntArray(ReadToString(data));
		m_camp = data.ReadInt32();
		m_fadeCondition = data.ReadInt32();
		m_titleID = ReadToString(data);
		m_skillPair = parserXMLIntArray(ReadToString(data));
		m_runePair1 = data.ReadInt32();
		m_runePair2 = data.ReadInt32();
		m_runePair3 = data.ReadInt32();
		m_runePair4 = data.ReadInt32();
		m_runePassive = data.ReadInt32();
		m_stageUpCostType1 = data.ReadInt32();
		m_stageUpCost1 = data.ReadInt32();
		m_stageUpCostType2 = data.ReadInt32();
		m_stageUpCost2 = data.ReadInt32();
		m_useableArtresource = parserXMLIntArray(ReadToString(data));
		m_trainSlot1 = data.ReadInt32();
		m_trainMaximum1 = data.ReadInt32();
		m_trainSlot2 = data.ReadInt32();
		m_trainMaximum2 = data.ReadInt32();
		m_trainSlot3 = data.ReadInt32();
		m_trainMaximum3 = data.ReadInt32();
		m_trainSlot4 = data.ReadInt32();
		m_trainMaximum4 = data.ReadInt32();
		m_stageUpTargetID = data.ReadInt32();
		m_maxQuality = data.ReadInt32();
		m_returnBack = data.ReadInt32();
		m_heroDes = ReadToString(data);
		m_skillMaxLevel = data.ReadInt32();
		m_accessMethod = ReadToString(data);
		m_systemBroadcasts = data.ReadInt32();
		m_Born = data.ReadInt32();
		m_Qosition = data.ReadInt32();
		m_BlockHit = data.ReadInt32();
		m_SabotageHit = data.ReadInt32();
		m_DamageBonusHit = data.ReadInt32();
		m_DamageReductionHit = data.ReadInt32();
		m_FuryId = data.ReadInt32();
		m_Gold = data.ReadInt32();
		m_Stuff = data.ReadInt32();
		m_Numbers = data.ReadInt32();
		m_Paxid = data.ReadInt32();
		m_SyntheticItemid = data.ReadInt32();
		m_SyntheticCount = data.ReadInt32();
		m_VampireRate = data.ReadInt32();
		m_msid = parserXMLIntArray(ReadToString(data));
		m_equipmentid = parserXMLIntArray(ReadToString(data));
		m_totalskill = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getNameID()
	{
		return this.m_NameID;
	}

	public	string	getDescriptionID()
	{
		return this.m_DescriptionID;
	}

	public	string	getTedian()
	{
		return this.m_tedian;
	}

	public	int	getQuality()
	{
		return this.m_Quality;
	}

	public	int	getHerotype()
	{
		return this.m_herotype;
	}

	public	int	getInitMaxHP()
	{
		return this.m_InitMaxHP;
	}

	public	int	getInitPhysicalAttack()
	{
		return this.m_InitPhysicalAttack;
	}

	public	int	getInitPhysicalDefence()
	{
		return this.m_InitPhysicalDefence;
	}

	public	int	getInitMagicAttack()
	{
		return this.m_InitMagicAttack;
	}

	public	int	getInitMagicDefence()
	{
		return this.m_InitMagicDefence;
	}

	public	int	getInitHit()
	{
		return this.m_InitHit;
	}

	public	int	getInitDodge()
	{
		return this.m_InitDodge;
	}

	public	int	getInitCritical()
	{
		return this.m_InitCritical;
	}

	public	int	getInitTenacity()
	{
		return this.m_InitTenacity;
	}

	public	int	getInitSpeed()
	{
		return this.m_InitSpeed;
	}

	public	float	getHPGrowth()
	{
		return this.m_HPGrowth;
	}

	public	int	getHPGrowthMultiple()
	{
		return this.m_HPGrowthMultiple;
	}

	public	float	getPhysicalAttackGrowth()
	{
		return this.m_PhysicalAttackGrowth;
	}

	public	int	getPhysicalAttackGrowthMultiple()
	{
		return this.m_PhysicalAttackGrowthMultiple;
	}

	public	float	getPhysicalDefenceGrowth()
	{
		return this.m_PhysicalDefenceGrowth;
	}

	public	int	getPhysicalDefenceGrowthMultiple()
	{
		return this.m_PhysicalDefenceGrowthMultiple;
	}

	public	float	getMagicAttackGrowth()
	{
		return this.m_MagicAttackGrowth;
	}

	public	int	getMagicAttackGrowthMultiple()
	{
		return this.m_MagicAttackGrowthMultiple;
	}

	public	float	getMagicDefenceGrowth()
	{
		return this.m_MagicDefenceGrowth;
	}

	public	int	getMagicDefenceGrowthMultiple()
	{
		return this.m_MagicDefenceGrowthMultiple;
	}

	public	float	getHitGrowth()
	{
		return this.m_HitGrowth;
	}

	public	int	getHitGrowthMultiple()
	{
		return this.m_HitGrowthMultiple;
	}

	public	float	getDodgeGrowth()
	{
		return this.m_DodgeGrowth;
	}

	public	int	getDodgeGrowthMultiple()
	{
		return this.m_DodgeGrowthMultiple;
	}

	public	float	getCriticalGrowth()
	{
		return this.m_CriticalGrowth;
	}

	public	int	getCriticalGrowthMultiple()
	{
		return this.m_CriticalGrowthMultiple;
	}

	public	float	getTenacityGrowth()
	{
		return this.m_TenacityGrowth;
	}

	public	int	getTenacityGrowthMultiple()
	{
		return this.m_TenacityGrowthMultiple;
	}

	public	float	getSpeedGrowth()
	{
		return this.m_SpeedGrowth;
	}

	public	int	getSpeedGrowthMultiple()
	{
		return this.m_SpeedGrowthMultiple;
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

	public	int	getMaxLevel()
	{
		return this.m_MaxLevel;
	}

	public	int	getExpNum()
	{
		return this.m_ExpNum;
	}

	public	int	getNormalskill()
	{
		return this.m_normalskill;
	}

	public	int	getSkill1ID()
	{
		return this.m_skill1ID;
	}

	public	int	getSkill2ID()
	{
		return this.m_skill2ID;
	}

	public	int	getSkill3ID()
	{
		return this.m_skill3ID;
	}

	public	int	getArtresources()
	{
		return this.m_artresources;
	}

	public	float	getMovespeed()
	{
		return this.m_movespeed;
	}

	public	int	getHPTransformFury()
	{
		return this.m_HPTransformFury;
	}

	public	int	getEntranceFury()
	{
		return this.m_entranceFury;
	}

	public	int	getWaveFury()
	{
		return this.m_waveFury;
	}

	public	int[]	getClientSignType()
	{
		return this.m_clientSignType;
	}

	public	int	getCamp()
	{
		return this.m_camp;
	}

	public	int	getFadeCondition()
	{
		return this.m_fadeCondition;
	}

	public	string	getTitleID()
	{
		return this.m_titleID;
	}

	public	int[]	getSkillPair()
	{
		return this.m_skillPair;
	}

	public	int	getRunePair1()
	{
		return this.m_runePair1;
	}

	public	int	getRunePair2()
	{
		return this.m_runePair2;
	}

	public	int	getRunePair3()
	{
		return this.m_runePair3;
	}

	public	int	getRunePair4()
	{
		return this.m_runePair4;
	}

	public	int	getRunePassive()
	{
		return this.m_runePassive;
	}

	public	int	getStageUpCostType1()
	{
		return this.m_stageUpCostType1;
	}

	public	int	getStageUpCost1()
	{
		return this.m_stageUpCost1;
	}

	public	int	getStageUpCostType2()
	{
		return this.m_stageUpCostType2;
	}

	public	int	getStageUpCost2()
	{
		return this.m_stageUpCost2;
	}

	public	int[]	getUseableArtresource()
	{
		return this.m_useableArtresource;
	}

	public	int	getTrainSlot1()
	{
		return this.m_trainSlot1;
	}

	public	int	getTrainMaximum1()
	{
		return this.m_trainMaximum1;
	}

	public	int	getTrainSlot2()
	{
		return this.m_trainSlot2;
	}

	public	int	getTrainMaximum2()
	{
		return this.m_trainMaximum2;
	}

	public	int	getTrainSlot3()
	{
		return this.m_trainSlot3;
	}

	public	int	getTrainMaximum3()
	{
		return this.m_trainMaximum3;
	}

	public	int	getTrainSlot4()
	{
		return this.m_trainSlot4;
	}

	public	int	getTrainMaximum4()
	{
		return this.m_trainMaximum4;
	}

	public	int	getStageUpTargetID()
	{
		return this.m_stageUpTargetID;
	}

	public	int	getMaxQuality()
	{
		return this.m_maxQuality;
	}

	public	int	getReturnBack()
	{
		return this.m_returnBack;
	}

	public	string	getHeroDes()
	{
		return this.m_heroDes;
	}

	public	int	getSkillMaxLevel()
	{
		return this.m_skillMaxLevel;
	}

	public	string	getAccessMethod()
	{
		return this.m_accessMethod;
	}

	public	int	getSystemBroadcasts()
	{
		return this.m_systemBroadcasts;
	}

	public	int	getBorn()
	{
		return this.m_Born;
	}

	public	int	getQosition()
	{
		return this.m_Qosition;
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

	public	int	getFuryId()
	{
		return this.m_FuryId;
	}

	public	int	getGold()
	{
		return this.m_Gold;
	}

	public	int	getStuff()
	{
		return this.m_Stuff;
	}

	public	int	getNumbers()
	{
		return this.m_Numbers;
	}

	public	int	getPaxid()
	{
		return this.m_Paxid;
	}

	public	int	getSyntheticItemid()
	{
		return this.m_SyntheticItemid;
	}

	public	int	getSyntheticCount()
	{
		return this.m_SyntheticCount;
	}

	public	int	getVampireRate()
	{
		return this.m_VampireRate;
	}

	public	int[]	getMsid()
	{
		return this.m_msid;
	}

	public	int[]	getEquipmentid()
	{
		return this.m_equipmentid;
	}

	public	int[]	getTotalskill()
	{
		return this.m_totalskill;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}