using UnityEngine;
using System.Collections.Generic;
using GNET;
using DreamFaction.LogSystem;

public  class NetSession
{
    private   LinkedList<OctetsStream> os = new LinkedList<OctetsStream>();
    private   Stream m_is = null;

    // 缓存数据
    public Octets ibuffer = new Octets(8192);
    public Octets obuffer = new Octets(8192);
    public Octets isecbuf = new Octets(8192);

    public NetSession(PollIO stream)
    {
        if (m_is == null)
            m_is = new Stream(this);
    }

    public void Uninit()
    {
        if (m_is != null )
            m_is = null;
    }

    public bool Output(Octets paramOctets)
    {
        if (paramOctets.size() + obuffer.size() > obuffer.capacity())
        {
            LogManager.LogError("FATAL, data overflow MAXIUM buffer size, obuffer=" + obuffer.size() +"capacity="
                + obuffer.capacity() + "Octets=" + paramOctets.size());
            return false;
        }

        obuffer.insert(obuffer.size(), paramOctets);

        return true;
    }

    public Octets Input()
    {
        isecbuf.insert(isecbuf.size(), ibuffer);
        ibuffer.clear();
        return isecbuf;
    }

    public void OnRecv()
    {
        if (m_is == null)
            return;

        Octets localOctets = Input();
        m_is.insert(m_is.size(), localOctets);
        localOctets.clear();
        try
        {
            Protocol localProtocol;
            while ((localProtocol = Protocol.Decode(m_is)) != null)
                Task.Dispatch(localProtocol);
        }
        catch (System.Exception ex)
        {
            LogManager.LogError(ex.Message);//消息包异常
        }
    }

    public void OnSend()
    {
        if ( os.Count != 0 )
        {
            do
            {
                OctetsStream localOctetsStream = os.First.Value;
                if (!Output(localOctetsStream))
                    break;
                os.RemoveFirst();
            }
            while (os.Count != 0);
        }
  
    }

    public Octets GetIBuffer()
	{
		return ibuffer;
	}

    public Octets GetOBuffer()
    {
        return obuffer;
    }

    public int PriorPolicy(int paramInt)
    {
        return Protocol.GetStub(paramInt).PriorPolicy();
    }

    public bool InputPolicy(int paramInt1, int paramInt2)
    {
        return Protocol.GetStub(paramInt1).SizePolicy(paramInt2);
    }

    public bool Send( Protocol nProtocol )
    {
        if (nProtocol == null )
            return false;

        OctetsStream localOctetsStream = new OctetsStream();
        nProtocol.Encode(localOctetsStream);
        if (nProtocol.SizePolicy(localOctetsStream.size()))
        {
            os.AddLast(localOctetsStream);
            return true;
        }
            
        return false;
     }
};