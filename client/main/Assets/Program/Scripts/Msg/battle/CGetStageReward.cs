using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CGetStageReward: Protocol
	{

        public byte stageid; // 章节id
        public byte difficulttype; // 第几个宝箱，从0开始

        public const int PROTOCOL_TYPE = 787940;

        public CGetStageReward()
            : base(PROTOCOL_TYPE)
		 {
             stageid = 0;
             difficulttype = 0;
		 } 

		public override object Clone()
		{
            CGetStageReward obj = new CGetStageReward();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(stageid);
            _os_.marshal(difficulttype);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            stageid = _os_.unmarshal_byte();
            difficulttype = _os_.unmarshal_byte();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
