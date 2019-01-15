using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.UI.Core;
namespace GNET
{
	public partial class SRefreshChargeSum: Protocol
	{

        public int chargesum;

        public const int PROTOCOL_TYPE = 787445;

        public SRefreshChargeSum()
            : base(PROTOCOL_TYPE)
		 {
             chargesum = 0;
		 } 

		public override object Clone()
		{
            SRefreshChargeSum obj = new SRefreshChargeSum();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(chargesum);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            chargesum = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 64; }

        public override void Process() 
		{
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("pay_bubble1"), chargesum), UI_HomeControler.Inst.GetTopTransform());
		}
	}	
}
