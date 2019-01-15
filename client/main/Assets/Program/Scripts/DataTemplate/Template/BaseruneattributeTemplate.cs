//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class BaseruneattributeTemplate : IExcelBean
{
	#region attribute
	//唯一标识
	private	int m_id;
	//属性库标记
	private	int m_bagId;
	//等级
	private	int m_level;
	//属性类型
	private	int m_attriType;
	//属性值
	private	int m_attriValue;
	//属性描述
	private	string m_attriDes;
	//数值显示
	private	int m_numshow;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_bagId = data.ReadInt32();
		m_level = data.ReadInt32();
		m_attriType = data.ReadInt32();
		m_attriValue = data.ReadInt32();
		m_attriDes = ReadToString(data);
		m_numshow = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getBagId()
	{
		return this.m_bagId;
	}

	public	int	getLevel()
	{
		return this.m_level;
	}

	public	int	getAttriType()
	{
		return this.m_attriType;
	}

	public	int	getAttriValue()
	{
		return this.m_attriValue;
	}

	public	string	getAttriDes()
	{
		return this.m_attriDes;
	}

	public	int	getNumshow()
	{
		return this.m_numshow;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}