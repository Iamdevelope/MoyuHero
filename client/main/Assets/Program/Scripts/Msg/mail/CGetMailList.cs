using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CGetMailList: Protocol
	{
        public int mailsize; // 从第几个开始往后取20个


        public const int PROTOCOL_TYPE = 786933;

        public CGetMailList()
            : base(PROTOCOL_TYPE)
		 {
             mailsize = 0;
		 } 

		public override object Clone()
		{
            CGetMailList obj = new CGetMailList();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(mailsize);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            mailsize = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
