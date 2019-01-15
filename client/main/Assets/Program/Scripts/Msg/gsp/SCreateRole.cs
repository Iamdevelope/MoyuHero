using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;

namespace GNET
{
    public partial class SCreateRole : Protocol
    {

        public RoleInfo newinfo; // ������Ľ�ɫ��Ϣ
        public int error; // ʧ��code

        public const int CREATE_OK = 1; // �ɹ�
        public const int CREATE_ERROR = 2; // ʧ��
        public const int CREATE_INVALID = 3; // ���Ʋ��Ϸ�
        public const int CREATE_DUPLICATED = 4; // ����
        public const int CREATE_OVERCOUNT = 5; // �������½�ɫ��������
        public const int CREATE_OVERLEN = 6; // ��ɫ������
        public const int CREATE_SHORTLEN = 7; // ��ɫ������

        public const int PROTOCOL_TYPE = 786436;

        public SCreateRole()
            : base(PROTOCOL_TYPE)
        {
            newinfo = new RoleInfo();
        }

        public override object Clone()
        {
            SCreateRole obj = new SCreateRole();
            return obj;
        }

        public override OctetsStream marshal(OctetsStream os)
        {
            os.marshal(newinfo);
            os.marshal(error);
            return os;
        }

        public override OctetsStream unmarshal(OctetsStream os)
        {
            newinfo.unmarshal(os);
            error = os.unmarshal_int();
            return os;
        }

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 1024; }

        public override void Process()
        {
            if (newinfo != null && error == CREATE_OK)
            {
                CEnterWorld EnterWorld = new CEnterWorld();
                if (EnterWorld != null)
                {
                    EnterWorld.roleid = newinfo.roleid;
                    EnterWorld.mac = MainGameControler.Inst.mlient_mac;
                    IOControler.GetInstance().SendProtocol(EnterWorld);
                }
            }
            else
            {
                switch(error)
                {
                    case CREATE_ERROR:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip1"));//"��ɫ����ʧ�ܣ�
                        break;
                    case CREATE_INVALID:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip2"));//"��ɫ���Ʋ��Ϸ���"
                        break;
                    case CREATE_DUPLICATED:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip3"));//"��ɫ�����ѱ�ռ�ã�"
                        break;
                    case CREATE_OVERCOUNT:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip4"));//"�޷���������Ľ�ɫ��"
                        break;
                    case CREATE_OVERLEN:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip5"));//"��ɫ���ƹ�����"
                        break;
                    case CREATE_SHORTLEN:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip6"));//"��ɫ���ƹ��̣�"
                        break;
                    default:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip7") + error);
                        break;
                }
                
            }
        }
    }
}
