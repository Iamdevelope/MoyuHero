//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class MysteriousshopTemplate : IExcelBean
{
	#region attribute
	//唯一ID
	private	int m_id;
	//商店ID
	private	int m_shopID;
	//可上架开始日期
	private	string m_onShelve;
	//可上架结束日期
	private	string m_offShelve;
	//所需资源类型
	private	int m_costType;
	//商品类型
	private	int m_commoditytype;
	//上架权重
	private	int m_sellweight;
	//商品ID
	private	int m_commodityid;
	//商品数量
	private	int m_commoditynum;
	//最小售价
	private	int m_mincost;
	//最大售价
	private	int m_maxcost;
	//售价单位
	private	int m_unitcost;
	//商品名称描述
	private	string m_commodityName;
	//商品内容描述
	private	string m_commodityDes;
	//图标资源名称
	private	string m_commodityresource;
	//商品排序
	private	int m_sorting;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_shopID = data.ReadInt32();
		m_onShelve = ReadToString(data);
		m_offShelve = ReadToString(data);
		m_costType = data.ReadInt32();
		m_commoditytype = data.ReadInt32();
		m_sellweight = data.ReadInt32();
		m_commodityid = data.ReadInt32();
		m_commoditynum = data.ReadInt32();
		m_mincost = data.ReadInt32();
		m_maxcost = data.ReadInt32();
		m_unitcost = data.ReadInt32();
		m_commodityName = ReadToString(data);
		m_commodityDes = ReadToString(data);
		m_commodityresource = ReadToString(data);
		m_sorting = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getShopID()
	{
		return this.m_shopID;
	}

	public	string	getOnShelve()
	{
		return this.m_onShelve;
	}

	public	string	getOffShelve()
	{
		return this.m_offShelve;
	}

	public	int	getCostType()
	{
		return this.m_costType;
	}

	public	int	getCommoditytype()
	{
		return this.m_commoditytype;
	}

	public	int	getSellweight()
	{
		return this.m_sellweight;
	}

	public	int	getCommodityid()
	{
		return this.m_commodityid;
	}

	public	int	getCommoditynum()
	{
		return this.m_commoditynum;
	}

	public	int	getMincost()
	{
		return this.m_mincost;
	}

	public	int	getMaxcost()
	{
		return this.m_maxcost;
	}

	public	int	getUnitcost()
	{
		return this.m_unitcost;
	}

	public	string	getCommodityName()
	{
		return this.m_commodityName;
	}

	public	string	getCommodityDes()
	{
		return this.m_commodityDes;
	}

	public	string	getCommodityresource()
	{
		return this.m_commodityresource;
	}

	public	int	getSorting()
	{
		return this.m_sorting;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}