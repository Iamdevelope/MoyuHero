//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class InterfacemodelTemplate : IExcelBean
{
	#region attribute
	//英雄ID
	private	int m_id;
	//位置1缩放比例
	private	float m_position1;
	//位置2缩放比例
	private	float m_position2;
	//位置3缩放比例
	private	float m_position3;
	//位置4缩放比例
	private	float m_position4;
	//位置5缩放比例
	private	float m_position5;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_position1 = ReadToSingle(data);
		m_position2 = ReadToSingle(data);
		m_position3 = ReadToSingle(data);
		m_position4 = ReadToSingle(data);
		m_position5 = ReadToSingle(data);
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	float	getPosition1()
	{
		return this.m_position1;
	}

	public	float	getPosition2()
	{
		return this.m_position2;
	}

	public	float	getPosition3()
	{
		return this.m_position3;
	}

	public	float	getPosition4()
	{
		return this.m_position4;
	}

	public	float	getPosition5()
	{
		return this.m_position5;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}