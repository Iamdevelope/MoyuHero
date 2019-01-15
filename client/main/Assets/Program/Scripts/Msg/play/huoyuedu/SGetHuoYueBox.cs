using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SGetHuoYueBox: Protocol
	{
        public const int END_OK = 1; // ³É¹¦
	    public const int END_ERROR = 2; // Ê§°Ü

        public int result;
        public LinkedList<int> droplist; // µôÂä°üid

        public const int PROTOCOL_TYPE = 788786;

        public SGetHuoYueBox()
            : base(PROTOCOL_TYPE)
		 {
             droplist = new LinkedList<int>();
             result = 0;
		 } 

		public override object Clone()
		{
            SGetHuoYueBox obj = new SGetHuoYueBox();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);

            _os_.compact_uint32(droplist.Count);
            LinkedListNode<int> firstNode = droplist.First;
            while (firstNode != null)
            {
                _os_.marshal(droplist.First.Value);

                droplist.RemoveFirst();
                firstNode = droplist.First;
            }
 
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ = _os_.unmarshal_int();
                droplist.AddFirst(_v_);
            }
 
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            if (result == 1)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_GetLivenessBox);
            }
            
		}
			
	}	
}
