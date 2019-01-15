using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using GNET;

public class WorldBossPanelController
{
    private bool m_IsWaitingBossShop = false;
    private bool m_IsWaitingGetBossRank = false;
    private bool m_IsWaitingGetWorldBoss = false;
    private bool m_IsWaitingGetMyWorldBoss = false;
    private bool m_NeedTestPanel = false;
    private bool m_ComeBackFromBattle = false;
    private bool WaitingMessageCheck()
    {
        return m_IsWaitingBossShop | m_IsWaitingGetBossRank | m_IsWaitingGetWorldBoss | m_IsWaitingGetMyWorldBoss;
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
    private void OnReceiveMyWorldBoss()
    {
        m_IsWaitingGetMyWorldBoss = false;
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetMyWorldBoss, OnReceiveMyWorldBoss);
        if (!WaitingMessageCheck())
            AddUI_WorldBoss();
    }
    private void AddUI_WorldBoss()
    {
        if (m_NeedTestPanel)
            UI_HomeControler.Inst.AddUI(UI_TestPanel.GetPath());
        if (m_ComeBackFromBattle)
            UI_HomeControler.Inst.AddUI(UI_WorldBoss.GetPath()).GetComponent<UI_WorldBoss>().ComebackFormBattle();
        else
            UI_HomeControler.Inst.AddUI(UI_WorldBoss.GetPath());
    }

    public void OpenWorldPanel(bool comeBackFromBattle, bool needTestPanel = true)
    {
        m_ComeBackFromBattle = comeBackFromBattle;
        m_NeedTestPanel = needTestPanel;

        if (comeBackFromBattle)
        {
            int _bossId = GetCurrentActiveBoss();
            if (_bossId == 2 || _bossId == 4)
            {
                m_IsWaitingGetMyWorldBoss = true;
                GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetMyWorldBoss, OnReceiveMyWorldBoss);
                var _cGetMyWordBoss = new CGetMyWordBoss();
                _cGetMyWordBoss.bossid = _bossId;
                IOControler.GetInstance().SendProtocol(new CGetMyWordBoss());
            }
            else
            {
                m_IsWaitingGetMyWorldBoss = false;
            }
        }
        else
            m_IsWaitingGetMyWorldBoss = false;

//        TimeUtils.SyncServerTime();
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

    private int GetCurrentActiveBoss()
    {
        int result = -1;
        foreach (var data in ObjectSelf.GetInstance().WorldBossMgr.m_BossDataMap.Values)
        {
            if (data.m_IsOpen > 0)//已开启
            {
                result = data.m_BossType;
                break;
            }
        }
        return result;
    }
}
