//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class AddruneattributeTemplate : IExcelBean
{
	#region attribute
	//唯一标识
	private	int m_id;
	//属性库标记
	private	int m_bagId;
	//属性类型
	private	int m_attriType;
	//属性值
	private	int m_attriValue;
	//随机权重值
	private	int m_weight;
	//属性描述1
	private	string m_attriDes1;
	//属性描述2
	private	string m_attriDes2;
	//是否百分比
	private	int m_ispercentage;
	//显示符号
	private	string m_symbol;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_bagId = data.ReadInt32();
		m_attriType = data.ReadInt32();
		m_attriValue = data.ReadInt32();
		m_weight = data.ReadInt32();
		m_attriDes1 = ReadToString(data);
		m_attriDes2 = ReadToString(data);
		m_ispercentage = data.ReadInt32();
		m_symbol = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getBagId()
	{
		return this.m_bagId;
	}

	public	int	getAttriType()
	{
		return this.m_attriType;
	}

	public	int	getAttriValue()
	{
		return this.m_attriValue;
	}

	public	int	getWeight()
	{
		return this.m_weight;
	}

	public	string	getAttriDes1()
	{
		return this.m_attriDes1;
	}

	public	string	getAttriDes2()
	{
		return this.m_attriDes2;
	}

	public	int	getIspercentage()
	{
		return this.m_ispercentage;
	}

	public	string	getSymbol()
	{
		return this.m_symbol;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}