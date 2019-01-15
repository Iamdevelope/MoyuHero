//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class RunhorselightTemplate : IExcelBean
{
	#region attribute
	//id
	private	int m_id;
	//数据类型
	private	int[] m_datatype;
	//颜色
	private	string m_color;
	//文本
	private	string m_text;
	//类型
	private	int m_dataclass;
	//排序1
	private	int m_sort1;
	//排序2
	private	int m_sort2;
	//排序3
	private	int m_sort3;
	//排序4
	private	int m_sort4;
	//排序5
	private	int m_sort5;
	//排序6
	private	int m_sort6;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_datatype = parserXMLIntArray(ReadToString(data));
		m_color = ReadToString(data);
		m_text = ReadToString(data);
		m_dataclass = data.ReadInt32();
		m_sort1 = data.ReadInt32();
		m_sort2 = data.ReadInt32();
		m_sort3 = data.ReadInt32();
		m_sort4 = data.ReadInt32();
		m_sort5 = data.ReadInt32();
		m_sort6 = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int[]	getDatatype()
	{
		return this.m_datatype;
	}

	public	string	getColor()
	{
		return this.m_color;
	}

	public	string	getText()
	{
		return this.m_text;
	}

	public	int	getDataclass()
	{
		return this.m_dataclass;
	}

	public	int	getSort1()
	{
		return this.m_sort1;
	}

	public	int	getSort2()
	{
		return this.m_sort2;
	}

	public	int	getSort3()
	{
		return this.m_sort3;
	}

	public	int	getSort4()
	{
		return this.m_sort4;
	}

	public	int	getSort5()
	{
		return this.m_sort5;
	}

	public	int	getSort6()
	{
		return this.m_sort6;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}