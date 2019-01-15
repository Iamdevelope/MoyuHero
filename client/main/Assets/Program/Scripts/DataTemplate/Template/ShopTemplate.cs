//
// autor : OgreZCD
// date : 2015-08-17
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ShopTemplate : IExcelBean
{
	#region attribute
	//商品id
	private	int m_id;
	//商品名称描述
	private	string m_commodityName;
	//商品内容描述
	private	string m_commodityDes;
	//页签id
	private	int m_tabID;
	//页签内排序
	private	int m_sorting;
	//商品类型
	private	int m_type;
	//参数id
	private	string m_para;
	//每日购买次数
	private	int m_dailyMaxBuy;
	//总购买次数
	private	int m_shelveMaxBuy;
	//所需资源类型
	private	int m_costType;
	//资源花费
	private	int[] m_cost;
	//提示标签类型
	private	int m_tagtype;
	//提示标签内容1
	private	string m_tagtext1;
	//提示标签内容2
	private	string m_tagtext2;
	//内容标签
	private	string m_contenttag;
	//上架日期
	private	string m_onShelve;
	//下架日期
	private	string m_offShelve;
	//打折花费
	private	int[] m_discountCost;
	//打折开始日期
	private	string m_discountOn;
	//打折结束日期
	private	string m_discountOff;
	//图标资源名称
	private	string m_resourceName;
	//图标底图
	private	string m_baseicon;
	//所需vip最低等级
	private	int m_vipLimit;
	//预览类型
	private	int m_previewType;
	//预览显示配置
	private	string m_previewContent;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_commodityName = ReadToString(data);
		m_commodityDes = ReadToString(data);
		m_tabID = data.ReadInt32();
		m_sorting = data.ReadInt32();
		m_type = data.ReadInt32();
		m_para = ReadToString(data);
		m_dailyMaxBuy = data.ReadInt32();
		m_shelveMaxBuy = data.ReadInt32();
		m_costType = data.ReadInt32();
		m_cost = parserXMLIntArray(ReadToString(data));
		m_tagtype = data.ReadInt32();
		m_tagtext1 = ReadToString(data);
		m_tagtext2 = ReadToString(data);
		m_contenttag = ReadToString(data);
		m_onShelve = ReadToString(data);
		m_offShelve = ReadToString(data);
		m_discountCost = parserXMLIntArray(ReadToString(data));
		m_discountOn = ReadToString(data);
		m_discountOff = ReadToString(data);
		m_resourceName = ReadToString(data);
		m_baseicon = ReadToString(data);
		m_vipLimit = data.ReadInt32();
		m_previewType = data.ReadInt32();
		m_previewContent = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getCommodityName()
	{
		return this.m_commodityName;
	}

	public	string	getCommodityDes()
	{
		return this.m_commodityDes;
	}

	public	int	getTabID()
	{
		return this.m_tabID;
	}

	public	int	getSorting()
	{
		return this.m_sorting;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	string	getPara()
	{
		return this.m_para;
	}

	public	int	getDailyMaxBuy()
	{
		return this.m_dailyMaxBuy;
	}

	public	int	getShelveMaxBuy()
	{
		return this.m_shelveMaxBuy;
	}

	public	int	getCostType()
	{
		return this.m_costType;
	}

	public	int[]	getCost()
	{
		return this.m_cost;
	}

	public	int	getTagtype()
	{
		return this.m_tagtype;
	}

	public	string	getTagtext1()
	{
		return this.m_tagtext1;
	}

	public	string	getTagtext2()
	{
		return this.m_tagtext2;
	}

	public	string	getContenttag()
	{
		return this.m_contenttag;
	}

	public	string	getOnShelve()
	{
		return this.m_onShelve;
	}

	public	string	getOffShelve()
	{
		return this.m_offShelve;
	}

	public	int[]	getDiscountCost()
	{
		return this.m_discountCost;
	}

	public	string	getDiscountOn()
	{
		return this.m_discountOn;
	}

	public	string	getDiscountOff()
	{
		return this.m_discountOff;
	}

	public	string	getResourceName()
	{
		return this.m_resourceName;
	}

	public	string	getBaseicon()
	{
		return this.m_baseicon;
	}

	public	int	getVipLimit()
	{
		return this.m_vipLimit;
	}

	public	int	getPreviewType()
	{
		return this.m_previewType;
	}

	public	string	getPreviewContent()
	{
		return this.m_previewContent;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}