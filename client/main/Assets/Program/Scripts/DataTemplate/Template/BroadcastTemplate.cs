//
// autor : OgreZCD
// date : 2015-07-15
//

using UnityEngine;
using System.Collections;
using System.IO;


public class BroadcastTemplate : IExcelBean
{
	#region attribute
	//索引
	private	int m_id;
	//标题
	private	string m_title;
	//内容
	private	string m_content;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_title = ReadToString(data);
		m_content = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getTitle()
	{
		return this.m_title;
	}

	public	string	getContent()
	{
		return this.m_content;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}