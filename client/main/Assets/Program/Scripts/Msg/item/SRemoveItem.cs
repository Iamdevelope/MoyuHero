using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;

namespace GNET
{
	public partial class SRemoveItem: Protocol
	{

        public byte bagid;
        public LinkedList<int> itemkeys;

        public const int PROTOCOL_TYPE = 787535;

        public SRemoveItem()
            : base(PROTOCOL_TYPE)
		 {
             itemkeys = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            SRemoveItem obj = new SRemoveItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bagid);
            _os_.compact_uint32(itemkeys.Count);
            LinkedListNode<int> firstNode2 = itemkeys.First;
            while (firstNode2 != null)
            {
                _os_.marshal(itemkeys.First.Value);

                itemkeys.RemoveFirst();
                firstNode2 = itemkeys.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bagid = _os_.unmarshal_byte();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ = _os_.unmarshal_int();
                itemkeys.AddFirst(_v_);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 1024; }

        public override void Process() 
        {
            if ((int)bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON || (int)bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP)// µÀ¾ß [4/14/2015 Zmy]
            {
                X_GUID _tmpGuid = new X_GUID();
                foreach (int var in itemkeys)
                {
                    _tmpGuid.GUID_value = var;
                    ObjectSelf.GetInstance().CommonItemContainer.EreaseItem(bagid,_tmpGuid);
                }
                _tmpGuid = null;

                int id = (int)bagid;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_KnapsackUpdateShow, id);
            }

           
        }
	}	
}
