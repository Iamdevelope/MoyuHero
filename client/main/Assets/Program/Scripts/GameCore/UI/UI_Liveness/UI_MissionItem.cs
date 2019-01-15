using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.UI;
using System.Text;
using DreamFaction.Utils;
using GNET;
public class UI_MissionItem : UI_MissionItemManage
{
    public int _GotoType;
    public GameObject m_Achieve;
    public override void InitUIData()
    {
        base.InitUIData();
        m_Achieve = selfTransform.FindChild("CompletedBG").gameObject;
    }
    //显示任务面板上的信息
    public void UpdateShow(Huoyue _huoyue)
    {
        _GotoType = _huoyue.huoyuetype;
        if (_huoyue.isok==1)
        {
            m_Achieve.SetActive(true);
            m_GOBtn.gameObject.SetActive(false);
        }
        else
        {
            m_Achieve.SetActive(false);
            m_GOBtn.gameObject.SetActive(true);
        }
        ActivitymissionTemplate mission = (ActivitymissionTemplate)DataTemplate.GetInstance().m_ActivitymissionTable.getTableData(_huoyue.huoyueid);
        m_GetLivenessNumText.text = mission.getActivitydegree().ToString();
        m_MissionDesText.text = GameUtils.getString(mission.getDes());
        m_LivenessNumMin.text = _huoyue.num.ToString();
        m_LivenssNumMax.text = _huoyue.numall.ToString();
    }
    //根据任务类型来判断前往不懂的界面
    protected override void OnClickGOBtn()
    {
        base.OnClickGOBtn();
        switch (_GotoType)
        {
            case 1:
            case 2:
            case 3:
            case 13:
            case 14:
                //关卡选择界面
                //UI_HomeControler.Inst.AddUI(UI_SelectFightArea.UI_ResPath);
                break;
            case 16:
                //宝藏遗迹
                break;
            case 19:
            case 20:
            case 24:
                //历练界面
                UI_HomeControler.Inst.AddUI(UI_LimitTest.UI_ResPath);
                break;
            case 8:
            case 9:
                //符文界面
                UI_HomeControler.Inst.AddUI(UI_Bag.UI_ResPath);
                break;
            case 11:
            case 12:
            case 21:
            case 23:
                //玩法界面
                UI_HomeControler.Inst.AddUI(UI_PlayingMethod.UI_ResPath);
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
