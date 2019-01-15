using System;
using System.Collections.Generic;
namespace GNET
{
    public class Mohe : Marshal
	{
        public int id; // id
        public int isopen; // 是否开启（1开启，0未开启）
        public int place; // 排序（0为随机排序，123为正常排序）

        public Mohe()
        {
            id = 0;
            isopen = 0;
            place = 0;
        }

        public Mohe(int _id_, int _isopen_, int _place_) 
        {
		    this.id = _id_;
		    this.isopen = _isopen_;
		    this.place = _place_;
	    }

		public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(id);
            _os_.marshal(isopen);
            _os_.marshal(place);

		    return _os_;

		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            id = _os_.unmarshal_int();
            isopen = _os_.unmarshal_int();
            place = _os_.unmarshal_int();
            return _os_;
		}

	}
}
