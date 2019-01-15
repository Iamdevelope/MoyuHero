using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CHeroJinjie: Protocol
	{

        public int herokey; // Ó¢ÐÛkey


        public const int PROTOCOL_TYPE = 787786;

        public CHeroJinjie()
            : base(PROTOCOL_TYPE)
		 {
             herokey = 0;
		 } 

		public override object Clone()
		{
            CHeroJinjie obj = new CHeroJinjie();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(herokey);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            herokey = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
