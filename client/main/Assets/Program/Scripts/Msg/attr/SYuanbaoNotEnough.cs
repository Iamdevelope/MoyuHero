using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class SYuanbaoNotEnough: Protocol
	{

        public const int PROTOCOL_TYPE = 787444;

        public SYuanbaoNotEnough()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SYuanbaoNotEnough obj = new SYuanbaoNotEnough();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{


            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{

		}
	}	
}
