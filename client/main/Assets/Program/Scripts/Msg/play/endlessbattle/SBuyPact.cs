using DreamFaction.GameEventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GNET
{
	public partial class SBuyPact: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

	    public int result;
        public int pactid; // 强者之约ID

        public const int PROTOCOL_TYPE = 788938;

        public SBuyPact()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SBuyPact obj = new SBuyPact();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(pactid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            pactid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {

            if (result == END_OK)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_LimitPactOk);
            }
        }
	}	
}
