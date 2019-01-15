//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class RuintreasureTemplate : IExcelBean
{
	#region attribute
	//ID
	private	int m_id;
	//奖励类型
	private	int m_type;
	//奖励参数1
	private	int m_parameter1;
	//奖励参数2
	private	string m_parameter2;
	//刷新权重1
	private	int m_weight1;
	//月卡首刷1
	private	int m_monthcard_refresh1;
	//刷新权重2
	private	int m_weight2;
	//月卡首刷2
	private	int m_monthcard_refresh2;
	//刷新权重3
	private	int m_weight3;
	//月卡首刷3
	private	int m_monthcard_refresh3;
	//刷新权重4
	private	int m_weight4;
	//月卡首刷4
	private	int m_monthcard_refresh4;
	//抽取权重
	private	int m_get_weight;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_type = data.ReadInt32();
		m_parameter1 = data.ReadInt32();
		m_parameter2 = ReadToString(data);
		m_weight1 = data.ReadInt32();
		m_monthcard_refresh1 = data.ReadInt32();
		m_weight2 = data.ReadInt32();
		m_monthcard_refresh2 = data.ReadInt32();
		m_weight3 = data.ReadInt32();
		m_monthcard_refresh3 = data.ReadInt32();
		m_weight4 = data.ReadInt32();
		m_monthcard_refresh4 = data.ReadInt32();
		m_get_weight = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	int	getParameter1()
	{
		return this.m_parameter1;
	}

	public	string	getParameter2()
	{
		return this.m_parameter2;
	}

	public	int	getWeight1()
	{
		return this.m_weight1;
	}

	public	int	getMonthcard_refresh1()
	{
		return this.m_monthcard_refresh1;
	}

	public	int	getWeight2()
	{
		return this.m_weight2;
	}

	public	int	getMonthcard_refresh2()
	{
		return this.m_monthcard_refresh2;
	}

	public	int	getWeight3()
	{
		return this.m_weight3;
	}

	public	int	getMonthcard_refresh3()
	{
		return this.m_monthcard_refresh3;
	}

	public	int	getWeight4()
	{
		return this.m_weight4;
	}

	public	int	getMonthcard_refresh4()
	{
		return this.m_monthcard_refresh4;
	}

	public	int	getGet_weight()
	{
		return this.m_get_weight;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}