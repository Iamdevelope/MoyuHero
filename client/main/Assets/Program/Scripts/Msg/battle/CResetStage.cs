using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CResetStage: Protocol
	{

        public int battleid; // ¹Ø¿¨ID

        public const int PROTOCOL_TYPE = 787953;

        public CResetStage()
            : base(PROTOCOL_TYPE)
		 {
            battleid = 0;
		 } 

		public override object Clone()
		{
            CResetStage obj = new CResetStage();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(battleid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            battleid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
