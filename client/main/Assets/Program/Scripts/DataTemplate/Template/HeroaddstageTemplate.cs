//
// autor : OgreZCD
// date : 2015-10-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class HeroaddstageTemplate : IExcelBean
{
	#region attribute
	//序号
	private	int m_id;
	//资质
	private	int m_Born;
	//定位
	private	int m_Qosition;
	//星级
	private	int m_Quality;
	//当前阶数
	private	int m_HalosPn;
	//需求等级
	private	int m_Level;
	//金币消耗
	private	int m_Gold;
	//需要材料id
	private	int[] m_Stuff;
	//材料数量
	private	int[] m_Numbers;
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
		m_Quality = data.ReadInt32();
		m_HalosPn = data.ReadInt32();
		m_Level = data.ReadInt32();
		m_Gold = data.ReadInt32();
		m_Stuff = parserXMLIntArray(ReadToString(data));
		m_Numbers = parserXMLIntArray(ReadToString(data));
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

	public	int	getQuality()
	{
		return this.m_Quality;
	}

	public	int	getHalosPn()
	{
		return this.m_HalosPn;
	}

	public	int	getLevel()
	{
		return this.m_Level;
	}

	public	int	getGold()
	{
		return this.m_Gold;
	}

	public	int[]	getStuff()
	{
		return this.m_Stuff;
	}

	public	int[]	getNumbers()
	{
		return this.m_Numbers;
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