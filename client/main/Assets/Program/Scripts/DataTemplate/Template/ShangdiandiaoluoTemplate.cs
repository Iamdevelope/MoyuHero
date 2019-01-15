//
// autor : OgreZCD
// date : 2015-11-03
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ShangdiandiaoluoTemplate : IExcelBean
{
	#region attribute
	//序列
	private	int m_id;
	//商店掉落id
	private	int m_DropId;
	//玩家最小等级
	private	int m_SmallLeve;
	//玩家最大等级
	private	int m_BigLeve;
	//道具id
	private	int[] m_ItemId;
	//权重
	private	int[] m_Weight;
	//消耗资源
	private	int[] m_consumerzy;
	//价格
	private	int[] m_Price;
	//数量
	private	int[] m_Number;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_DropId = data.ReadInt32();
		m_SmallLeve = data.ReadInt32();
		m_BigLeve = data.ReadInt32();
		m_ItemId = parserXMLIntArray(ReadToString(data));
		m_Weight = parserXMLIntArray(ReadToString(data));
		m_consumerzy = parserXMLIntArray(ReadToString(data));
		m_Price = parserXMLIntArray(ReadToString(data));
		m_Number = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getDropId()
	{
		return this.m_DropId;
	}

	public	int	getSmallLeve()
	{
		return this.m_SmallLeve;
	}

	public	int	getBigLeve()
	{
		return this.m_BigLeve;
	}

	public	int[]	getItemId()
	{
		return this.m_ItemId;
	}

	public	int[]	getWeight()
	{
		return this.m_Weight;
	}

	public	int[]	getConsumerzy()
	{
		return this.m_consumerzy;
	}

	public	int[]	getPrice()
	{
		return this.m_Price;
	}

	public	int[]	getNumber()
	{
		return this.m_Number;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}