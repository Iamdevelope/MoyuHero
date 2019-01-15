
package chuhan.gsp.play.raidboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSelectHelpers__ extends xio.Protocol { }

/** 返回选中的助战好友
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSelectHelpers extends __SSelectHelpers__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788538;

	public int getType() {
		return 788538;
	}

	public java.util.LinkedList<chuhan.gsp.play.SelectHelpers> helpers;

	public SSelectHelpers() {
		helpers = new java.util.LinkedList<chuhan.gsp.play.SelectHelpers>();
	}

	public SSelectHelpers(java.util.LinkedList<chuhan.gsp.play.SelectHelpers> _helpers_) {
		this.helpers = _helpers_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.SelectHelpers _v_ : helpers)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(helpers.size());
		for (chuhan.gsp.play.SelectHelpers _v_ : helpers) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.SelectHelpers _v_ = new chuhan.gsp.play.SelectHelpers();
			_v_.unmarshal(_os_);
			helpers.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSelectHelpers) {
			SSelectHelpers _o_ = (SSelectHelpers)_o1_;
			if (!helpers.equals(_o_.helpers)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += helpers.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(helpers).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

