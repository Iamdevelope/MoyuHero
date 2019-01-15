using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using System.Collections.Generic;
using GNET;
using DreamFaction.GameCore;

public class UI_TestPanel : UI_BaseTestPanel
{
    private static readonly string Path = "UI_WorldBoss/UI_TestPanel_2_10";

    private bool m_IsWaitingBossShop = false;
    private bool m_IsWaitingGetBossRank = false;
    private bool m_IsWaitingGetWorldBoss = false;

    private bool m_GotoWorldBoss = false;
    private Transform m_CaptionLayoutPoint;

    private IFunctionTipsController m_TipsController;
    public static string GetPath()
    {
        return Path;
    }

    public override void InitUIData()
    {
        base.InitUIData();
        m_TipsController = CreateFunctionTipsController();
        m_CaptionLayoutPoint = selfTransform.FindChild("Background/LayoutPoint");
        HomeControler.Inst.PushFunly(10, 97);

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_InterfaceChange, m_TipsController.Refresh);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetWorldBoss, m_TipsController.Refresh);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        m_TopTittleText.text = GameUtils.getString("Flounder_content1");
        m_BackText.text = GameUtils.getString("common_button_return");
        m_LengendWarBtnText.text = GameUtils.getString("legend_of_the_war_content29");
        m_LimitTestBtnText.text = GameUtils.getString("ultimatetrial_content20");
        m_NoneBtnText.text = GameUtils.getString("legend_of_the_war_content24");

        UI_CaptionManager.GetInstance().AwakeUp(m_CaptionLayoutPoint);
        if (m_GotoWorldBoss)
        {
            OnClickLengendWarBtn();
        }
    }
    public void GotoWorldBoss()
    {
        m_GotoWorldBoss = true;
    }

    protected override void OnClickBackBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(Path);
    }
    protected override void OnClickLengendWarBtn()
    {
        //m_ProtocolQeuee.Enqueue(new ProtocolPackage(new CBossShop(), GameEventID.G_SBossShop));
        //m_ProtocolQeuee.Enqueue(new ProtocolPackage(new CGetBossRank(), GameEventID.G_SGetBossRank));
        //m_ProtocolQeuee.Enqueue(new ProtocolPackage(new CGetWordBoss(), GameEventID.G_GetWorldBoss));
        //SendProtocolQueue();

        TimeUtils.SyncServerTime();
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_SBossShop, OnReceiveBossShop);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_SGetBossRank, OnReceiveGetBossRank);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetWorldBoss, OnReceiveWorldBoss);
        m_IsWaitingBossShop = true;
        m_IsWaitingGetBossRank = true;
        m_IsWaitingGetWorldBoss = true;
        IOControler.GetInstance().SendProtocol(new CBossShop());
        IOControler.GetInstance().SendProtocol(new CGetBossRank());
        IOControler.GetInstance().SendProtocol(new CGetWordBoss());

    }
    protected override void OnClickLimitTestBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_LimitTest.UI_ResPath);
    }

    public void GotoLimitWindow()
    {
        OnClickLimitTestBtn();
    }


    protected void AddUI_WorldBoss()
    {
        var go = UI_HomeControler.Inst.AddUI(UI_WorldBoss.GetPath());
        if (m_GotoWorldBoss)
        {
            m_GotoWorldBoss = false;
            UI_WorldBoss _worldBoss = go.GetComponent<UI_WorldBoss>();
            _worldBoss.ComebackFormBattle();
        }
    }

    //protected void SendProtocolQueue()
    //{
    //    if (m_LastProtocol != null)
    //    {
    //        GameEventDispatcher.Inst.removeEventListener(m_LastProtocol.m_CallbackEventID, SendProtocolQueue);
    //    }
    //    if (m_ProtocolQeuee.Count == 0) //没有继续要发的协议了
    //    {
    //        m_LastProtocol = null;
    //        AddUI_WorldBoss();
    //    }
    //    else if (m_ProtocolQeuee.Count > 0)
    //    {
    //        ProtocolPackage _pack = m_ProtocolQeuee.Dequeue();
    //        GameEventDispatcher.Inst.addEventListener(_pack.m_CallbackEventID, SendProtocolQueue);
    //        m_LastProtocol = _pack;
    //        IOControler.GetInstance().SendProtocol(_pack.m_Protocol);
    //    }
    //}

    private bool WaitingMessageCheck()
    {
        return m_IsWaitingBossShop | m_IsWaitingGetBossRank | m_IsWaitingGetWorldBoss;
    }
    private void OnReceiveBossShop()
    {
        m_IsWaitingBossShop = false;
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBossShop, OnReceiveBossShop);
        if (!WaitingMessageCheck())
            AddUI_WorldBoss();
    }
    private void OnReceiveGetBossRank()
    {
        m_IsWaitingGetBossRank = false;
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SGetBossRank, OnReceiveGetBossRank);
        if (!WaitingMessageCheck())
            AddUI_WorldBoss();
    }
    private void OnReceiveWorldBoss()
    {
        m_IsWaitingGetWorldBoss = false;
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetWorldBoss, OnReceiveWorldBoss);
        if (!WaitingMessageCheck())
            AddUI_WorldBoss();
    }



    void OnDestroy()
    {
        var _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            UI_CaptionManager.GetInstance().Release(m_CaptionLayoutPoint);
        if (m_IsWaitingBossShop)
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBossShop, OnReceiveBossShop);
        if (m_IsWaitingGetBossRank)
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SGetBossRank, OnReceiveGetBossRank);
        if (m_IsWaitingGetWorldBoss)
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetWorldBoss, OnReceiveWorldBoss);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_InterfaceChange, m_TipsController.Refresh);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetWorldBoss, m_TipsController.Refresh);
        //if (m_LastProtocol != null)
        //    GameEventDispatcher.Inst.removeEventListener(m_LastProtocol.m_CallbackEventID, SendProtocolQueue);
    }

    //生成功能提示控制器
    IFunctionTipsController CreateFunctionTipsController()
    {
        GameObject _go;
        FunctionTipsController _controller = new FunctionTipsController();

        var _manager = FunctionTipsManager.GetInstance();
        _go = selfTransform.FindChild("Layout/LengendWarBtn/FlashTips").gameObject;
        _controller.AddControlledObject(_go, _manager.CheckWorldBoss);

        _go = selfTransform.FindChild("Layout/LimitTestBtn/FlashTips").gameObject;
        _controller.AddControlledObject(_go, _manager.CheckLimitTest);

        return _controller;
    }

}
