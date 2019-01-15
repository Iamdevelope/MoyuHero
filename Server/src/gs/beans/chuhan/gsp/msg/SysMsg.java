
package chuhan.gsp.msg;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class SysMsg implements Marshal {
	public int msgid; // 消息内容Id，当为0时直接显示text
	public java.util.ArrayList<com.goldhuman.Common.Octets> params; // 消息内容参数，msgId不为0时有效
	public java.lang.String text; // 直接文本，msgid为0时有效：title@content
	public long sendroleid; // 发送者的id
	public byte msgtype; // msg类型 0-系统 1-好友
	public java.lang.String sendname; // 发送者姓名
	public long sendtime; // 发送时间

	public SysMsg() {
		params = new java.util.ArrayList<com.goldhuman.Common.Octets>();
		text = "";
		sendname = "";
	}

	public SysMsg(int _msgid_, java.util.ArrayList<com.goldhuman.Common.Octets> _params_, java.lang.String _text_, long _sendroleid_, byte _msgtype_, java.lang.String _sendname_, long _sendtime_) {
		this.msgid = _msgid_;
		this.params = _params_;
		this.text = _text_;
		this.sendroleid = _sendroleid_;
		this.msgtype = _msgtype_;
		this.sendname = _sendname_;
		this.sendtime = _sendtime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(msgid);
		_os_.compact_uint32(params.size());
		for (com.goldhuman.Common.Octets _v_ : params) {
			_os_.marshal(_v_);
		}
		_os_.marshal(text, "UTF-16LE");
		_os_.marshal(sendroleid);
		_os_.marshal(msgtype);
		_os_.marshal(sendname, "UTF-16LE");
		_os_.marshal(sendtime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		msgid = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			com.goldhuman.Common.Octets _v_;
			_v_ = _os_.unmarshal_Octets();
			params.add(_v_);
		}
		text = _os_.unmarshal_String("UTF-16LE");
		sendroleid = _os_.unmarshal_long();
		msgtype = _os_.unmarshal_byte();
		sendname = _os_.unmarshal_String("UTF-16LE");
		sendtime = _os_.unmarshal_long();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SysMsg) {
			SysMsg _o_ = (SysMsg)_o1_;
			if (msgid != _o_.msgid) return false;
			if (!params.equals(_o_.params)) return false;
			if (!text.equals(_o_.text)) return false;
			if (sendroleid != _o_.sendroleid) return false;
			if (msgtype != _o_.msgtype) return false;
			if (!sendname.equals(_o_.sendname)) return false;
			if (sendtime != _o_.sendtime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += msgid;
		_h_ += params.hashCode();
		_h_ += text.hashCode();
		_h_ += (int)sendroleid;
		_h_ += (int)msgtype;
		_h_ += sendname.hashCode();
		_h_ += (int)sendtime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(msgid).append(",");
		_sb_.append(params).append(",");
		_sb_.append("T").append(text.length()).append(",");
		_sb_.append(sendroleid).append(",");
		_sb_.append(msgtype).append(",");
		_sb_.append("T").append(sendname.length()).append(",");
		_sb_.append(sendtime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

