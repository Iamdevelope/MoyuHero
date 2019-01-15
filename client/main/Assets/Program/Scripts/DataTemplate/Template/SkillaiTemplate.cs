//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class SkillaiTemplate : IExcelBean
{
	#region attribute
	//被调用的ID
	private	int m_id;
	//条件1
	private	int m_Condition1;
	//条件参数1
	private	int[] m_Param1;
	//条件2
	private	int m_Condition2;
	//条件参数2
	private	int[] m_Param2;
	//条件3
	private	int m_Condition3;
	//条件参数3
	private	int[] m_Param3;
	//条件4
	private	int m_Condition4;
	//条件参数4
	private	int[] m_Param4;
	//条件5
	private	int m_Condition5;
	//条件参数5
	private	int[] m_Param5;
	//目标参数
	private	int m_target;
	#endregion


	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_Condition1 = data.ReadInt32();
		m_Param1 = parserXMLIntArray(ReadToString(data));
		m_Condition2 = data.ReadInt32();
		m_Param2 = parserXMLIntArray(ReadToString(data));
		m_Condition3 = data.ReadInt32();
		m_Param3 = parserXMLIntArray(ReadToString(data));
		m_Condition4 = data.ReadInt32();
		m_Param4 = parserXMLIntArray(ReadToString(data));
		m_Condition5 = data.ReadInt32();
		m_Param5 = parserXMLIntArray(ReadToString(data));
		m_target = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getCondition1()
	{
		return this.m_Condition1;
	}

	public	int[]	getParam1()
	{
		return this.m_Param1;
	}

	public	int	getCondition2()
	{
		return this.m_Condition2;
	}

	public	int[]	getParam2()
	{
		return this.m_Param2;
	}

	public	int	getCondition3()
	{
		return this.m_Condition3;
	}

	public	int[]	getParam3()
	{
		return this.m_Param3;
	}

	public	int	getCondition4()
	{
		return this.m_Condition4;
	}

	public	int[]	getParam4()
	{
		return this.m_Param4;
	}

	public	int	getCondition5()
	{
		return this.m_Condition5;
	}

	public	int[]	getParam5()
	{
		return this.m_Param5;
	}

	public	int	getTarget()
	{
		return this.m_target;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}