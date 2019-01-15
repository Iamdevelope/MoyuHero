using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using System.Linq;
namespace GNET
{
	public partial class SRefreshNewShop: Protocol
	{

        public Hashtable shopmap;        // 整个商城map，key为76表的序列号,value为NewShopList

        public const int PROTOCOL_TYPE = 788838;

        public SRefreshNewShop()
            : base(PROTOCOL_TYPE)
		 {
             shopmap = new Hashtable();
		 } 

		public override object Clone()
		{
            SRefreshNewShop obj = new SRefreshNewShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(shopmap.Count);
            foreach (DictionaryEntry de in shopmap)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((NewShopList)de.Value);
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                NewShopList value = new NewShopList();
                key = _os_.unmarshal_int();
                value.unmarshal(_os_);
                shopmap.Add(key, value);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            #region MyRegion
            //ObjectSelf.GetInstance().StoreList.Clear();

            //foreach (DictionaryEntry item in shopmap)
            //{
            //    NewShopList nsl = (NewShopList)item.Value;
            //    BaseStore bs = new BaseStore();
            //    bs.MStoreId = (int)item.Key;
            //    bs.MRefreshTime = (int)nsl.lasttime;
            //    bs.MRefreshCount = nsl.refreshnum;

            //    bs.MGoodsList.Clear();

            //    foreach (var o in nsl.shoplist)
            //    {
            //        NewShop ns = o as NewShop;
            //        Goods gs = new Goods();
            //        gs.MTabelId = ns.itemid;
            //        gs.MCosId = ns.costtype;
            //        gs.MPrice = ns.price;
            //        gs.MNumbar = ns.num;
            //        gs.MIsbuy = ns.isbuy;

            //        bs.MGoodsList.Add(gs);
            //    }

            //    ObjectSelf.GetInstance().StoreList.Add(bs);
            //} 
            #endregion

            ObjectSelf.GetInstance().StoreContainer.RefreshStore(shopmap);

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_SGetStore);
		}
	}	
}
