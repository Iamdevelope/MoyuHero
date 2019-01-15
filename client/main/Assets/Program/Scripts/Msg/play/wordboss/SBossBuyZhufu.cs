using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SBossBuyZhufu: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

        public int result;
        public int zhufunum; // 祝福次数
        public int shouwangzl; // 守望之灵
        public int bossid; // 值为1234，代表第几个boss

        public const int PROTOCOL_TYPE = 788896;

        public SBossBuyZhufu()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SBossBuyZhufu obj = new SBossBuyZhufu();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            _os_.marshal(result);
            _os_.marshal(zhufunum);
            _os_.marshal(shouwangzl);
            _os_.marshal(bossid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            zhufunum = _os_.unmarshal_int();
            shouwangzl = _os_.unmarshal_int();
            bossid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().WorldBossMgr.m_ShouWangZL = shouwangzl;
            ObjectSelf.GetInstance().WorldBossMgr.m_BossDataMap[bossid].m_BossRoleDB.m_BlessNum = zhufunum;
            WorldBossCallbackParaPackage package = new WorldBossCallbackParaPackage();
            package.m_Result = result;
            package.m_BossID = bossid;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_SBuyBossBlessing, package);
		}
			
	}	
}
