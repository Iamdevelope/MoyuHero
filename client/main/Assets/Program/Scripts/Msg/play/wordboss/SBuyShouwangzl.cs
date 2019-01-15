using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SBuyShouwangzl: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

        public int result;
        public int shouwangzl; // 守望之灵总数

        public const int PROTOCOL_TYPE = 788898;

        public SBuyShouwangzl()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SBuyShouwangzl obj = new SBuyShouwangzl();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            _os_.marshal(result);
            _os_.marshal(shouwangzl);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            shouwangzl = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            if (result == SBuyShouwangzl.END_OK)
            {
                ObjectSelf.GetInstance().WorldBossMgr.m_ShouWangZL = shouwangzl;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_SBuyWatcherSoul);
            }
		}
			
	}	
}
