using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBossPass: Protocol
	{

        public LinkedList<fightInfo> fightinfolist; // ’Ω∂∑–≈œ¢

        public const int PROTOCOL_TYPE = 788889;

        public CBossPass()
            : base(PROTOCOL_TYPE)
		 {
             fightinfolist = new LinkedList<fightInfo>();
		 } 

		public override object Clone()
		{
            CBossPass obj = new CBossPass();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(fightinfolist.Count);
            LinkedListNode<fightInfo> firstNode = fightinfolist.First;
            while (firstNode != null)
            {
                _os_.marshal(fightinfolist.First.Value);

                fightinfolist.RemoveFirst();
                firstNode = fightinfolist.First;
            }  

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                fightInfo _v_ = new fightInfo();
                _v_.unmarshal(_os_);
                fightinfolist.AddLast(_v_);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 655350; }

        public override void Process() { }
	}	
}
