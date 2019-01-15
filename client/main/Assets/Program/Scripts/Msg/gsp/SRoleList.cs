using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.Utils;

namespace GNET
{
    public partial class SRoleList: Protocol
	{

        public LinkedList<RoleInfo> role;

        public const int PROTOCOL_TYPE = 786434;

        public SRoleList()
            : base(PROTOCOL_TYPE)
        {
            role = new LinkedList<RoleInfo>();
        }

		public override object Clone()
		{
            SRoleList obj = new SRoleList();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.compact_uint32(role.Count);
            LinkedListNode<RoleInfo> firstNode = role.First;
            while (firstNode != null)
            {
                os.marshal(role.First.Value);

                role.RemoveFirst();
                firstNode = role.First;
            }
            
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            for (int _size_ = os.uncompact_uint32(); _size_ > 0; --_size_)
            {
                RoleInfo _v_ = new RoleInfo();
                _v_.unmarshal(os);
                role.AddFirst(_v_);
            }

			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 4096; }

        public override void Process() 
        {
            if (role.Count > 0)
            {
                RoleInfo Info = role.First.Value;
                if (Info != null)
                {
                    CEnterWorld EnterWorld = new CEnterWorld();
                    EnterWorld.roleid = Info.roleid;
                    EnterWorld.mac = MainGameControler.Inst.mlient_mac;
                    IOControler.GetInstance().SendProtocol(EnterWorld);

                    MainGameControler.Inst.roleid = Info.roleid;
                        
                }
            }
            else
            {
                CCreateRole CreateRole = new CCreateRole();
                string name = GameUtils.getString("login_content4"); //"сн©м"
                CreateRole.firsthero = 0;
                CreateRole.name = name;
                IOControler.GetInstance().SendProtocol(CreateRole);

            }
        }
	}	
}
