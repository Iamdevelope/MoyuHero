using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SLotteryItem: Protocol
	{
        public const int END_OK = 1; // 成功
	    public const int END_ERROR = 2; // 失败

        public int result;
        public int lotterytype;
        public LinkedList<LotteryItemget> itemlist;

        public const int PROTOCOL_TYPE = 788764;

        public SLotteryItem()
            : base(PROTOCOL_TYPE)
		 {
             itemlist = new LinkedList<LotteryItemget>();
             result = 0;
             lotterytype = 0;
		 } 

		public override object Clone()
		{
            SLotteryItem obj = new SLotteryItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(lotterytype);

            _os_.compact_uint32(itemlist.Count);
            LinkedListNode<LotteryItemget> firstNode = itemlist.First;
            while (firstNode != null)
            {
                _os_.marshal(itemlist.First.Value);

                itemlist.RemoveFirst();
                firstNode = itemlist.First;
            }
 
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            lotterytype = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                LotteryItemget _v_ = new LotteryItemget();
                _v_.unmarshal(_os_);
                itemlist.AddFirst(_v_);
            }
 
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
			if (result == END_OK)
			{
				if (lotterytype == 1)
				{
					UI_RelicTreasure.Inst.SuccessBuyOne(itemlist);
				}
				else if (lotterytype == 2)
				{
					UI_RelicTreasure.Inst.SuccessBuyTen(itemlist);
				}
				else if (lotterytype == 3)
				{
					//
					UI_RelicTreasure.Inst.SuccessBuyOne(itemlist);
				}
				else if (lotterytype == 4)
				{
					UI_RelicTreasure.Inst.SuccessRefresh(itemlist);
				}
				else
				{
					//Debug.Log("Slorrery 未知类型");
				}
                UI_Recruit.inst.RefreshController();
			}
			else
			{
				//
				Debug.Log("SLotteryItem 失败");
			}
			
		}
			
	}	
}
