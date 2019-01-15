using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBuySmShop: Protocol
	{

        public int smshopid; // …Ò√ÿ…ÃµÍid

        public const int PROTOCOL_TYPE = 787950;

        public CBuySmShop()
            : base(PROTOCOL_TYPE)
		 {
             smshopid = 0;
		 } 

		public override object Clone()
		{
            CBuySmShop obj = new CBuySmShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(smshopid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            smshopid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
