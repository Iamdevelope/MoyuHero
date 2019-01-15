using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SRefreshStageBattle: Protocol
	{


        public const int PROTOCOL_TYPE = 787939;

        public int stageid; // ÕÂ½Úid
        public StageBattle stagebattle;

        public SRefreshStageBattle()
            : base(PROTOCOL_TYPE)
        {
            stagebattle = new StageBattle();
        }

		public override object Clone()
		{
            SRefreshStageBattle obj = new SRefreshStageBattle();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(stageid);
            os.marshal(stagebattle);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            stageid = os.unmarshal_int();
            stagebattle.unmarshal(os);

			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 4096; }

        public override void Process() 
        {
            ObjectSelf.GetInstance().BattleStageData.UpdateSmallBattleStage(stageid, stagebattle);

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_StageDataRefresh, stagebattle);
        }
	}	
}
