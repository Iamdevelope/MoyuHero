using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CTanxianBegin: Protocol
	{
        public LinkedList<int> team; // 小队英雄key列表
        public int tanxianid; // 探险id

        public const int PROTOCOL_TYPE = 788984;

        public CTanxianBegin()
            : base(PROTOCOL_TYPE)
		 {
             team = new LinkedList<int>();
             tanxianid = 0;
		 } 

		public override object Clone()
		{
            CTanxianBegin obj = new CTanxianBegin();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(team.Count);
            LinkedListNode<int> firstNode2 = team.First;
            while (firstNode2 != null)
            {
                _os_.marshal(team.First.Value);

                team.RemoveFirst();
                firstNode2 = team.First;
            }

            _os_.marshal(tanxianid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                team.AddLast(_v_);
            }
            tanxianid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 1024; }

        public override void Process() { }
	}	
}
