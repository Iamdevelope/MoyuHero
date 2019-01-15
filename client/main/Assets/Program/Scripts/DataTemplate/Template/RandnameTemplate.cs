//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class RandnameTemplate : IExcelBean
{
	#region attribute
	//唯一ID
	private	int m_id;
	//类型
	private	int m_type;
	//名字库
	private	string m_name;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_type = data.ReadInt32();
		m_name = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	string	getName()
	{
		return this.m_name;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}