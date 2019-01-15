using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CGetGameAct: Protocol
	{

        public int actid; // »î¶¯ID

        public const int PROTOCOL_TYPE = 789050;

        public CGetGameAct()
            : base(PROTOCOL_TYPE)
		 {
             actid = 0;
		 } 

		public override object Clone()
		{
            CGetGameAct obj = new CGetGameAct();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(actid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            actid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
