using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SModItemNum: Protocol
	{
        // 改变物品数量
       	public byte bagid;
	    public int itemkey;
	    public int curnum;

        public const int PROTOCOL_TYPE = 787534;

        public SModItemNum()
            : base(PROTOCOL_TYPE)
		 {
             this.bagid = 0;
             this.itemkey = 0;
             this.curnum = 0;
		 } 

		public override object Clone()
		{
            SModItemNum obj = new SModItemNum();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            _os_.marshal(bagid);
            _os_.marshal(itemkey);
            _os_.marshal(curnum);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            bagid = _os_.unmarshal_byte();
            itemkey = _os_.unmarshal_int();
            curnum = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process() 
        {
            if ((int)bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON || (int)bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP)// 道具 [4/14/2015 Zmy]
            {
                int delta = 0;
                delta = ObjectSelf.GetInstance().CommonItemContainer.RepairItemCount(bagid,itemkey, curnum);

                if (delta > 0)
                {
                    LinkedList<Item> itemdata = new LinkedList<Item>();
                    X_GUID guid = new X_GUID();
                    guid.GUID_value = itemkey;
                    BaseItem bi = ObjectSelf.GetInstance().CommonItemContainer.FindItem(bagid, guid);
                    if (bi != null)
                    {
                        Item item = new Item();
                        item.id = bi.GetItemRowData().getId();
                        item.key = itemkey;
                        item.number = (short)delta;
                        itemdata.AddLast(item);
                        //GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_ShowGift, itemdata);
                    }
                }
            }
            int id = (int)bagid;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_ModItemNum, id);
        }
	}	
}
