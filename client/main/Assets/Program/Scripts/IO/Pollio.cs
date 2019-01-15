using UnityEngine;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;

public class PollIO : CThread
{
   
    public bool          m_bRun = true;
    public NetSession    m_poSession = null;
    public static PollIO mPollIO = null;

    private const int    m_dwRecvSize = 16384;
    private const int    m_dwSendSize = 16384;

    private TcpClient myTcpClient = null;
    private delegate void ReadMsgCallBack();
    private ReadMsgCallBack readMsgCallBack = null;

    public ManualResetEvent connectDone = new ManualResetEvent(false);

  
    public PollIO( )
    {
        if (m_poSession == null)
            m_poSession = new NetSession(this);

        if(readMsgCallBack == null)
            readMsgCallBack  = new ReadMsgCallBack(asyncread);

        m_bRun = true;
    }

    private void asyncread()
    {
		try
		{
        	Octets nOctest = m_poSession.GetIBuffer();
	        if (myTcpClient.GetStream().CanRead)
	        {
                int numberOfBytesRead = 0;
                numberOfBytesRead = myTcpClient.GetStream().Read(nOctest.array(), 0, nOctest.capacity());
                if (numberOfBytesRead > 0)
                {
                    nOctest.resize(numberOfBytesRead);
                }
                else
                {
                    Close();
                }
            }
        }
		catch (Exception localException)
		{
			Debug.Log(localException.Message);
			Close();
		}
    }

    public void asyncwrite()
    {
		try
		{
	        m_poSession.OnSend();
	        Octets nOctest = m_poSession.GetOBuffer();
       		 if (nOctest.size() == 0)
            	return;

        	if( myTcpClient.GetStream().CanWrite )
        	{
                myTcpClient.GetStream().Write(nOctest.array(), 0, nOctest.size());
                myTcpClient.GetStream().Flush();
                nOctest.erase(0, nOctest.size());
            }
        }
		catch (Exception localException)
		{
			Debug.Log(localException.Message);
			Close();
		}
    }

    public void UnInit()
    {
        m_bRun = false;
        stop();
        
        if (m_poSession != null)
        {
            m_poSession.Uninit();
            m_poSession = null;
        }
        if (readMsgCallBack != null)
            readMsgCallBack = null;

        myTcpClient.Close();
    }


    public void Open(string IpAddr, int port)
    {
        try
        {
            connectDone.Reset();

            myTcpClient = new TcpClient();
            myTcpClient.SendTimeout = 1000;
            myTcpClient.ReceiveTimeout = 1000;
            myTcpClient.BeginConnect(IPAddress.Parse(IpAddr), port, new AsyncCallback(ConnectCallback), myTcpClient);

            connectDone.WaitOne();

        }
        catch (SocketException ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void ConnectCallback(IAsyncResult ar)
    {
        connectDone.Set();
      
        TcpClient client = (TcpClient)ar.AsyncState;
        if (client.Connected == true)
        {
            client.EndConnect(ar);

            SocketManager.GetInstance().PushEvent(EvtType.netevt_establish);

        }
        else
        {
            SocketManager.GetInstance().PushEvent(EvtType.netevt_fail);
        }
    }

    public void Close()
    {
		myTcpClient.Close();
        SocketManager.GetInstance().PushEvent(EvtType.netevt_fail);
    }

	public override void abort() {
		m_bRun = false;
	}

    public override void run()
    {
        while (m_bRun)
        {
            if (SocketManager.GetState() == SocketState.state_connectok)
            {
                //--开始异步接收消息
                IAsyncResult result = readMsgCallBack.BeginInvoke(null, null);
                while(!result.IsCompleted)
                {
                    Thread.Sleep(10);
                }
                //---结束异步
                readMsgCallBack.EndInvoke(result);

				lock(m_poSession)
				{
					if(m_poSession.GetIBuffer().size() > 0 )
					{
						m_poSession.OnRecv();
					}
				}

            }
        }
    }
};


