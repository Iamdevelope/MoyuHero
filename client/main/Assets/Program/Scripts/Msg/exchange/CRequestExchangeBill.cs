using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class CRequestExchangeBill: Protocol
	{

        public int goodid;  //商品ID
        public int goodnum; //商品数量

        public const int PROTOCOL_TYPE = 788142;

        public CRequestExchangeBill()
            : base(PROTOCOL_TYPE)
		 {
             goodid  = 0;
             goodnum = 0;
		 } 

		public override object Clone()
		{
            CRequestExchangeBill obj = new CRequestExchangeBill();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(goodid);
            _os_.marshal(goodnum);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            goodid = _os_.unmarshal_int();
            goodnum = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 65535; }

        public override void Process() {}
	}	
}
