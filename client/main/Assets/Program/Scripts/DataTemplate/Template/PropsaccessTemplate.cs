//
// autor : OgreZCD
// date : 2015-10-27
//

using UnityEngine;
using System.Collections;
using System.IO;


public class PropsaccessTemplate : IExcelBean
{
	#region attribute
	//序列号
	private	int m_id;
	//道具id
	private	int m_propsid;
	//道具名称
	private	string m_comment;
	//获得途径类型1
	private	int m_accessType1;
	//获取具体1
	private	int m_accessThing1;
	//类型对应图标1
	private	string m_icon1;
	//文本描述1
	private	string m_textcomment1;
	//获得途径类型2
	private	int m_accessType2;
	//获取具体2
	private	int m_accessThing2;
	//类型对应图标2
	private	string m_icon2;
	//文本描述2
	private	string m_textcomment2;
	//获得途径类型3
	private	int m_accessType3;
	//获取具体3
	private	int m_accessThing3;
	//类型对应图标3
	private	string m_icon3;
	//文本描述3
	private	string m_textcomment3;
	//获得途径类型4
	private	int m_accessType4;
	//获取具体4
	private	int m_accessThing4;
	//类型对应图标4
	private	string m_icon4;
	//文本描述4
	private	string m_textcomment4;
	//获得途径类型5
	private	int m_accessType5;
	//获取具体5
	private	int m_accessThing5;
	//类型对应图标5
	private	string m_icon5;
	//文本描述5
	private	string m_textcomment5;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_propsid = data.ReadInt32();
		m_comment = ReadToString(data);
		m_accessType1 = data.ReadInt32();
		m_accessThing1 = data.ReadInt32();
		m_icon1 = ReadToString(data);
		m_textcomment1 = ReadToString(data);
		m_accessType2 = data.ReadInt32();
		m_accessThing2 = data.ReadInt32();
		m_icon2 = ReadToString(data);
		m_textcomment2 = ReadToString(data);
		m_accessType3 = data.ReadInt32();
		m_accessThing3 = data.ReadInt32();
		m_icon3 = ReadToString(data);
		m_textcomment3 = ReadToString(data);
		m_accessType4 = data.ReadInt32();
		m_accessThing4 = data.ReadInt32();
		m_icon4 = ReadToString(data);
		m_textcomment4 = ReadToString(data);
		m_accessType5 = data.ReadInt32();
		m_accessThing5 = data.ReadInt32();
		m_icon5 = ReadToString(data);
		m_textcomment5 = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getPropsid()
	{
		return this.m_propsid;
	}

	public	string	getComment()
	{
		return this.m_comment;
	}

	public	int	getAccessType1()
	{
		return this.m_accessType1;
	}

	public	int	getAccessThing1()
	{
		return this.m_accessThing1;
	}

	public	string	getIcon1()
	{
		return this.m_icon1;
	}

	public	string	getTextcomment1()
	{
		return this.m_textcomment1;
	}

	public	int	getAccessType2()
	{
		return this.m_accessType2;
	}

	public	int	getAccessThing2()
	{
		return this.m_accessThing2;
	}

	public	string	getIcon2()
	{
		return this.m_icon2;
	}

	public	string	getTextcomment2()
	{
		return this.m_textcomment2;
	}

	public	int	getAccessType3()
	{
		return this.m_accessType3;
	}

	public	int	getAccessThing3()
	{
		return this.m_accessThing3;
	}

	public	string	getIcon3()
	{
		return this.m_icon3;
	}

	public	string	getTextcomment3()
	{
		return this.m_textcomment3;
	}

	public	int	getAccessType4()
	{
		return this.m_accessType4;
	}

	public	int	getAccessThing4()
	{
		return this.m_accessThing4;
	}

	public	string	getIcon4()
	{
		return this.m_icon4;
	}

	public	string	getTextcomment4()
	{
		return this.m_textcomment4;
	}

	public	int	getAccessType5()
	{
		return this.m_accessType5;
	}

	public	int	getAccessThing5()
	{
		return this.m_accessThing5;
	}

	public	string	getIcon5()
	{
		return this.m_icon5;
	}

	public	string	getTextcomment5()
	{
		return this.m_textcomment5;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}