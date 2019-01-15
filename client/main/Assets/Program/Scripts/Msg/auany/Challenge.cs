using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.LogSystem;

namespace GNET
{
    public partial class Challenge: Protocol
	{
        public int version;
        public const int PROTOCOL_TYPE = 103;

        public Challenge()
            : base(PROTOCOL_TYPE)
		 {
            
		 } 

		public override object Clone()
		{
            Challenge obj = new Challenge();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(version);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            version = os.unmarshal_int();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 64; }

		public override void Process()
        {
      
            string nServerID = ConfigsManager.Inst.GetClientConfig(ClientConfigs.ServerID);
            if (nServerID != string.Empty )
            {
                // �������汾��
                if (version == 2052)
                {
                    // 1��login״̬
                    UserLogin auany = new UserLogin();
                    auany.account = new Octets();
                    auany.account.setString(MainGameControler.Inst.mPlatId);
                    auany.response = new Octets();
                    auany.response.setString(MainGameControler.Inst.mToken);
                    auany.challenge = new Octets();
                    auany.challenge.setString(nServerID);
                    IOControler.GetInstance().SendLink(auany);
                }
                else
                {
                    // �汾�Ų���
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_GameTips.UI_ResPath);
                    UI_GameTips.Inst.AddMsg(GameUtils.getString("server_msg_tip6") + version);//"��Ϸ�汾�Ÿ���������ƥ�䣡��ǰ�������汾�ţ�" 
                }
            }
            else
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("error #100053"));
            }
        }
	}	
}
