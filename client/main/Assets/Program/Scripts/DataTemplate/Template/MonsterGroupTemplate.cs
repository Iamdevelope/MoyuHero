//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class MonstergroupTemplate : IExcelBean
{
	#region attribute
	//怪物组id
	private	int m_id;
	//怪物组类型
	private	int m_grouptype;
	//怪物id
	private	int[] m_monsterid;
	//怪物几率
	private	int[] m_monsterprobability;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_grouptype = data.ReadInt32();
		m_monsterid = parserXMLIntArray(ReadToString(data));
		m_monsterprobability = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getGrouptype()
	{
		return this.m_grouptype;
	}

	public	int[]	getMonsterid()
	{
		return this.m_monsterid;
	}

	public	int[]	getMonsterprobability()
	{
		return this.m_monsterprobability;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}