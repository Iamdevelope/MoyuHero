
package chuhan.gsp.friends;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSendMsg__ extends xio.Protocol { }

/** 发送消息
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSendMsg extends __CSendMsg__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure() {
			@Override
			protected boolean process() throws Exception {
				lock(xdb.Lockeys.get(xtable.Locks.ROLELOCK, roleId, byroleid));
				if(!FriendRole.getFriendRole(roleId, false).sendMsg(byroleid, msg)) {
					return false;
				}
				return true;
			}
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788245;

	public int getType() {
		return 788245;
	}

	public long byroleid;
	public java.lang.String msg;

	public CSendMsg() {
		msg = "";
	}

	public CSendMsg(long _byroleid_, java.lang.String _msg_) {
		this.byroleid = _byroleid_;
		this.msg = _msg_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(byroleid);
		_os_.marshal(msg, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		byroleid = _os_.unmarshal_long();
		msg = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSendMsg) {
			CSendMsg _o_ = (CSendMsg)_o1_;
			if (byroleid != _o_.byroleid) return false;
			if (!msg.equals(_o_.msg)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)byroleid;
		_h_ += msg.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(byroleid).append(",");
		_sb_.append("T").append(msg.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

