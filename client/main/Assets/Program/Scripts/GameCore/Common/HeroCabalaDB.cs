/********************************************************************
	created:	2015/10/21
	created:	21:10:2015   20:39
	filename: 	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common\HeroCabalaDB.cs
	file path:	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common
	file base:	HeroCabalaDB
	file ext:	cs
	author:		Zhao Mingyang
	
	purpose:	秘术数据缓存。解析得到英雄身上所有秘术的等级和经验
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.LogSystem;

public class HeroCabalaDB
{
    private List<CabalaData> m_CabalaList = new List<CabalaData>();
    public HeroCabalaDB()
    {
        ClearUp();
    }

    public List<CabalaData> CabalaList
    {
        get { return m_CabalaList; }
    }
    public void ClearUp()
    {
        m_CabalaList.Clear();
    }

    public void ParserCabalaDB(string _CabalaDB, bool _isUpdate, int _heroID)
    {
        if (string.IsNullOrEmpty(_CabalaDB))
            return;

        if (_isUpdate == false)//非刷新数据 表示是新数据 需要分配内存空间存储数据。 [10/21/2015 Zmy]
        {
            InitCabalaTableID(_heroID);
        }
        string[] _dataList = _CabalaDB.Split(':');
        if (_dataList.Length != m_CabalaList.Count)
        {
            LogManager.LogError("!!!Error：Parse _CabalaDB.length != CabalaList.count !!!");
            return;
        }
        for (int i = 0; i < _dataList.Length && i < m_CabalaList.Count; ++i)
        {
            CabalaData _data = m_CabalaList[i];
            try
            {
                _data.IntensifyLev = int.Parse(_dataList[i].Split('|')[0]);
                _data.CurExp = int.Parse(_dataList[i].Split('|')[1]);
            }
            catch (System.Exception ex)
            {
                LogManager.LogError("!!!Error:ParserCabalaDB data :" + _dataList[i]);
            }
        }
    }

    public void InitCabalaTableID(int _heroID)
    {
        ClearUp();

        HeroTemplate _row = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(_heroID);
        int[] _IdList = _row.getMsid();
        for (int i = 0; i < _IdList.Length; ++i )
        {
            CabalaData _data = new CabalaData();
            _data.TableID = _IdList[i];

            m_CabalaList.Add(_data);
        }
    }

    public void CopyData(HeroCabalaDB _db)
    {
        if (_db.CabalaList.Count <= 0)
            return;

        m_CabalaList.Clear();

        for (int i = 0; i < _db.CabalaList.Count; ++i )
        {
            m_CabalaList.Add(_db.CabalaList[i]);
        }
    }
}

public class CabalaData
{
    private int m_TableID;              //秘术表ID
    private int m_IntensifyLev;         //强化等级
    private int m_CurExp;               //当前经验

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

    public int CurExp
    {
        get { return m_CurExp; }
        set { m_CurExp = value; }
    }
}