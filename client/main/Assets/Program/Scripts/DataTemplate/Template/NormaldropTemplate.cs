//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class NormaldropTemplate : IExcelBean
{
	#region attribute
	//掉落包id
	private	int m_id;
	//掉落包类型
	private	int m_normaldroptype;
	//掉落次数
	private	int m_normaldroptime;
	//掉落小包
	private	int[] m_innerdrop;
	//掉落小包几率
	private	int[] m_innerdropprob;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_normaldroptype = data.ReadInt32();
		m_normaldroptime = data.ReadInt32();
		m_innerdrop = parserXMLIntArray(ReadToString(data));
		m_innerdropprob = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getNormaldroptype()
	{
		return this.m_normaldroptype;
	}

	public	int	getNormaldroptime()
	{
		return this.m_normaldroptime;
	}

	public	int[]	getInnerdrop()
	{
		return this.m_innerdrop;
	}

	public	int[]	getInnerdropprob()
	{
		return this.m_innerdropprob;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}