using UnityEngine;
using System.Collections;

using DreamFaction.UI;
using DreamFaction.LogSystem;
using DreamFaction.UI.Core;


public enum UICommonType : int
{
    CommonObtain = 0,
    CommonRewardBox,
    CommonRewardView,
    Common,
    CommonMsgBox,
    CommonGodSoul,
    CommonHero,
    CommonHeroProp,
    MaxValue,
}

public class UICommonFactory
{
    static readonly string[] resPaths = new string[(int)(UICommonType.MaxValue)]
    {
        "UI_Common/UI_CommonObtain_1_10",
        "UI_Common/UI_CommonRewardBox_1_11",
        "UI_Common/UI_CommonRewardView_1_12",
        "UI_Common/UI_CommonCommon_1_13",
        "UI_Common/UI_CommonMsgBox_1_14",
        "UI_Common/UI_CommonGodSoul_1_15",
        "UI_Common/UI_CommonHero_1_16",
        "UI_Common/UI_CommonHeroProp_1_17",
    };


    public static BaseUI GenerateUICommon(UICommonType type)
    {
        
        GameObject go = null;
        try
        {
            //go = UIResourceMgr.LoadPrefab(resPaths[(int)type]) as GameObject;
            if (UI_HomeControler.Inst != null)
            {
                go = UI_HomeControler.Inst.AddUI(resPaths[(int)type]);
            }
            else if (UI_FightControler.Inst != null)
            {
                go = UI_FightControler.Inst.AddUI(resPaths[(int)type]);
            }
        }
        catch (System.Exception ex)
        {
            LogManager.LogError(ex);
            return null;
        }

        switch (type)
        {
            case UICommonType.CommonObtain:
                UICommon_Obtain commonProp = go.GetComponent<UICommon_Obtain>();
                UICommonManager.Inst.AddUI(UICommonType.CommonObtain, commonProp);
                return commonProp;
            case UICommonType.CommonRewardBox:
                UICommon_RewardBox box = go.GetComponent<UICommon_RewardBox>();
                UICommonManager.Inst.AddUI(UICommonType.CommonRewardBox, box);
                return box;
            case UICommonType.CommonRewardView:
                UICommon_RewardView view = go.GetComponent<UICommon_RewardView>();
                UICommonManager.Inst.AddUI(UICommonType.CommonRewardView, view);
                return view;
            case UICommonType.Common:
                UICommon_Common monster = go.GetComponent<UICommon_Common>();
                UICommonManager.Inst.AddUI(UICommonType.Common, monster);
                return monster;
            case UICommonType.CommonMsgBox:
                UICommon_MsgBox msgBox = go.GetComponent<UICommon_MsgBox>();
                UICommonManager.Inst.AddUI(UICommonType.CommonMsgBox, msgBox);
                return msgBox;
            case UICommonType.CommonGodSoul:
                UICommon_GodSoul godSoul = go.GetComponent<UICommon_GodSoul>();
                UICommonManager.Inst.AddUI(UICommonType.CommonGodSoul, godSoul);
                return godSoul;
            case UICommonType.CommonHero:
                UICommon_Hero hero = go.GetComponent<UICommon_Hero>();
                UICommonManager.Inst.AddUI(UICommonType.CommonHero, hero);
                return hero;
            case UICommonType.CommonHeroProp:
                UICommon_HeroProp heroProp = go.GetComponent<UICommon_HeroProp>();
                UICommonManager.Inst.AddUI(UICommonType.CommonHeroProp, heroProp);
                return heroProp;
            default:
                LogManager.LogError("不支持的通用UI类型" + type);
                return null;
        }
    }
}
