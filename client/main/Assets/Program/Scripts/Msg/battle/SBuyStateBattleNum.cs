using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SBuyStateBattleNum: Protocol
	{

        public const int END_OK = 1; // 成功
	    public const int END_ERROR = 2; // 失败

        public int endtype;
        public int buytype; // 购买类型：1为扫荡，2为关卡（需要关卡id）



        public const int PROTOCOL_TYPE = 787949;

        public SBuyStateBattleNum()
            : base(PROTOCOL_TYPE)
		 {
             endtype = 0;
             buytype = 0;

		 } 

		public override object Clone()
		{
            SBuyStateBattleNum obj = new SBuyStateBattleNum();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(endtype);
            _os_.marshal(buytype);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            endtype = _os_.unmarshal_int();
            buytype = _os_.unmarshal_int();

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            if (buytype==2)
            {
                if (endtype==END_OK)
                {
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_FightNumSucceed);    
                }
            }
        }
	}	
}
