using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class COpenMohe: Protocol
	{

        public int place; // Ä§ºÐÎ»ÖÃ1~3

        public const int PROTOCOL_TYPE = 787946;

        public COpenMohe()
            : base(PROTOCOL_TYPE)
		 {
             place = 0;
		 } 

		public override object Clone()
		{
            COpenMohe obj = new COpenMohe();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(place);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            place = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
