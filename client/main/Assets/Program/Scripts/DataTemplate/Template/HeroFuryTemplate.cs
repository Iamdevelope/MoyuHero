//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class HerofuryTemplate : IExcelBean
{
	#region attribute
	//等级
	private	int m_id;
	//模板
	private	int[] m_template;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_template = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int[]	getTemplate()
	{
		return this.m_template;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}