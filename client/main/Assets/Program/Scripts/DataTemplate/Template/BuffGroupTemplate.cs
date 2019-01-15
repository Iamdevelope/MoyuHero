//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class BuffgroupTemplate : IExcelBean
{
	#region attribute
	//buffID
	private	int m_id;
	//参数
	private	int[] m_param;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_param = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int[]	getParam()
	{
		return this.m_param;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}