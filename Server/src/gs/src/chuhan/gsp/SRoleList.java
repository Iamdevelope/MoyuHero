
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRoleList__ extends xio.Protocol { }

/** 服务器发给客户端，已有角色信息列表
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRoleList extends __SRoleList__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786434;

	public int getType() {
		return 786434;
	}

	public java.util.LinkedList<chuhan.gsp.RoleInfo> role;

	public SRoleList() {
		role = new java.util.LinkedList<chuhan.gsp.RoleInfo>();
	}

	public SRoleList(java.util.LinkedList<chuhan.gsp.RoleInfo> _role_) {
		this.role = _role_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.RoleInfo _v_ : role)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(role.size());
		for (chuhan.gsp.RoleInfo _v_ : role) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.RoleInfo _v_ = new chuhan.gsp.RoleInfo();
			_v_.unmarshal(_os_);
			role.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRoleList) {
			SRoleList _o_ = (SRoleList)_o1_;
			if (!role.equals(_o_.role)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += role.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(role).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

