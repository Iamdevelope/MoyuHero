using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CEndBattle: Protocol
	{

        public int pass; // 0未通过，1通过1，2通过2，3全通


        public const int PROTOCOL_TYPE = 787942;

        public CEndBattle()
            : base(PROTOCOL_TYPE)
		 {
            pass = 0;
		 } 

		public override object Clone()
		{
            CEndBattle obj = new CEndBattle();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(pass);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            pass = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
