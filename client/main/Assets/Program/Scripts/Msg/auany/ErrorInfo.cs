using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;

namespace GNET
{

    public enum ErrCode : int
    {
        ERR_SUCCESS = 0,           // 成功
        ERR_ACCOUNTLOCKED = 8,     // 多次重复登陆
        ERR_EXCEED_MAXNUM = 9,     // 游戏服务器人数已达上限
   
    };

    public partial class ErrorInfo: Protocol
	{
        public int errcode;
        public Octets info;

        public const int PROTOCOL_TYPE = 102;

        public ErrorInfo()
            : base(PROTOCOL_TYPE)
		 {
            
		 } 

		public override object Clone()
		{
            ErrorInfo obj = new ErrorInfo();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(errcode);
            os.marshal(info);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            errcode = os.unmarshal_int();
            info = os.unmarshal_Octets();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 512; }

		public override void Process()
        {
            //1 用户名错误
            //2 用户名错误
            //3 用户名错误
            //4 用户名已存在
            if (errcode == -135 )
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("server_msg_tip1"));
            }
            else if(errcode == (int)ErrCode.ERR_ACCOUNTLOCKED)
            {
             
            }
            else
            {
                // TODO...
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("user_name_does_not_exist"));
            }
        }
	}	
}
