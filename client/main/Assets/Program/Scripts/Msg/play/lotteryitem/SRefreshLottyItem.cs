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
			ObjectSelf.GetInstance().mapkey = lotteryitem.mapkey; // �ڼ��� ��1��ʼ
			ObjectSelf.GetInstance().mapvalue = lotteryitem.mapvalue; // �ڼ��� ��1��ʼ
			ObjectSelf.GetInstance().superlist = lotteryitem.superlist;    //�ż���������list
			ObjectSelf.GetInstance().ismonthfirsthave = lotteryitem.ismonthfirsthave; // �Ƿ����¿���ˢ��0û�У�1��
			ObjectSelf.GetInstance().ishavefree = lotteryitem.ishavefree; // �Ƿ�����ѳ齱��0�У���0��Ϊ����ʱ���룩
			ObjectSelf.GetInstance().lotteryitemmap = lotteryitem.lotteryitemmap;        // �ż���������Ϣ��keyΪ������valueΪLotteryItemlayer��
		}
	}	
}
