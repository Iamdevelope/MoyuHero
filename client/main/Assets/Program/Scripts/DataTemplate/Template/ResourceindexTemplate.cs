//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ResourceindexTemplate : IExcelBean
{
	#region attribute
	//资源ID
	private	int m_id;
	//资源名称
	private	string m_name;
	//资源图标1
	private	string m_icon1;
	//资源图标2
	private	string m_icon2;
	//资源图标3
	private	string m_icon3;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_name = ReadToString(data);
		m_icon1 = ReadToString(data);
		m_icon2 = ReadToString(data);
		m_icon3 = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getName()
	{
		return this.m_name;
	}

	public	string	getIcon1()
	{
		return this.m_icon1;
	}

	public	string	getIcon2()
	{
		return this.m_icon2;
	}

	public	string	getIcon3()
	{
		return this.m_icon3;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}