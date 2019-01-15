//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ActivitymissionTemplate : IExcelBean
{
	#region attribute
	//ID
	private	int m_id;
	//所需通关关卡
	private	int m_stagecondition;
	//所需玩家等级
	private	int m_levelcondition;
	//筛选类型
	private	int m_selecttype;
	//任务类型
	private	int m_type;
	//任务参数
	private	string m_parameter;
	//所需次数
	private	int m_times;
	//奖励活跃度
	private	int m_activitydegree;
	//是否付费
	private	int m_paytype;
	//任务描述
	private	string m_des;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_stagecondition = data.ReadInt32();
		m_levelcondition = data.ReadInt32();
		m_selecttype = data.ReadInt32();
		m_type = data.ReadInt32();
		m_parameter = ReadToString(data);
		m_times = data.ReadInt32();
		m_activitydegree = data.ReadInt32();
		m_paytype = data.ReadInt32();
		m_des = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getStagecondition()
	{
		return this.m_stagecondition;
	}

	public	int	getLevelcondition()
	{
		return this.m_levelcondition;
	}

	public	int	getSelecttype()
	{
		return this.m_selecttype;
	}

	public	int	getType()
	{
		return this.m_type;
	}

	public	string	getParameter()
	{
		return this.m_parameter;
	}

	public	int	getTimes()
	{
		return this.m_times;
	}

	public	int	getActivitydegree()
	{
		return this.m_activitydegree;
	}

	public	int	getPaytype()
	{
		return this.m_paytype;
	}

	public	string	getDes()
	{
		return this.m_des;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}