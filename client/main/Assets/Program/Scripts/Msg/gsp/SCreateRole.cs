using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;

namespace GNET
{
    public partial class SCreateRole : Protocol
    {

        public RoleInfo newinfo; // 创建后的角色信息
        public int error; // 失败code

        public const int CREATE_OK = 1; // 成功
        public const int CREATE_ERROR = 2; // 失败
        public const int CREATE_INVALID = 3; // 名称不合法
        public const int CREATE_DUPLICATED = 4; // 重名
        public const int CREATE_OVERCOUNT = 5; // 创建的新角色数量过多
        public const int CREATE_OVERLEN = 6; // 角色名过长
        public const int CREATE_SHORTLEN = 7; // 角色名过短

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
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip1"));//"角色创建失败！
                        break;
                    case CREATE_INVALID:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip2"));//"角色名称不合法！"
                        break;
                    case CREATE_DUPLICATED:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip3"));//"角色名称已被占用！"
                        break;
                    case CREATE_OVERCOUNT:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip4"));//"无法创建更多的角色！"
                        break;
                    case CREATE_OVERLEN:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip5"));//"角色名称过长！"
                        break;
                    case CREATE_SHORTLEN:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip6"));//"角色名称过短！"
                        break;
                    default:
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("create_role_tip7") + error);
                        break;
                }
                
            }
        }
    }
}
