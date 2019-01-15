using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class teamtanxian : Marshal
	{
        public int tanxianid; // 探险id
        public LinkedList<int> team; // 小队英雄key列表
        

        public teamtanxian()
        {
            tanxianid = 0;
            team = new LinkedList<int>();
        }

        public teamtanxian(int _tanxianid_, LinkedList<int> _lotteryitemlist_)
        {
            this.tanxianid = _tanxianid_;
            this.team = _lotteryitemlist_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(tanxianid);
            _os_.compact_uint32(team.Count);
            LinkedListNode<int> firstNode2 = team.First;
            while (firstNode2 != null)
            {
                _os_.marshal(team.First.Value);

                team.RemoveFirst();
                firstNode2 = team.First;
            }        
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            tanxianid = _os_.unmarshal_int();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ ;
                _v_ = _os_.unmarshal_int();
                team.AddLast(_v_);
            }
  
            return _os_;
		}

	}
}
