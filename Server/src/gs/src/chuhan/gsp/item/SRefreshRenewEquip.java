
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshRenewEquip__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshRenewEquip extends __SRefreshRenewEquip__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787551;

	public int getType() {
		return 787551;
	}

	public byte isbaoji; // 是否暴击

	public SRefreshRenewEquip() {
	}

	public SRefreshRenewEquip(byte _isbaoji_) {
		this.isbaoji = _isbaoji_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(isbaoji);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		isbaoji = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshRenewEquip) {
			SRefreshRenewEquip _o_ = (SRefreshRenewEquip)_o1_;
			if (isbaoji != _o_.isbaoji) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)isbaoji;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(isbaoji).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshRenewEquip _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = isbaoji - _o_.isbaoji;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

