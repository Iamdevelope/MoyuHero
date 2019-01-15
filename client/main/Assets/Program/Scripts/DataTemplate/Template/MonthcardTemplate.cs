//
// autor : OgreZCD
// date : 2015-08-18
//

using UnityEngine;
using System.Collections;
using System.IO;


public class MonthcardTemplate : IExcelBean
{
	#region attribute
	//月卡ID
	private	int m_id;
	//月卡名称
	private	string m_name;
	//月卡描述
	private	string m_des;
	//每日金币
	private	int m_dailygold;
	//每日魔钻
	private	int m_dailydiamond;
	//经验收益
	private	float m_expBonus;
	//每日首刷必出5星符文次数
	private	int m_refresh5Star;
	//战斗加速
	private	int m_FightSpeed;
	//维持天数
	private	int m_duration;
	//首次购买额外VIP经验
	private	int m_vipexperience;
	//月卡图片
	private	string m_icon;
	//图片底图
	private	string m_baseicon;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_name = ReadToString(data);
		m_des = ReadToString(data);
		m_dailygold = data.ReadInt32();
		m_dailydiamond = data.ReadInt32();
		m_expBonus = ReadToSingle(data);
		m_refresh5Star = data.ReadInt32();
		m_FightSpeed = data.ReadInt32();
		m_duration = data.ReadInt32();
		m_vipexperience = data.ReadInt32();
		m_icon = ReadToString(data);
		m_baseicon = ReadToString(data);
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

	public	int	getDailygold()
	{
		return this.m_dailygold;
	}

	public	int	getDailydiamond()
	{
		return this.m_dailydiamond;
	}

	public	float	getExpBonus()
	{
		return this.m_expBonus;
	}

	public	int	getRefresh5Star()
	{
		return this.m_refresh5Star;
	}

	public	int	getFightSpeed()
	{
		return this.m_FightSpeed;
	}

	public	int	getDuration()
	{
		return this.m_duration;
	}

	public	int	getVipexperience()
	{
		return this.m_vipexperience;
	}

	public	string	getIcon()
	{
		return this.m_icon;
	}

	public	string	getBaseicon()
	{
		return this.m_baseicon;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}