//
// autor : OgreZCD
// date : 2015-10-26
//

using UnityEngine;
using System.Collections;
using System.IO;


public class HeroupgradexpTemplate : IExcelBean
{
	#region attribute
	//序号
	private	int m_id;
	//资质
	private	int m_Born;
	//等级
	private	int m_Level;
	//经验
	private	int m_Exp;
	//消耗金币
	private	int m_consumermoney;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_Born = data.ReadInt32();
		m_Level = data.ReadInt32();
		m_Exp = data.ReadInt32();
		m_consumermoney = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getBorn()
	{
		return this.m_Born;
	}

	public	int	getLevel()
	{
		return this.m_Level;
	}

	public	int	getExp()
	{
		return this.m_Exp;
	}

	public	int	getConsumermoney()
	{
		return this.m_consumermoney;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}