//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class LoginbonusTemplate : IExcelBean
{
	#region attribute
	//唯一id
	private	int m_id;
	//天数
	private	int m_day;
	//类型
	private	int m_type;
	//库
	private	int m_room;
	//资源id及数量
	private	int[] m_rewardAndNum;
	//显示数量
	private	int m_showNum;
	//下个库
	private	int m_nextRoom;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_day = data.ReadInt32();
		m_type = data.ReadInt32();
		m_room = data.ReadInt32();
		m_rewardAndNum = parserXMLIntArray(ReadToString(data));
		m_showNum = data.ReadInt32();
		m_nextRoom = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getDay()
	{
		return this.m_day;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getRoom()
	{
		return this.m_room;
	}

	public	int[]	getRewardAndNum()
	{
		return this.m_rewardAndNum;
	}

	public	int	getShowNum()
	{
		return this.m_showNum;
	}

	public	int	getNextRoom()
	{
		return this.m_nextRoom;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}