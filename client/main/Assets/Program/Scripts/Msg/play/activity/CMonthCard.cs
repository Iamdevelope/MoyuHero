using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CMonthCard: Protocol
	{

        public int cardid;

        public const int PROTOCOL_TYPE = 789040;

        public CMonthCard()
            : base(PROTOCOL_TYPE)
		 {
             cardid = 0;
		 } 

		public override object Clone()
		{
            CMonthCard obj = new CMonthCard();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(cardid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            cardid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
