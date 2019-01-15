using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshLotty: Protocol
	{

        public Lotty lotty;   // ” º˛œÍœ∏–≈œ¢

        public const int PROTOCOL_TYPE = 788735;

        public SRefreshLotty()
            : base(PROTOCOL_TYPE)
		 {
             lotty = new Lotty();
		 } 

		public override object Clone()
		{
            SRefreshLotty obj = new SRefreshLotty();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(lotty);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            lotty.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
			ObjectSelf.GetInstance().freetime = lotty.freetime;
			ObjectSelf.GetInstance().firstget = lotty.firstget;
			ObjectSelf.GetInstance().dreamexp = lotty.dreamexp;
			ObjectSelf.GetInstance().dreamfree = lotty.dreamfree;
			ObjectSelf.GetInstance().dream = lotty.dream;

            ObjectSelf.GetInstance().SetJiuGuanData(lotty);

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_JiuGuanDataUpdate);
		}
	}	
}
