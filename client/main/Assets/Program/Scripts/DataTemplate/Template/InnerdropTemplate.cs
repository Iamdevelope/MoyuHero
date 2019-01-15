//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class InnerdropTemplate : IExcelBean
{
	#region attribute
	//掉落标识
	private	int m_id;
	//小包id
	private	int m_innerdropid;
	//小包类型
	private	int m_innerdroptype;
	//掉落次数
	private	int m_innerdroptime;
	//掉落权重
	private	int m_dropwight;
	//掉落物id
	private	int m_objectid;
	//掉落数量
	private	int m_dropnum;
	//掉落参数1
	private	int m_dropparameter1;
	//掉落参数2
	private	string m_dropparameter2;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_innerdropid = data.ReadInt32();
		m_innerdroptype = data.ReadInt32();
		m_innerdroptime = data.ReadInt32();
		m_dropwight = data.ReadInt32();
		m_objectid = data.ReadInt32();
		m_dropnum = data.ReadInt32();
		m_dropparameter1 = data.ReadInt32();
		m_dropparameter2 = ReadToString(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	int	getInnerdropid()
	{
		return this.m_innerdropid;
	}

	public	int	getInnerdroptype()
	{
		return this.m_innerdroptype;
	}

	public	int	getInnerdroptime()
	{
		return this.m_innerdroptime;
	}

	public	int	getDropwight()
	{
		return this.m_dropwight;
	}

	public	int	getObjectid()
	{
		return this.m_objectid;
	}

	public	int	getDropnum()
	{
		return this.m_dropnum;
	}

	public	int	getDropparameter1()
	{
		return this.m_dropparameter1;
	}

	public	string	getDropparameter2()
	{
		return this.m_dropparameter2;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}