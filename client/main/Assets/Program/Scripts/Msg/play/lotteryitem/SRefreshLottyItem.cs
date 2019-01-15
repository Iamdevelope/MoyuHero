using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshLottyItem: Protocol
	{

        public LotteryItemAll lotteryitem;

        public const int PROTOCOL_TYPE = 788765;

        public SRefreshLottyItem()
            : base(PROTOCOL_TYPE)
		 {
             lotteryitem = new LotteryItemAll();
		 } 

		public override object Clone()
		{
            SRefreshLottyItem obj = new SRefreshLottyItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(lotteryitem);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            lotteryitem.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
			ObjectSelf.GetInstance().mapkey = lotteryitem.mapkey; // 第几层 从1开始
			ObjectSelf.GetInstance().mapvalue = lotteryitem.mapvalue; // 第几个 从1开始
			ObjectSelf.GetInstance().superlist = lotteryitem.superlist;    //遗迹宝藏特殊list
			ObjectSelf.GetInstance().ismonthfirsthave = lotteryitem.ismonthfirsthave; // 是否有月卡首刷，0没有，1有
			ObjectSelf.GetInstance().ishavefree = lotteryitem.ishavefree; // 是否有免费抽奖，0有，非0则为倒计时（秒）
			ObjectSelf.GetInstance().lotteryitemmap = lotteryitem.lotteryitemmap;        // 遗迹宝藏总信息（key为层数，value为LotteryItemlayer）
		}
	}	
}
