using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SGetMSZQ: Protocol
	{
        public const int END_OK = 1; // ³É¹¦
        public const int END_ERROR = 2; // Ê§°Ü

	    public int result;

        public const int PROTOCOL_TYPE = 789037;

        public SGetMSZQ()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SGetMSZQ obj = new SGetMSZQ();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            if (result==END_OK)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_GetPower);
            }

        }
	}	
}
