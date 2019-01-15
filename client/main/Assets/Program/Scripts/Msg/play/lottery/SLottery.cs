using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SLottery: Protocol
	{
		public const int END_OK = 1; // ??
		public const int END_ERROR = 2; // ??

		public int result;
		public int lotterytype;
		public LinkedList<int> herolist; // ????(???)
		public LinkedList<Items> items; // ???????

        public const int PROTOCOL_TYPE = 788734;

        public SLottery()
            : base(PROTOCOL_TYPE)
		 {
             herolist = new LinkedList<int>();
			 items = new LinkedList<Items>();
             result = 0;
             lotterytype = 0;
		 } 

		public override object Clone()
		{
            SLottery obj = new SLottery();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(lotterytype);

            _os_.compact_uint32(herolist.Count);
            LinkedListNode<int> firstNode = herolist.First;
            while (firstNode != null)
            {
                _os_.marshal(herolist.First.Value);

                herolist.RemoveFirst();
                firstNode = herolist.First;
            }

			_os_.compact_uint32(items.Count);
			LinkedListNode<Items> firstNode1 = items.First;
			while (firstNode1 != null)
			{
				_os_.marshal(items.First.Value);
				
				items.RemoveFirst();
				firstNode1 = items.First;
			}
 
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            lotterytype = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ = _os_.unmarshal_int();
                herolist.AddFirst(_v_);
            }

			for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
			{
				Items _v_ = new Items();
				_v_.unmarshal(_os_);
				items.AddFirst(_v_);
			}
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            //if (result == END_OK)
            //{
            //    if (lotterytype == 1)
            //    {
            //        UI_HeroRecruit.inst.InitTipsText();
            //        UI_HeroRecruit.inst.SuccessBuyOne(herolist);
            //    }
            //    else if (lotterytype == 2)
            //    {
            //        UI_HeroRecruit.inst.InitTipsText();
            //        UI_HeroRecruit.inst.SuccessBuyTen(herolist);
            //    }
            //    else if (lotterytype == 3)
            //    {
            //        UI_HeroRecruit.inst.InitTipsText();
            //        UI_HeroRecruit.inst.SuccessExchangeHero(herolist);
            //    }
            //    else if (lotterytype == 4)
            //    {
            //        UI_HeroRecruit.inst.InitTipsText();
            //        UI_HeroRecruit.inst.SuccessBuyOne(herolist);
            //    }
            //    else
            //    {
            //        //Debug.Log("Slorrery 未知类型");
            //    }
            //    UI_Recruit.inst.RefreshController();
            //}
            //else
            //{
            //    UI_HeroRecruit.inst.RefreshDreamValue();
            //    Debug.Log("SLotery fail");
            //}

            if (result == END_OK)
            {
                Debug.LogError("--------物品" + items.Count + "个");
                foreach (Items item in items)
                {
                    ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(item.itemid);

                    Debug.LogError(itemT.getId() + "个数" + item.num);
                }

                Debug.LogError("--------英雄" + herolist.Count + "个");
                foreach (int heroId in herolist)
                {
                    HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(heroId);

                    Debug.LogError(heroT.getId());
                }
            }
		}
			
	}	
}
