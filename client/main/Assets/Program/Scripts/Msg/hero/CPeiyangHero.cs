using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CPeiyangHero: Protocol
	{

        public int herokey; // 英雄key
        public byte slotnum; // 培养位置
        public byte isreset; // 是否重置（0为非重置，1为重置）


        public const int PROTOCOL_TYPE = 787779;

        public CPeiyangHero()
            : base(PROTOCOL_TYPE)
		 {
             herokey = 0;
             slotnum = 0;
		 } 

		public override object Clone()
		{
            CPeiyangHero obj = new CPeiyangHero();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(herokey);
            _os_.marshal(slotnum);
            _os_.marshal(isreset);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            herokey = _os_.unmarshal_int();
            slotnum = _os_.unmarshal_byte();
            isreset = _os_.unmarshal_byte();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
