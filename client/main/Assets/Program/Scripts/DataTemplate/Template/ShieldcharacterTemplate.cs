//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ShieldcharacterTemplate : IExcelBean
{
	#region attribute
	//编号
	private	int m_id;
	//屏蔽字
	private	string m_word;
	//起名屏蔽
	private	int m_name;
	//聊天屏蔽
	private	int m_chat;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_word = ReadToString(data);
		m_name = data.ReadInt32();
		m_chat = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getWord()
	{
		return this.m_word;
	}

	public	int	getName()
	{
		return this.m_name;
	}

	public	int	getChat()
	{
		return this.m_chat;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}