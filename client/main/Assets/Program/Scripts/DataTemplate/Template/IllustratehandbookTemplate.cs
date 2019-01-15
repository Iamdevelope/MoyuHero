//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class IllustratehandbookTemplate : IExcelBean
{
	#region attribute
	//图鉴id
	private	int m_id;
	//类型
	private	int m_type;
	//具体的id
	private	int m_contentId;
	//达成奖励
	private	int m_reward;
	//排序id
	private	int m_sortingId;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_type = data.ReadInt32();
		m_contentId = data.ReadInt32();
		m_reward = data.ReadInt32();
		m_sortingId = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getContentId()
	{
		return this.m_contentId;
	}

	public	int	getReward()
	{
		return this.m_reward;
	}

	public	int	getSortingId()
	{
		return this.m_sortingId;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}