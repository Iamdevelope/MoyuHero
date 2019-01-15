
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendTodayRecoverd__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendTodayRecoverd extends __SSendTodayRecoverd__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787553;

	public int getType() {
		return 787553;
	}

	public byte tili; // 今天已经恢复的体力次数
	public byte huoli; // 今天已经恢复的活力次数

	public SSendTodayRecoverd() {
	}

	public SSendTodayRecoverd(byte _tili_, byte _huoli_) {
		this.tili = _tili_;
		this.huoli = _huoli_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(tili);
		_os_.marshal(huoli);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		tili = _os_.unmarshal_byte();
		huoli = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendTodayRecoverd) {
			SSendTodayRecoverd _o_ = (SSendTodayRecoverd)_o1_;
			if (tili != _o_.tili) return false;
			if (huoli != _o_.huoli) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)tili;
		_h_ += (int)huoli;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(tili).append(",");
		_sb_.append(huoli).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SSendTodayRecoverd _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = tili - _o_.tili;
		if (0 != _c_) return _c_;
		_c_ = huoli - _o_.huoli;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

