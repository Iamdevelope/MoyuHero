using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBuyStateBattleNum: Protocol
	{

        public int buytype; // 购买类型：1为扫荡，2为关卡（需要关卡id）
        public int battleid; // 关卡ID

        public const int PROTOCOL_TYPE = 787948;

        public CBuyStateBattleNum()
            : base(PROTOCOL_TYPE)
		 {
             buytype = 0;
             battleid = 0;
		 } 

		public override object Clone()
		{
            CBuyStateBattleNum obj = new CBuyStateBattleNum();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(buytype);
            _os_.marshal(battleid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            buytype = _os_.unmarshal_int();
            battleid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
