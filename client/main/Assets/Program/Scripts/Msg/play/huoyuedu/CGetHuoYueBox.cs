using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CGetHuoYueBox: Protocol
	{
        public int boxnum; // 第几个宝箱，从1开始

        public const int PROTOCOL_TYPE = 788785;

        public CGetHuoYueBox()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CGetHuoYueBox obj = new CGetHuoYueBox();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(boxnum);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            boxnum = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
