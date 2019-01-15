using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBuyShouwangzl: Protocol
	{
        public int num; // ¹ºÂòÊýÁ¿

        public const int PROTOCOL_TYPE = 788897;

        public CBuyShouwangzl()
            : base(PROTOCOL_TYPE)
		 {
             num = 0;
		 } 

		public override object Clone()
		{
            CBuyShouwangzl obj = new CBuyShouwangzl();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(num);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            num = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
