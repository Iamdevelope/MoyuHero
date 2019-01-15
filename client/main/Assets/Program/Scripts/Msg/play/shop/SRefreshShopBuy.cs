using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshShopBuy: Protocol
	{

        public Shopbuy shopbuy;

        public const int PROTOCOL_TYPE = 788835;

        public SRefreshShopBuy()
            : base(PROTOCOL_TYPE)
		 {
             shopbuy = new Shopbuy();
		 } 

		public override object Clone()
		{
            SRefreshShopBuy obj = new SRefreshShopBuy();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(shopbuy);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            shopbuy.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            //更新人物商城数据信息;
            ObjectSelf.GetInstance().RefreshShopBuyInfo(shopbuy);

            //刷新商店逻辑;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_RefreshShopInfo, shopbuy);

		}
	}	
}
