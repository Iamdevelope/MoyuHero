using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using System.Threading;
using System;

public enum SocketState
{
    state_invalid       = 0,    //无连接状态
    state_connect       = 1,    //链接中
    state_connectok     = 2,    //连接OK
};

public enum EvtType
{
    netevt_invalid    = 0,
    netevt_connect    = 1,
    netevt_establish  = 2,
    netevt_fail       = 3,
};

public class SNetEvent
{
    public EvtType eType;
    public int value;

    public SNetEvent()
    {
        eType = EvtType.netevt_invalid;
        value = 0;
    }
};

public class SocketManager
{
    private string      m_szRemoteIP;
    private ushort      m_wPort;
    public  static      SocketState m_nState = SocketState.state_invalid;
    public  static      PollIO m_Pollio = null;


    // 事件队列
    static private Queue<SNetEvent> m_oEvtQueue = new Queue<SNetEvent>();

    static private SocketManager mInst = null;
    static public SocketManager GetInstance()
    {
        if (mInst == null)
        {
            mInst = new SocketManager();
        }
        return mInst;
    }


    public static void OnOpen(string dwIP, ushort wPort)
    {
        if (m_Pollio == null)
            m_Pollio = new PollIO();

        mInst.SetRemoteIP(dwIP);
        mInst.SetRemotePort(wPort);
        SetState(SocketState.state_connect);

        // 设置连接事件
        mInst.PushEvent(EvtType.netevt_connect);

        
    }

    public static bool Send(Protocol protocol)
    {
        if (GetState() == SocketState.state_connectok && m_Pollio != null )
        {
            return m_Pollio.m_poSession.Send(protocol);
        }
        return false;
    }

    public void Uninit()
    {
        if(m_Pollio != null)
        {
            m_Pollio.UnInit();
            m_Pollio = null;
        }
    }

    private void OnDelSession( )
    {
        Uninit();

        SetState(SocketState.state_invalid);

        IOControler.GetInstance().stopLoading();

        if (SceneManager.Inst.CurScene.Equals(SceneEntry.Fight.ToString()) == false) // 战斗场景屏蔽tips框 [7/17/2015 Zmy]
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_NetTips, GameUtils.getString("server_msg_tip1"));  
        }
        else
        {
            if (FightControler.Inst.GetFightState() == FightState.FightLose || FightControler.Inst.GetFightState() == FightState.FightWin)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_NetTips, GameUtils.getString("server_msg_tip1"));  
            }
        }
    }

    private void OnAddSession()
    {
        SetState(SocketState.state_connectok);

        m_Pollio.start();//开启线程

        IOControler.GetInstance().stopLoading();
    }

    public void OnConnect()
    {
        m_Pollio.Open(GetRemoteIPStr(), GetRemotePort());
    }

    public void PushEvent(EvtType eType, int value = 0)
    {
        SNetEvent oEvent = new SNetEvent();
        oEvent.eType = eType;
        oEvent.value = value;

        lock (m_oEvtQueue)
        {
            m_oEvtQueue.Enqueue(oEvent);
        }
    }

    public void run()
    {

        if (GetState() == SocketState.state_connectok)
        {
            if (m_Pollio != null)
                m_Pollio.asyncwrite();
        }

        if (m_oEvtQueue.Count < 1 )
        {
            return;
        }

        lock (m_oEvtQueue)
        {
            SNetEvent events = m_oEvtQueue.Dequeue();
            if (events != null)
            {
                switch (events.eType)
                {
                    case EvtType.netevt_connect:
                        {
                            OnConnect();
                        }
                        break;
                    case EvtType.netevt_establish:
                        {
                            OnAddSession();
                        }
                        break;
                    case EvtType.netevt_fail:
                        {
                            OnDelSession();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        
    }


    public static void SetState(SocketState eState){ m_nState = eState; }
    public static SocketState GetState(){return m_nState; }
    public void  SetRemoteIP(string dwIP){ m_szRemoteIP = dwIP; }
    public  string GetRemoteIPStr() { return m_szRemoteIP; }
    public void SetRemotePort(ushort wPort){ m_wPort = wPort;}
    public ushort GetRemotePort() { return m_wPort; }

}