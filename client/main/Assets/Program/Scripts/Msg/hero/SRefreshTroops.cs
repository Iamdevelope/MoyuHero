using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshTroops: Protocol
	{
	
        public LinkedList<Troop> troops;

        public const int PROTOCOL_TYPE = 787734;

        public SRefreshTroops()
            : base(PROTOCOL_TYPE)
		 {
             troops = new LinkedList<Troop>();
		 } 

		public override object Clone()
		{
            SRefreshTroops obj = new SRefreshTroops();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(troops.Count);
            LinkedListNode<Troop> firstNode2 = troops.First;
            while (firstNode2 != null)
            {
                _os_.marshal(troops.First.Value);

                troops.RemoveFirst();
                firstNode2 = troops.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Troop _v_ = new Troop();
                _v_.unmarshal(_os_);
                troops.AddFirst(_v_);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            int nCount = 0;
            
            X_GUID pGUID = new X_GUID();
            foreach (Troop item in troops)// ±à¶ÓÐÅÏ¢ [3/31/2015 Zmy]
            {
                if (nCount >= 0 && nCount < GlobalMembers.MAX_MATRIX_COUNT)
                {
                    ObjectSelf.GetInstance().Teams.SetNumTypeDic(item.troopnum,item.trooptype);
                    pGUID.GUID_value = item.location1;
                    ObjectSelf.GetInstance().Teams.SetTeamMember(item.troopnum, pGUID, 0);
                    pGUID.GUID_value = item.location2;
                    ObjectSelf.GetInstance().Teams.SetTeamMember(item.troopnum, pGUID, 1);
                    pGUID.GUID_value = item.location3;
                    ObjectSelf.GetInstance().Teams.SetTeamMember(item.troopnum, pGUID, 2);
                    pGUID.GUID_value = item.location4;
                    ObjectSelf.GetInstance().Teams.SetTeamMember(item.troopnum, pGUID, 3);
                    pGUID.GUID_value = item.location5;
                    ObjectSelf.GetInstance().Teams.SetTeamMember(item.troopnum, pGUID, 4);

                    ObjectSelf.GetInstance().Teams.m_GodSoulID1 = item.sh1;
                    ObjectSelf.GetInstance().Teams.m_GodSoulID2 = item.sh2;
                    ObjectSelf.GetInstance().Teams.m_GodSoulID3 = item.sh3;
                    ObjectSelf.GetInstance().Teams.m_GodSoulID4 = item.sh4;
                    nCount++;
                }
            }
            pGUID = null;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Formation_Update);
		}
			
	}	
}
