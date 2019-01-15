//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class HerocloneTemplate : IExcelBean
{
	#region attribute
	//ID
	private	int m_id;
	//排序ID
	private	int m_sortID;
	//克隆消耗资源ID
	private	int m_cloneCostId;
	//克隆消耗资源数量
	private	int m_cloneCostValue;
	//开启所需资源
	private	int m_openCondition;
	//开启描述
	private	string m_openConditionDes;
	//是否初始拥有
	private	int m_is_had;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_sortID = data.ReadInt32();
		m_cloneCostId = data.ReadInt32();
		m_cloneCostValue = data.ReadInt32();
		m_openCondition = data.ReadInt32();
		m_openConditionDes = ReadToString(data);
		m_is_had = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getSortID()
	{
		return this.m_sortID;
	}

	public	int	getCloneCostId()
	{
		return this.m_cloneCostId;
	}

	public	int	getCloneCostValue()
	{
		return this.m_cloneCostValue;
	}

	public	int	getOpenCondition()
	{
		return this.m_openCondition;
	}

	public	string	getOpenConditionDes()
	{
		return this.m_openConditionDes;
	}

	public	int	getIs_had()
	{
		return this.m_is_had;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}