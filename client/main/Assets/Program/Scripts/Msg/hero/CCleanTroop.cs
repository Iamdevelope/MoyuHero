using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CCleanTroop: Protocol
	{


        public int troopid;



        public const int PROTOCOL_TYPE = 787739;

        public CCleanTroop()
            : base(PROTOCOL_TYPE)
		 {

             troopid = 0;

		 } 

		public override object Clone()
		{
            CCleanTroop obj = new CCleanTroop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            _os_.marshal(troopid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            troopid = _os_.unmarshal_int();

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
