//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class HerorecruitTemplate : IExcelBean
{
	#region attribute
	//英雄ID
	private	int m_id;
	//招募初始权重1
	private	int m_initialweight1;
	//招募增量组1
	private	int m_pulsrange1;
	//招募增量权重1
	private	int m_weightpuls1;
	//招募初始权重2
	private	int m_initialweight2;
	//招募增量组2
	private	int m_pulsrange2;
	//招募增量权重2
	private	int m_weightpuls2;
	//招募初始权重3
	private	int m_initialweight3;
	//招募增量组3
	private	int m_pulsrange3;
	//招募增量权重3
	private	int m_weightpuls3;
	//招募初始权重4
	private	int m_initialweight4;
	//招募增量组4
	private	int m_pulsrange4;
	//招募增量权重4
	private	int m_weightpuls4;
	//招募初始等级
	private	int m_herolevel;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_initialweight1 = data.ReadInt32();
		m_pulsrange1 = data.ReadInt32();
		m_weightpuls1 = data.ReadInt32();
		m_initialweight2 = data.ReadInt32();
		m_pulsrange2 = data.ReadInt32();
		m_weightpuls2 = data.ReadInt32();
		m_initialweight3 = data.ReadInt32();
		m_pulsrange3 = data.ReadInt32();
		m_weightpuls3 = data.ReadInt32();
		m_initialweight4 = data.ReadInt32();
		m_pulsrange4 = data.ReadInt32();
		m_weightpuls4 = data.ReadInt32();
		m_herolevel = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getInitialweight1()
	{
		return this.m_initialweight1;
	}

	public	int	getPulsrange1()
	{
		return this.m_pulsrange1;
	}

	public	int	getWeightpuls1()
	{
		return this.m_weightpuls1;
	}

	public	int	getInitialweight2()
	{
		return this.m_initialweight2;
	}

	public	int	getPulsrange2()
	{
		return this.m_pulsrange2;
	}

	public	int	getWeightpuls2()
	{
		return this.m_weightpuls2;
	}

	public	int	getInitialweight3()
	{
		return this.m_initialweight3;
	}

	public	int	getPulsrange3()
	{
		return this.m_pulsrange3;
	}

	public	int	getWeightpuls3()
	{
		return this.m_weightpuls3;
	}

	public	int	getInitialweight4()
	{
		return this.m_initialweight4;
	}

	public	int	getPulsrange4()
	{
		return this.m_pulsrange4;
	}

	public	int	getWeightpuls4()
	{
		return this.m_weightpuls4;
	}

	public	int	getHerolevel()
	{
		return this.m_herolevel;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}