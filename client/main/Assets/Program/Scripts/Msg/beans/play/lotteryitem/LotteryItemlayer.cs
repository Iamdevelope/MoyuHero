using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class LotteryItemlayer : Marshal
	{
        public LinkedList<LotteryItem> lotteryitemlist; // “≈º£±¶≤ÿ√ø≤„list
        

        public LotteryItemlayer()
        {
            lotteryitemlist = new LinkedList<LotteryItem>();
        }

        public LotteryItemlayer(LinkedList<LotteryItem> _lotteryitemlist_)
        {
            this.lotteryitemlist = _lotteryitemlist_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(lotteryitemlist.Count);
            LinkedListNode<LotteryItem> firstNode2 = lotteryitemlist.First;
            while (firstNode2 != null)
            {
                _os_.marshal(lotteryitemlist.First.Value);

                lotteryitemlist.RemoveFirst();
                firstNode2 = lotteryitemlist.First;
            }        
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                LotteryItem _v_ = new LotteryItem();
                _v_.unmarshal(_os_);
                lotteryitemlist.AddLast(_v_);
            }
  
            return _os_;
		}

	}
}
