using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CRemoveMailList: Protocol
	{
        public LinkedList<int> maillist; // 邮件列表
        public int mailsize; // 从第几个开始往后取20个


        public const int PROTOCOL_TYPE = 786936;

        public CRemoveMailList()
            : base(PROTOCOL_TYPE)
		 {
             maillist = new LinkedList<int>();
             mailsize = 0;
		 } 

		public override object Clone()
		{
            CRemoveMailList obj = new CRemoveMailList();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(maillist.Count);
            LinkedListNode<int> firstNode2 = maillist.First;
            while (firstNode2 != null)
            {
                _os_.marshal(maillist.First.Value);

                maillist.RemoveFirst();
                firstNode2 = maillist.First;
            }
            _os_.marshal(mailsize);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                maillist.AddLast(_v_);
            }
            mailsize = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
