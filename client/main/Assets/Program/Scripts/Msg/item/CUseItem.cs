using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CUseItem: Protocol
	{
        public byte bagid; // 暂时只有bag，soul，collect里的物品能用
        public int itemkey;
        public short num;
        public int dstkey; // 当给别的物品或武将使用时，具体是什么由使用的物品自己判断

        public const int PROTOCOL_TYPE = 787545;

        public CUseItem()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CUseItem obj = new CUseItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bagid);
            _os_.marshal(itemkey);
            _os_.marshal(num);
            _os_.marshal(dstkey);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bagid = _os_.unmarshal_byte();
            itemkey = _os_.unmarshal_int();
            num = _os_.unmarshal_short();
            dstkey = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 1024; }

        public override void Process() { }
	}	
}
