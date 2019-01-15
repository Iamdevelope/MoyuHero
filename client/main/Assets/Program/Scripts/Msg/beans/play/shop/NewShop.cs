using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class NewShop : Marshal
	{
        public int itemid; // 77表的道具ID
        public int costtype; // 消耗资源
        public int price; // 价格
        public int num; // 数量
        public int isbuy; // 0未购买，1为已购买
      

        public NewShop()
        {

        }

        public NewShop(int _itemid_, int _costtype_, int _price_, int _num_, int _isbuy_)
        {
            this.itemid = _itemid_;
            this.costtype = _costtype_;
            this.price = _price_;
            this.num = _num_;
            this.isbuy = _isbuy_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(itemid);
            _os_.marshal(costtype);
            _os_.marshal(price);
            _os_.marshal(num);
            _os_.marshal(isbuy);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            itemid = _os_.unmarshal_int();
            costtype = _os_.unmarshal_int();
            price = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            isbuy = _os_.unmarshal_int();
            return _os_;
		}
	}
}
