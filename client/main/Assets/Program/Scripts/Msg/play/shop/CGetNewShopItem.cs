using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GNET
{
	public partial class CGetNewShopItem: Protocol
	{
        public int shopid; // 76±íµÄID


        public const int PROTOCOL_TYPE = 788841;

        public CGetNewShopItem()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CGetNewShopItem obj = new CGetNewShopItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(shopid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            shopid = _os_.unmarshal_int();

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 1024; }

        public override void Process()
        {
        }
    }
}
