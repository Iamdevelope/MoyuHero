using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using UnityEngine;

namespace GNET
{
	public partial class SGetNewShopItem: Protocol
	{

        public static int END_OK = 1; // 成功
	    public static int END_NOT_OK = 2; // 失败

	    public int result; // 结果
        public int shopid; // 76表的ID


        public const int PROTOCOL_TYPE = 788842;

        public SGetNewShopItem()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SGetNewShopItem obj = new SGetNewShopItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(shopid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            shopid = _os_.unmarshal_int();

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{

		}
	}	
}
