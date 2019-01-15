using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBeginBattle: Protocol
	{

        public int battleid; // ¹Ø¿¨ID
        public short troopid; // Õ½¶ÓID

        public const int PROTOCOL_TYPE = 787938;

        public CBeginBattle()
            : base(PROTOCOL_TYPE)
		 {
            battleid = 0;
            troopid = 0;
		 } 

		public override object Clone()
		{
            CBeginBattle obj = new CBeginBattle();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(battleid);
            _os_.marshal(troopid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            battleid = _os_.unmarshal_int();
            troopid = _os_.unmarshal_short();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
