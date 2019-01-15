//
// autor : OgreZCD
// date : 2015-07-29
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ArtresourceTemplate : IExcelBean
{
	#region attribute
	//id
	private	int m_id;
	//名称ID
	private	string m_NameID;
	//描述ID
	private	string m_DescriptionID;
	//属性类型
	private	int[] m_attriType;
	//属性描述
	private	string[] m_attriDes;
	//是否百分比
	private	int[] m_ispercentage;
	//属性显示符号
	private	string[] m_symbol;
	//属性值
	private	int[] m_attriValue;
	//额外被动技能
	private	int m_bonusPassiveSkill;
	//被动技能描述
	private	string m_bonusPassiveSkilldes;
	//角色默认美术资源
	private	string m_artresources;
	//默认缩放比例
	private	float m_artresources_zoom;
	//角色动作资源
	private	string[] m_actionresource;
	//头像图标资源
	private	string m_headiconresource;
	//头像大图资源
	private	string m_headartresource;
	//集火特效大小
	private	float m_FireSignSize;
	//待机语音
	private	string m_readysound;
	//死亡语音
	private	string m_diesound;
	//时装资源
	private	string m_fashionresource;
	//技能1
	private	float m_Skill1;
	//技能2
	private	float m_Skill2;
	//受到攻击
	private	float m_Hurt1;
	//位移受击
	private	float m_Hurt2;
	//死亡
	private	float m_Die1;
	//走
	private	float m_Walk1;
	//跑
	private	float m_Run1;
	//跳
	private	float m_Jump1;
	//战斗待机
	private	float m_Fidle1;
	//切状态用战斗待机
	private	float m_Fidle2;
	//摆造型
	private	float m_Pose1;
	//惊恐
	private	float m_Alarm1;
	//普通待机
	private	float m_Nidle1;
	//休闲待机
	private	float m_Nidle2;
	//召唤动作
	private	float m_Summon1;
	//拾取动作
	private	float m_Pick1;
	//眩晕动作
	private	float m_Dizzy1;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_NameID = ReadToString(data);
		m_DescriptionID = ReadToString(data);
		m_attriType = parserXMLIntArray(ReadToString(data));
		m_attriDes = parserXMLStringArray(ReadToString(data));
		m_ispercentage = parserXMLIntArray(ReadToString(data));
		m_symbol = parserXMLStringArray(ReadToString(data));
		m_attriValue = parserXMLIntArray(ReadToString(data));
		m_bonusPassiveSkill = data.ReadInt32();
		m_bonusPassiveSkilldes = ReadToString(data);
		m_artresources = ReadToString(data);
		m_artresources_zoom = ReadToSingle(data);
		m_actionresource = parserXMLStringArray(ReadToString(data));
		m_headiconresource = ReadToString(data);
		m_headartresource = ReadToString(data);
		m_FireSignSize = ReadToSingle(data);
		m_readysound = ReadToString(data);
		m_diesound = ReadToString(data);
		m_fashionresource = ReadToString(data);
		m_Skill1 = ReadToSingle(data);
		m_Skill2 = ReadToSingle(data);
		m_Hurt1 = ReadToSingle(data);
		m_Hurt2 = ReadToSingle(data);
		m_Die1 = ReadToSingle(data);
		m_Walk1 = ReadToSingle(data);
		m_Run1 = ReadToSingle(data);
		m_Jump1 = ReadToSingle(data);
		m_Fidle1 = ReadToSingle(data);
		m_Fidle2 = ReadToSingle(data);
		m_Pose1 = ReadToSingle(data);
		m_Alarm1 = ReadToSingle(data);
		m_Nidle1 = ReadToSingle(data);
		m_Nidle2 = ReadToSingle(data);
		m_Summon1 = ReadToSingle(data);
		m_Pick1 = ReadToSingle(data);
		m_Dizzy1 = ReadToSingle(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getNameID()
	{
		return this.m_NameID;
	}

	public	string	getDescriptionID()
	{
		return this.m_DescriptionID;
	}

	public	int[]	getAttriType()
	{
		return this.m_attriType;
	}

	public	string[]	getAttriDes()
	{
		return this.m_attriDes;
	}

	public	int[]	getIspercentage()
	{
		return this.m_ispercentage;
	}

	public	string[]	getSymbol()
	{
		return this.m_symbol;
	}

	public	int[]	getAttriValue()
	{
		return this.m_attriValue;
	}

	public	int	getBonusPassiveSkill()
	{
		return this.m_bonusPassiveSkill;
	}

	public	string	getBonusPassiveSkilldes()
	{
		return this.m_bonusPassiveSkilldes;
	}

	public	string	getArtresources()
	{
		return this.m_artresources;
	}

	public	float	getArtresources_zoom()
	{
		return this.m_artresources_zoom;
	}

	public	string[]	getActionresource()
	{
		return this.m_actionresource;
	}

	public	string	getHeadiconresource()
	{
		return this.m_headiconresource;
	}

	public	string	getHeadartresource()
	{
		return this.m_headartresource;
	}

	public	float	getFireSignSize()
	{
		return this.m_FireSignSize;
	}

	public	string	getReadysound()
	{
		return this.m_readysound;
	}

	public	string	getDiesound()
	{
		return this.m_diesound;
	}

	public	string	getFashionresource()
	{
		return this.m_fashionresource;
	}

	public	float	getSkill1()
	{
		return this.m_Skill1;
	}

	public	float	getSkill2()
	{
		return this.m_Skill2;
	}

	public	float	getHurt1()
	{
		return this.m_Hurt1;
	}

	public	float	getHurt2()
	{
		return this.m_Hurt2;
	}

	public	float	getDie1()
	{
		return this.m_Die1;
	}

	public	float	getWalk1()
	{
		return this.m_Walk1;
	}

	public	float	getRun1()
	{
		return this.m_Run1;
	}

	public	float	getJump1()
	{
		return this.m_Jump1;
	}

	public	float	getFidle1()
	{
		return this.m_Fidle1;
	}

	public	float	getFidle2()
	{
		return this.m_Fidle2;
	}

	public	float	getPose1()
	{
		return this.m_Pose1;
	}

	public	float	getAlarm1()
	{
		return this.m_Alarm1;
	}

	public	float	getNidle1()
	{
		return this.m_Nidle1;
	}

	public	float	getNidle2()
	{
		return this.m_Nidle2;
	}

	public	float	getSummon1()
	{
		return this.m_Summon1;
	}

	public	float	getPick1()
	{
		return this.m_Pick1;
	}

	public	float	getDizzy1()
	{
		return this.m_Dizzy1;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}