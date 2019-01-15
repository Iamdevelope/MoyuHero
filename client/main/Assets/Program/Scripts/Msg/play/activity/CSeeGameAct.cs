using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CSeeGameAct: Protocol
	{

        public int teamid; // ·Ö×éID

        public const int PROTOCOL_TYPE = 789053;

        public CSeeGameAct()
            : base(PROTOCOL_TYPE)
		 {
             teamid = 0;
		 } 

		public override object Clone()
		{
            CSeeGameAct obj = new CSeeGameAct();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(teamid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            teamid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
