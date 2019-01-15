using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CSweepBattle: Protocol
	{

        public int battleid; // 关卡ID
        public int troopid; // 战队ID
        public byte num; // 扫荡次数，1为1次，其他为10次

        public const int PROTOCOL_TYPE = 787944;

        public CSweepBattle()
            : base(PROTOCOL_TYPE)
		 {
            battleid = 0;
            troopid = 0;
            num = 1;
		 } 

		public override object Clone()
		{
            CSweepBattle obj = new CSweepBattle();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(battleid);
            _os_.marshal(troopid);
            _os_.marshal(num);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            battleid = _os_.unmarshal_int();
            troopid = _os_.unmarshal_int();
            num = _os_.unmarshal_byte();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
