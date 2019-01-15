//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class AttributetrainTemplate : IExcelBean
{
	#region attribute
	//唯一标识
	private	int m_id;
	//属性库标记
	private	int m_bagId;
	//次数
	private	int m_times;
	//属性类型
	private	int m_attriType;
	//属性值
	private	int m_attriValue;
	//资源ID
	private	int m_costType;
	//资源消耗
	private	int m_cost;
	#endregion
	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_bagId = data.ReadInt32();
		m_times = data.ReadInt32();
		m_attriType = data.ReadInt32();
		m_attriValue = data.ReadInt32();
		m_costType = data.ReadInt32();
		m_cost = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getBagId()
	{
		return this.m_bagId;
	}

	public	int	getTimes()
	{
		return this.m_times;
	}

	public	int	getAttriType()
	{
		return this.m_attriType;
	}

	public	int	getAttriValue()
	{
		return this.m_attriValue;
	}

	public	int	getCostType()
	{
		return this.m_costType;
	}

	public	int	getCost()
	{
		return this.m_cost;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}