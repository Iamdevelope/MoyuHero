//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class RunecostTemplate : IExcelBean
{
	#region attribute
	//唯一标识
	private	int m_id;
	//属性库标记
	private	int m_bagId;
	//等级
	private	int m_level;
	//资源ID1
	private	int m_attriType1;
	//资源值1
	private	int m_attriValue1;
	//资源ID2
	private	int m_attriType2;
	//资源值2
	private	int m_attriValue2;
	//返还ID1
	private	int m_returnType1;
	//返还值1
	private	int m_returnValue1;
	//返还ID2
	private	int m_returnType2;
	//返还值2
	private	int m_returnValue2;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_bagId = data.ReadInt32();
		m_level = data.ReadInt32();
		m_attriType1 = data.ReadInt32();
		m_attriValue1 = data.ReadInt32();
		m_attriType2 = data.ReadInt32();
		m_attriValue2 = data.ReadInt32();
		m_returnType1 = data.ReadInt32();
		m_returnValue1 = data.ReadInt32();
		m_returnType2 = data.ReadInt32();
		m_returnValue2 = data.ReadInt32();
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

	public	int	getAttriType1()
	{
		return this.m_attriType1;
	}

	public	int	getAttriValue1()
	{
		return this.m_attriValue1;
	}

	public	int	getAttriType2()
	{
		return this.m_attriType2;
	}

	public	int	getAttriValue2()
	{
		return this.m_attriValue2;
	}

	public	int	getReturnType1()
	{
		return this.m_returnType1;
	}

	public	int	getReturnValue1()
	{
		return this.m_returnValue1;
	}

	public	int	getReturnType2()
	{
		return this.m_returnType2;
	}

	public	int	getReturnValue2()
	{
		return this.m_returnValue2;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}