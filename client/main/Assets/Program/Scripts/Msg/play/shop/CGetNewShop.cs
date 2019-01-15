using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GNET
{
	public partial class CGetNewShop: Protocol
	{

        public const int PROTOCOL_TYPE = 788837;

        public CGetNewShop()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CGetNewShop obj = new CGetNewShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 1024; }

        public override void Process()
        {

        }
    }
}
