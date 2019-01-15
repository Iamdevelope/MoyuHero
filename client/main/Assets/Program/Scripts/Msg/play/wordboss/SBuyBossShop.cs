using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SBuyBossShop: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

        public int result;
        public int bossshopid; // 商品ID
        public int hunternum; // 今日猎人集市累计兑换次数
        public int chuanshuozs; // 传说之石
        public LinkedList<int> droplist; // 掉落小包ID


        public const int PROTOCOL_TYPE = 788894;

        public SBuyBossShop()
            : base(PROTOCOL_TYPE)
		 {
             droplist = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            SBuyBossShop obj = new SBuyBossShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            _os_.marshal(result);
            _os_.marshal(bossshopid);
            _os_.marshal(hunternum);
            _os_.marshal(chuanshuozs);

            _os_.compact_uint32(droplist.Count);
            LinkedListNode<int> firstNode = droplist.First;
            while (firstNode != null)
            {
                _os_.marshal(droplist.First.Value);

                droplist.RemoveFirst();
                firstNode = droplist.First;
            }
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            bossshopid = _os_.unmarshal_int();
            hunternum = _os_.unmarshal_int();
            chuanshuozs = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                droplist.AddFirst(_v_);
            } 
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().WorldBossMgr.m_ChuanShuoZS = chuanshuozs;
            ObjectSelf.GetInstance().WorldBossMgr.m_ShopExchangeNum = hunternum;

            WorldBossCallbackParaPackage package = new WorldBossCallbackParaPackage();
            package.m_Result = result;
            package.m_BossShopID = bossshopid;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_SBuyBossShop, package);
		}
			
	}	
}
