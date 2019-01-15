using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using UnityEngine;

namespace GNET
{
	public partial class SBuyNewShop: Protocol
	{

        public static int END_OK = 1; // 成功
	    public static int END_NOT_OK = 2; // 失败

	    public int result; // 结果
        public int shopid; // 76表的ID
        public int itemid; // 77表的道具ID
        public int costtype; // 消耗资源
        public int price; // 价格
        public int num; // 数量

        public const int PROTOCOL_TYPE = 788840;

        public SBuyNewShop()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SBuyNewShop obj = new SBuyNewShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(shopid);
            _os_.marshal(itemid);
            _os_.marshal(costtype);
            _os_.marshal(price);
            _os_.marshal(num);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            shopid = _os_.unmarshal_int();
            itemid = _os_.unmarshal_int();
            costtype = _os_.unmarshal_int();
            price = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{

		}
	}	
}
