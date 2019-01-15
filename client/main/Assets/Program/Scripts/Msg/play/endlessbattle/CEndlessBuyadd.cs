using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CEndlessBuyadd: Protocol
	{

        public int addnum; // ¹ºÂòµÄÊôÐÔ1~4

        public const int PROTOCOL_TYPE = 788942;

        public CEndlessBuyadd()
            : base(PROTOCOL_TYPE)
		 {
             addnum = 0;
		 } 

		public override object Clone()
		{
            CEndlessBuyadd obj = new CEndlessBuyadd();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(addnum);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            addnum = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
