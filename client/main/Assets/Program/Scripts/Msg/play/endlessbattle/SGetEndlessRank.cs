using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SGetEndlessRank: Protocol
	{

        public LinkedList<EndlessRankInfo> rank1_50;
        public LinkedList<EndlessRankInfo> rank51_100;
        public LinkedList<EndlessRankInfo> rank101_;

        public const int PROTOCOL_TYPE = 788945;

        public SGetEndlessRank()
            : base(PROTOCOL_TYPE)
		 {
             rank1_50 = new LinkedList<EndlessRankInfo>();
             rank51_100 = new LinkedList<EndlessRankInfo>();
             rank101_ = new LinkedList<EndlessRankInfo>();
		 } 

		public override object Clone()
		{
            SGetEndlessRank obj = new SGetEndlessRank();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(rank1_50.Count);
            LinkedListNode<EndlessRankInfo> firstNode = rank1_50.First;
            while (firstNode != null)
            {
                _os_.marshal(rank1_50.First.Value);

                rank1_50.RemoveFirst();
                firstNode = rank1_50.First;
            }

            _os_.compact_uint32(rank51_100.Count);
            LinkedListNode<EndlessRankInfo> firstNode1 = rank51_100.First;
            while (firstNode1 != null)
            {
                _os_.marshal(rank51_100.First.Value);

                rank51_100.RemoveFirst();
                firstNode1 = rank51_100.First;
            }

            _os_.compact_uint32(rank101_.Count);
            LinkedListNode<EndlessRankInfo> firstNode2 = rank101_.First;
            while (firstNode2 != null)
            {
                _os_.marshal(rank101_.First.Value);

                rank101_.RemoveFirst();
                firstNode2 = rank101_.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                EndlessRankInfo _v_ = new EndlessRankInfo();
                _v_.unmarshal(_os_);
                rank1_50.AddLast(_v_);
            }

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                EndlessRankInfo _v_ = new EndlessRankInfo();
                _v_.unmarshal(_os_);
                rank51_100.AddLast(_v_);
            }

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                EndlessRankInfo _v_ = new EndlessRankInfo();
                _v_.unmarshal(_os_);
                rank101_.AddLast(_v_);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().LimitFightMgr.CopyRankInfo(rank1_50, rank51_100, rank101_);

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_LimitRankUpdate);
		}
			
	}	
}
