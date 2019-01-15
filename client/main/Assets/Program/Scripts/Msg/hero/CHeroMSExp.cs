using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CHeroMSExp: Protocol
	{

        public int herokey; // 英雄key
        public int mslocation; // 秘术位置 从1开始
        public LinkedList<int> itemidlist; // 物品配表ID
        public LinkedList<int> itemnumlist; // 物品数量



        public const int PROTOCOL_TYPE = 787788;

        public CHeroMSExp()
            : base(PROTOCOL_TYPE)
		 {
             itemidlist = new LinkedList<int>();
             itemnumlist = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            CHeroMSExp obj = new CHeroMSExp();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(herokey);
            _os_.marshal(mslocation);
            _os_.compact_uint32(itemidlist.Count);
            LinkedListNode<int> firstNode = itemidlist.First;
            while (firstNode != null)
            {
                _os_.marshal(itemidlist.First.Value);

                itemidlist.RemoveFirst();
                firstNode = itemidlist.First;
            }

            _os_.compact_uint32(itemnumlist.Count);
            LinkedListNode<int> firstNode2 = itemnumlist.First;
            while (firstNode2 != null)
            {
                _os_.marshal(itemnumlist.First.Value);

                itemnumlist.RemoveFirst();
                firstNode2 = itemnumlist.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            herokey = _os_.unmarshal_int();
            mslocation = _os_.unmarshal_int();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int num = _os_.unmarshal_int();
                itemidlist.AddFirst(num);
            }

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int num = _os_.unmarshal_int();
                itemnumlist.AddFirst(num);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
