using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBuyPact: Protocol
	{

        public int pactid; // 强者之约ID

        public const int PROTOCOL_TYPE = 788937;

        public CBuyPact()
            : base(PROTOCOL_TYPE)
		 {
             pactid = 0;
		 } 

		public override object Clone()
		{
            CBuyPact obj = new CBuyPact();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(pactid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            pactid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
