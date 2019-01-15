using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CGetMyWordBoss: Protocol
	{

        public int bossid; // 值为1234，代表第几个boss

        public const int PROTOCOL_TYPE = 788885;

        public CGetMyWordBoss()
            : base(PROTOCOL_TYPE)
		 {
             bossid = 0;
		 } 

		public override object Clone()
		{
            CGetMyWordBoss obj = new CGetMyWordBoss();
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
