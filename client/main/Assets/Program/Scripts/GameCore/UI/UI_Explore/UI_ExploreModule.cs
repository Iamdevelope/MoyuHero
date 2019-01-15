using UnityEngine;
using System.Collections;
using GNET;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using System;
using DreamFaction.GameCore;
using DreamFaction.UI;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.Utils;

/// <summary>
/// 探险任务状态;
/// </summary>
public enum EXPLORE_TASK_STATE : int
{
    None = -1,
    ExploringOver = 1,          //探险中，可以领取奖励;
    ExploringNotOver = 2,       //探险中，没到领取时间;
    NotStarted = 3,             //尚未开始;
    Over = 4,                   //探险结束;
}

public class UI_ExploreModule
{
    public static bool CheckAndOpenExploreUI()
    {
        BattleStageMgr stageMgr = ObjectSelf.GetInstance().BattleStageData;

        int star = -1;
        int stageId = DataTemplate.GetInstance().GetGameConfig().getExplore_open_stage();
        
        if (stageMgr.IsCopyScenePass(stageId, out star))
        {
            UI_HomeControler.Inst.AddUI(UI_ExploreMgr.UI_ResPath);
            return true;
        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_open"));
        }

        return false;
    }

    public static void OnTimeUpBtnClick(int exploreId)
    {
        //VIP等级不够;
        int openLv = VIPModule.GetExploreAccelerateVipLv();
        if(ObjectSelf.GetInstance().VipLevel < openLv)
        {
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("explore_bubble15"), openLv), UI_HomeControler.Inst.GetTopTransform());
            return;
        }

        tanxianinit mData = ObjectSelf.GetInstance().GetExploreTaskDataById(exploreId);
        int minutes = UI_ExploreModule.GetTaskMinuteToEnd(mData);
        int cost = UI_ExploreModule.GetCostByMinutes(minutes);
        //谈确认框;
        UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();

        if (box == null)
        {
            LogManager.LogError("提示窗is null");
            return;
        }

        UI_RechargeBox.CurOpenType = EM_RECHARGEBOX_OPEN_TYPE.EXPLORE_TIMEUP_HINT;
        UI_RechargeBox.Data = exploreId;
        box.SetIsNeedDescription(true);
        box.SetMoneyInfoActive(true);
        box.SetDescription_text(GameUtils.getString("explore_bubble7"));
        box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
        box.SetConNum(cost.ToString());
        box.SetConsume_Image(GameUtils.GetSpriteByResourceType(EM_RESOURCE_TYPE.Gold));
        box.SetMoneyInfo((int)EM_RESOURCE_TYPE.Gold, ObjectSelf.GetInstance().Gold);

        box.SetLeftClick(() =>
        {
            //InterfaceControler.GetInst().AddMsgBox("打开快速充值界面", parent);
            //UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
            box.OnCloes();
            //已经过时;
            TimeSpan ts = GetTaskTimeToEnd(mData);
            if (ts <= TimeSpan.Zero)
            {
                return;
            }

            //钱不够;
        
            if (ObjectSelf.GetInstance().Gold < cost)
            {
                InterfaceControler.GetInst().ShowGoldNotEnougth();
                return;
            }

            UI_ExploreModule.SendOtherProtocol(CTanXianOther.END_SPEED, mData.tanxianid);
        });
        box.SetRightBtn_text(GameUtils.getString("common_button_close"));


    }

    public static void OnCallBackBtnClick(int exploreId)
    {
        UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();

        if (box == null)
        {
            LogManager.LogError("提示窗is null");
            return;
        }

        box.SetDescription_text(GameUtils.getString("explore_bubble8"));
        box.SetIsNeedDescription(false);
        box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
        box.SetLeftClick(() =>
        {
            UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
            UI_ExploreModule.SendOtherProtocol(CTanXianOther.END_NULL, exploreId);
        });

        box.SetRightBtn_text(GameUtils.getString("common_button_close"));
    }

    public static int GetTaskMinuteToEnd(tanxianinit data)
    {
        TimeSpan ts = GetTaskTimeToEnd(data);

        return Mathf.CeilToInt((float)ts.TotalMinutes);
    }

    public static TimeSpan GetTaskTimeToEnd(tanxianinit data)
    {
        DateTime endTime = TimeUtils.ConverMillionSecToDateTime(data.endtime, ObjectSelf.GetInstance().ServerTimeZone);

        return TimeUtils.GetTimeSpan(endTime, ObjectSelf.GetInstance().ServerDateTime);
    }

    /// <summary>
    /// 根据剩余时间获取花费数值,像下取整;
    /// </summary>
    /// <param name="minutes"></param>
    public static int GetCostByMinutes(int minutes)
    {
        float paramA = DataTemplate.GetInstance().GetGameConfig().getExplore_param_a();
        float paramB = DataTemplate.GetInstance().GetGameConfig().getExplore_param_b();
        float paramC = DataTemplate.GetInstance().GetGameConfig().getExplore_param_c();

        return Mathf.FloorToInt(paramA * Mathf.Pow(minutes, 2.0f) + paramB * minutes + paramC);
    }

    public static EXPLORE_TASK_STATE GetExploreTaskState(tanxianinit data)
    {
        switch (data.tanxiantype)
        {
            case 0:        //未开始;
                
                return EXPLORE_TASK_STATE.NotStarted;
            case 1:        //进行中;
                int minutes = GetTaskMinuteToEnd(data);

                //判断是否可以领取奖励;
                if (minutes <= 0)
                {
                    //可以领奖;
                    return EXPLORE_TASK_STATE.ExploringOver;
                }
                else
                {
                    //不可以领奖;
                    return EXPLORE_TASK_STATE.ExploringNotOver;
                }
            case 2:        //已完成;
                return EXPLORE_TASK_STATE.Over;
            default:
                break;
        }

        return EXPLORE_TASK_STATE.Over;
    }

    /// <summary>
    /// 获取当前出战的小队个数;
    /// </summary>
    /// <returns></returns>
    public static int GetExploreTeamsCount()
    {
        Dictionary<int, teamtanxian> datas = ObjectSelf.GetInstance().GetExploreTeamData();

        if (datas != null)
        {
            int count = 0;

            foreach (KeyValuePair<int, teamtanxian> item in datas)
            {
                if ((item.Value == null) || (item.Value.team.Count <= 0))
                {
                    continue;
                }

                count++;
            }

            return count;
        }

        return 0;
    }

    public static int GetMaxTeamCount()
    {
        return DataTemplate.GetInstance().GetGameConfig().getExplore_queue();
    }

    public static void SendOtherProtocol(int type, int param)
    {
        CTanXianOther ctxo = new CTanXianOther();
        ctxo.endtype = type;
        ctxo.tanxianid = param;
        IOControler.GetInstance().SendProtocol(ctxo);
    }

    /// <summary>
    /// 根据探险id获取满足条件的所有ObjectCard;
    /// </summary>
    /// <param name="exploreId"></param>
    /// <returns></returns>
    public static List<ObjectCard> GetCardList(int exploreId, EM_SORT_OBJECT_CARD sortType)
    {
        List<ObjectCard> result = new List<ObjectCard>();

        ExplorequestTemplate et = DataTemplate.GetInstance().GetExplorequestTemplateById(exploreId);
        if (et == null)
        {
            return result;
        }

        HeroContainer hc = ObjectSelf.GetInstance().HeroContainerBag;

        foreach (ObjectCard oc in hc.GetHeroList())
        {
            if (IsEnough(oc, et) && !ObjectSelf.GetInstance().IsInExploring(oc.GetGuid()))
            {
                result.Add(oc);
            }
        }

        SortObjectCard(result, sortType);

        return result;
    }

    public static void SortObjectCard(List<ObjectCard> cards, EM_SORT_OBJECT_CARD sortType)
    {
        switch (sortType)
        {
            case EM_SORT_OBJECT_CARD.LEVEL:
                cards.Sort(CompareByLevel);
                break;
            case EM_SORT_OBJECT_CARD.QUALITY:
                cards.Sort(CompareByQuality);
                break;
            case EM_SORT_OBJECT_CARD.NONE:
                break;
            default:
                break;
        }
    }

    public static int CompareByLevel(ObjectCard oc1, ObjectCard oc2)
    {
        //按等级排;
        if (oc1.GetHeroData().Level > oc2.GetHeroData().Level)
        {
            return 1;
        }

        if (oc1.GetHeroData().Level < oc2.GetHeroData().Level)
        {
            return -1;
        }

        //等级相同按品质排序;
        if (oc1.GetHeroData().Level == oc2.GetHeroData().Level)
        {
            if (oc1.GetHeroRow().getQuality() > oc2.GetHeroRow().getQuality())
            {
                return 1;
            }
            else if (oc1.GetHeroRow().getQuality() < oc2.GetHeroRow().getQuality())
            {
                return -1;
            }

            //品质相同按id排序;
            if (oc1.GetHeroRow().getQuality() == oc2.GetHeroRow().getQuality())
            {
                return oc1.GetHeroData().TableID - oc2.GetHeroData().TableID;
            }
        }

        return 0;
    }

    public static int CompareByQuality(ObjectCard oc1, ObjectCard oc2)
    {
        //按品质排;
        if (oc1.GetHeroRow().getQuality() > oc2.GetHeroRow().getQuality())
        {
            return 1;
        }
        else if (oc1.GetHeroRow().getQuality() < oc2.GetHeroRow().getQuality())
        {
            return -1;
        }

        //品质相同按等级排序;
        if (oc1.GetHeroRow().getQuality() == oc2.GetHeroRow().getQuality())
        {
            if (oc1.GetHeroData().Level > oc2.GetHeroData().Level)
            {
                return 1;
            }

            if (oc1.GetHeroData().Level < oc2.GetHeroData().Level)
            {
                return -1;
            }

            //等级相同按id排序;
            if (oc1.GetHeroData().Level == oc2.GetHeroData().Level)
            {
                return oc1.GetHeroData().TableID - oc2.GetHeroData().TableID;
            }
        }

        return 0;
    }

    public static List<tanxianinit> SortTaskList(List<tanxianinit> datas)
    {
        datas.Sort((tanxianinit left, tanxianinit right) =>
            {
                EXPLORE_TASK_STATE state1 = GetExploreTaskState(left);
                EXPLORE_TASK_STATE state2 = GetExploreTaskState(right);

                return (int)state1 - (int)state2; 
            }
        );

        return datas;
    }

    //是否满足条件
    static bool IsEnough(ObjectCard oc, ExplorequestTemplate et)
    {
        int needHeroType = et.getNeedHeroType();

        bool result = false;

        switch(needHeroType)
        {
            case 1:
                bool lvEnough = oc.GetHeroData().Level >= et.getNeedHeroLevel();
                bool campEnough = new List<int>(et.getNeedHeroCamp()).Contains(oc.GetHeroRow().getCamp());
                bool qualityEnough = oc.GetHeroRow().getQuality() >= et.getNeedHeroStar();
                
                result = lvEnough && campEnough && qualityEnough;
                break;
            case 2:
                bool inIds1 = new List<int>(et.getNeedHeroID1()).Contains(oc.GetHeroData().TableID);
                bool inIds2 = new List<int>(et.getNeedHeroID2()).Contains(oc.GetHeroData().TableID);
                bool inIds3 = new List<int>(et.getNeedHeroID3()).Contains(oc.GetHeroData().TableID);
                bool inIds4 = new List<int>(et.getNeedHeroID4()).Contains(oc.GetHeroData().TableID);
                bool inIds5 = new List<int>(et.getNeedHeroID5()).Contains(oc.GetHeroData().TableID);

                result = inIds1 || inIds2 || inIds3 || inIds4 || inIds5;
                break;
            default:
                break;
        }

        return result;
    }
}
