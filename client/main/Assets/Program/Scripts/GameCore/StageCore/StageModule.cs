using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork;
using System.Collections.Generic;
using System;
using DreamFaction.Utils;

public class StageModule 
{
    public static EM_STAGE_TYPE GetStageType(int stageId)
    {
        return GetStageType(GetStageTemplateById(stageId));
    }
    /// <summary>
    /// 根据难度+关卡类型 = 表格中的关卡类型;
    /// </summary>
    /// <param name="difficultType"></param>
    /// <param name="stageType"></param>
    /// <returns></returns>
    public static EM_STAGE_TYPE GetStageType(EM_STAGE_DIFFICULTTYPE difficultType, EM_STAGE_STAGETYPE stageType)
    {
        switch (stageType)
        {
            case EM_STAGE_STAGETYPE.MAIN:
                {
                    switch (difficultType)
	                {
                        case EM_STAGE_DIFFICULTTYPE.NONE:
                            break;
                        case EM_STAGE_DIFFICULTTYPE.NORMAL:
                            return EM_STAGE_TYPE.MAIN_QUEST1;
                        case EM_STAGE_DIFFICULTTYPE.HARD:
                            return EM_STAGE_TYPE.MAIN_QUEST2;
                        case EM_STAGE_DIFFICULTTYPE.HARDEST:
                            return EM_STAGE_TYPE.MAIN_QUEST3;
                        default:
                            break;
	                }
                }
                break;
            case EM_STAGE_STAGETYPE.SIDE:
                return EM_STAGE_TYPE.SIDE_QUEST;
            case EM_STAGE_STAGETYPE.SPECIAL:
                return EM_STAGE_TYPE.SPEC_QUEST;
            default:
                break;
        }
        return EM_STAGE_TYPE.NONE;
    }

    public static EM_STAGE_TYPE GetStageType(StageTemplate stageT)
    {
        return (EM_STAGE_TYPE)(stageT.m_stagetype);
    }

    public static EM_STAGE_STAGETYPE GetStageStageType(StageTemplate stageT)
    {
        EM_STAGE_STAGETYPE type = EM_STAGE_STAGETYPE.MAIN;
        switch (GetStageType(stageT))
        {
            case EM_STAGE_TYPE.LIMIT_TEST:
                return EM_STAGE_STAGETYPE.LITMIT_TIMES;
            case EM_STAGE_TYPE.MAIN_QUEST1:
            case EM_STAGE_TYPE.MAIN_QUEST2:
            case EM_STAGE_TYPE.MAIN_QUEST3:
                return EM_STAGE_STAGETYPE.MAIN;
            case EM_STAGE_TYPE.SIDE_QUEST:
                return EM_STAGE_STAGETYPE.SIDE;
            case EM_STAGE_TYPE.SPEC_QUEST:
                return EM_STAGE_STAGETYPE.SPECIAL;
            case EM_STAGE_TYPE.ACTIVE_QUEST_DIJING:
            case EM_STAGE_TYPE.ACTIVE_QUEST_YANLONG:
                return EM_STAGE_STAGETYPE.ACTIVE;
            case EM_STAGE_TYPE.BOSS_SHOUWANGZHE:
            case EM_STAGE_TYPE.BOSS_CHUANSHUO:
                return EM_STAGE_STAGETYPE.BOSS;
            default:
                return EM_STAGE_STAGETYPE.NONE;
        }
    }

    public static bool IsStageLevelById(int stageLevelId)
    {
        StageTemplate stageT = GetStageTemplateById(stageLevelId);

        if (stageT != null)
        {
            return stageT.m_stagetype == 1 || stageT.m_stagetype == 2 || stageT.m_stagetype == 3 || stageT.m_stagetype == 4;
        }

        return false;
    }

    /// <summary>
    /// 根据关卡数据判断当前关卡难度;
    /// </summary>
    /// <param name="stageT"></param>
    /// <returns></returns>
    public static EM_STAGE_DIFFICULTTYPE GetStageDifficultType(StageTemplate stageT)
    {
        switch (stageT.m_stagetype)
        {
            case 1:
            case 4:
            case 5:
                return EM_STAGE_DIFFICULTTYPE.NORMAL;
            case 2:
                return EM_STAGE_DIFFICULTTYPE.HARD;
            case 3:
                return EM_STAGE_DIFFICULTTYPE.HARDEST;
            default:
                return EM_STAGE_DIFFICULTTYPE.NORMAL;
        }
    }

    public static bool IsStageTemplate(int tableId)
    {
        return DataTemplate.GetInstance().m_StageTable.tableContainsKey(tableId);
    }

    public static StageTemplate GetStageTemplateById(int tableid)
    {
        return (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(tableid);
    }

    public static ChapterinfoTemplate GetChapterinfoTemplateById(int tableid)
    {
        return (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(tableid);
    }

    public static int GetChapterinfoIdByStageId(int stageId)
    {
        foreach (var item in DataTemplate.GetInstance().m_ChapterTable.getData())
        {
            if (item.Value == null)
            {
                continue;
            }
            
            ChapterinfoTemplate chapterT = item.Value as ChapterinfoTemplate;

            if (chapterT == null)
            {
                continue;
            }

            int length = chapterT.getStageID().Length;

            for (int i = 0; i < length; i++ )
            {
                if (chapterT.getStageID()[i] == stageId)
                {
                    return item.Key;
                }
            }
        }

        return -1;
    }

    public static List<StageTemplate> GetStageTemplatesByChapterID(int chapterID)
    {
        ChapterinfoTemplate chapterT = GetChapterinfoTemplateById(chapterID);

        return GetStageTemplatesByChapter(chapterT);
    }
    
    public static List<StageTemplate> GetStageTemplatesByChapter(ChapterinfoTemplate chapterT)
    {
        if (chapterT == null || chapterT.getStageID().Length <= 0)
        {
            return null;
        }

        List<StageTemplate> result = new List<StageTemplate>();

        for (int i = 0, j = chapterT.getStageID().Length; i < j; i++)
        {
            StageTemplate st = GetStageTemplateById(chapterT.getStageID()[i]);
            if (st == null)
            {
                continue;
            }

            result.Add(st);
        }

        return result;
    }

    /// <summary>
    /// 获得冒险章节总数目;(id>1000的不属于冒险章节)
    /// </summary>
    /// <returns></returns>
    public static int GetChapterCount()
    {
        List<int> keys = DataTemplate.GetInstance().m_ChapterTable.GetDataKeys();
        int count = 0;
        
        for (int i = 0; i < keys.Count; i++ )
        {
            if (keys[i] < 1000)
            {
                count++;
            }
        }

        return count;
    }

    public static bool isReward(int difficulttype, int rewardnum)
    {
        int reward = 0;
        if (difficulttype == 1)
        {
            reward = rewardnum % 10;
        }
        else if (difficulttype == 2)
        {
            reward = rewardnum % 100 / 10;
        }
        else if (difficulttype == 3)
        {
            reward = rewardnum / 100;
        }
        return reward != 0;
    }

    public static BattleStage GetPlayerBattleStageInfo()
    {
        if (ObjectSelf.GetInstance().GetIsPrompt())
        {
            return ObjectSelf.GetInstance().BattleStageData.GetBattleStageByChapterId(1001);
        }
        else
        {
            return ObjectSelf.GetInstance().BattleStageData.GetBattleStageByChapterId(ObjectSelf.GetInstance().GetCurChapterID());
        }
    }

    /// <summary>
    /// 获取当前章节所含有的指定类型的关卡的个数
    /// </summary>
    /// <param name="chapterT"></param>
    /// <returns></returns>
    public static int GetStageCount(ChapterinfoTemplate chapterT, int stageType)
    {
        List<StageTemplate> datas = GetStageDatas(chapterT, stageType);

        return datas == null ? 0 : datas.Count;
    }

    /// <summary>
    /// 获取当前章节所含有的指定类型的关卡的个数
    /// </summary>
    /// <param name="chapterT"></param>
    /// <returns></returns>
    public static int GetStageCount(ChapterinfoTemplate chapterT, EM_STAGE_TYPE stageType)
    {
        return GetStageCount(chapterT, (int)stageType);
    }

    /// <summary>
    /// 获取当前章节所含有的指定类型的关卡的所有数据;
    /// </summary>
    /// <param name="chapterT"></param>
    /// <returns></returns>
    public static List<StageTemplate> GetStageDatas(ChapterinfoTemplate chapterT, int stageType)
    {
        if (chapterT == null)
        {
            return null;
        }

        List<StageTemplate> result = new List<StageTemplate>();
        for (int i = 0, j = chapterT.getStageID().Length; i < j; i++)
        {
            StageTemplate stageT = GetStageTemplateById(chapterT.getStageID()[i]);

            if (stageT == null || stageT.m_stagetype != stageType)
            {
                continue;
            }

            result.Add(stageT);
        }

        return result;
    }

    /// <summary>
    /// 获取当前章节所含有的指定类型的关卡的所有数据;
    /// </summary>
    /// <param name="chapterT"></param>
    /// <returns></returns>
    public static List<StageTemplate> GetStageDatas(ChapterinfoTemplate chapterT, EM_STAGE_TYPE stageType)
    {
        return GetStageDatas(chapterT, (int)stageType);
    }

    /// <summary>
    /// 获得指定章节，指定难度等级的获得星星的总数
    /// </summary>
    /// <param name="chapterT"></param>
    /// <param name="difficultLevel"></param>
    /// <returns></returns>
    public static int GetTotalStarsCount(ChapterinfoTemplate chapterT, int difficultLevel)
    {
        if (chapterT == null || chapterT.getStageID().Length <= 0)
        {
            return -1;
        }

        if (difficultLevel < 1 || difficultLevel > 3)
        {
            return -1;
        }

        int result = 0;
        int adder = 0;       //根据difficultLevel（难度等级）获取星星累加值;
        int stageCount = 0;


        switch (difficultLevel)
        {
            case 1:
                stageCount += GetStageCount(chapterT, EM_STAGE_TYPE.MAIN_QUEST1);
                stageCount += GetStageCount(chapterT, EM_STAGE_TYPE.SIDE_QUEST);                
                adder = 3;
                break;
            case 2:
                stageCount += GetStageCount(chapterT, EM_STAGE_TYPE.MAIN_QUEST2);
                adder = 3;
                break;
            case 3:
                stageCount += GetStageCount(chapterT, EM_STAGE_TYPE.MAIN_QUEST3);
                adder = 3;
                break;
            default:
                break;
        }
        result = adder * stageCount;
        return result;
    }

    /// <summary>
    /// 获取当前对应章节所获得的当前和所有星星个数;
    /// </summary>
    /// <param name="chapterT"></param>
    /// <param name="difficultLevel"></param>
    /// <returns></returns>
    public static bool GetCurTotalStarsCount(ChapterinfoTemplate chapterT, EM_STAGE_DIFFICULTTYPE difficultLevel, out int cur, out int total)
    {
        total = cur = -1;

        if (chapterT == null)
        {
            return false;
        }

        int chapterId = chapterT.getId();
        int tmpStarCount = 0;

        List<StageData> data = ObjectSelf.GetInstance().BattleStageData.GetStageDataListByChapterId(chapterId);
        if (data != null && data.Count > 0)
        {
            total = cur = 0;

            for (int i = 0; i < chapterT.getStageID().Length; i++)
            {
                StageTemplate stageT = GetStageTemplateById(chapterT.getStageID()[i]);
                if (difficultLevel == EM_STAGE_DIFFICULTTYPE.NORMAL)
                {
                    if (stageT.m_stagetype == 4 || stageT.m_stagetype == 1)
                    {
                        total += 3;
                        if (ObjectSelf.GetInstance().BattleStageData.IsCopyScenePass(chapterId, stageT.m_stageid, out tmpStarCount))
                        {
                            cur += tmpStarCount;
                        }
                        
                    }
                }
                if (difficultLevel == EM_STAGE_DIFFICULTTYPE.HARD)
                    //if (difficultLevel == 2)
                {
                    if (stageT.m_stagetype == 2)
                    {
                        total += 3;
                        if (ObjectSelf.GetInstance().BattleStageData.IsCopyScenePass(chapterId, stageT.m_stageid, out tmpStarCount))
                        {
                            cur += tmpStarCount;
                        }
                    }
                }
                if (difficultLevel == EM_STAGE_DIFFICULTTYPE.HARDEST)
                    //if (difficultLevel == 3)
                {
                    if (stageT.m_stagetype == 3)
                    {
                        total += 3;
                        if (ObjectSelf.GetInstance().BattleStageData.IsCopyScenePass(chapterId, stageT.m_stageid, out tmpStarCount))
                        {
                            cur += tmpStarCount;
                        }
                    }
                }
            }

            return true;
        }

        return false;
    }

    public static bool GetCurTotalStarsCount(int chapterId, EM_STAGE_DIFFICULTTYPE difficultLevel, out int cur, out int total)
    {
        ChapterinfoTemplate chapterT = GetChapterinfoTemplateById(chapterId);

        return GetCurTotalStarsCount(chapterT, difficultLevel, out cur, out total);
    }

    public static List<StageTemplate> AddList(List<StageTemplate> resultList, List<StageTemplate> addList)
    {
        if (addList != null && addList.Count > 0)
        {
            for (int i = 0; i < addList.Count; i++ )
            {
                resultList.Add(addList[i]);
            }
        }

        return resultList;
    }

    /// <summary>
    /// 获得指定章节拥有的关卡难度个数;
    /// </summary>
    /// <param name="chapterT"></param>
    /// <returns></returns>
    public static int GetDifficultCount(ChapterinfoTemplate chapterT)
    {
        if (chapterT == null)
        {
            return -1;
        }

        int count = 0;

        for (int i = 0, j = chapterT.getDifficult().Length; i < j;  i++)
        {
            if (chapterT.getDifficult()[i] > 0)
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// 判断某章节宝箱奖励是否领取;
    /// </summary>
    /// <param name="chapterId"></param>
    /// <returns></returns>
    public static bool IsRewardGot(int chapterId)
    {
        Dictionary<int, BattleStage> data = ObjectSelf.GetInstance().BattleStageData.GetBattleDatas();

        if (data.ContainsKey(chapterId))
        {
            return data[chapterId].m_bRewardGot == 1;
        }

        return false;
    }

   
    /// <summary>
    /// 判断某章节宝箱奖励是否领取;
    /// </summary>
    /// <param name="stageId"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    public static bool IsRewardGot(int chapterId, int idx)
    {
        return ObjectSelf.GetInstance().BattleStageData.GetRewardGot(chapterId, idx);
    }

    /// <summary>
    /// 获得当前开放的最后章节id;
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerLastChapterID()
    {
        Dictionary<int, BattleStage> stags = ObjectSelf.GetInstance().BattleStageData.GetBattleDatas();

        int count = 0;
        foreach (var item in stags)
        {
            if (item.Key > 1000)
            {
                continue;
            }
            else
            {
                count++;
            }

        }

        return count;
    }

    /// <summary>
    /// 获得当前开放的最后的关卡id;
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerLastLevelID(EM_STAGE_DIFFICULTTYPE difficultType)
    {
        //Dictionary<int, BattleStage> stages = ObjectSelf.GetInstance().BattleStageData.GetBattleDatas();

        //BattleStage bs = stages[GetPlayerLastChapterID()];

        return GetLastStageIdInTheChapter(GetPlayerLastChapterID(), difficultType);
    }

    /// <summary>
    /// 获得当前主线关卡是该章的第几节;
    /// </summary>
    /// <param name="stageT"></param>
    /// <returns></returns>
    public static int GetStageNumInChapter(StageTemplate stageT)
    {
        return stageT.m_stageid / 1000 % 100;
    }

    /// <summary>
    /// 获得玩家当前章节可以进入的最后(按id排序)一个关卡信息;
    /// </summary>
    /// <param name="chapterId"></param>
    /// <returns></returns>
    public static int GetLastStageIdInTheChapter(ChapterinfoTemplate chapterT, EM_STAGE_DIFFICULTTYPE difficultType)
    {
        List<StageData> bs = ObjectSelf.GetInstance().BattleStageData.GetStageDataListByChapterId(chapterT.getId());

        if (bs == null || bs.Count <= 0)
        {
            return -1;
        }

        int lastStageId = -1;
        int temp = -1;
        for (int i = 0; i < bs.Count; i++)
        {
            temp = bs[i].m_StageID;
            StageTemplate stageT = GetStageTemplateById(temp);
            if (stageT == null)
            {
                continue;
            }

            //只选主线;
            EM_STAGE_STAGETYPE stageType = GetStageStageType(stageT);
            if (stageType != EM_STAGE_STAGETYPE.MAIN)
            {
                continue;
            }

            EM_STAGE_DIFFICULTTYPE type = GetStageDifficultType(stageT);

            if (type == difficultType)
            {
                lastStageId = temp;
            }
        }

        return lastStageId;
    }

    public static int GetLastStageIdInTheChapter(int chapterId, EM_STAGE_DIFFICULTTYPE difficultType)
    {
        ChapterinfoTemplate chapterT = GetChapterinfoTemplateById(chapterId);
        if (chapterT == null)
        {
            return -1;
        }

        return GetLastStageIdInTheChapter(chapterT, difficultType);
    }

    /// <summary>
    /// 获得当前章节中指定难度第一个关卡;
    /// </summary>
    /// <param name="chapterT"></param>
    /// <param name="difficultType"></param>
    /// <returns></returns>
    public static int GetFirstStageIdInTheChapter(ChapterinfoTemplate chapterT, EM_STAGE_DIFFICULTTYPE difficultType)
    {
        if (chapterT == null)
        {
            throw new Exception("Null Refrence Exception");
        }

        int[] stageIds = chapterT.getStageID();

        if (stageIds == null || stageIds.Length <= 0)
        {
            return -1;
        }

        int temp = -1;
        for (int i = 0, j = stageIds.Length; i < j; i++)
        {
            temp = stageIds[i];
            StageTemplate stageT = GetStageTemplateById(temp);
            if (stageT == null)
            {
                continue;
            }

            //只选主线;
            EM_STAGE_STAGETYPE stageType = GetStageStageType(stageT);
            if (stageType != EM_STAGE_STAGETYPE.MAIN)
            {
                continue;
            }

            EM_STAGE_DIFFICULTTYPE type = GetStageDifficultType(stageT);

            if (type == difficultType)
            {
                return temp;
            }
        }

        return -1;
    }

    /// <summary>
    /// 根据购买次数，获得当前购买扫荡次数需要花费的钱数;
    /// </summary>
    /// <param name="buyTimes">[0-n]</param>
    /// <returns></returns>
    public static int GetBuyRapidCost(int buyTimes)
    {
        GameConfig gc = DataTemplate.GetInstance().GetGameConfig();
        int[] costs = gc.getSweep_reset_cost();

        if (buyTimes >= costs.Length)
        {
            return costs[costs.Length - 1];
        }

        return costs[buyTimes];
    }

    /// <summary>
    /// 判断是否是神秘商店；不要问我为什么这么判断，因为，服务器当时写的时候就是如果不在关卡表那就是神秘商店;
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool IsMysteriousShop(int id)
    {
        if ((id > 0) && (DataTemplate.GetInstance().m_StageTable.tableContainsKey(id) == false))
        {
            return true;
        }

        return false;
    }

    public static string GetBossName(StageTemplate stageData)
    {
        MonstergroupTemplate _monsterGroup = null;
        MonsterTemplate _bossTemp = null;
        for (int i = stageData.m_monstergroup.Length - 1; i >= 0; i--)
        {
            _monsterGroup = null;
            _bossTemp = null;
            if (stageData.m_monstergroup[i] <= 0)
                continue;
            _monsterGroup = DataTemplate.GetInstance().m_MonsterGroupTable.getTableData(stageData.m_monstergroup[i]) as MonstergroupTemplate;
            if (_monsterGroup == null || _monsterGroup.getGrouptype() != 2 || GameUtils.GetObjectClassById(_monsterGroup.getMonsterid()[0]) != EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER)
                continue;

            _bossTemp = DataTemplate.GetInstance().m_MonsterTable.getTableData(_monsterGroup.getMonsterid()[0]) as MonsterTemplate;
            if (_bossTemp != null)
                break;
        }
        if (_bossTemp == null)
            return null;
        else
            return GameUtils.getString(_bossTemp.getMonstername());
    }
}
