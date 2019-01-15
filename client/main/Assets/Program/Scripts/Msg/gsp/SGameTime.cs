using DreamFaction.GameNetWork;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class SGameTime: Protocol
	{

        public long gametime;

        public const int PROTOCOL_TYPE = 786439;

        public SGameTime()
            : base(PROTOCOL_TYPE)
		 {
             gametime = 0;
		 } 

		public override object Clone()
		{
            SGameTime obj = new SGameTime();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(gametime);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            gametime =  os.unmarshal_long();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 10; }

        public override void Process() 
        {
            ObjectSelf.GetInstance().ServerTime = gametime;
        }
	}	
}
