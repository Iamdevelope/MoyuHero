using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshItem: Protocol
	{

        public byte bagid;
        public Item data;

        public const int PROTOCOL_TYPE = 787541;

        public SRefreshItem()
            : base(PROTOCOL_TYPE)
		 {
             data = new Item();
		 } 

		public override object Clone()
		{
            SRefreshItem obj = new SRefreshItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bagid);
            _os_.marshal(data);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            bagid = _os_.unmarshal_byte();
            data.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process()
        {
            if ((int)bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON || (int)bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP)// µÀ¾ß [4/14/2015 Zmy]
            {
                ObjectSelf.GetInstance().CommonItemContainer.RefreshItem(bagid,data);
            }
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.Net_RefreshItem, data);
        }
	}	
}
