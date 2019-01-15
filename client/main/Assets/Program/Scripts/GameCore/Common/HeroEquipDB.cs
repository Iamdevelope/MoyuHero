/********************************************************************
	created:	2015/10/21
	created:	21:10:2015   18:07
	filename: 	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common\HeroEquipDB.cs
	file path:	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common
	file base:	HeroEquipDB
	file ext:	cs
	author:		Zhao Mingyang
	
	purpose:	装备数据缓存。解析得到装备ID，当前强化等级
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.LogSystem;

public class HeroEquipDB
{
    List<EquipData> m_EquipList = new List<EquipData>();

    public HeroEquipDB()
    {
        ClearUp();
    }

    public List<EquipData> EquipList
    {
        get { return m_EquipList; }
    }
    public void ClearUp()
    {
        m_EquipList.Clear();
    }

    public void ParserEquipDB(string _EquipDB)
    {
        if (string.IsNullOrEmpty(_EquipDB))
            return;
        //每次写入前。清空一次源数据。 [10/21/2015 Zmy]
        ClearUp();

        string[] _dataList = _EquipDB.Split(':');
        for (int i = 0; i < _dataList.Length; ++i )
        {
            EquipData _data = new EquipData();
            try
            {
                _data.TableID = int.Parse(_dataList[i].Split('|')[0]);
                _data.IntensifyLev = int.Parse(_dataList[i].Split('|')[1]);
                m_EquipList.Add(_data);
            }
            catch (System.Exception ex)
            {
                LogManager.LogError("!!!Error:OnParserEquipDB data :" + _dataList[i]);
            }
        }
    }

    public void CopyData(HeroEquipDB _db)
    {
        if (_db.EquipList.Count <= 0)
            return;

        m_EquipList.Clear();

        for (int i = 0; i < _db.EquipList.Count; ++i )
        {
            m_EquipList.Add(_db.EquipList[i]);
        }
    }
}

public class EquipData
{
    private int m_TableID;              //装备表ID
    private int m_IntensifyLev;         //强化等级

    public int TableID
    {
        get { return m_TableID; }
        set { m_TableID = value; }
    }

    public int IntensifyLev
    {
        get { return m_IntensifyLev; }
        set { m_IntensifyLev = value; }
    }
}