//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;

public class HeroexpTemplate : IExcelBean
{
	#region attribute
	//等级
	private	int m_id;
	//经验列
	private	int[] m_exp;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
        m_exp = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int[]	getExp()
	{
		return this.m_exp;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}