
package chuhan.gsp.friends;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CToTi__ extends xio.Protocol { }

/** 送体力
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CToTi extends __CToTi__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure() {
			@Override
			protected boolean process() throws Exception {
				lock(xdb.Lockeys.get(xtable.Locks.ROLELOCK, roleId, byroleid));
				if(!FriendRole.getFriendRole(roleId, false).toFriTi(byroleid)) {
					return false;
				}
				return true;
			}
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788243;

	public int getType() {
		return 788243;
	}

	public long byroleid;

	public CToTi() {
	}

	public CToTi(long _byroleid_) {
		this.byroleid = _byroleid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(byroleid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		byroleid = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CToTi) {
			CToTi _o_ = (CToTi)_o1_;
			if (byroleid != _o_.byroleid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)byroleid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(byroleid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CToTi _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(byroleid - _o_.byroleid);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

