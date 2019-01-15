//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class RunepassiveTemplate : IExcelBean
{
	#region attribute
	//id
	private	int m_id;
	//属性1
	private	int m_attribute1;
	//类型1
	private	int m_type1;
	//数值1
	private	int m_value1;
	//符号1
	private	string m_symbol1;
	//描述1
	private	string m_des1;
	//属性2
	private	int m_attribute2;
	//类型2
	private	int m_type2;
	//数值2
	private	int m_value2;
	//符号2
	private	string m_symbol2;
	//描述2
	private	string m_des2;
	//属性3
	private	int m_attribute3;
	//类型3
	private	int m_type3;
	//数值3
	private	int m_value3;
	//符号3
	private	string m_symbol3;
	//描述3
	private	string m_des3;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_attribute1 = data.ReadInt32();
		m_type1 = data.ReadInt32();
		m_value1 = data.ReadInt32();
		m_symbol1 = ReadToString(data);
		m_des1 = ReadToString(data);
		m_attribute2 = data.ReadInt32();
		m_type2 = data.ReadInt32();
		m_value2 = data.ReadInt32();
		m_symbol2 = ReadToString(data);
		m_des2 = ReadToString(data);
		m_attribute3 = data.ReadInt32();
		m_type3 = data.ReadInt32();
		m_value3 = data.ReadInt32();
		m_symbol3 = ReadToString(data);
		m_des3 = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getAttribute1()
	{
		return this.m_attribute1;
	}

	public	int	getType1()
	{
		return this.m_type1;
	}

	public	int	getValue1()
	{
		return this.m_value1;
	}

	public	string	getSymbol1()
	{
		return this.m_symbol1;
	}

	public	string	getDes1()
	{
		return this.m_des1;
	}

	public	int	getAttribute2()
	{
		return this.m_attribute2;
	}

	public	int	getType2()
	{
		return this.m_type2;
	}

	public	int	getValue2()
	{
		return this.m_value2;
	}

	public	string	getSymbol2()
	{
		return this.m_symbol2;
	}

	public	string	getDes2()
	{
		return this.m_des2;
	}

	public	int	getAttribute3()
	{
		return this.m_attribute3;
	}

	public	int	getType3()
	{
		return this.m_type3;
	}

	public	int	getValue3()
	{
		return this.m_value3;
	}

	public	string	getSymbol3()
	{
		return this.m_symbol3;
	}

	public	string	getDes3()
	{
		return this.m_des3;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}