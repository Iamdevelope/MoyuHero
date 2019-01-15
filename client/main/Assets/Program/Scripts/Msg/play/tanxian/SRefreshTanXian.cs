using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshTanXian: Protocol
	{

        public stagetxall tanxianinfo;

        public const int PROTOCOL_TYPE = 788983;

        public SRefreshTanXian()
            : base(PROTOCOL_TYPE)
		 {
             tanxianinfo = new stagetxall();
		 } 

		public override object Clone()
		{
            SRefreshTanXian obj = new SRefreshTanXian();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(tanxianinfo);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            tanxianinfo.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().SetExploreData(tanxianinfo);

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ExploreData_Update);
		}
	}	
}
