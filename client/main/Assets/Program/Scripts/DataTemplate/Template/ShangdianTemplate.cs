//
// autor : OgreZCD
// date : 2015-11-03
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ShangdianTemplate : IExcelBean
{
	#region attribute
	//序列号
	private	int m_id;
	//商店名称
	private	string m_StoreName;
	//商店图标
	private	string m_StoreIocn;
	//商店开启条件
	private	int m_StoreOpen;
	//条件数据
	private	int m_ConditionalData;
	//商店开启条件描述
	private	string m_ConditionDescription;
	//商店是否显示
	private	int m_WhetherDisplay;
	//商店刷新时间
	private	int[] m_RefreshTime;
	//刷新货币类型
	private	int m_CurrencyType;
	//货币消耗
	private	int[] m_Consumption;
	//商店货币类型
	private	int m_StoreCurrencyType;
	//商店货币图标
	private	string m_StoreCurrencyIcon;
	//货币商店描述
	private	string m_MoneyStoreDescription;
	//道具是否可重复购买
	private	int m_RepeatPurchase;
	//商品
	private	int[] m_Commodity;
	//商品是否推荐
	private	int[] m_Recommend;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_StoreName = ReadToString(data);
		m_StoreIocn = ReadToString(data);
		m_StoreOpen = data.ReadInt32();
		m_ConditionalData = data.ReadInt32();
		m_ConditionDescription = ReadToString(data);
		m_WhetherDisplay = data.ReadInt32();
		m_RefreshTime = parserXMLIntArray(ReadToString(data));
		m_CurrencyType = data.ReadInt32();
		m_Consumption = parserXMLIntArray(ReadToString(data));
		m_StoreCurrencyType = data.ReadInt32();
		m_StoreCurrencyIcon = ReadToString(data);
		m_MoneyStoreDescription = ReadToString(data);
		m_RepeatPurchase = data.ReadInt32();
		m_Commodity = parserXMLIntArray(ReadToString(data));
		m_Recommend = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getStoreName()
	{
		return this.m_StoreName;
	}

	public	string	getStoreIocn()
	{
		return this.m_StoreIocn;
	}

	public	int	getStoreOpen()
	{
		return this.m_StoreOpen;
	}

	public	int	getConditionalData()
	{
		return this.m_ConditionalData;
	}

	public	string	getConditionDescription()
	{
		return this.m_ConditionDescription;
	}

	public	int	getWhetherDisplay()
	{
		return this.m_WhetherDisplay;
	}

	public	int[]	getRefreshTime()
	{
		return this.m_RefreshTime;
	}

	public	int	getCurrencyType()
	{
		return this.m_CurrencyType;
	}

	public	int[]	getConsumption()
	{
		return this.m_Consumption;
	}

	public	int	getStoreCurrencyType()
	{
		return this.m_StoreCurrencyType;
	}

	public	string	getStoreCurrencyIcon()
	{
		return this.m_StoreCurrencyIcon;
	}

	public	string	getMoneyStoreDescription()
	{
		return this.m_MoneyStoreDescription;
	}

	public	int	getRepeatPurchase()
	{
		return this.m_RepeatPurchase;
	}

	public	int[]	getCommodity()
	{
		return this.m_Commodity;
	}

	public	int[]	getRecommend()
	{
		return this.m_Recommend;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}