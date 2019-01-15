using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CHeroLevelUpSpeed: Protocol
	{

        public int herokey; // 英雄key
        public byte levelnum; // 升级数（根据策划案为1级和5级）填写0即为使用物品
        public int itemid; // 物品配表ID
        public int itemnum; // 物品使用数量


        public const int PROTOCOL_TYPE = 787783;

        public CHeroLevelUpSpeed()
            : base(PROTOCOL_TYPE)
		 {
             herokey = 0;
             levelnum = 0;
		 } 

		public override object Clone()
		{
            CHeroLevelUpSpeed obj = new CHeroLevelUpSpeed();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(herokey);
            _os_.marshal(levelnum);
            _os_.marshal(itemid);
            _os_.marshal(itemnum);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            herokey = _os_.unmarshal_int();
            levelnum = _os_.unmarshal_byte();
            itemid = _os_.unmarshal_int();
            itemnum = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
