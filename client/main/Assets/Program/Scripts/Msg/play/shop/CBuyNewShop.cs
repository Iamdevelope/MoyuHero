using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GNET
{
	public partial class CBuyNewShop: Protocol
	{
        public int shopid; // 76表的ID
        public int itemid; // 77表的道具ID
        public int costtype; // 消耗资源
        public int price; // 价格
        public int num; // 数量

        public const int PROTOCOL_TYPE = 788839;

        public CBuyNewShop()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CBuyNewShop obj = new CBuyNewShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(shopid);
            _os_.marshal(itemid);
            _os_.marshal(costtype);
            _os_.marshal(price);
            _os_.marshal(num);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            shopid = _os_.unmarshal_int();
            itemid = _os_.unmarshal_int();
            costtype = _os_.unmarshal_int();
            price = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 1024; }

        public override void Process()
        {
        }
    }
}
