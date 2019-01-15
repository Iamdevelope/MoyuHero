using UnityEngine;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
using Platform;
using System.Collections;
using DreamFaction.Utils;

public class IOControler : BaseControler
{
    static  public IOControler mInst = null;
    private float m_CurLifeTime = 60 * 5;//5分钟
    private float m_lastTime    = 0;
    static private Hashtable m_reconnect = new Hashtable();     // 缓存消息

    static public IOControler GetInstance()
    {
        return mInst;
    }

    protected override void InitData()
    {
        mInst = this;
        if (Init())
        {
            m_lastTime = m_CurLifeTime;
        }
       
    }

    void Update()
    {
        base.UpdateData();
    }



    protected override void UpdateData()
    {
        HttpManager.GetInstance().run();
        SocketManager.GetInstance().run();
        TaskPool.GetInstance().run();

        // 链接状态
        if(SocketManager.GetState() == SocketState.state_connectok)
        {
            m_CurLifeTime -= Time.deltaTime;
            if (m_CurLifeTime <= 0)
            {
                // 心跳包
                KeepAlive alive = new KeepAlive();
                alive.code = 505;
                SendLink(alive);

                m_CurLifeTime = m_lastTime;
            }
        }
    }

    protected override void DestroyData()
    {
        UnInit();
    }

    public bool Init()
    {
        // 注册消息
        ProtocolList.RegisterProtocols();

        return true;
    }

    public void UnInit()
    {
        SocketManager.GetInstance().Uninit();
    }

    private static void Push(Protocol protocol)
    {
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_NetTips, GameUtils.getString("server_msg_tip1"));

        if (m_reconnect.ContainsKey(protocol.getType()))
        {
            m_reconnect.Remove(protocol.getType());
            m_reconnect.Add(protocol.getType(), protocol);
        }
        else
        {
            m_reconnect.Add(protocol.getType(), protocol);
        }
    }

    public static void fullSend()
    {
        if (m_reconnect.Count > 0)
        {
            foreach (DictionaryEntry de in m_reconnect)
            {
                mInst.SendProtocol((Protocol)de.Value);
            }
        }
        m_reconnect.Clear();

    }

    public static void Connect(string dwIP, ushort wPort)
    {
        //开始连接
        GetInstance().startLoading();

        SocketManager.OnOpen(dwIP, wPort);
    }

    // 客户端给GS发消息
    public void SendProtocol(Protocol protocol )
    {
        startLoading();

        Send msg = new Send();
        msg.ptype = protocol.getType();
        msg.pdata = new OctetsStream().marshal(protocol);

        if (!SocketManager.Send(msg))
        {
            stopLoading();
            Push(protocol);
        }
    }

    // link 指定消息
    public void SendLink( Protocol protocol )
    {
        if(!SocketManager.Send(protocol))
            Push(protocol);
    }

    public void SendPlatform( PlatformBase protocol )
    {
		StartCoroutine(HttpManager.GetInstance().SendPlatfom(protocol));
	}
	
    public void startLoading()
    {
        Invoke("Load", 0.3f);
    }

    public void stopLoading()
    {
        CancelInvoke("Load");
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_Connection.UI_ResPath);
    }

    public void Load()
    {
       GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_Connection.UI_ResPath);
    }
};

