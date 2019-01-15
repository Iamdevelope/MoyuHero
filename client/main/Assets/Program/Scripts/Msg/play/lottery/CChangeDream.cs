using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CChangeDream: Protocol
	{

        public int isfree; // 0 ’∑—£¨1√‚∑—

        public const int PROTOCOL_TYPE = 788736;

        public CChangeDream()
            : base(PROTOCOL_TYPE)
		 {
             isfree = 0;
		 } 

		public override object Clone()
		{
            CChangeDream obj = new CChangeDream();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(isfree);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            isfree = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
