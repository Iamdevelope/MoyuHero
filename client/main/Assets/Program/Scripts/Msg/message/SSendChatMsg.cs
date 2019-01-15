using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SSendChatMsg: Protocol
	{


        public const int PROTOCOL_TYPE = 787635;

        public SSendChatMsg()
            : base(PROTOCOL_TYPE)
		 {
            
		 } 

		public override object Clone()
		{
            SSendChatMsg obj = new SSendChatMsg();
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
