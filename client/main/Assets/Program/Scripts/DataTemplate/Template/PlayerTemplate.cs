//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class PlayerTemplate : IExcelBean
{
	#region attribute
	//等级
	private	int m_id;
	//经验列
	private	int m_exp;
	//入场奖励怒气
	private	int m_entranceFury;
	//每波奖励怒气
	private	int m_waveFury;
	//怒气上限
	private	int m_MaxFury;
	//体力上限等级成长
	private	int m_extraAp;
	//英雄上限等级成长
	private	int m_extraHeroPackset;
	//背包上限等级成长
	private	int m_extraCommonItemPackset;
	//等级恢复体力数
	private	int m_apRecover;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_exp = data.ReadInt32();
		m_entranceFury = data.ReadInt32();
		m_waveFury = data.ReadInt32();
		m_MaxFury = data.ReadInt32();
		m_extraAp = data.ReadInt32();
		m_extraHeroPackset = data.ReadInt32();
		m_extraCommonItemPackset = data.ReadInt32();
		m_apRecover = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getExp()
	{
		return this.m_exp;
	}

	public	int	getEntranceFury()
	{
		return this.m_entranceFury;
	}

	public	int	getWaveFury()
	{
		return this.m_waveFury;
	}

	public	int	getMaxFury()
	{
		return this.m_MaxFury;
	}

	public	int	getExtraAp()
	{
		return this.m_extraAp;
	}

	public	int	getExtraHeroPackset()
	{
		return this.m_extraHeroPackset;
	}

	public	int	getExtraCommonItemPackset()
	{
		return this.m_extraCommonItemPackset;
	}

	public	int	getApRecover()
	{
		return this.m_apRecover;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}