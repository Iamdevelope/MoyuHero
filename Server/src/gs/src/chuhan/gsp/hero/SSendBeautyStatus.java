
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendBeautyStatus__ extends xio.Protocol { }

/** 缠绵状态
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendBeautyStatus extends __SSendBeautyStatus__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787756;

	public int getType() {
		return 787756;
	}

	public long lastsleeptime; // 上次睡觉时间
	public byte sleeptimes; // 今天缠绵次数，注意过零点时清零

	public SSendBeautyStatus() {
	}

	public SSendBeautyStatus(long _lastsleeptime_, byte _sleeptimes_) {
		this.lastsleeptime = _lastsleeptime_;
		this.sleeptimes = _sleeptimes_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(lastsleeptime);
		_os_.marshal(sleeptimes);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		lastsleeptime = _os_.unmarshal_long();
		sleeptimes = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendBeautyStatus) {
			SSendBeautyStatus _o_ = (SSendBeautyStatus)_o1_;
			if (lastsleeptime != _o_.lastsleeptime) return false;
			if (sleeptimes != _o_.sleeptimes) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)lastsleeptime;
		_h_ += (int)sleeptimes;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lastsleeptime).append(",");
		_sb_.append(sleeptimes).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SSendBeautyStatus _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(lastsleeptime - _o_.lastsleeptime);
		if (0 != _c_) return _c_;
		_c_ = sleeptimes - _o_.sleeptimes;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

