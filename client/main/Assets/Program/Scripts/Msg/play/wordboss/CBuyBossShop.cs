using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBuyBossShop: Protocol
	{
        public int bossshopid; // …Ã∆∑ID

        public const int PROTOCOL_TYPE = 788893;

        public CBuyBossShop()
            : base(PROTOCOL_TYPE)
		 {
             bossshopid = 0;
		 } 

		public override object Clone()
		{
            CBuyBossShop obj = new CBuyBossShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bossshopid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bossshopid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
