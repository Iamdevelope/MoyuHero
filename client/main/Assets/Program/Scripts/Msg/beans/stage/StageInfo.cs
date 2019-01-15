using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
    public class StageInfo : Marshal
	{
       	public int id;
	    public byte starsum; // 0-3
	    public LinkedList<StageBattle> stagebattles;
        public int rewardgot; // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取

        public StageInfo()
        {
            stagebattles = new LinkedList<StageBattle>();
        }

        public StageInfo(int _id_, byte _starsum_, LinkedList<StageBattle> _stagebattles_, byte _rewardgot_)
        {
            this.id = _id_;
            this.starsum = _starsum_;
            this.stagebattles = _stagebattles_;
            this.rewardgot = _rewardgot_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(id);
            _os_.marshal(starsum);

            _os_.compact_uint32(stagebattles.Count);
            LinkedListNode<StageBattle> firstNode = stagebattles.First;
            while (firstNode != null)
            {
                _os_.marshal(stagebattles.First.Value);

                stagebattles.RemoveFirst();
                firstNode = stagebattles.First;
            }

            _os_.marshal(rewardgot);
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            id = _os_.unmarshal_int();
            starsum = _os_.unmarshal_byte();
            for (int size = _os_.uncompact_uint32(); size > 0; --size)
            {
                StageBattle _v_ = new StageBattle();
                _v_.unmarshal(_os_);
                stagebattles.AddFirst(_v_);
            }

            rewardgot = _os_.unmarshal_int();

            return _os_;
		}

	}
}
