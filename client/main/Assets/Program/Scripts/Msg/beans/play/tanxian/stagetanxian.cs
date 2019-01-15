using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class stagetanxian : Marshal
	{
        public LinkedList<tanxianinit> stagetx; // 每章节探险列表
        

        public stagetanxian()
        {
            stagetx = new LinkedList<tanxianinit>();
        }

        public stagetanxian(LinkedList<tanxianinit> _stagetanxian_)
        {
            this.stagetx = _stagetanxian_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(stagetx.Count);
            LinkedListNode<tanxianinit> firstNode2 = stagetx.First;
            while (firstNode2 != null)
            {
                _os_.marshal(stagetx.First.Value);

                stagetx.RemoveFirst();
                firstNode2 = stagetx.First;
            }        
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                tanxianinit _v_ = new tanxianinit();
                _v_.unmarshal(_os_);
                stagetx.AddLast(_v_);
            }
  
            return _os_;
		}

	}
}
