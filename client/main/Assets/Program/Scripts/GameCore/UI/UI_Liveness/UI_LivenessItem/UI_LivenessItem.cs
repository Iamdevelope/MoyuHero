using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GNET;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.UI;

public class UI_LivenessItem : UI_LivenessItemBase
{
    private int _GotoType;
   
    public void Data(object data)
    {
        UI_Liveness.HuoYueData profile = data as UI_Liveness.HuoYueData;
        if (profile == null) return;
        _GotoType = profile.m_huoyuetype;
        ActivitymissionTemplate staticData = DataTemplate.GetInstance().GetActivitymissionTemplateById(profile.m_huoyueid);

        //LivenessIcon.overrideSprite = 
        m_LivenessDesText.text = GameUtils.getString("liveness_content2");
        m_GetLivenessNumText.text = staticData.getActivitydegree().ToString();
        m_MissionDesText.text = GameUtils.getString(staticData.getDes());
        m_Finish.text = GameUtils.getString("liveness_content3");
        m_LivenessNumMin.text = profile.m_num.ToString();
        m_LivenessNumMax.text = "/"+profile.m_numall.ToString();

        bool flag = profile.m_isok == 1;
        m_CompletedBG.SetActive(flag);
        m_GOBtn.gameObject.SetActive(!flag);
        
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();
        switch (_GotoType)
        {
            case 1:
            case 2:
            case 3:
            case 13:
            case 14:
                //关卡选择界面
                UI_HomeControler.Inst.AddUI(UI_SelectLevelMgrNew.UI_ResPath);
                break;
            case 16:
                //宝藏遗迹
                GameObject panel= UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
                if (panel)
                {
                    panel.GetComponent<UI_Recruit>().OpenRelicBtn();
                }
                break;
            case 19:
            case 20:
                //极限试炼
                UI_HomeControler.Inst.AddUI(UI_LimitTest.UI_ResPath);// 极限试炼
                //UI_HomeControler.Inst.AddUI(UI_TestPanel.GetPath());
                break;
            case 24:
                //世界boss界面
                WorldBossPanelController temp = new WorldBossPanelController();
                temp.OpenWorldPanel(false,false);
                break;
            case 8:
            case 9:
                //背包界面
                UI_HomeControler.Inst.AddUI(UI_Bag.UI_ResPath);
                break;
            case 11:
            case 12:
                //探险界面
                UI_ExploreModule.CheckAndOpenExploreUI();
                break;
            case 21:
                //缪斯奏曲界面
                UI_HomeControler.Inst.AddUI(UI_GetPower.UI_ResPath);
                break;
            case 23:
                //许愿台界面
                UI_HomeControler.Inst.AddUI(UI_SacredAltar.UI_ResPath);
                break;
            case 17:
            case 25:
                //商店道具页
                UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);
                break;
            case 15:
                //招募界面
                UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
                break;
            case 10:
                //神器界面
                UI_HomeControler.Inst.AddUI(UI_Artifact.UI_ResPath);
                break;
            case 22:
                //无需跳转
                break;
            case 18:
                //商店金币页
                UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);
                UI_ShopMgr.SetCurShowTab(SHOP_TAB.GOLD);
                break;
            case 4:
                //英雄信息界面
                FailButtonClick(0);
                break;
            case 5:
                //技能提升界面
                FailButtonClick(3);
                break;
            case 6:
                //英雄熔灵界面
                UI_HomeControler.Inst.AddUI(UI_HeroLitholysin.UI_ResPath);
                break;
            case 26:
                //符文熔炼界面
                UI_HomeControler.Inst.AddUI(UI_RuneExp.UI_ResPath);
                break;
            case 7:
                //英雄培养界面
                FailButtonClick(2);
                break;
            default:
                break;
        }
        UI_HomeControler.Inst.ReMoveUI(UI_Liveness.UI_ResPath);
    }
    private void FailButtonClick(int type)
    {
        UI_HomeControler.Inst.AddUI(UI_HeroInfo.UI_ResPath);
        UI_HeroInfo._instance.DefeatShow(type);
    }
}
