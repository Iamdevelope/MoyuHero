using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GNET
{
	public partial class SRefreshSingleGameAct: Protocol
	{
        public gactivity gameactivity;

        public const int PROTOCOL_TYPE = 789052;

        public SRefreshSingleGameAct()
            : base(PROTOCOL_TYPE)
		 {
             gameactivity = new gactivity();
		 } 

		public override object Clone()
		{
            SRefreshSingleGameAct obj = new SRefreshSingleGameAct();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(gameactivity);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            gameactivity.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 65535; }

        public override void Process() 
        {
            ObjectSelf.GetInstance().GetActivityOverviewMar().RefreshSingleGameAct(gameactivity);
        }
	}	
}
