using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using Platform;
using DreamFaction.GameCore;

namespace GNET
{
	public partial class SReplyExchangeBill: Protocol
	{

        public string billid;      //������
        public int    goodid;      //��ƷID
        public string goodname;    //��Ʒ����
        public int    goodnum;     //��Ʒ����
        public string price;       //�۸�
        public int    zoneid;      //gs_zoneid;

        public const int PROTOCOL_TYPE = 788143;

        public SReplyExchangeBill()
            : base(PROTOCOL_TYPE)
		 {
             billid = "";
             goodid = 0;
             goodname = "";
             goodnum = 0;
             price = "";
             zoneid = 0;
		 } 

		public override object Clone()
		{
            SReplyExchangeBill obj = new SReplyExchangeBill();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(billid);
            _os_.marshal(goodid);
            _os_.marshal(goodname);
            _os_.marshal(goodnum);
            _os_.marshal(price);
            _os_.marshal(zoneid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            billid = _os_.unmarshal_String();
            goodid = _os_.unmarshal_int();
            goodname = _os_.unmarshal_String();
            goodnum = _os_.unmarshal_int();
            price = _os_.unmarshal_String();
            zoneid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 65535; }

        public override void Process() 
        {
            if (SceneManager.Inst.CurScene.Equals(SceneEntry.Home.ToString()))
            {
                //TODO
                CExchange exchange = new CExchange();
                exchange.uid = MainGameControler.Inst.mPlatId;
                exchange.token = MainGameControler.Inst.mToken;
                exchange.billid = this.billid;
                exchange.goodsid = this.goodid;
                exchange.goodsname = this.goodname;
                exchange.goodsnum = this.goodnum;
                exchange.price = this.price;
                exchange.zoneid = this.zoneid;
                IOControler.GetInstance().SendPlatform(exchange);
            }
        }
	}	
}
