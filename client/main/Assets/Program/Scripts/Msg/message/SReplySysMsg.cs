using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SReplySysMsg: Protocol
	{

        public const int PROTOCOL_TYPE = 787639;

        public LinkedList<SysMsg> msgs; // 只发这次上线后没发过的，旧的在前，新的在后

        public SReplySysMsg()
            : base(PROTOCOL_TYPE)
		 {
             msgs = new LinkedList<SysMsg>();
		 }

		public override object Clone()
		{
            SReplySysMsg obj = new SReplySysMsg();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
           
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process() 
        {
           
        }
	}	
}
