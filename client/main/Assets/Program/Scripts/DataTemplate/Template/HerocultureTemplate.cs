//
// autor : OgreZCD
// date : 2015-10-15
//

using UnityEngine;
using System.Collections;
using System.IO;


public class HerocultureTemplate : IExcelBean
{
	#region attribute
	//序列号
	private	int m_id;
	//资质
	private	int m_Born;
	//定位
	private	int m_Qosition;
	//元素类型
	private	int m_Element;
	//元素等级
	private	int m_ElementLeve;
	//元素消耗道具
	private	int m_Consumption;
	//消耗数量
	private	int m_Number;
	//枚举属性
	private	int[] m_Attribute;
	//数值
	private	int[] m_Value;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_Born = data.ReadInt32();
		m_Qosition = data.ReadInt32();
		m_Element = data.ReadInt32();
		m_ElementLeve = data.ReadInt32();
		m_Consumption = data.ReadInt32();
		m_Number = data.ReadInt32();
		m_Attribute = parserXMLIntArray(ReadToString(data));
		m_Value = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getBorn()
	{
		return this.m_Born;
	}

	public	int	getQosition()
	{
		return this.m_Qosition;
	}

	public	int	getElement()
	{
		return this.m_Element;
	}

	public	int	getElementLeve()
	{
		return this.m_ElementLeve;
	}

	public	int	getConsumption()
	{
		return this.m_Consumption;
	}

	public	int	getNumber()
	{
		return this.m_Number;
	}

	public	int[]	getAttribute()
	{
		return this.m_Attribute;
	}

	public	int[]	getValue()
	{
		return this.m_Value;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}