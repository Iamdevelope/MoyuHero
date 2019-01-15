
namespace GNET
{
    public class Stream : OctetsStream
    {

        public NetSession session = null;
        public bool check_policy = true;
        public int checked_size = 0;

        public Stream(NetSession paramSession)
        {
            this.session = paramSession;
        }

        public Stream(NetSession paramSession, int paramInt)
            : base(paramInt)
        {
            this.session = paramSession;
        }
    }
}
