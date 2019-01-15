
package chuhan.gsp.msg;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class ChatMsg implements Marshal {
	public java.lang.String rolename; // 消息提示ID
	public java.lang.String chatmsg; // 聊天信息

	public ChatMsg() {
		rolename = "";
		chatmsg = "";
	}

	public ChatMsg(java.lang.String _rolename_, java.lang.String _chatmsg_) {
		this.rolename = _rolename_;
		this.chatmsg = _chatmsg_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(rolename, "UTF-16LE");
		_os_.marshal(chatmsg, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		rolename = _os_.unmarshal_String("UTF-16LE");
		chatmsg = _os_.unmarshal_String("UTF-16LE");
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof ChatMsg) {
			ChatMsg _o_ = (ChatMsg)_o1_;
			if (!rolename.equals(_o_.rolename)) return false;
			if (!chatmsg.equals(_o_.chatmsg)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += rolename.hashCode();
		_h_ += chatmsg.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(rolename.length()).append(",");
		_sb_.append("T").append(chatmsg.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

