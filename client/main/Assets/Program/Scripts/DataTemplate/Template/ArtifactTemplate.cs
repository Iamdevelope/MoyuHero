//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ArtifactTemplate : IExcelBean
{
	#region attribute
	//神器id
	private	int m_id;
	//神器名称
	private	string m_name;
	//神器等级
	private	int m_level;
	//美术资源名称
	private	string m_resourceName;
	//特效名称
	private	string m_effectName;
	//神器类型
	private	int m_type;
	//开启等级
	private	int m_playerLevel;
	//属性类型
	private	int[] m_attriType;
	//属性值
	private	int[] m_attriValue;
	//英雄id
	private	int[] m_heroID;
	//所需数量
	private	int[] m_heroNum;
	//属性权重
	private	int[] m_weight;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_name = ReadToString(data);
		m_level = data.ReadInt32();
		m_resourceName = ReadToString(data);
		m_effectName = ReadToString(data);
		m_type = data.ReadInt32();
		m_playerLevel = data.ReadInt32();
		m_attriType = parserXMLIntArray(ReadToString(data));
		m_attriValue = parserXMLIntArray(ReadToString(data));
		m_heroID = parserXMLIntArray(ReadToString(data));
		m_heroNum = parserXMLIntArray(ReadToString(data));
		m_weight = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getName()
	{
		return this.m_name;
	}

	public	int	getLevel()
	{
		return this.m_level;
	}

	public	string	getResourceName()
	{
		return this.m_resourceName;
	}

	public	string	getEffectName()
	{
		return this.m_effectName;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getPlayerLevel()
	{
		return this.m_playerLevel;
	}

	public	int[]	getAttriType()
	{
		return this.m_attriType;
	}

	public	int[]	getAttriValue()
	{
		return this.m_attriValue;
	}

	public	int[]	getHeroID()
	{
		return this.m_heroID;
	}

	public	int[]	getHeroNum()
	{
		return this.m_heroNum;
	}

	public	int[]	getWeight()
	{
		return this.m_weight;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}