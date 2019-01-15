/********************************************************************
	created:	2015/10/21
	created:	21:10:2015   20:02
	filename: 	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common\HeroTrainDB.cs
	file path:	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common
	file base:	HeroTrainDB
	file ext:	cs
	author:		Zhao Mingyang
	
	purpose:	培养数据缓存。固定四种元素类型，每种类型有不同等级
*********************************************************************/

using UnityEngine;
using System.Collections;
using DreamFaction.LogSystem;

public class HeroTrainDB
{
    public enum ENUM_TRAIN_TYPE
    {
        element_1 = 0,  //火
        element_2 = 1,  //土
        element_3 = 2,  //水
        element_4 = 3   //风
    }

    private int[] m_TrainLevel = new int[GlobalMembers.MAX_TRAIN_SLOT_COUNT];

    public HeroTrainDB()
    {
        ClearUp();
    }

    public int[] TrainLevel
    {
        get { return m_TrainLevel; }
    }

    public int GetTrainLevForType(ENUM_TRAIN_TYPE _type)
    {
        return m_TrainLevel[(int)_type];
    }
    public void ClearUp()
    {
        for (int i = 0; i < m_TrainLevel.Length; ++i )
        {
            m_TrainLevel[i] = 0;
        }
    }

    public void ParserTrainDB(string _TrainDB)
    {
        if (string.IsNullOrEmpty(_TrainDB))
            return;

        string[] _dataList = _TrainDB.Split(':');
        if (_dataList.Length != GlobalMembers.MAX_TRAIN_SLOT_COUNT)
        {
            LogManager.LogError("!!!Error:ParserTrainDB split length  != MAX_TRAIN_SLOT_COUNT! string.length :" + _dataList.Length);
            return;
        }
        for (int i = 0; i < _dataList.Length; ++i)
        {
            try
            {
                m_TrainLevel[i] = int.Parse(_dataList[i]);
            }
            catch (System.Exception ex)
            {
                LogManager.LogError("!!!Error:ParserTrainDB data :" + _dataList[i]);
            }
        }
    }

    public void CopyData(HeroTrainDB _db)
    {
        for (int i = 0; i < _db.TrainLevel.Length; ++i )
        {
            m_TrainLevel[i] = _db.TrainLevel[i];
        }
    }
	
}
