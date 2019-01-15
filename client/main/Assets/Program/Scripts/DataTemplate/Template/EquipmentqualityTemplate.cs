//
// autor : OgreZCD
// date : 2015-10-28
//

using UnityEngine;
using System.Collections;
using System.IO;


public class EquipmentqualityTemplate : IExcelBean
{
	#region attribute
	//装备id
	private	int m_id;
	//装备名称
	private	string m_Name;
	//装备图标
	private	string m_Icon;
	//定位
	private	int m_Qosition;
	//装备部位
	private	int m_Parts;
	//品质颜色
	private	int m_QialityColor;
	//品质等级
	private	int m_QualityLevel;
	//升品英雄等级需求
	private	int m_reqlevel;
	//升品需求道具id
	private	int[] m_PropId;
	//需求数量
	private	int[] m_numbers;
	//需求金币
	private	int m_demandmoney;
	//下一装备等级id
	private	int m_NextId;
	//品质属性类型
	private	int[] m_QualityAttribute;
	//数值
	private	int[] m_Numerical;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_Name = ReadToString(data);
		m_Icon = ReadToString(data);
		m_Qosition = data.ReadInt32();
		m_Parts = data.ReadInt32();
		m_QialityColor = data.ReadInt32();
		m_QualityLevel = data.ReadInt32();
		m_reqlevel = data.ReadInt32();
		m_PropId = parserXMLIntArray(ReadToString(data));
		m_numbers = parserXMLIntArray(ReadToString(data));
		m_demandmoney = data.ReadInt32();
		m_NextId = data.ReadInt32();
		m_QualityAttribute = parserXMLIntArray(ReadToString(data));
		m_Numerical = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getName()
	{
		return this.m_Name;
	}

	public	string	getIcon()
	{
		return this.m_Icon;
	}

	public	int	getQosition()
	{
		return this.m_Qosition;
	}

	public	int	getParts()
	{
		return this.m_Parts;
	}

	public	int	getQialityColor()
	{
		return this.m_QialityColor;
	}

	public	int	getQualityLevel()
	{
		return this.m_QualityLevel;
	}

	public	int	getReqlevel()
	{
		return this.m_reqlevel;
	}

	public	int[]	getPropId()
	{
		return this.m_PropId;
	}

	public	int[]	getNumbers()
	{
		return this.m_numbers;
	}

	public	int	getDemandmoney()
	{
		return this.m_demandmoney;
	}

	public	int	getNextId()
	{
		return this.m_NextId;
	}

	public	int[]	getQualityAttribute()
	{
		return this.m_QualityAttribute;
	}

	public	int[]	getNumerical()
	{
		return this.m_Numerical;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}