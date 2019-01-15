using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GNET
{
	public partial class SDuihuanlb: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

	    public int result;
        public LinkedList<int> innerdropidlist; // 掉落小包ID

        public const int PROTOCOL_TYPE = 789047;

        public SDuihuanlb()
            : base(PROTOCOL_TYPE)
		 {
             innerdropidlist = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            SDuihuanlb obj = new SDuihuanlb();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);

            _os_.compact_uint32(innerdropidlist.Count);
            LinkedListNode<int> firstNode2 = innerdropidlist.First;
            while (firstNode2 != null)
            {
                _os_.marshal(innerdropidlist.First.Value);

                innerdropidlist.RemoveFirst();
                firstNode2 = innerdropidlist.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                innerdropidlist.AddLast(_v_);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 4096; }

        public override void Process() 
        {
            if (result == 1)
            {
                ObjectSelf.GetInstance().GetSettingData().DuihuanlbCopy(innerdropidlist);
            }
            if (result == 2)
            {
                SpreeCode.Inst.LBCodeError();
            }
        }
	}	
}
