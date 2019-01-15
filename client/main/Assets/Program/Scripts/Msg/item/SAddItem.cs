using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SAddItem: Protocol
	{

        public byte bagid;
        public LinkedList<Item> itemdate;

        public const int PROTOCOL_TYPE = 787536;

        public SAddItem()
            : base(PROTOCOL_TYPE)
		 {
             itemdate = new LinkedList<Item>();
		 } 

		public override object Clone()
		{
            SAddItem obj = new SAddItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bagid);
            _os_.compact_uint32(itemdate.Count);
            LinkedListNode<Item> firstNode = itemdate.First;
            while (firstNode != null)
            {
                _os_.marshal(itemdate.First.Value);

                itemdate.RemoveFirst();
                firstNode = itemdate.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            bagid = _os_.unmarshal_byte();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Item _v_ = new Item();
                _v_.unmarshal(_os_);
                itemdate.AddFirst(_v_);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process()
        {
            if ((int)bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON || (int)bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP)
            {
                foreach (Item var in itemdate)
                { 
                    //9.15日改动 这个消息会在获得物品时发送过来 从而避免分不清是物品获得还是首次登陆获取数据
                    ObjectSelf.GetInstance().CommonItemContainer.RefreshItem(bagid,var);
                    //ObjectSelf.GetInstance().CommonItemContainer.AddItem(bagid, var);
                }
                int id = (int)(bagid);
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_KnapsackAdd, id);
               // GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_ShowGift, itemdate);
            }
            
        }
	}	
}
