//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ExplorequestTemplate : IExcelBean
{
	#region attribute
	//任务ID
	private	int m_id;
	//章节ID
	private	int m_chapterID;
	//探险任务名称
	private	string m_name;
	//探险任务描述
	private	string m_des;
	//所需英雄描述
	private	string m_needHeroDes;
	//奖励描述
	private	string m_bonusDes;
	//探险任务品质
	private	int m_quality;
	//探险任务类型
	private	int m_type;
	//消耗时间
	private	int m_time;
	//消耗行动力
	private	int m_cost;
	//探险任务奖励
	private	int m_bonus;
	//权重
	private	int m_weight;
	//坐标
	private	int[] m_coordinate;
	//任务需求类型
	private	int m_needHeroType;
	//等级需求
	private	int m_needHeroLevel;
	//所需英雄阵营
	private	int[] m_needHeroCamp;
	//所需英雄星级
	private	int m_needHeroStar;
	//所需英雄数量
	private	int m_needNum;
	//需要英雄id1
	private	int[] m_needHeroID1;
	//需要英雄id2
	private	int[] m_needHeroID2;
	//需要英雄id3
	private	int[] m_needHeroID3;
	//需要英雄id4
	private	int[] m_needHeroID4;
	//需要英雄id5
	private	int[] m_needHeroID5;
	//召回队伍花费
	private	int m_forceReturnCost;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_chapterID = data.ReadInt32();
		m_name = ReadToString(data);
		m_des = ReadToString(data);
		m_needHeroDes = ReadToString(data);
		m_bonusDes = ReadToString(data);
		m_quality = data.ReadInt32();
		m_type = data.ReadInt32();
		m_time = data.ReadInt32();
		m_cost = data.ReadInt32();
		m_bonus = data.ReadInt32();
		m_weight = data.ReadInt32();
		m_coordinate = parserXMLIntArray(ReadToString(data));
		m_needHeroType = data.ReadInt32();
		m_needHeroLevel = data.ReadInt32();
		m_needHeroCamp = parserXMLIntArray(ReadToString(data));
		m_needHeroStar = data.ReadInt32();
		m_needNum = data.ReadInt32();
		m_needHeroID1 = parserXMLIntArray(ReadToString(data));
		m_needHeroID2 = parserXMLIntArray(ReadToString(data));
		m_needHeroID3 = parserXMLIntArray(ReadToString(data));
		m_needHeroID4 = parserXMLIntArray(ReadToString(data));
		m_needHeroID5 = parserXMLIntArray(ReadToString(data));
		m_forceReturnCost = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getChapterID()
	{
		return this.m_chapterID;
	}

	public	string	getName()
	{
		return this.m_name;
	}

	public	string	getDes()
	{
		return this.m_des;
	}

	public	string	getNeedHeroDes()
	{
		return this.m_needHeroDes;
	}

	public	string	getBonusDes()
	{
		return this.m_bonusDes;
	}

	public	int	getQuality()
	{
		return this.m_quality;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getTime()
	{
		return this.m_time;
	}

	public	int	getCost()
	{
		return this.m_cost;
	}

	public	int	getBonus()
	{
		return this.m_bonus;
	}

	public	int	getWeight()
	{
		return this.m_weight;
	}

	public	int[]	getCoordinate()
	{
		return this.m_coordinate;
	}

	public	int	getNeedHeroType()
	{
		return this.m_needHeroType;
	}

	public	int	getNeedHeroLevel()
	{
		return this.m_needHeroLevel;
	}

	public	int[]	getNeedHeroCamp()
	{
		return this.m_needHeroCamp;
	}

	public	int	getNeedHeroStar()
	{
		return this.m_needHeroStar;
	}

	public	int	getNeedNum()
	{
		return this.m_needNum;
	}

	public	int[]	getNeedHeroID1()
	{
		return this.m_needHeroID1;
	}

	public	int[]	getNeedHeroID2()
	{
		return this.m_needHeroID2;
	}

	public	int[]	getNeedHeroID3()
	{
		return this.m_needHeroID3;
	}

	public	int[]	getNeedHeroID4()
	{
		return this.m_needHeroID4;
	}

	public	int[]	getNeedHeroID5()
	{
		return this.m_needHeroID5;
	}

	public	int	getForceReturnCost()
	{
		return this.m_forceReturnCost;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}