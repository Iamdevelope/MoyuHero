//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class BossboxTemplate : IExcelBean
{
	#region attribute
	//掉落标识
	private	int m_id;
	//BOSS宝箱ID
	private	int m_bossboxid;
	//筛选等级
	private	int m_dorplevel;
	//奖励id
	private	int m_rewardid;
	//奖励数量
	private	int m_rewardnum;
	//筛选权重
	private	int m_dropwight1;
	//筛选权重增量
	private	int m_dropwight1plus;
	//掉落权重
	private	int m_dropwight2;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_bossboxid = data.ReadInt32();
		m_dorplevel = data.ReadInt32();
		m_rewardid = data.ReadInt32();
		m_rewardnum = data.ReadInt32();
		m_dropwight1 = data.ReadInt32();
		m_dropwight1plus = data.ReadInt32();
		m_dropwight2 = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getBossboxid()
	{
		return this.m_bossboxid;
	}

	public	int	getDorplevel()
	{
		return this.m_dorplevel;
	}

	public	int	getRewardid()
	{
		return this.m_rewardid;
	}

	public	int	getRewardnum()
	{
		return this.m_rewardnum;
	}

	public	int	getDropwight1()
	{
		return this.m_dropwight1;
	}

	public	int	getDropwight1plus()
	{
		return this.m_dropwight1plus;
	}

	public	int	getDropwight2()
	{
		return this.m_dropwight2;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}