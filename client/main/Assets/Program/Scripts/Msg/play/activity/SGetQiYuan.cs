using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SGetQiYuan: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

	    public int result;
        public LinkedList<int> innerdropidlist; // 掉落小包ID

        public const int PROTOCOL_TYPE = 789039;

        public SGetQiYuan()
            : base(PROTOCOL_TYPE)
		 {
             innerdropidlist = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            SGetQiYuan obj = new SGetQiYuan();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);

            _os_.compact_uint32(innerdropidlist.Count);
            LinkedListNode<int> firstNode = innerdropidlist.First;
            while (firstNode != null)
            {
                _os_.marshal(innerdropidlist.First.Value);

                innerdropidlist.RemoveFirst();
                firstNode = innerdropidlist.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ = _os_.unmarshal_int();
                innerdropidlist.AddFirst(_v_);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            if (result==END_OK)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_SacredAltarUIShow);
                if (innerdropidlist.Count>0)
                {
                    foreach (var item in innerdropidlist)
                    {
                        UI_SacredAltar._instance.m_FallItemList.Add(item);
                    }
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_SacredAltarSuccend);
                }
                else
                {
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_SacredAltarTips);
                }
            }

        }
	}	
}
