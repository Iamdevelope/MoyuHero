using System.Collections.Generic;
using DreamFaction.GameEventSystem;

namespace GNET
{
    public partial class SSendMsgNotify: Protocol
	{

        public int msgid; // 消息提示ID
        public List<Octets> parameters = new List<Octets>(); // 参数

        public const int PROTOCOL_TYPE = 787633;

        public SSendMsgNotify()
            : base(PROTOCOL_TYPE)
		 {
             parameters = new List<Octets>();
		 } 

		public override object Clone()
		{
            SSendMsgNotify obj = new SSendMsgNotify();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(msgid);
            os.compact_uint32(parameters.Count);
		    foreach(Octets _v_ in parameters) {
			    os.marshal(_v_);
		    }
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            msgid = os.unmarshal_int();
            for (int _size_ = os.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Octets _v_;
                _v_ = os.unmarshal_Octets();
                parameters.Add(_v_);
            }
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process() 
        {
            var _table = DataTemplate.GetInstance().m_CaptionTable;
            if (_table.getData().ContainsKey(msgid))
            {

                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_ReceiveCaptionMessage,this);
            }
            else
            {
                // 通知UI显示提示信息
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MsgNotify, this);
            }
        
        }
	}	
}
