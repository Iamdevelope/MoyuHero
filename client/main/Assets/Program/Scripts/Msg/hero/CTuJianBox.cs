using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CTuJianBox: Protocol
	{

        public int boxid; // ±¶œ‰ID


        public const int PROTOCOL_TYPE = 787763;

        public CTuJianBox()
            : base(PROTOCOL_TYPE)
		 {
             boxid = 0;
		 } 

		public override object Clone()
		{
            CTuJianBox obj = new CTuJianBox();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(boxid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            boxid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
