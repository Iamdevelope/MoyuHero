using UnityEngine;
using DreamFaction.GameCore;
using DreamFaction.Utils;

namespace GNET
{
    public partial class Response: Protocol
	{

        public Octets identity;
        public Octets response;

        public const int PROTOCOL_TYPE = 104;

        public Response()
            : base(PROTOCOL_TYPE)
		 {
             
		 } 

		public override object Clone()
		{
            Response obj = new Response();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(identity);
            os.marshal(response);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            identity = os.unmarshal_Octets();
            response = os.unmarshal_Octets();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process()
        {

            string accont = identity.getString("utf-8");
            string respns = response.getString("utf-8");
            if ( SocketManager.GetState() == SocketState.state_connectok )
            {
                CRoleList roleList = new CRoleList();
                roleList.mac = GameUtils.GetMac();
                IOControler.GetInstance().SendProtocol(roleList);

                MainGameControler.Inst.mlient_mac = roleList.mac;
            }
        }
	}	
}
