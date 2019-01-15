using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SGetBossRank: Protocol
	{

        public LinkedList<BossRankInfo> rank;
        public int ranknum; // ≈≈√˚
	    public long num; // …À∫¶

        public const int PROTOCOL_TYPE = 788900;

        public SGetBossRank()
            : base(PROTOCOL_TYPE)
		 {
             rank = new LinkedList<BossRankInfo>();
             ranknum = 0;
             num = 0;
		 } 

		public override object Clone()
		{
            SGetBossRank obj = new SGetBossRank();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(rank.Count);
            LinkedListNode<BossRankInfo> firstNode = rank.First;
            while (firstNode != null)
            {
                _os_.marshal(rank.First.Value);

                rank.RemoveFirst();
                firstNode = rank.First;
            }
            _os_.marshal(ranknum);
            _os_.marshal(num);
           
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                BossRankInfo _v_ = new BossRankInfo();
                _v_.unmarshal(_os_);
                rank.AddLast(_v_);
            }
            ranknum = _os_.unmarshal_int();
            num = _os_.unmarshal_long();
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 655350; }

        public override void Process() 
		{
            var WorldBossManager = ObjectSelf.GetInstance().WorldBossMgr;
            WorldBossManager.m_MyRanking = ranknum;
            WorldBossManager.m_MyTotalDamage = num;

            WorldBossManager.m_RankingList.Clear();
            foreach (var data in rank)
            {
                WorldBossManager.m_RankingList.Add(data);
            }
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_SGetBossRank);
		}
			
	}	
}
