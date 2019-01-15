
package chuhan.gsp.play.camp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SHelpers__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SHelpers extends __SHelpers__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788644;

	public int getType() {
		return 788644;
	}

	public java.util.LinkedList<chuhan.gsp.play.Helpers> helpers;

	public SHelpers() {
		helpers = new java.util.LinkedList<chuhan.gsp.play.Helpers>();
	}

	public SHelpers(java.util.LinkedList<chuhan.gsp.play.Helpers> _helpers_) {
		this.helpers = _helpers_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.Helpers _v_ : helpers)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(helpers.size());
		for (chuhan.gsp.play.Helpers _v_ : helpers) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.Helpers _v_ = new chuhan.gsp.play.Helpers();
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
		if (_o1_ instanceof SHelpers) {
			SHelpers _o_ = (SHelpers)_o1_;
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

