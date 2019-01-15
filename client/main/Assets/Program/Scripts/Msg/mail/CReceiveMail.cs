using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CReceiveMail: Protocol
	{
        public int mailkey; // 邮件key
        public int isget; // 是否领取附件


        public const int PROTOCOL_TYPE = 786938;

        public CReceiveMail()
            : base(PROTOCOL_TYPE)
		 {
             mailkey = 0;
             isget = 0;
		 } 

		public override object Clone()
		{
            CReceiveMail obj = new CReceiveMail();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(mailkey);
            _os_.marshal(isget);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            mailkey = _os_.unmarshal_int();
            isget = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
