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
	public partial class SGetWordBoss: Protocol
	{
        public bossdata wordboss;

        public const int PROTOCOL_TYPE = 788884;

        public SGetWordBoss()
            : base(PROTOCOL_TYPE)
		 {
             wordboss = new bossdata();
 
		 } 

		public override object Clone()
		{
            SGetWordBoss obj = new SGetWordBoss();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(wordboss);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            wordboss.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {

            ObjectSelf.GetInstance().WorldBossMgr.RefeashWorldBoss(wordboss);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_GetWorldBoss);
        }
	}	
}
