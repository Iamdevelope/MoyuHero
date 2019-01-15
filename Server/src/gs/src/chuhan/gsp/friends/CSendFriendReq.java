
package chuhan.gsp.friends;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSendFriendReq__ extends xio.Protocol { }

/** 发送一个好友请求
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSendFriendReq extends __CSendFriendReq__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure() {
			@Override
			protected boolean process() throws Exception {
				lock(xdb.Lockeys.get(xtable.Locks.ROLELOCK, roleId, byreqroleid));
				if(!FriendRole.getFriendRole(roleId, false).request(byreqroleid)) {
					return false;
				}
				return true;
			}
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788234;

	public int getType() {
		return 788234;
	}

	public long byreqroleid;

	public CSendFriendReq() {
	}

	public CSendFriendReq(long _byreqroleid_) {
		this.byreqroleid = _byreqroleid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(byreqroleid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		byreqroleid = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSendFriendReq) {
			CSendFriendReq _o_ = (CSendFriendReq)_o1_;
			if (byreqroleid != _o_.byreqroleid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)byreqroleid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(byreqroleid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CSendFriendReq _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(byreqroleid - _o_.byreqroleid);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

