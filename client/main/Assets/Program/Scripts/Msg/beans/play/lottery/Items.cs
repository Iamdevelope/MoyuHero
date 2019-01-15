using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
	public class Items : Marshal
	{
		public int itemid;
		public int num;
      

		public Items()
        {
			itemid = 0;
			num = 0;
        }

		public Items(int _itemid_, int _num_) {
			this.itemid = _itemid_;
			this.num = _num_;
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
			_os_.marshal(itemid);
			_os_.marshal(num);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
			itemid = _os_.unmarshal_int();
			num = _os_.unmarshal_int();
            return _os_;
		}
	}
}
