//
// autor : OgreZCD
// date : 2015-10-22
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ItemTemplate : IExcelBean
{
	#region attribute
	//id
	private	int m_id;
	//名称
	private	string m_name;
	//描述
	private	string m_des;
	//图标
	private	string m_Icon;
	//小图标
	private	string m_Icon_s;
	//包裹ID
	private	int m_bagID;
	//物品类名
	private	string m_className;
	//最大叠加数
	private	int m_stackNum;
	//可否出售
	private	int m_ifSell;
	//出售价格
	private	int m_sellPrice;
	//道具类型
	private	int m_type;
	//道具品质
	private	int m_quality;
	//当日使用次数
	private	int m_ifUse;
	//使用后掉落包id
	private	int[] m_dropPackId;
	//使用后提示
	private	string m_useTips;
	//系统广播
	private	int m_systemBroadcasts;
	//使用后跳转Ui对应序号
	private	int m_usejumpType;
	//获得途径序列号
	private	int m_accessQuence;
	//符文类型
	private	int m_rune_type;
	//专属类id
	private	int[] m_rune_specialHeroId;
	//符文星级
	private	int m_rune_quality;
	//属性基础库1
	private	int m_rune_baseAttri1;
	//属性基础库2
	private	int m_rune_baseAttri2;
	//属性基础库3
	private	int m_rune_baseAttri3;
	//附加属性库1
	private	int m_rune_addAttri1;
	//附加属性库2
	private	int m_rune_addAttri2;
	//附加属性库3
	private	int m_rune_addAttri3;
	//附加属性库4
	private	int m_rune_addAttri4;
	//强化消耗与返还库
	private	int m_rune_strengthenId;
	//熔炼点基础量
	private	int m_rune_smelt;
	//鉴定消耗资源1
	private	int m_rune_exposeCostType1;
	//鉴定消耗值1
	private	int m_rune_exposeCostValue1;
	//鉴定消耗资源2
	private	int m_rune_exposeCostType2;
	//鉴定消耗值2
	private	int m_rune_exposeCostValue2;
	//鉴定消耗资源3
	private	int m_rune_exposeCostType3;
	//鉴定消耗值3
	private	int m_rune_exposeCostValue3;
	//鉴定消耗资源4
	private	int m_rune_exposeCostType4;
	//鉴定消耗值4
	private	int m_rune_exposeCostValue4;
	//专属类描述
	private	string m_specialHeroDes;
	//提升经验值
	private	int m_improvexperience;
	//英雄经验
	private	int m_HeroExp;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_name = ReadToString(data);
		m_des = ReadToString(data);
		m_Icon = ReadToString(data);
		m_Icon_s = ReadToString(data);
		m_bagID = data.ReadInt32();
		m_className = ReadToString(data);
		m_stackNum = data.ReadInt32();
		m_ifSell = data.ReadInt32();
		m_sellPrice = data.ReadInt32();
		m_type = data.ReadInt32();
		m_quality = data.ReadInt32();
		m_ifUse = data.ReadInt32();
		m_dropPackId = parserXMLIntArray(ReadToString(data));
		m_useTips = ReadToString(data);
		m_systemBroadcasts = data.ReadInt32();
		m_usejumpType = data.ReadInt32();
		m_accessQuence = data.ReadInt32();
		m_rune_type = data.ReadInt32();
		m_rune_specialHeroId = parserXMLIntArray(ReadToString(data));
		m_rune_quality = data.ReadInt32();
		m_rune_baseAttri1 = data.ReadInt32();
		m_rune_baseAttri2 = data.ReadInt32();
		m_rune_baseAttri3 = data.ReadInt32();
		m_rune_addAttri1 = data.ReadInt32();
		m_rune_addAttri2 = data.ReadInt32();
		m_rune_addAttri3 = data.ReadInt32();
		m_rune_addAttri4 = data.ReadInt32();
		m_rune_strengthenId = data.ReadInt32();
		m_rune_smelt = data.ReadInt32();
		m_rune_exposeCostType1 = data.ReadInt32();
		m_rune_exposeCostValue1 = data.ReadInt32();
		m_rune_exposeCostType2 = data.ReadInt32();
		m_rune_exposeCostValue2 = data.ReadInt32();
		m_rune_exposeCostType3 = data.ReadInt32();
		m_rune_exposeCostValue3 = data.ReadInt32();
		m_rune_exposeCostType4 = data.ReadInt32();
		m_rune_exposeCostValue4 = data.ReadInt32();
		m_specialHeroDes = ReadToString(data);
		m_improvexperience = data.ReadInt32();
		m_HeroExp = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getName()
	{
		return this.m_name;
	}

	public	string	getDes()
	{
		return this.m_des;
	}

	public	string	getIcon()
	{
		return this.m_Icon;
	}

	public	string	getIcon_s()
	{
		return this.m_Icon_s;
	}

	public	int	getBagID()
	{
		return this.m_bagID;
	}

	public	string	getClassName()
	{
		return this.m_className;
	}

	public	int	getStackNum()
	{
		return this.m_stackNum;
	}

	public	int	getIfSell()
	{
		return this.m_ifSell;
	}

	public	int	getSellPrice()
	{
		return this.m_sellPrice;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getQuality()
	{
		return this.m_quality;
	}

	public	int	getIfUse()
	{
		return this.m_ifUse;
	}

	public	int[]	getDropPackId()
	{
		return this.m_dropPackId;
	}

	public	string	getUseTips()
	{
		return this.m_useTips;
	}

	public	int	getSystemBroadcasts()
	{
		return this.m_systemBroadcasts;
	}

	public	int	getUsejumpType()
	{
		return this.m_usejumpType;
	}

	public	int	getAccessQuence()
	{
		return this.m_accessQuence;
	}

	public	int	getRune_type()
	{
		return this.m_rune_type;
	}

	public	int[]	getRune_specialHeroId()
	{
		return this.m_rune_specialHeroId;
	}

	public	int	getRune_quality()
	{
		return this.m_rune_quality;
	}

	public	int	getRune_baseAttri1()
	{
		return this.m_rune_baseAttri1;
	}

	public	int	getRune_baseAttri2()
	{
		return this.m_rune_baseAttri2;
	}

	public	int	getRune_baseAttri3()
	{
		return this.m_rune_baseAttri3;
	}

	public	int	getRune_addAttri1()
	{
		return this.m_rune_addAttri1;
	}

	public	int	getRune_addAttri2()
	{
		return this.m_rune_addAttri2;
	}

	public	int	getRune_addAttri3()
	{
		return this.m_rune_addAttri3;
	}

	public	int	getRune_addAttri4()
	{
		return this.m_rune_addAttri4;
	}

	public	int	getRune_strengthenId()
	{
		return this.m_rune_strengthenId;
	}

	public	int	getRune_smelt()
	{
		return this.m_rune_smelt;
	}

	public	int	getRune_exposeCostType1()
	{
		return this.m_rune_exposeCostType1;
	}

	public	int	getRune_exposeCostValue1()
	{
		return this.m_rune_exposeCostValue1;
	}

	public	int	getRune_exposeCostType2()
	{
		return this.m_rune_exposeCostType2;
	}

	public	int	getRune_exposeCostValue2()
	{
		return this.m_rune_exposeCostValue2;
	}

	public	int	getRune_exposeCostType3()
	{
		return this.m_rune_exposeCostType3;
	}

	public	int	getRune_exposeCostValue3()
	{
		return this.m_rune_exposeCostValue3;
	}

	public	int	getRune_exposeCostType4()
	{
		return this.m_rune_exposeCostType4;
	}

	public	int	getRune_exposeCostValue4()
	{
		return this.m_rune_exposeCostValue4;
	}

	public	string	getSpecialHeroDes()
	{
		return this.m_specialHeroDes;
	}

	public	int	getImprovexperience()
	{
		return this.m_improvexperience;
	}

	public	int	getHeroExp()
	{
		return this.m_HeroExp;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}