
package chuhan.gsp.play.activity;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshLogin__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshLogin extends __SRefreshLogin__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 789043;

	public int getType() {
		return 789043;
	}

	public int signnum7;
	public int signnum28;

	public SRefreshLogin() {
	}

	public SRefreshLogin(int _signnum7_, int _signnum28_) {
		this.signnum7 = _signnum7_;
		this.signnum28 = _signnum28_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(signnum7);
		_os_.marshal(signnum28);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		signnum7 = _os_.unmarshal_int();
		signnum28 = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshLogin) {
			SRefreshLogin _o_ = (SRefreshLogin)_o1_;
			if (signnum7 != _o_.signnum7) return false;
			if (signnum28 != _o_.signnum28) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += signnum7;
		_h_ += signnum28;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(signnum7).append(",");
		_sb_.append(signnum28).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshLogin _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = signnum7 - _o_.signnum7;
		if (0 != _c_) return _c_;
		_c_ = signnum28 - _o_.signnum28;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

