using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CHeroCompose: Protocol
	{

        public int heroid; // ”¢–€≈‰±ÌID

        public const int PROTOCOL_TYPE = 787792;

        public CHeroCompose()
            : base(PROTOCOL_TYPE)
		 {
             heroid = 0;
		 } 

		public override object Clone()
		{
            CHeroCompose obj = new CHeroCompose();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(heroid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            heroid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
