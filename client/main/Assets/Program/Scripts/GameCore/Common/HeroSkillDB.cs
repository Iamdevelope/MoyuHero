/********************************************************************
	created:	2015/10/22
	created:	22:10:2015   10:34
	filename: 	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common\HeroSkillDB.cs
	file path:	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common
	file base:	HeroSkillDB
	file ext:	cs
	author:		Zhao Mingyang
	
	purpose:	英雄技能数据缓存，存储解析后的ID
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.LogSystem;

public class HeroSkillDB 
{
    private List<int> m_SkillList = new List<int>();
    public HeroSkillDB()
    {
        ClearUp();
    }

    public List<int> SkillList
    {
        get { return m_SkillList; }
    }

    public void ClearUp()
    {
        m_SkillList.Clear();
    }

    public void ParserSkillDB(string _SkillDB)
    {
        if (string.IsNullOrEmpty(_SkillDB))
            return;
        //每次写入前。清空一次源数据。 [10/21/2015 Zmy]
        ClearUp();

        string[] _dataList = _SkillDB.Split(':');
        for (int i = 0; i < _dataList.Length; ++i)
        {
            try
            {
                m_SkillList.Add(int.Parse(_dataList[i]));
            }
            catch (System.Exception ex)
            {
                LogManager.LogError("!!!Error:ParserSkillDB data :" + _dataList[i]);
            }
        }
    }

    public void CopyData(HeroSkillDB _db)
    {
        if (_db.SkillList.Count <= 0)
            return;

        m_SkillList.Clear();

        for (int i = 0; i < _db.SkillList.Count; ++i )
        {
            m_SkillList.Add(_db.SkillList[i]);
        }
        
    }
}
