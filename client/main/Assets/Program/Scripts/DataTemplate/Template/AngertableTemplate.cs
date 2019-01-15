//
// autor : OgreZCD
// date : 2015-10-16
//

using UnityEngine;
using System.Collections;
using System.IO;


public class AngertableTemplate : IExcelBean
{
	#region attribute
	//怒气序列
	private	int m_id;
	//每损失1%生命奖励怒气值
	private	int m_HPTransformFury;
	//每波奖励怒气
	private	int m_waveFury;
	//每次攻击怒气
	private	int m_AttackFury;
	//击杀怒气
	private	int m_KillFury;
	//每次受击怒气
	private	int m_GethitFury;
	//开场怒气
	private	int m_StartFury;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_HPTransformFury = data.ReadInt32();
		m_waveFury = data.ReadInt32();
		m_AttackFury = data.ReadInt32();
		m_KillFury = data.ReadInt32();
		m_GethitFury = data.ReadInt32();
		m_StartFury = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getHPTransformFury()
	{
		return this.m_HPTransformFury;
	}

	public	int	getWaveFury()
	{
		return this.m_waveFury;
	}

	public	int	getAttackFury()
	{
		return this.m_AttackFury;
	}

	public	int	getKillFury()
	{
		return this.m_KillFury;
	}

	public	int	getGethitFury()
	{
		return this.m_GethitFury;
	}

	public	int	getStartFury()
	{
		return this.m_StartFury;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}