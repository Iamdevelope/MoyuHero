using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBossBuyZhufu: Protocol
	{
        public int bossid; // 值为1234，代表第几个boss

        public const int PROTOCOL_TYPE = 788895;

        public CBossBuyZhufu()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CBossBuyZhufu obj = new CBossBuyZhufu();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bossid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bossid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
