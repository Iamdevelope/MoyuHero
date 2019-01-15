
package chuhan.gsp.friends;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CDelFriend__ extends xio.Protocol { }

/** 删除好友
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CDelFriend extends __CDelFriend__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure() {
			@Override
			protected boolean process() throws Exception {
				lock(xdb.Lockeys.get(xtable.Locks.ROLELOCK, roleId, friendroleid));
				if(!FriendRole.getFriendRole(roleId, false).delete(friendroleid)) {
					return false;
				}
				return true;
			}
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788246;

	public int getType() {
		return 788246;
	}

	public long friendroleid;

	public CDelFriend() {
	}

	public CDelFriend(long _friendroleid_) {
		this.friendroleid = _friendroleid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(friendroleid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		friendroleid = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CDelFriend) {
			CDelFriend _o_ = (CDelFriend)_o1_;
			if (friendroleid != _o_.friendroleid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)friendroleid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(friendroleid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CDelFriend _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(friendroleid - _o_.friendroleid);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

