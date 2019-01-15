using DreamFaction.GameNetWork;
using DreamFaction.GameCore;

namespace GNET
{
    public partial class SEnterWorld: Protocol
	{

        public RoleDetail mydata;

        public const int PROTOCOL_TYPE = 786438;

        public SEnterWorld()
            : base(PROTOCOL_TYPE)
        {
            mydata = new RoleDetail();
        }

		public override object Clone()
		{
            SEnterWorld obj = new SEnterWorld();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(mydata);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            mydata.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 655350; }

        public override void Process() 
        {
            ObjectSelf.GetInstance().Copy(mydata);
            if ( SceneManager.Inst.CurScene.Equals(SceneEntry.Login.ToString()))
            {
                SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
            }
            IOControler.fullSend();
        }
	}	
}
