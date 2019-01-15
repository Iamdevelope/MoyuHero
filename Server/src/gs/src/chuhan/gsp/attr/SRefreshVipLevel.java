
package chuhan.gsp.attr;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshVipLevel__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshVipLevel extends __SRefreshVipLevel__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787438;

	public int getType() {
		return 787438;
	}

	public byte viplv;
	public int vipexp;

	public SRefreshVipLevel() {
	}

	public SRefreshVipLevel(byte _viplv_, int _vipexp_) {
		this.viplv = _viplv_;
		this.vipexp = _vipexp_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(viplv);
		_os_.marshal(vipexp);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		viplv = _os_.unmarshal_byte();
		vipexp = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshVipLevel) {
			SRefreshVipLevel _o_ = (SRefreshVipLevel)_o1_;
			if (viplv != _o_.viplv) return false;
			if (vipexp != _o_.vipexp) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)viplv;
		_h_ += vipexp;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(viplv).append(",");
		_sb_.append(vipexp).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshVipLevel _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = viplv - _o_.viplv;
		if (0 != _c_) return _c_;
		_c_ = vipexp - _o_.vipexp;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

