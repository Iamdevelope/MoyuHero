//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class LevelamendmentTemplate : IExcelBean
{
	#region attribute
	//等级
	private	int m_id;
	//等级修正模板
	private	float[] m_levelAmendment;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_levelAmendment = parserXMLFloatArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	float[]	getLevelAmendment()
	{
		return this.m_levelAmendment;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}