using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SGetMyWordBoss: Protocol
	{
        public int bossid; // 值为1234，代表第几个boss
        public bossrole mywordboss;

        public const int PROTOCOL_TYPE = 788886;

        public SGetMyWordBoss()
            : base(PROTOCOL_TYPE)
		 {
             mywordboss = new bossrole();
 
		 } 

		public override object Clone()
		{
            SGetMyWordBoss obj = new SGetMyWordBoss();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bossid);
            _os_.marshal(mywordboss);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bossid = _os_.unmarshal_int();
            mywordboss.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            ObjectSelf.GetInstance().WorldBossMgr.RefeashBossRole(bossid, mywordboss);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_GetMyWorldBoss);
        }
	}	
}
