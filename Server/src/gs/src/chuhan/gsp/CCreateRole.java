
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CCreateRole__ extends xio.Protocol { }

/** 客户端请求创建
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CCreateRole extends __CCreateRole__ {
	@Override
	protected void process() {
		// protocol handle
		final int userID=((gnet.link.Dispatch)this.getContext()).userid;
		new PCreateRole(this,userID).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786435;

	public int getType() {
		return 786435;
	}

	public java.lang.String name;
	public int firsthero;

	public CCreateRole() {
		name = "";
	}

	public CCreateRole(java.lang.String _name_, int _firsthero_) {
		this.name = _name_;
		this.firsthero = _firsthero_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(name, "UTF-16LE");
		_os_.marshal(firsthero);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		name = _os_.unmarshal_String("UTF-16LE");
		firsthero = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CCreateRole) {
			CCreateRole _o_ = (CCreateRole)_o1_;
			if (!name.equals(_o_.name)) return false;
			if (firsthero != _o_.firsthero) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += name.hashCode();
		_h_ += firsthero;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(name.length()).append(",");
		_sb_.append(firsthero).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

