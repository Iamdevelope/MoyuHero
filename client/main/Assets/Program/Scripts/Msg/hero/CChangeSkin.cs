using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CChangeSkin: Protocol
	{

        public int herokey; // ”¢–€key
        public int skinid; // ∆§∑ÙID


        public const int PROTOCOL_TYPE = 787769;

        public CChangeSkin()
            : base(PROTOCOL_TYPE)
		 {
             herokey = 0;
             skinid = 0;
		 } 

		public override object Clone()
		{
            CChangeSkin obj = new CChangeSkin();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(herokey);
            _os_.marshal(skinid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            herokey = _os_.unmarshal_int();
            skinid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
