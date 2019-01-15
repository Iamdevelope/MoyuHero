//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class RechargeTemplate : IExcelBean
{
	#region attribute
	//充值档编号
	private	int m_id;
	//平台编号
	private	int m_platformID;
	//充值档标识
	private	string m_rechargeCode;
	//充值类型
	private	int m_type;
	//充值参数
	private	int m_rechargeParam;
	//货币类型
	private	int m_moneyType;
	//货币价格
	private	int m_moneyNum;
	//资源图标
	private	string m_icon;
	//标题
	private	string m_title;
	//描述
	private	string m_des;
	//首充奖励
	private	int m_firstBuy;
	//首充标题描述
	private	string m_firstBuyTitle;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_platformID = data.ReadInt32();
		m_rechargeCode = ReadToString(data);
		m_type = data.ReadInt32();
		m_rechargeParam = data.ReadInt32();
		m_moneyType = data.ReadInt32();
		m_moneyNum = data.ReadInt32();
		m_icon = ReadToString(data);
		m_title = ReadToString(data);
		m_des = ReadToString(data);
		m_firstBuy = data.ReadInt32();
		m_firstBuyTitle = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getPlatformID()
	{
		return this.m_platformID;
	}

	public	string	getRechargeCode()
	{
		return this.m_rechargeCode;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getRechargeParam()
	{
		return this.m_rechargeParam;
	}

	public	int	getMoneyType()
	{
		return this.m_moneyType;
	}

	public	int	getMoneyNum()
	{
		return this.m_moneyNum;
	}

	public	string	getIcon()
	{
		return this.m_icon;
	}

	public	string	getTitle()
	{
		return this.m_title;
	}

	public	string	getDes()
	{
		return this.m_des;
	}

	public	int	getFirstBuy()
	{
		return this.m_firstBuy;
	}

	public	string	getFirstBuyTitle()
	{
		return this.m_firstBuyTitle;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}