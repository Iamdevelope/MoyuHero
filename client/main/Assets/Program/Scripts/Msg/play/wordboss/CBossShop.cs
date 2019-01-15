using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBossShop: Protocol
	{

        public const int PROTOCOL_TYPE = 788891;

        public CBossShop()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CBossShop obj = new CBossShop();
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

        public override void Process() { }
	}	
}
