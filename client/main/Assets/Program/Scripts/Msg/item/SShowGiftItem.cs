using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SShowGiftItem: Protocol
	{

 
        public LinkedList<int> giftitems;

        public const int PROTOCOL_TYPE = 787564;

        public SShowGiftItem()
            : base(PROTOCOL_TYPE)
		 {
             giftitems = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            SShowGiftItem obj = new SShowGiftItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            _os_.compact_uint32(giftitems.Count);
            LinkedListNode<int> firstNode = giftitems.First;
            while (firstNode != null)
            {
                _os_.marshal(giftitems.First.Value);

                giftitems.RemoveFirst();
                firstNode = giftitems.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                giftitems.AddFirst(_v_);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 512; }

        public override void Process()
        {
        
            foreach (var item in giftitems)
            {
                UI_ItemsManage._instance.giftIDList.Add(item);
            }
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_ShowGift);            
        }
	}	
}
