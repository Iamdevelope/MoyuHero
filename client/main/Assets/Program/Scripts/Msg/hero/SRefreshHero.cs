using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshHero: Protocol
	{

        public Hero heroinfo;

        public const int PROTOCOL_TYPE = 787735;

        public SRefreshHero()
            : base(PROTOCOL_TYPE)
		 {
             heroinfo = new Hero();
		 } 

		public override object Clone()
		{
            SRefreshHero obj = new SRefreshHero();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(heroinfo);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            heroinfo.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().HeroContainerBag.RefreshHero(heroinfo);
            //GameEventDispatcher.Inst.dispatchEvent(GameEventID.HE_PeiyangUp); 
		}
	}	
}
