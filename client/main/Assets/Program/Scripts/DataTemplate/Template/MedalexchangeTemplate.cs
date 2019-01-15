//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class MedalexchangeTemplate : IExcelBean
{
	#region attribute
	//兑换id
	private	int m_id;
	//兑换类型
	private	int m_exchangeType;
	//资源所需数量
	private	int m_needNum;
	//奖励ID
	private	int m_rewardId;
	//数量
	private	int m_rewardNum;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_exchangeType = data.ReadInt32();
		m_needNum = data.ReadInt32();
		m_rewardId = data.ReadInt32();
		m_rewardNum = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getExchangeType()
	{
		return this.m_exchangeType;
	}

	public	int	getNeedNum()
	{
		return this.m_needNum;
	}

	public	int	getRewardId()
	{
		return this.m_rewardId;
	}

	public	int	getRewardNum()
	{
		return this.m_rewardNum;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}