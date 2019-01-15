//
// autor : OgreZCD
// date : 2015-10-22
//

using UnityEngine;
using System.Collections;
using System.IO;


public class PropsjumpuiTemplate : IExcelBean
{
	#region attribute
	//序号
	private	int m_id;
	//注释
	private	string m_comment;
	//跳转至UI路径
	private	string m_jumpUIpath;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_comment = ReadToString(data);
		m_jumpUIpath = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getComment()
	{
		return this.m_comment;
	}

	public	string	getJumpUIpath()
	{
		return this.m_jumpUIpath;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}