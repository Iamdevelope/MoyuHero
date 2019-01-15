using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CAddTroop: Protocol
	{

        public int trooptype; // ’Û–Õ¿‡–Õ
        public int herokey;
        public int troopid;
        public int locationid;


        public const int PROTOCOL_TYPE = 787737;

        public CAddTroop()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CAddTroop obj = new CAddTroop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(trooptype);
            _os_.marshal(herokey);
            _os_.marshal(troopid);
            _os_.marshal(locationid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            trooptype = _os_.unmarshal_int();
            herokey = _os_.unmarshal_int();
            troopid = _os_.unmarshal_int();
            locationid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
