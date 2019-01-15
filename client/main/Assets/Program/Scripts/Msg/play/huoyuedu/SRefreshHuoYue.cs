using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.UI.Core;
namespace GNET
{
	public partial class SRefreshHuoYue: Protocol
	{
        public int huoyuenum; // 活跃度
        public int getnum; // 领取记录，个位第一个，十位第二个~~
        public LinkedList<Huoyue> huoyuelist;

        public const int PROTOCOL_TYPE = 788784;

        public SRefreshHuoYue()
            : base(PROTOCOL_TYPE)
		 {
             huoyuelist = new LinkedList<Huoyue>();
		 } 

		public override object Clone()
		{
            SRefreshHuoYue obj = new SRefreshHuoYue();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(huoyuenum);
            _os_.marshal(getnum);

            _os_.compact_uint32(huoyuelist.Count);
            LinkedListNode<Huoyue> firstNode = huoyuelist.First;
            while (firstNode != null)
            {
                _os_.marshal(huoyuelist.First.Value);

                huoyuelist.RemoveFirst();
                firstNode = huoyuelist.First;
            }
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            huoyuenum = _os_.unmarshal_int();
            getnum = _os_.unmarshal_int();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Huoyue _v_ = new Huoyue();
                _v_.unmarshal(_os_);
                huoyuelist.AddFirst(_v_);
            }
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            var objectSelf = ObjectSelf.GetInstance();
            if (objectSelf != null)
            {
                objectSelf.Liveness = huoyuenum;
                objectSelf.LivenessClaimNum = getnum;
            }
            if (UI_Liveness._instance!=null)
            {
                UI_Liveness _Liveness = UI_Liveness._instance;
                _Liveness.m_LivenessNum = huoyuenum;
                _Liveness.isLivenessBox = getnum;
                _Liveness.m_MissionID.Clear();
                _Liveness.CopyData(huoyuelist);
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_GetLiveness);
            }
		}
			
	}	
}
