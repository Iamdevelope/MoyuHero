//
// autor : OgreZCD
// date : 2015-11-02
//

using UnityEngine;
using System.Collections;
using System.IO;


public class MsTemplate : IExcelBean
{
	#region attribute
	//序号
	private	int m_id;
	//秘术类型
	private	int m_mstype;
	//秘术名称
	private	string m_msname;
	//秘术描述
	private	string m_ddes;
	//秘术简略描述
	private	string m_lowdes;
	//秘术图标
	private	string m_icon;
	//秘术开启星级需求
	private	int m_stardemand;
	//秘术开启阶数需求
	private	int m_stagedemand;
	//秘术等级
	private	int[] m_level;
	//对应秘术加成value
	private	int[] m_value;
	//升级验值
	private	int[] m_consumexpevalue;
	//升级消耗资源id
	private	int[] m_consumzyid;
	//消耗资源数量
	private	int[] m_consumnb;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_mstype = data.ReadInt32();
		m_msname = ReadToString(data);
		m_ddes = ReadToString(data);
		m_lowdes = ReadToString(data);
		m_icon = ReadToString(data);
		m_stardemand = data.ReadInt32();
		m_stagedemand = data.ReadInt32();
		m_level = parserXMLIntArray(ReadToString(data));
		m_value = parserXMLIntArray(ReadToString(data));
		m_consumexpevalue = parserXMLIntArray(ReadToString(data));
		m_consumzyid = parserXMLIntArray(ReadToString(data));
		m_consumnb = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getMstype()
	{
		return this.m_mstype;
	}

	public	string	getMsname()
	{
		return this.m_msname;
	}

	public	string	getDdes()
	{
		return this.m_ddes;
	}

	public	string	getLowdes()
	{
		return this.m_lowdes;
	}

	public	string	getIcon()
	{
		return this.m_icon;
	}

	public	int	getStardemand()
	{
		return this.m_stardemand;
	}

	public	int	getStagedemand()
	{
		return this.m_stagedemand;
	}

	public	int[]	getLevel()
	{
		return this.m_level;
	}

	public	int[]	getValue()
	{
		return this.m_value;
	}

	public	int[]	getConsumexpevalue()
	{
		return this.m_consumexpevalue;
	}

	public	int[]	getConsumzyid()
	{
		return this.m_consumzyid;
	}

	public	int[]	getConsumnb()
	{
		return this.m_consumnb;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}