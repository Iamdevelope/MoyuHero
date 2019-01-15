//
// autor : OgreZCD
// date : 2015-10-13
//

using UnityEngine;
using System.Collections;
using System.IO;


public class SkillupcostTemplate : IExcelBean
{
	#region attribute
	//技能ID
	private	int m_id;
	//当前等级
	private	int m_skillLevel;
	//升级消耗资源
	private	int[] m_upgradeCostId;
	//消耗数量
	private	int[] m_upgradeCostNum;
	//升级后技能ID
	private	int m_upgradeSkillID;
	//升级需求星级
	private	int m_upgradeStarCondition;
	//升级描述
	private	string m_upgradeDes;
	//升级消耗资源2
	private	int[] m_upgradeCostId2;
	//消耗数量2
	private	int[] m_upgradeCostNum2;
	//升级消耗资源3
	private	int[] m_upgradeCostId3;
	//消耗数量3
	private	int[] m_upgradeCostNum3;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_skillLevel = data.ReadInt32();
		m_upgradeCostId = parserXMLIntArray(ReadToString(data));
		m_upgradeCostNum = parserXMLIntArray(ReadToString(data));
		m_upgradeSkillID = data.ReadInt32();
		m_upgradeStarCondition = data.ReadInt32();
		m_upgradeDes = ReadToString(data);
		m_upgradeCostId2 = parserXMLIntArray(ReadToString(data));
		m_upgradeCostNum2 = parserXMLIntArray(ReadToString(data));
		m_upgradeCostId3 = parserXMLIntArray(ReadToString(data));
		m_upgradeCostNum3 = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getSkillLevel()
	{
		return this.m_skillLevel;
	}

	public	int[]	getUpgradeCostId()
	{
		return this.m_upgradeCostId;
	}

	public	int[]	getUpgradeCostNum()
	{
		return this.m_upgradeCostNum;
	}

	public	int	getUpgradeSkillID()
	{
		return this.m_upgradeSkillID;
	}

	public	int	getUpgradeStarCondition()
	{
		return this.m_upgradeStarCondition;
	}

	public	string	getUpgradeDes()
	{
		return this.m_upgradeDes;
	}

	public	int[]	getUpgradeCostId2()
	{
		return this.m_upgradeCostId2;
	}

	public	int[]	getUpgradeCostNum2()
	{
		return this.m_upgradeCostNum2;
	}

	public	int[]	getUpgradeCostId3()
	{
		return this.m_upgradeCostId3;
	}

	public	int[]	getUpgradeCostNum3()
	{
		return this.m_upgradeCostNum3;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}