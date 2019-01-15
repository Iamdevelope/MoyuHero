//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class UltimatetrialrewardTemplate : IExcelBean
{
	#region attribute
	//奖励id
	private	int m_id;
	//玩家等级段
	private	int m_levelrange;
	//排名位置1
	private	int m_rank1;
	//排名位置2
	private	int m_rank2;
	//奖励id
	private	int[] m_reward_id;
	//奖励数量
	private	int[] m_reward_num;
	//排名描述
	private	string m_rankdes;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_levelrange = data.ReadInt32();
		m_rank1 = data.ReadInt32();
		m_rank2 = data.ReadInt32();
		m_reward_id = parserXMLIntArray(ReadToString(data));
		m_reward_num = parserXMLIntArray(ReadToString(data));
		m_rankdes = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getLevelrange()
	{
		return this.m_levelrange;
	}

	public	int	getRank1()
	{
		return this.m_rank1;
	}

	public	int	getRank2()
	{
		return this.m_rank2;
	}

	public	int[]	getReward_id()
	{
		return this.m_reward_id;
	}

	public	int[]	getReward_num()
	{
		return this.m_reward_num;
	}

	public	string	getRankdes()
	{
		return this.m_rankdes;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}