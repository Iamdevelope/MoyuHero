using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBeginEndless: Protocol
	{

        public short troopid; // Õ½¶ÓID

        public const int PROTOCOL_TYPE = 788933;

        public CBeginEndless()
            : base(PROTOCOL_TYPE)
		 {
            troopid = 0;
		 } 

		public override object Clone()
		{
            CBeginEndless obj = new CBeginEndless();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(troopid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            troopid = _os_.unmarshal_short();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
