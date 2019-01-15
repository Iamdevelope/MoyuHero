using UnityEngine;
using System.Collections;

public class ArtifactDB 
{
    public int      m_ArtifactTableID;                                                        //神器ID
    public int[]    m_IntoRecord = new int[GlobalMembers.MAX_ARTIFACT_HERO_COUNT];            //神器注入记录
    public ArtifactDB()
    {
        CleanUp();
    }

    public void CleanUp()
    {
        for (int i = 0; i < GlobalMembers.MAX_ARTIFACT_HERO_COUNT; i++)
        {
            m_IntoRecord[i] = 0;
        }
    }

	/// <summary>
	/// 获取当前神器的等级
	/// </summary>
	/// <returns></returns>
	public int GetLevel()
	{
		return m_ArtifactTableID % 10;
	}

	/// <summary>
	/// 获取当前神器总共铸魂了多少个英雄
	/// </summary>
	/// <returns></returns>
	public int GetAllRecord()
	{
		int count = 0;
		for (int i = 0; i < m_IntoRecord.Length; ++i)
		{
			count += m_IntoRecord[i];
		}

		return count;
	}

    public void Copy(GNET.Artifact _db)
    {
        m_ArtifactTableID = _db.artifactid;
        m_IntoRecord[0] = _db.heronum1;
        m_IntoRecord[1] = _db.heronum2;
        m_IntoRecord[2] = _db.heronum3;
        m_IntoRecord[3] = _db.heronum4;
        m_IntoRecord[4] = _db.heronum5;
    }
	
}