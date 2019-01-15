using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SBossShop: Protocol
	{
        public LinkedList<int> shoplist; // 今天可买的物品表
        public int hunternum; // 今日猎人集市累计兑换次数
        public int chuanshuozs; // 传说之石

        public const int PROTOCOL_TYPE = 788892;

        public SBossShop()
            : base(PROTOCOL_TYPE)
		 {
             shoplist = new LinkedList<int>();
             hunternum = 0;
             chuanshuozs = 0;
		 } 

		public override object Clone()
		{
            SBossShop obj = new SBossShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{


            _os_.compact_uint32(shoplist.Count);
            LinkedListNode<int> firstNode = shoplist.First;
            while (firstNode != null)
            {
                _os_.marshal(shoplist.First.Value);

                shoplist.RemoveFirst();
                firstNode = shoplist.First;
            }

            _os_.marshal(hunternum);
            _os_.marshal(chuanshuozs);
 
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                shoplist.AddFirst(_v_);
            }
            hunternum = _os_.unmarshal_int();
            chuanshuozs = _os_.unmarshal_int();
 
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().WorldBossMgr.m_ShopList.Clear();
            foreach (int item in shoplist)
            {
                ObjectSelf.GetInstance().WorldBossMgr.m_ShopList.Add(item);
            }
            ObjectSelf.GetInstance().WorldBossMgr.m_ChuanShuoZS = chuanshuozs;
            ObjectSelf.GetInstance().WorldBossMgr.m_ShopExchangeNum = hunternum;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_SBossShop);
		}
			
	}	
}
