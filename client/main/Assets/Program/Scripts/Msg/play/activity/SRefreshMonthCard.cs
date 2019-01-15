using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction;
namespace GNET
{
	public partial class SRefreshMonthCard: Protocol
	{

        public LinkedList<Monthcard> monthcardlist;

        public const int PROTOCOL_TYPE = 789042;

        public SRefreshMonthCard()
            : base(PROTOCOL_TYPE)
		 {
             monthcardlist = new LinkedList<Monthcard>();
		 } 

		public override object Clone()
		{
            SRefreshMonthCard obj = new SRefreshMonthCard();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(monthcardlist.Count);
            LinkedListNode<Monthcard> firstNode = monthcardlist.First;
            while (firstNode != null)
            {
                _os_.marshal(monthcardlist.First.Value);

                monthcardlist.RemoveFirst();
                firstNode = monthcardlist.First;
            }
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Monthcard _v_ = new Monthcard();
                _v_.unmarshal(_os_);
                monthcardlist.AddFirst(_v_);
            }
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().SetMonthCardData(monthcardlist);

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_RefreshMonthCard);
		}
	}	
}
