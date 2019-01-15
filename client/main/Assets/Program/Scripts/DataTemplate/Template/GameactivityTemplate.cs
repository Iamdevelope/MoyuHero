//
// autor : OgreZCD
// date : 2015-07-23
//

using UnityEngine;
using System.Collections;
using System.IO;


public class GameactivityTemplate : IExcelBean
{
	#region attribute
	//id
	private	int m_id;
	//类型
	private	int m_type;
	//任务组
	private	int m_team;
	//起始时间
	private	string m_beginday;
	//结束时间
	private	string m_deadline;
	//标题描述
	private	string m_titledes;
	//内容描述
	private	string m_contentdes;
	//每日最大次数
	private	int m_daymax;
	//期间最大次数
	private	int m_periodmax;
	//参数1
	private	int m_parameter1;
	//参数2
	private	int m_parameter2;
	//参数3
	private	int m_parameter3;
	//奖励id
	private	string m_drop;
	//显示类型
	private	int[] m_dropdestype;
	//显示奖励
	private	string m_dropdes;
	//显示数量
	private	int[] m_numdes;
	//显示文本
	private	string m_textdes;
	//跳转类型
	private	string m_jumpstype;
	//排序
	private	int m_sort;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_type = data.ReadInt32();
		m_team = data.ReadInt32();
		m_beginday = ReadToString(data);
		m_deadline = ReadToString(data);
		m_titledes = ReadToString(data);
		m_contentdes = ReadToString(data);
		m_daymax = data.ReadInt32();
		m_periodmax = data.ReadInt32();
		m_parameter1 = data.ReadInt32();
		m_parameter2 = data.ReadInt32();
		m_parameter3 = data.ReadInt32();
		m_drop = ReadToString(data);
		m_dropdestype = parserXMLIntArray(ReadToString(data));
		m_dropdes = ReadToString(data);
		m_numdes = parserXMLIntArray(ReadToString(data));
		m_textdes = ReadToString(data);
		m_jumpstype = ReadToString(data);
		m_sort = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getTeam()
	{
		return this.m_team;
	}

	public	string	getBeginday()
	{
		return this.m_beginday;
	}

	public	string	getDeadline()
	{
		return this.m_deadline;
	}

	public	string	getTitledes()
	{
		return this.m_titledes;
	}

	public	string	getContentdes()
	{
		return this.m_contentdes;
	}

	public	int	getDaymax()
	{
		return this.m_daymax;
	}

	public	int	getPeriodmax()
	{
		return this.m_periodmax;
	}

	public	int	getParameter1()
	{
		return this.m_parameter1;
	}

	public	int	getParameter2()
	{
		return this.m_parameter2;
	}

	public	int	getParameter3()
	{
		return this.m_parameter3;
	}

	public	string	getDrop()
	{
		return this.m_drop;
	}

	public	int[]	getDropdestype()
	{
		return this.m_dropdestype;
	}

	public	string	getDropdes()
	{
		return this.m_dropdes;
	}

	public	int[]	getNumdes()
	{
		return this.m_numdes;
	}

	public	string	getTextdes()
	{
		return this.m_textdes;
	}

	public	string	getJumpstype()
	{
		return this.m_jumpstype;
	}

	public	int	getSort()
	{
		return this.m_sort;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}