using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
namespace GNET
{
	public partial class SRefreshStage: Protocol
	{

        public int id;
        public byte starsum;
        public byte rewardgot;

        public const int PROTOCOL_TYPE = 787935;

        public SRefreshStage()
            : base(PROTOCOL_TYPE)
        {
            
        }

		public override object Clone()
		{
            SRefreshStage obj = new SRefreshStage();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(id);
            os.marshal(starsum);
            os.marshal(rewardgot);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            id = os.unmarshal_int();
            starsum = os.unmarshal_byte();
            rewardgot = os.unmarshal_byte();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 4096; }

        public override void Process() 
        {
            ObjectSelf.GetInstance().SetIsNewMap(false);
            ObjectSelf.GetInstance().BattleStageData.UpdateBigBattleStage(id, starsum, rewardgot);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_ShowBox);
        }
	}	
}
