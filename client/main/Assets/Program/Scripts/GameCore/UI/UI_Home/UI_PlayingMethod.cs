using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;

public class UI_PlayingMethod : UI_PlayingMethodManage
{

    public static string UI_ResPath = "UI_Home/UI_PlayingMethod_2_1";
    private IFunctionTipsController m_TipsController;

    public override void InitUIData()
    {
        base.InitUIData();
        m_Name.text = GameUtils.getString("muse_music_content6");
        m_GetPowerText.text = GameUtils.getString("muse_music_title");
        m_SacredAltarText.text = GameUtils.getString("54activitymission33des").Substring(0,4);
        m_ExplorationText.text = GameUtils.getString("muse_music_content7");
        HomeControler.Inst.PushFunly(11, 109);

        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(M_CapPos);

        m_TipsController = CreateFunctionTipsController();
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_InterfaceChange, m_TipsController.Refresh);
    }
    void OnDestroy()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(M_CapPos);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_InterfaceChange, m_TipsController.Refresh);
    }
    //缪斯奏曲
    protected override void OnClickGetPowerBtn()
    {
        base.OnClickGetPowerBtn();
        UI_HomeControler.Inst.AddUI(UI_GetPower.UI_ResPath);

        //UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }
    //神圣祭坛
    protected override void OnClickSacredAltarBtn()
    {
        base.OnClickSacredAltarBtn();
        UI_HomeControler.Inst.AddUI(UI_SacredAltar.UI_ResPath);
        //UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }
    //探险
    protected override void OnClickExplorationBtn()
    {
        base.OnClickExplorationBtn();
        //UI_HomeControler.Inst.AddUI(UI_ExploreMgr.UI_ResPath);
        if (UI_ExploreModule.CheckAndOpenExploreUI())
        {
            //UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
        }
    }
    protected override void OnClickBackBtn()
    {
        base.OnClickBackBtn();
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    //生成功能提示控制器
    IFunctionTipsController CreateFunctionTipsController()
    {
        GameObject _go;
        FunctionTipsController _controller = new FunctionTipsController();

        var _manager = FunctionTipsManager.GetInstance();
        _go = selfTransform.FindChild("UI_Main/GetPowerBtn/FlashTips").gameObject;
        _controller.AddControlledObject(_go, _manager.CheckInGetPowerTime);

        _go = selfTransform.FindChild("UI_Main/SacredAltarBtn/FlashTips").gameObject;
        _controller.AddControlledObject(_go, _manager.CheckSacredAltar);

        _go = selfTransform.FindChild("UI_Main/ExplorationBtn/FlashTips").gameObject;
        _controller.AddControlledObject(_go, _manager.CheckExploreAward);

        return _controller;
    }
}
