using System;
using System.Collections.Generic;
namespace GNET
{
    public class Smshopdata : Marshal
	{
        public int id; // id
        public int isopen; // 是否开启（1开启，0未开启）
        public int price; // 价格

        public Smshopdata()
        {
            id = 0;
            isopen = 0;
            price = 0;
        }

        public Smshopdata(int _id_, int _isopen_, int _price_) 
        {
		    this.id = _id_;
		    this.isopen = _isopen_;
            this.price = _price_;
	    }

		public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(id);
            _os_.marshal(isopen);
            _os_.marshal(price);

		    return _os_;

		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            id = _os_.unmarshal_int();
            isopen = _os_.unmarshal_int();
            price = _os_.unmarshal_int();
            return _os_;
		}

	}
}
