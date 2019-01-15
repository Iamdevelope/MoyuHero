/********************************************************************
    created:	2015/04/07
    created:	7:4:2015   17:08
    filename: 	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common\BattleStage.cs
    file path:	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common
    file base:	BattleStage
    file ext:	cs
    author:		Zhao Mingyang
	
    purpose:	关卡数据
*********************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;

using System;

/// <summary>
/// 单独一个小关卡的数据信息
/// </summary>
public class StageData
{
    public int m_StageID;          //关卡ID
    public int m_StageStar;        //关卡星级
    public int m_FightSum;         //已挑战次数
    public int m_BuyBattleNum;     //已购买挑战次数

    //扫荡
    private int m_rapidClearNums; // 今日扫荡剩余次数
    private int m_rapidClearBuyTimes; // 今日扫荡剩余购买次数
    private int m_buyBattleHaveNum; // 购买关卡剩余次数
    public StageData()
    {
        m_StageID = -1;
        m_StageStar = -1;
        m_FightSum = -1;
        m_BuyBattleNum = -1;
    }
}

/// <summary>
/// 章节战斗大关卡数据
/// </summary>
public class BattleStage
{
    public int m_BattlePieceNum; //章节
    public List<StageData>  m_BattleStage = new List<StageData>();
    public int m_StarSum;        //总星数
    public int m_bRewardGot;    //个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取

    public BattleStage()
    {
        m_BattlePieceNum = -1;
        m_BattleStage.Clear();
        m_StarSum = -1;
        m_bRewardGot = 0;
    }
    public StageData GetStageData(int _id)
    {
        for (int i = 0; i < m_BattleStage.Count; ++i)
        {
            if (m_BattleStage[i].m_StageID == _id)
            {
                return m_BattleStage[i];
            }
        }

        return null;
    }

    public bool IsAlreadyPassStage(int _ID, out int _starNum)
    {
        // change by zcd
        // return表示章节是否开启
        // startnum 不在列表中的也就是未开启的为-1,否则赋值为当前星级
         _starNum = -1;
        for (int i = 0; i < m_BattleStage.Count; ++i)
        {
            if (m_BattleStage[i].m_StageID == _ID)
            {
                //星级大于0以上的代表已通过 [4/7/2015 Zmy]
                _starNum = m_BattleStage[i].m_StageStar;
                if (m_BattleStage[i].m_StageStar > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 更新关卡数据
    /// </summary>
    /// <param name="_data"></param>
    /// <returns></returns>
    public bool UpdateBattleStageData(StageBattle _data)
    {
        //m_StarSum += _Star;
        StageData pData = GetStageData(_data.id);
        if (pData == null)
        {
            StageData pNew = new StageData();
            pNew.m_StageID = _data.id;
            pNew.m_StageStar = _data.maxstar;
            pNew.m_FightSum = _data.fightnum;
            pNew.m_BuyBattleNum = _data.buybattlenum;
            m_BattleStage.Add(pNew);
            return true;
        }
        else
        {
            pData.m_StageStar = _data.maxstar;
            pData.m_FightSum = _data.fightnum;
            pData.m_BuyBattleNum = _data.buybattlenum;
        }
        return false;
    }
}

public class SpecialStage
{
    public int m_StageID;       //关卡ID 或者 商店ID
    public int m_Time;          //倒计时时间;
    public int m_BattlePieceNum;//所属章节ID
    public SpecialStage()
    {
        ClearUp();
    }

    public void ClearUp()
    {
        m_StageID = 0;
        m_Time = 0;
        m_BattlePieceNum = 0;
    }

    public void CopyData(int nID, int nTime, int nNum)
    {
        this.m_StageID = nID;
        this.m_Time = nTime;
        this.m_BattlePieceNum = nNum;
    }
}
/// <summary>
/// 关卡总数据
/// </summary>
public class BattleStageMgr
{
    public Dictionary<int, BattleStage> m_BattleStageList = new Dictionary<int, BattleStage>();
    public SpecialStage m_SpecialStage = new SpecialStage();
    public bool m_IsOpenSpecialStage; //是否开启了特殊关卡或者神秘商店 （此关卡或商店是全局唯一的）
    public List<Smshopdata> m_MysteriousShopItemCollection; //神秘商店的物品集合
    // 新开启的关卡列表
    private Dictionary<int, bool> mViewList = new Dictionary<int, bool>();
    public BattleStageMgr()
    {
        m_BattleStageList.Clear();
        m_IsOpenSpecialStage = false;
        ConfigsManager.Inst.getNewStages(ref mViewList);
    }

    public void LoadMysteriousShop(Hashtable dataTable)
    {
        if(dataTable == null || dataTable.Count <= 0)
            return;

        if (m_MysteriousShopItemCollection == null)
            m_MysteriousShopItemCollection = new List<Smshopdata>();
        else
            m_MysteriousShopItemCollection.Clear();

        foreach (DictionaryEntry temp in dataTable)
        {
            m_MysteriousShopItemCollection.Add((Smshopdata)temp.Value);
        }
    }
    public void CopyBattleStageData(StageInfo _info)
    {
        LogManager.LogToFile("start copy chapter ...");
        BattleStage pNewData = new BattleStage();
        pNewData.m_BattlePieceNum   = _info.id;
        pNewData.m_StarSum          = _info.starsum;
        pNewData.m_bRewardGot       = _info.rewardgot;

        foreach (StageBattle item in _info.stagebattles)
        {
            StageData pData = new StageData();
            pData.m_StageID = item.id;
            pData.m_StageStar = item.maxstar;
            pData.m_FightSum = item.fightnum;
            pData.m_BuyBattleNum = item.buybattlenum;
            //if (pData.m_FightSum == 0)
            //{
            //    // 是否已经查看
            //    if (mViewList.ContainsKey(pData.m_StageID))
            //    {
            //        // 插入未开启列表
            //        mViewList.Add(pData.m_StageID, true);
            //    }
            //}

            pNewData.m_BattleStage.Add(pData);
        }
        if (_info.id!=1001)
        {
            ObjectSelf.GetInstance().SetCurChapterID(_info.id);
        }
        m_BattleStageList.Add(_info.id, pNewData);
    }
    
    /// <summary>
    /// 返回指定章节内的某一关卡是否已经通过 。并输出该关卡通过后的星级数据。未通过返回false 星级-1;
    /// </summary>
    /// <param name="_pieceNum">章节</param>
    /// <param name="stagetID">关卡ID</param>
    /// <param name="_StageStar">输出星级数</param>
    /// <returns></returns>
    public bool IsCopyScenePass(int _pieceNum, int stagetID, out int _StageStar)
    {
        _StageStar = -1;
        if (m_BattleStageList.ContainsKey(_pieceNum) == false)
        {
            _StageStar = -1;
            return false;
        }

        BattleStage pData = m_BattleStageList[_pieceNum];

        return pData.IsAlreadyPassStage(stagetID,out _StageStar);
    }

    public StageData GetStageDataByStageId(int stageId)
    {
        foreach (var item in m_BattleStageList)
        {
            if (item.Value.m_BattleStage == null || item.Value.m_BattleStage.Count <= 0)
            {
                continue;
            }

            for (int i = 0; i < item.Value.m_BattleStage.Count; i++ )
            {
                if (item.Value.m_BattleStage[i].m_StageID == stageId)
                {
                    return item.Value.m_BattleStage[i];
                }
            }
        }

        return null;
    }

    public bool IsCopyScenePass(int stageId, out int stageStar)
    {
        stageStar = -1;

        ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(stageId);

        if (ct == null)
        {
            return false;
        }

        int chapterId = ct.getId();

        return IsCopyScenePass(chapterId, stageId, out stageStar);
    }

    /// <summary>
    /// 判断指定关卡是否开启;
    /// </summary>
    /// <param name="stageId"></param>
    /// <returns></returns>
    public bool IsStageOpen(int stageId)
    {
        ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(stageId);

        if (ct == null)
        {
            return false;
        }

        int chapterId = ct.getId();

        BattleStage bs = GetBattleStageByChapterId(chapterId);

        if (bs != null)
        {
            StageData sd = bs.GetStageData(stageId);
            if (sd != null)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 刷新小关卡数据
    /// </summary>
    /// <param name="_pieceNum">章节id</param>
    /// <param name="pData"></param>
    public void UpdateSmallBattleStage(int _pieceNum,StageBattle pData)
    {
        if (m_BattleStageList.ContainsKey(_pieceNum) == false)
        {
            //开启新章节的关卡数据 [4/7/2015 Zmy]
            BattleStage pNewData = new BattleStage();
            pNewData.m_BattlePieceNum = _pieceNum;
            pNewData.m_StarSum = 0;
            pNewData.m_bRewardGot = 0;

            StageData pNew = new StageData();
            pNew.m_StageID = pData.id;
            pNew.m_StageStar = pData.maxstar;
            pNew.m_FightSum = pData.fightnum;
            pNew.m_BuyBattleNum = pData.buybattlenum;
            pNewData.m_BattleStage.Add(pNew);
            m_BattleStageList.Add(_pieceNum, pNewData);

            
            if (pNew.m_FightSum == 0)
            {
                mViewList.Add(pNew.m_StageID, true);
                // 更新到数据
                ConfigsManager.Inst.updateNewStages(mViewList);
            }
        }
        else
        {
            BattleStage BattleData = m_BattleStageList[_pieceNum];
            if (BattleData.UpdateBattleStageData(pData))
            {
                if (mViewList.ContainsKey(pData.id) == false)
                    mViewList.Add(pData.id, true);
                // 更新到数据
                ConfigsManager.Inst.updateNewStages(mViewList);
            }
        }
    }

    public void UpdateBigBattleStage(int _piece,int _starSum, byte _reward)
    {
        if (m_BattleStageList.ContainsKey(_piece) == false)
        {
            //开启新章节的关卡数据 [4/7/2015 Zmy]
            BattleStage pNewData = new BattleStage();
            pNewData.m_BattlePieceNum = _piece;
            pNewData.m_StarSum = _starSum;
            pNewData.m_bRewardGot = _reward;
            if (_piece!=1001)
            {
                ObjectSelf.GetInstance().SetCurChapterID(_piece);
            }
            m_BattleStageList.Add(_piece, pNewData);
            ObjectSelf.GetInstance().SetIsFight(false);
            ObjectSelf.GetInstance().SetIsNewMap(true);
        }
        else
        {
            BattleStage BattleData = m_BattleStageList[_piece];
            BattleData.m_StarSum = _starSum;
            BattleData.m_bRewardGot = _reward;
        }
    }
    /// <summary>
    /// 请求进入战斗后，立即检查一下特殊关卡的数据。匹配后客户端清空特殊关卡的数据
    /// </summary>
    /// <param name="nID"></param>
    public void CheckSpecialStageData(int nID)
    {
        if ( m_IsOpenSpecialStage && nID == m_SpecialStage.m_StageID)
        {
            m_SpecialStage.ClearUp();
            m_IsOpenSpecialStage = false;
        }
    }

    public SpecialStage GetSpecialStageData()
    {
        return m_SpecialStage;
    }

    /// <summary>
    /// 关卡是否是新开启
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool isStageNew(int id)
    {
        bool ret = false;
        if (mViewList.ContainsKey(id))
        {
            mViewList.Remove(id);
            ConfigsManager.Inst.updateNewStages(mViewList);
            
            ret = true;
        }
        return ret;
    }

    public List<int> GetNewStageList()
    {
        if (mViewList != null && mViewList.Count > 0)
        {
            List<int> result = new List<int>();

            foreach (var item in mViewList)
            {
                if (item.Value && !result.Contains(item.Key))
                {
                    result.Add(item.Key);
                }
            }

            return result;
        }

        return null;
    }

    [Obsolete("Please use ObjectSelf.GetInstance().BattleStageData.GetRewardGot(ChapterId, boxIdx) instead")]
    public int GetRewardGot(int chapterId)
    {
        if (m_BattleStageList != null && m_BattleStageList.Count > 0 && m_BattleStageList.ContainsKey(chapterId))
        {
            int val = m_BattleStageList[chapterId].m_bRewardGot;

            return val;
        }

        return 0;
    }

    public bool GetRewardGot(int chapterId, int boxIdx)
    {
        if (m_BattleStageList != null && m_BattleStageList.Count > 0 && m_BattleStageList.ContainsKey(chapterId))
        {
            int val = m_BattleStageList[chapterId].m_bRewardGot;
            string valStr = val.ToString();
            int length = valStr.Length;
            
            if (boxIdx < length && valStr[boxIdx] == '1')
            {
                return true;
            }
        }

        return false;
    }

    public Dictionary<int, BattleStage> GetBattleDatas()
    {
        return m_BattleStageList;
    }

    public BattleStage GetBattleStageByChapterId(int chapterId)
    {
        if (m_BattleStageList != null && m_BattleStageList.Count > 0 && m_BattleStageList.ContainsKey(chapterId))
        {
            return m_BattleStageList[chapterId];
        }

        return null;
    }

    public List<StageData> GetStageDataListByChapterId(int chapterId)
    {
        BattleStage bs = GetBattleStageByChapterId(chapterId);

        if (bs != null)
        {
            return bs.m_BattleStage;
        }

        return null;
    }

    public int GetStageCount()
    {
        if (m_BattleStageList != null)
        {
            return m_BattleStageList.Count;
        }

        return -1;
    }
}