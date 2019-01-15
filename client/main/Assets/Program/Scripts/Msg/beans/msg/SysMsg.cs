using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
    public class SysMsg : Marshal
	{
  
        public int msgid;               // 消息内容Id，当为0时直接显示text
        public ArrayList parameters;    // 消息内容参数，msgId不为0时有效
	    public string text;             // 直接文本，msgid为0时有效：title@content
	    public long sendroleid;         // 发送者的id
	    public byte msgtype;            // msg类型 0-系统 1-好友
	    public string sendname;         // 发送者姓名
	    public long sendtime;           // 发送时间

        public SysMsg()
        {
           parameters = new ArrayList();
		   text = "";
		   sendname = "";
        }

    	public SysMsg(int _msgid_, ArrayList _params_, string _text_, long _sendroleid_, byte _msgtype_, string _sendname_, long _sendtime_)
        {
		    this.msgid = _msgid_;
            this.parameters = _params_;
		    this.text = _text_;
		    this.sendroleid = _sendroleid_;
		    this.msgtype = _msgtype_;
		    this.sendname = _sendname_;
		    this.sendtime = _sendtime_;
	    }

		public override OctetsStream marshal(OctetsStream _os_)
		{
           	_os_.marshal(msgid);
            _os_.compact_uint32(parameters.Count);
            foreach (Octets _v_ in parameters)
            {
			    _os_.marshal(_v_);
		    }
		    _os_.marshal(text);
		    _os_.marshal(sendroleid);
		    _os_.marshal(msgtype);
		    _os_.marshal(sendname);
		    _os_.marshal(sendtime);

		    return _os_;

		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
           
            msgid = _os_.unmarshal_int();
		    for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) 
            {
			    Octets _v_;
			    _v_ = _os_.unmarshal_Octets();
                parameters.Add(_v_);
		    }
		    text = _os_.unmarshal_String();
		    sendroleid = _os_.unmarshal_long();
		    msgtype = _os_.unmarshal_byte();
		    sendname = _os_.unmarshal_String();
		    sendtime = _os_.unmarshal_long();
            return _os_;
		}

	}
}
