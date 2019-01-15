
package chuhan.gsp.mail;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SIsHaveNotOpen__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SIsHaveNotOpen extends __SIsHaveNotOpen__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786935;

	public int getType() {
		return 786935;
	}

	public int ishave; // 是否有未读邮件

	public SIsHaveNotOpen() {
	}

	public SIsHaveNotOpen(int _ishave_) {
		this.ishave = _ishave_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(ishave);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		ishave = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SIsHaveNotOpen) {
			SIsHaveNotOpen _o_ = (SIsHaveNotOpen)_o1_;
			if (ishave != _o_.ishave) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += ishave;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(ishave).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SIsHaveNotOpen _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = ishave - _o_.ishave;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

