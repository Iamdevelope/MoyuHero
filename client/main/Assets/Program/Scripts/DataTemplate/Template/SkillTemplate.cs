//
// autor : OgreZCD
// date : 2015-10-17
//

using UnityEngine;
using System.Collections;
using System.IO;


public class SkillTemplate : IExcelBean
{
	#region attribute
	//技能ID
	private	int m_id;
	//技能名称
	private	string m_skillName;
	//技能名称资源
	private	string m_skillNameRes;
	//技能图标
	private	string m_skillIcon;
	//技能描述
	private	string m_skillDes;
	//技能等级
	private	int m_skillLevel;
	//技能类型
	private	int m_skillType;
	//技能编号
	private	int m_skillNo;
	//释放消耗资源类型1
	private	int m_skillCostType1;
	//释放消耗资源ID1
	private	int m_skillCostId1;
	//释放消耗数量1
	private	int m_skillCostNum1;
	//释放消耗资源类型2
	private	int m_skillCostType2;
	//释放消耗资源ID2
	private	int m_skillCostId2;
	//释放消耗数量2
	private	int m_skillCostNum2;
	//释放消耗资源类型3
	private	int m_skillCostType3;
	//释放消耗资源ID3
	private	int m_skillCostId3;
	//释放消耗数量3
	private	int m_skillCostNum3;
	//血量判断类型
	private	int m_hpConditionType;
	//血量判断值
	private	int m_hpConditionNum;
	//怒气判断类型
	private	int m_rpConditionType;
	//怒气判断值
	private	int m_rpConditionNum;
	//标准模式模板
	private	int[] m_normalTemplate;
	//标准模板优先级
	private	int[] m_normalpriority;
	//优先攻击模板
	private	int[] m_attFirstTemplate;
	//优先攻击模板优先
	private	int[] m_attFirstpriority;
	//优先回复模板
	private	int[] m_defFirstTemplate;
	//优先回复模板优先级
	private	int[] m_defFirstpriority;
	//冷却时间
	private	int m_cooldown;
	//相同技能是否进入CD
	private	int m_cooldownAlike;
	//被伤害打断类型
	private	int m_damageInterrupt;
	//伤害打断数值
	private	int m_damageInterruptType;
	//是否可打断技能
	private	int m_interruptSkill;
	//技能释放对象
	private	int m_target;
	//攻击距离（米）
	private	float m_attDistance;
	//有效范围
	private	float m_coverage;
	//技能目标个数
	private	int m_targetNum;
	//临时buff-自己
	private	int[] m_temporarySelfBuff;
	//临时buff-目标
	private	int[] m_temporaryTargetBuff;
	//buff效果组
	private	int[] m_buffList;
	//攻击音效
	private	string m_attackSound;
	//受击特效ID
	private	string m_underAttackEffID;
	//受击特效连接
	private	int m_underAttackEffLink;
	//受击音效
	private	string m_underAttackSound;
	//弹道特效ID
	private	string[] m_ballIsticEffID;
	//子弹发射绑点
	private	string m_bullEffectPoint;
	//受击命中绑点
	private	string m_effectHitPoint;
	//子弹发射音效
	private	string m_bulletsFiredSound;
	//弹道速度
	private	int m_ballIsticSpeed;
	//弹道音效
	private	string m_ballIsticSound;
	//动作组
	private	string[] m_action;
	//命中奖励怒气
	private	int m_InitHitFury;
	//释放技能奖励怒气
	private	int m_skillAttackFury;
	//削弱目标奖励百分比
	private	int m_WeakenTargetFuryReward;
	//击杀目标奖励怒气
	private	int m_killFury;
	//震屏条件
	private	int m_VibrationScreen;
	//技能释放类型
	private	int m_skillReleaseType;
	//打击点帧数时间
	private	int m_hitFrame;
	//技能逻辑ID
	private	int m_skillLogicID;
	//逻辑参数
	private	int[] m_Param;
	//技能伤害固定值
	private	int m_dmgfixvalue;
	//技能伤害系数
	private	float m_Spelldmgparam;
	//技能附带命中率
	private	int m_skillHit;
	//技能附带暴击率
	private	int m_skillCrit;
	//技能攻击类型
	private	int m_skillhittype;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_skillName = ReadToString(data);
		m_skillNameRes = ReadToString(data);
		m_skillIcon = ReadToString(data);
		m_skillDes = ReadToString(data);
		m_skillLevel = data.ReadInt32();
		m_skillType = data.ReadInt32();
		m_skillNo = data.ReadInt32();
		m_skillCostType1 = data.ReadInt32();
		m_skillCostId1 = data.ReadInt32();
		m_skillCostNum1 = data.ReadInt32();
		m_skillCostType2 = data.ReadInt32();
		m_skillCostId2 = data.ReadInt32();
		m_skillCostNum2 = data.ReadInt32();
		m_skillCostType3 = data.ReadInt32();
		m_skillCostId3 = data.ReadInt32();
		m_skillCostNum3 = data.ReadInt32();
		m_hpConditionType = data.ReadInt32();
		m_hpConditionNum = data.ReadInt32();
		m_rpConditionType = data.ReadInt32();
		m_rpConditionNum = data.ReadInt32();
		m_normalTemplate = parserXMLIntArray(ReadToString(data));
		m_normalpriority = parserXMLIntArray(ReadToString(data));
		m_attFirstTemplate = parserXMLIntArray(ReadToString(data));
		m_attFirstpriority = parserXMLIntArray(ReadToString(data));
		m_defFirstTemplate = parserXMLIntArray(ReadToString(data));
		m_defFirstpriority = parserXMLIntArray(ReadToString(data));
		m_cooldown = data.ReadInt32();
		m_cooldownAlike = data.ReadInt32();
		m_damageInterrupt = data.ReadInt32();
		m_damageInterruptType = data.ReadInt32();
		m_interruptSkill = data.ReadInt32();
		m_target = data.ReadInt32();
		m_attDistance = ReadToSingle(data);
		m_coverage = ReadToSingle(data);
		m_targetNum = data.ReadInt32();
		m_temporarySelfBuff = parserXMLIntArray(ReadToString(data));
		m_temporaryTargetBuff = parserXMLIntArray(ReadToString(data));
		m_buffList = parserXMLIntArray(ReadToString(data));
		m_attackSound = ReadToString(data);
		m_underAttackEffID = ReadToString(data);
		m_underAttackEffLink = data.ReadInt32();
		m_underAttackSound = ReadToString(data);
		m_ballIsticEffID = parserXMLStringArray(ReadToString(data));
		m_bullEffectPoint = ReadToString(data);
		m_effectHitPoint = ReadToString(data);
		m_bulletsFiredSound = ReadToString(data);
		m_ballIsticSpeed = data.ReadInt32();
		m_ballIsticSound = ReadToString(data);
		m_action = parserXMLStringArray(ReadToString(data));
		m_InitHitFury = data.ReadInt32();
		m_skillAttackFury = data.ReadInt32();
		m_WeakenTargetFuryReward = data.ReadInt32();
		m_killFury = data.ReadInt32();
		m_VibrationScreen = data.ReadInt32();
		m_skillReleaseType = data.ReadInt32();
		m_hitFrame = data.ReadInt32();
		m_skillLogicID = data.ReadInt32();
		m_Param = parserXMLIntArray(ReadToString(data));
		m_dmgfixvalue = data.ReadInt32();
		m_Spelldmgparam = ReadToSingle(data);
		m_skillHit = data.ReadInt32();
		m_skillCrit = data.ReadInt32();
		m_skillhittype = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getSkillName()
	{
		return this.m_skillName;
	}

	public	string	getSkillNameRes()
	{
		return this.m_skillNameRes;
	}

	public	string	getSkillIcon()
	{
		return this.m_skillIcon;
	}

	public	string	getSkillDes()
	{
		return this.m_skillDes;
	}

	public	int	getSkillLevel()
	{
		return this.m_skillLevel;
	}

	public	int	getSkillType()
	{
		return this.m_skillType;
	}

	public	int	getSkillNo()
	{
		return this.m_skillNo;
	}

	public	int	getSkillCostType1()
	{
		return this.m_skillCostType1;
	}

	public	int	getSkillCostId1()
	{
		return this.m_skillCostId1;
	}

	public	int	getSkillCostNum1()
	{
		return this.m_skillCostNum1;
	}

	public	int	getSkillCostType2()
	{
		return this.m_skillCostType2;
	}

	public	int	getSkillCostId2()
	{
		return this.m_skillCostId2;
	}

	public	int	getSkillCostNum2()
	{
		return this.m_skillCostNum2;
	}

	public	int	getSkillCostType3()
	{
		return this.m_skillCostType3;
	}

	public	int	getSkillCostId3()
	{
		return this.m_skillCostId3;
	}

	public	int	getSkillCostNum3()
	{
		return this.m_skillCostNum3;
	}

	public	int	getHpConditionType()
	{
		return this.m_hpConditionType;
	}

	public	int	getHpConditionNum()
	{
		return this.m_hpConditionNum;
	}

	public	int	getRpConditionType()
	{
		return this.m_rpConditionType;
	}

	public	int	getRpConditionNum()
	{
		return this.m_rpConditionNum;
	}

	public	int[]	getNormalTemplate()
	{
		return this.m_normalTemplate;
	}

	public	int[]	getNormalpriority()
	{
		return this.m_normalpriority;
	}

	public	int[]	getAttFirstTemplate()
	{
		return this.m_attFirstTemplate;
	}

	public	int[]	getAttFirstpriority()
	{
		return this.m_attFirstpriority;
	}

	public	int[]	getDefFirstTemplate()
	{
		return this.m_defFirstTemplate;
	}

	public	int[]	getDefFirstpriority()
	{
		return this.m_defFirstpriority;
	}

	public	int	getCooldown()
	{
		return this.m_cooldown;
	}

	public	int	getCooldownAlike()
	{
		return this.m_cooldownAlike;
	}

	public	int	getDamageInterrupt()
	{
		return this.m_damageInterrupt;
	}

	public	int	getDamageInterruptType()
	{
		return this.m_damageInterruptType;
	}

	public	int	getInterruptSkill()
	{
		return this.m_interruptSkill;
	}

	public	int	getTarget()
	{
		return this.m_target;
	}

	public	float	getAttDistance()
	{
		return this.m_attDistance;
	}

	public	float	getCoverage()
	{
		return this.m_coverage;
	}

	public	int	getTargetNum()
	{
		return this.m_targetNum;
	}

	public	int[]	getTemporarySelfBuff()
	{
		return this.m_temporarySelfBuff;
	}

	public	int[]	getTemporaryTargetBuff()
	{
		return this.m_temporaryTargetBuff;
	}

	public	int[]	getBuffList()
	{
		return this.m_buffList;
	}

	public	string	getAttackSound()
	{
		return this.m_attackSound;
	}

	public	string	getUnderAttackEffID()
	{
		return this.m_underAttackEffID;
	}

	public	int	getUnderAttackEffLink()
	{
		return this.m_underAttackEffLink;
	}

	public	string	getUnderAttackSound()
	{
		return this.m_underAttackSound;
	}

	public	string[]	getBallIsticEffID()
	{
		return this.m_ballIsticEffID;
	}

	public	string	getBullEffectPoint()
	{
		return this.m_bullEffectPoint;
	}

	public	string	getEffectHitPoint()
	{
		return this.m_effectHitPoint;
	}

	public	string	getBulletsFiredSound()
	{
		return this.m_bulletsFiredSound;
	}

	public	int	getBallIsticSpeed()
	{
		return this.m_ballIsticSpeed;
	}

	public	string	getBallIsticSound()
	{
		return this.m_ballIsticSound;
	}

	public	string[]	getAction()
	{
		return this.m_action;
	}

	public	int	getInitHitFury()
	{
		return this.m_InitHitFury;
	}

	public	int	getSkillAttackFury()
	{
		return this.m_skillAttackFury;
	}

	public	int	getWeakenTargetFuryReward()
	{
		return this.m_WeakenTargetFuryReward;
	}

	public	int	getKillFury()
	{
		return this.m_killFury;
	}

	public	int	getVibrationScreen()
	{
		return this.m_VibrationScreen;
	}

	public	int	getSkillReleaseType()
	{
		return this.m_skillReleaseType;
	}

	public	int	getHitFrame()
	{
		return this.m_hitFrame;
	}

	public	int	getSkillLogicID()
	{
		return this.m_skillLogicID;
	}

	public	int[]	getParam()
	{
		return this.m_Param;
	}

	public	int	getDmgfixvalue()
	{
		return this.m_dmgfixvalue;
	}

	public	float	getSpelldmgparam()
	{
		return this.m_Spelldmgparam;
	}

	public	int	getSkillHit()
	{
		return this.m_skillHit;
	}

	public	int	getSkillCrit()
	{
		return this.m_skillCrit;
	}

	public	int	getSkillhittype()
	{
		return this.m_skillhittype;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}