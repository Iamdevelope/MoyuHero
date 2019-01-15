//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class LegendexchargeTemplate : IExcelBean
{
	#region attribute
	//兑换id
	private	int m_id;
	//兑换类型
	private	int m_type;
	//传说之石消耗
	private	int m_cost;
	//奖励id
	private	int m_reward;
	//显示调用
	private	string m_show;
	//贩卖几率
	private	int m_probability;
	//排序列
	private	int m_sort;
	//显示名称
	private	string m_name;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_type = data.ReadInt32();
		m_cost = data.ReadInt32();
		m_reward = data.ReadInt32();
		m_show = ReadToString(data);
		m_probability = data.ReadInt32();
		m_sort = data.ReadInt32();
		m_name = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getCost()
	{
		return this.m_cost;
	}

	public	int	getReward()
	{
		return this.m_reward;
	}

	public	string	getShow()
	{
		return this.m_show;
	}

	public	int	getProbability()
	{
		return this.m_probability;
	}

	public	int	getSort()
	{
		return this.m_sort;
	}

	public	string	getName()
	{
		return this.m_name;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}