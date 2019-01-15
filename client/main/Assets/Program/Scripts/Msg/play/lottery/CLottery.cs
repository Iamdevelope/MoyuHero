using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CLottery: Protocol
	{
        public const int ONE = 1;
	    public const int TEN = 2;
	    public const int DREAM = 3;
	    public const int FREE = 4;
		public const int NORMALONE = 5;
		public const int NORMALTEN = 6;
		public const int TOPONE = 7;
		public const int TOPTEN = 8;

	    public int lotterytype;


        public const int PROTOCOL_TYPE = 788733;

        public CLottery()
            : base(PROTOCOL_TYPE)
		 {
             lotterytype = 0;
		 } 

		public override object Clone()
		{
            CLottery obj = new CLottery();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(lotterytype);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            lotterytype = _os_.unmarshal_int();

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
