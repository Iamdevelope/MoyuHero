//
// autor : OgreZCD
// date : 2015-10-28
//

using UnityEngine;
using System.Collections;
using System.IO;


public class EquipmentstrengthTemplate : IExcelBean
{
	#region attribute
	//序列
	private	int m_id;
	//定位
	private	int m_Qosition;
	//装备部位
	private	int m_Parts;
	//需求等级
	private	int m_Level;
	//强化等级
	private	int m_sthequipmentlevel;
	//消耗道具
	private	int[] m_propid;
	//道具数量
	private	int[] m_numbers;
	//特殊装备消耗道具
	private	int[] m_propid2;
	//特殊装备消耗道具数量
	private	int[] m_numbers2;
	//属性枚举
	private	int[] m_attribute;
	//数值
	private	int[] m_value;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_Qosition = data.ReadInt32();
		m_Parts = data.ReadInt32();
		m_Level = data.ReadInt32();
		m_sthequipmentlevel = data.ReadInt32();
		m_propid = parserXMLIntArray(ReadToString(data));
		m_numbers = parserXMLIntArray(ReadToString(data));
		m_propid2 = parserXMLIntArray(ReadToString(data));
		m_numbers2 = parserXMLIntArray(ReadToString(data));
		m_attribute = parserXMLIntArray(ReadToString(data));
		m_value = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getQosition()
	{
		return this.m_Qosition;
	}

	public	int	getParts()
	{
		return this.m_Parts;
	}

	public	int	getLevel()
	{
		return this.m_Level;
	}

	public	int	getSthequipmentlevel()
	{
		return this.m_sthequipmentlevel;
	}

	public	int[]	getPropid()
	{
		return this.m_propid;
	}

	public	int[]	getNumbers()
	{
		return this.m_numbers;
	}

	public	int[]	getPropid2()
	{
		return this.m_propid2;
	}

	public	int[]	getNumbers2()
	{
		return this.m_numbers2;
	}

	public	int[]	getAttribute()
	{
		return this.m_attribute;
	}

	public	int[]	getValue()
	{
		return this.m_value;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}