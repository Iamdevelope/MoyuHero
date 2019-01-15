using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CSplitEquip: Protocol
	{

        public LinkedList<int> equipkeylist;


        public const int PROTOCOL_TYPE = 787567;

        public CSplitEquip()
            : base(PROTOCOL_TYPE)
		 {
             equipkeylist = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            CSplitEquip obj = new CSplitEquip();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(equipkeylist.Count);
            LinkedListNode<int> firstNode = equipkeylist.First;
            while (firstNode != null)
            {
                _os_.marshal(equipkeylist.First.Value);

                equipkeylist.RemoveFirst();
                firstNode = equipkeylist.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int equipkey = _os_.unmarshal_int();
                equipkeylist.AddFirst(equipkey);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

		public override void Process()
		{

		}
	}	
}
