using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class SPlayStatus: Protocol
	{

        public byte playid; // 玩法ID 参见PlayType
	    public byte status; // 每个玩法自定义
	    public long nexttime;

        public const int PROTOCOL_TYPE = 788335;

        public SPlayStatus()
            : base(PROTOCOL_TYPE)
        {
            playid = 0;
            status = 0;
            nexttime = 0;
        }

		public override object Clone()
		{
            SPlayStatus obj = new SPlayStatus();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(playid);
            os.marshal(status);
            os.marshal(nexttime);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{

            playid = os.unmarshal_byte();
            status = os.unmarshal_byte();
            nexttime = os.unmarshal_long();

			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 4096; }

        public override void Process() 
        {
			

        }
	}	
}
