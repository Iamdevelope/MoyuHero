//
// autor : OgreZCD
// date : 2015-08-17
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ExchangeTemplate : IExcelBean
{
	#region attribute
	//商品ID
	private	int m_id;
	//服务器IDs
	private	string m_serverids;
	//平台
	private	string m_platform;
	//价格
	private	int m_price;
	//魔钻
	private	int m_yuanbao;
	//名称
	private	string m_name;
	//图标资源
	private	string m_icon;
	//图标底图
	private	string m_baseicon;
	//标题
	private	string m_title;
	//赠送
	private	int m_present;
	//描述
	private	string m_detail;
	//预览类型
	private	int m_previewType;
	//预览配置
	private	string m_previewContent;
	//VIP等级限制
	private	int m_vipLimit;
	//排序
	private	int m_sorting;
	//月卡ID
	private	int m_monthcardID;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_serverids = ReadToString(data);
		m_platform = ReadToString(data);
		m_price = data.ReadInt32();
		m_yuanbao = data.ReadInt32();
		m_name = ReadToString(data);
		m_icon = ReadToString(data);
		m_baseicon = ReadToString(data);
		m_title = ReadToString(data);
		m_present = data.ReadInt32();
		m_detail = ReadToString(data);
		m_previewType = data.ReadInt32();
		m_previewContent = ReadToString(data);
		m_vipLimit = data.ReadInt32();
		m_sorting = data.ReadInt32();
		m_monthcardID = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getServerids()
	{
		return this.m_serverids;
	}

	public	string	getPlatform()
	{
		return this.m_platform;
	}

	public	int	getPrice()
	{
		return this.m_price;
	}

	public	int	getYuanbao()
	{
		return this.m_yuanbao;
	}

	public	string	getName()
	{
		return this.m_name;
	}

	public	string	getIcon()
	{
		return this.m_icon;
	}

	public	string	getBaseicon()
	{
		return this.m_baseicon;
	}

	public	string	getTitle()
	{
		return this.m_title;
	}

	public	int	getPresent()
	{
		return this.m_present;
	}

	public	string	getDetail()
	{
		return this.m_detail;
	}

	public	int	getPreviewType()
	{
		return this.m_previewType;
	}

	public	string	getPreviewContent()
	{
		return this.m_previewContent;
	}

	public	int	getVipLimit()
	{
		return this.m_vipLimit;
	}

	public	int	getSorting()
	{
		return this.m_sorting;
	}

	public	int	getMonthcardID()
	{
		return this.m_monthcardID;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}