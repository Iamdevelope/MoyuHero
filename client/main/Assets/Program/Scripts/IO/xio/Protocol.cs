
using System.Collections;
using GNET;

public abstract class Protocol : Marshal
{
    public abstract void Process();
    public abstract object   Clone();
    public abstract int PriorPolicy();
    public abstract bool SizePolicy(int paramInt);
    protected int m_Type = 0;
    protected int prior_policy = 0;
    protected int size_policy = 0;
   

    private static Hashtable m_Protocol = new Hashtable(); // 消息列表
    public  Hashtable GetHashProtocol() { return m_Protocol; }

    public static int typeHigh(int type) { return type >> 16; }
    public static int typeLow(int type) { return type & 0x0000ffff; }

    public Protocol(int Type )
    {
        m_Type = Type;
    }

    // 注册消息
    public static void Register(int Type,object proto)
    {
        if (!m_Protocol.ContainsKey(Type))
        {
            m_Protocol.Add(Type,proto);
        }
        else
        {
            System.Console.Write("is have Protocol waring [ type = " + Type + "]");
        }
    }

    public int getType()
    {
        return this.m_Type;
    }

    // 译码
    public void Encode(OctetsStream paramOctetsStream)
    {
        paramOctetsStream.compact_uint32(this.m_Type).marshal(new OctetsStream().marshal(this));
    }

    // 解码
    public static Protocol Decode(Stream paramStream)
    {

        Protocol localProtocol = null;
        int i = 0;
        int j = 0;

        try
        {

            bool loop = true;
            while (loop)
            {

                if (paramStream.eos())
                    return null;

                if (paramStream.check_policy)
                {
                    paramStream.Begin();
                    i = paramStream.uncompact_uint32();
                    j = paramStream.uncompact_uint32();
                    paramStream.Rollback();
                    if (!paramStream.session.InputPolicy(i, j))
                    {
                        System.Console.Write("Protocol Decode CheckPolicy Error:type=" + i + ",size=" + j);
                        throw new MarshalException();
                    }
                    paramStream.check_policy = false;
                    paramStream.checked_size = j;
                }

                Stream localStream = new Stream(paramStream.session, paramStream.checked_size);
                paramStream.Begin();
                i = paramStream.uncompact_uint32();
                paramStream.unmarshal(localStream);
                paramStream.Commit();

                if ((localProtocol = Protocol.Create(i)) != null)
                {
                    localProtocol.unmarshal(localStream);
                    loop = false;
                }
                else
                {
                    System.Console.Write("Protocol Decode Error:type=" + i);
                }

                paramStream.check_policy = true;
            }
        }
        catch (MarshalException localMarshalException)
        {
            paramStream.Rollback();
            if (localProtocol != null)
            {
                System.Console.Write("Protocol Decode Unmarshal Error:type=" + i + ",size=" + j + localMarshalException.Message);
                throw new MarshalException();
            }
            System.Console.Write("Protocol Decode Warning:uncomplete data,protocol type=" + i + ",size=" + j);
        }

        return localProtocol;
    }

    public static  Protocol Create(int type)
    {
        Protocol stub = GetStub(type);
        if (stub == null )
        {
            return null;
        }
        return (Protocol)stub.Clone();
    }

    public static Protocol GetStub(int type)
    {
        if (m_Protocol.ContainsKey(type) )
        {
            return m_Protocol[type] as Protocol;
        }
        return null;
    }
    
    

}
