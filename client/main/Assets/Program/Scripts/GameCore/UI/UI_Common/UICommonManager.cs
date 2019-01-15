using UnityEngine;
using UnityEngine.Events;

using System.Collections;
using System.Collections.Generic;

using DreamFaction.UI;
using DreamFaction.UI.Core;

/// <summary>
/// 主要是用于UICommon数量的管理;
/// </summary>

public class UICommonManager
{
    private static UICommonManager mInst = null;

    private Dictionary<UICommonType, List<BaseUI>> uiDic = new Dictionary<UICommonType, List<BaseUI>>();

    public static UICommonManager Inst
    {
        get
        {
            if (mInst == null)
            {
                mInst = new UICommonManager();
            }
            return mInst;
        }
    }

    private UICommonManager()
    {

    }

    //对应ui界面最多存在的ui实体个数;
    public static readonly byte[] UIMaxCounts = new byte[(int)(UICommonType.MaxValue)]
    {
        1,1,1,1,1,1,1,1
    };

    /// <summary>
    /// 可以在这里控制UI的显示个数;
    /// </summary>
    /// <param name="uiType"></param>
    /// <param name="ui"></param>
    public void AddUI(UICommonType uiType, BaseUI ui)
    {
        if (uiDic.ContainsKey(uiType))
        {
            if (uiDic[uiType] == null)
            {
                uiDic[uiType] = new List<BaseUI>();
            }

        }
        else
        {
            List<BaseUI> que = new List<BaseUI>();
            uiDic.Add(uiType, que);
        }
        
        uiDic[uiType].Add(ui);
    }

    /// <summary>
    /// 可以在这里控制UI的显示个数;
    /// </summary>
    /// <param name="uiType"></param>
    /// <param name="ui"></param>
    public void RemoveUI(UICommonType uiType, BaseUI ui)
    {
        if (uiDic.ContainsKey(uiType))
        {
            if (uiDic[uiType] == null || uiDic[uiType].Count <= 0)
            {
                return;
            }
            else
            {
                uiDic[uiType].Remove(ui);
                if (UI_HomeControler.Inst != null)
                {
                    UI_HomeControler.Inst.ReMoveUI(ui.gameObject);
                }
                else if (UI_FightControler.Inst != null)
                {
                    UI_FightControler.Inst.ReMoveUI(ui.gameObject);
                }
            }
        }
    }

    private bool IsIdValide(int id)
    {
        if (id > 0)
        {
            return true;
        }
        else
        {
            Debug.LogError("无效的id");
            return false;
        }
    }

    /// <summary>
    /// 通用弹窗接口，物品(不带获得途径的)、怪物....
    /// </summary>
    /// <param name="tableId"></param>
    /// <returns></returns>
    public UICommon_Common ShowCommon(int tableId)
    {
        if (!IsIdValide(tableId))
        {
            return null;
        }
        UICommon_Common commonUI = UICommonFactory.GenerateUICommon(UICommonType.Common) as UICommon_Common;

        if (commonUI != null)
        {
            commonUI.SetData(tableId);
        }

        return commonUI;
    }

    /// <summary>
    /// 神魂通用UI;
    /// </summary>
    /// <param name="tableId"></param>
    /// <returns></returns>
    public UICommon_GodSoul ShowGodSoul(int tableId)
    {
        if (!IsIdValide(tableId))
        {
            return null;
        }
        UICommon_GodSoul commonUI = UICommonFactory.GenerateUICommon(UICommonType.CommonGodSoul) as UICommon_GodSoul;

        if (commonUI != null)
        {
            commonUI.SetData(tableId);
        }

        return commonUI;
    }

    /// <summary>
    /// 英雄信息弹窗;
    /// </summary>
    /// <param name="objectCard"></param>
    /// <returns></returns>
    public UICommon_Hero ShowHero(ObjectCard objectCard)
    {
        UICommon_Hero commonUI = UICommonFactory.GenerateUICommon(UICommonType.CommonHero) as UICommon_Hero;

        if (commonUI != null)
        {
            commonUI.SetData(objectCard);
        }

        return commonUI;
    }

    /// <summary>
    /// 英雄属性;
    /// </summary>
    /// <param name="objectCreature"></param>
    /// <returns></returns>
    public UICommon_HeroProp ShowHeroProp(ObjectCreature objectCreature)
    {
        UICommon_HeroProp commonUI = UICommonFactory.GenerateUICommon(UICommonType.CommonHeroProp) as UICommon_HeroProp;

        if (commonUI != null)
        {
            commonUI.SetData(objectCreature);
        }

        return commonUI;
    }



    /// <summary>
    /// 开启通用弹窗;
    /// </summary>
    /// <param name="title"></param>
    /// <param name="detail"></param>
    /// <param name="hint"></param>
    /// <param name="yesBtnTxt"></param>
    /// <param name="yesAction"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public UICommon_MsgBox ShowMsgBox(string title, string detail, string hint = "", string yesBtnTxt = "", UnityAction<object> yesAction = null, UnityAction<object> noAction = null, object param = null)
    {
        UICommon_MsgBox msgBox = UICommonFactory.GenerateUICommon(UICommonType.CommonMsgBox) as UICommon_MsgBox;

        if (msgBox != null)
        {
            msgBox.SetData(title, detail, hint, yesBtnTxt, yesAction, noAction, param);
        }

        return msgBox;
    }

    /// <summary>
    /// 物品获得途径的通用弹窗;
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public UICommon_Obtain ShowHeroObtain(int itemId)
    {
        if (!IsIdValide(itemId))
        {
            return null;
        }
        UICommon_Obtain commonUI = UICommonFactory.GenerateUICommon(UICommonType.CommonObtain) as UICommon_Obtain;

        if (commonUI != null)
        {
            commonUI.SetData(itemId);
        }

        return commonUI;
    }

    /// <summary>
    /// 宝箱预览通用弹窗;
    /// </summary>
    /// <param name="dropId"></param>
    /// <returns></returns>
    public UICommon_RewardBox ShowRewardBox(int dropId, int starNum)
    {
        if (!IsIdValide(dropId))
        {
            return null;
        }
        UICommon_RewardBox commonUI = UICommonFactory.GenerateUICommon(UICommonType.CommonRewardBox) as UICommon_RewardBox;

        if (commonUI != null)
        {
            commonUI.SetData(dropId, starNum);
        }

        return commonUI;
    }

    /// <summary>
    /// 获得奖励弹窗;
    /// </summary>
    /// <param name="dropId"></param>
    /// <returns></returns>
    public UICommon_RewardView ShowRewardView(int dropId)
    {
        if (!IsIdValide(dropId))
        {
            return null;
        }
        UICommon_RewardView commonUI = UICommonFactory.GenerateUICommon(UICommonType.CommonRewardView) as UICommon_RewardView;

        if (commonUI != null)
        {
            commonUI.SetData(dropId);
        }

        return commonUI;
    }
    /// <summary>
    /// 未获得英雄碎片显示窗
    /// </summary>
    /// <param name="heroTableId">英雄表英雄id</param>
    /// <returns></returns>
    public UICommon_HeroFragment ShowHeroFragment(int heroTableId)
    {
        if (!IsIdValide(heroTableId))
        {
            return null;
        }
        UICommon_HeroFragment commonUI = UI_HomeControler.Inst.AddUI("UI_Common/UI_CommonHeroFragment_1_18").GetComponent<UICommon_HeroFragment>();
        if (commonUI != null)
        {
            commonUI.InitData(heroTableId);
        }
        return commonUI;
    }

    /// <summary>
    /// 技能通用UI
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="card"></param>
    /// <param name="idx">[1-6] 第几个技能 </param>
    /// <returns></returns>
    public UI_SkillPopMgr ShowSkill(SkillTemplate skillT, ObjectCard card, int idx, UI_SkillPopMgr.SkillPopUIType type = UI_SkillPopMgr.SkillPopUIType.Default)
    {
        bool isSkillOpen = card.GetHeroData().QualityLev >= idx;

        string path = "";
       
        //if (isSkillOpen)
        //    path = "HeroStrengthen/UI_SkillOpen_1_20";
        //else
        //    path = "HeroStrengthen/UI_SkillNotOpen_1_20";

        switch (type)
        {
            case UI_SkillPopMgr.SkillPopUIType.Default:
                if (isSkillOpen)
                    path = "HeroStrengthen/UI_SkillOpen_1_20";
                else
                    path = "HeroStrengthen/UI_SkillNotOpen_1_20";

                break;
            case UI_SkillPopMgr.SkillPopUIType.Locked:
            case UI_SkillPopMgr.SkillPopUIType.LevelUp:
                path = "HeroStrengthen/UI_SkillNotOpen_1_20";
                break;
            case UI_SkillPopMgr.SkillPopUIType.UnLocked:
                path = "HeroStrengthen/UI_SkillOpen_1_20";
                break;
            default:
                break;
        }

        GameObject go = UI_HomeControler.Inst.AddUI(path);
        UI_SkillPopMgr uiSkillPopMgr = go.GetComponent<UI_SkillPopMgr>();


        uiSkillPopMgr.ShowSkillPopUI(skillT, card, idx, type);

        return uiSkillPopMgr;
    }
    /// <summary>
    /// 英雄定位弹窗
    /// </summary>
    public void ShowHeroLocatUI()
    {
        UI_HomeControler.Inst.AddUI(UI_HeroLocatUIMgr.UI_ResPath);
    }
    public UICommon_HeroGet ShowHeroGet(ObjectCard oc)
    {
        UICommon_HeroGet commonUI = UI_HomeControler.Inst.AddUI("UI_Common/UI_CommonHeroGet_1_6").GetComponent<UICommon_HeroGet>();
        if (commonUI != null)
        {
            commonUI.SetData(oc,UICommon_HeroGet.PanelType.FragmentRecruit);
        }
        return commonUI;
    }
}
