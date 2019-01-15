//
// autor : OgreZCD
// date : 2015-10-28
//

using UnityEngine;
using System.Collections;
using System.IO;


public class VipTemplate : IExcelBean
{
	#region attribute
	//vip等级
	private	int m_id;
	//vip经验
	private	int m_vipExp;
	//增加活力上限
	private	int m_extraAp;
	//每日可购买活力次数
	private	int m_maxBuyAp;
	//每日可使用活力药上限
	private	int m_maxUseApPotion;
	//增加pvp体力上限
	private	int m_extraPVPAp;
	//每日可购买pvp体力次数
	private	int m_maxBuyPVPAp;
	//每日可使用pvp体力药上限
	private	int m_maxUsePVPApPotion;
	//好友上限
	private	int m_maxFriends;
	//恢复1技能点耗时（秒）
	private	int m_reSkillTime;
	//技能点购买次数上限
	private	int m_skillconlimit;
	//增加探险行动力上限
	private	int m_extraEp;
	//每日可购买探险行动力次数
	private	int m_maxBuyEp;
	//每日可使用探险行动力药上限
	private	int m_maxUseEpPotion;
	//是否可使用探险加速功能
	private	int m_ifCanAccelerate;
	//是否可重置冒险关卡
	private	int m_ifCanBuyStageReset;
	//重置精英关卡次数上限
	private	int m_stageResetBuyTimes;
	//重置精英关卡花费魔钻
	private	int[] m_resetcost;
	//是否可重置限时关卡
	private	int m_ifCanBuyStageReset1;
	//重置限时关卡次数上限
	private	int m_stageResetBuyTimes1;
	//是否可关卡扫荡
	private	int m_ifCanRapidClear;
	//是否可重置限时关卡
	private	int m_ifCanBuyLimiteStageReset;
	//重置限时关卡次数上限
	private	int m_stageResetBuyLimiteTimes;
	//每日扫荡次数
	private	int m_rapidClearNums;
	//每日可购买扫荡次数
	private	int m_rapidClearBuyTimes;
	//特权描述
	private	string[] m_privilegedDes;
	//是否新特权
	private	int[] m_isNew;
	//是否可扫荡10次
	private	int m_isClearten;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_vipExp = data.ReadInt32();
		m_extraAp = data.ReadInt32();
		m_maxBuyAp = data.ReadInt32();
		m_maxUseApPotion = data.ReadInt32();
		m_extraPVPAp = data.ReadInt32();
		m_maxBuyPVPAp = data.ReadInt32();
		m_maxUsePVPApPotion = data.ReadInt32();
		m_maxFriends = data.ReadInt32();
		m_reSkillTime = data.ReadInt32();
		m_skillconlimit = data.ReadInt32();
		m_extraEp = data.ReadInt32();
		m_maxBuyEp = data.ReadInt32();
		m_maxUseEpPotion = data.ReadInt32();
		m_ifCanAccelerate = data.ReadInt32();
		m_ifCanBuyStageReset = data.ReadInt32();
		m_stageResetBuyTimes = data.ReadInt32();
		m_resetcost = parserXMLIntArray(ReadToString(data));
		m_ifCanBuyStageReset1 = data.ReadInt32();
		m_stageResetBuyTimes1 = data.ReadInt32();
		m_ifCanRapidClear = data.ReadInt32();
		m_ifCanBuyLimiteStageReset = data.ReadInt32();
		m_stageResetBuyLimiteTimes = data.ReadInt32();
		m_rapidClearNums = data.ReadInt32();
		m_rapidClearBuyTimes = data.ReadInt32();
		m_privilegedDes = parserXMLStringArray(ReadToString(data));
		m_isNew = parserXMLIntArray(ReadToString(data));
		m_isClearten = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getVipExp()
	{
		return this.m_vipExp;
	}

	public	int	getExtraAp()
	{
		return this.m_extraAp;
	}

	public	int	getMaxBuyAp()
	{
		return this.m_maxBuyAp;
	}

	public	int	getMaxUseApPotion()
	{
		return this.m_maxUseApPotion;
	}

	public	int	getExtraPVPAp()
	{
		return this.m_extraPVPAp;
	}

	public	int	getMaxBuyPVPAp()
	{
		return this.m_maxBuyPVPAp;
	}

	public	int	getMaxUsePVPApPotion()
	{
		return this.m_maxUsePVPApPotion;
	}

	public	int	getMaxFriends()
	{
		return this.m_maxFriends;
	}

	public	int	getReSkillTime()
	{
		return this.m_reSkillTime;
	}

	public	int	getSkillconlimit()
	{
		return this.m_skillconlimit;
	}

	public	int	getExtraEp()
	{
		return this.m_extraEp;
	}

	public	int	getMaxBuyEp()
	{
		return this.m_maxBuyEp;
	}

	public	int	getMaxUseEpPotion()
	{
		return this.m_maxUseEpPotion;
	}

	public	int	getIfCanAccelerate()
	{
		return this.m_ifCanAccelerate;
	}

	public	int	getIfCanBuyStageReset()
	{
		return this.m_ifCanBuyStageReset;
	}

	public	int	getStageResetBuyTimes()
	{
		return this.m_stageResetBuyTimes;
	}

	public	int[]	getResetcost()
	{
		return this.m_resetcost;
	}

	public	int	getIfCanBuyStageReset1()
	{
		return this.m_ifCanBuyStageReset1;
	}

	public	int	getStageResetBuyTimes1()
	{
		return this.m_stageResetBuyTimes1;
	}

	public	int	getIfCanRapidClear()
	{
		return this.m_ifCanRapidClear;
	}

	public	int	getIfCanBuyLimiteStageReset()
	{
		return this.m_ifCanBuyLimiteStageReset;
	}

	public	int	getStageResetBuyLimiteTimes()
	{
		return this.m_stageResetBuyLimiteTimes;
	}

	public	int	getRapidClearNums()
	{
		return this.m_rapidClearNums;
	}

	public	int	getRapidClearBuyTimes()
	{
		return this.m_rapidClearBuyTimes;
	}

	public	string[]	getPrivilegedDes()
	{
		return this.m_privilegedDes;
	}

	public	int[]	getIsNew()
	{
		return this.m_isNew;
	}

	public	int	getIsClearten()
	{
		return this.m_isClearten;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}