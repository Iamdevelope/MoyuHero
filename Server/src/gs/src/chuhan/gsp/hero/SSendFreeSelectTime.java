
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendFreeSelectTime__ extends xio.Protocol { }

/** 上线发送免费点将时间
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendFreeSelectTime extends __SSendFreeSelectTime__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787753;

	public int getType() {
		return 787753;
	}

	public long goldselecttime;
	public long silverselecttime;
	public long bronzeselecttime;
	public byte todaytimes; // 铜牌今天已免费领取次数

	public SSendFreeSelectTime() {
	}

	public SSendFreeSelectTime(long _goldselecttime_, long _silverselecttime_, long _bronzeselecttime_, byte _todaytimes_) {
		this.goldselecttime = _goldselecttime_;
		this.silverselecttime = _silverselecttime_;
		this.bronzeselecttime = _bronzeselecttime_;
		this.todaytimes = _todaytimes_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(goldselecttime);
		_os_.marshal(silverselecttime);
		_os_.marshal(bronzeselecttime);
		_os_.marshal(todaytimes);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		goldselecttime = _os_.unmarshal_long();
		silverselecttime = _os_.unmarshal_long();
		bronzeselecttime = _os_.unmarshal_long();
		todaytimes = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendFreeSelectTime) {
			SSendFreeSelectTime _o_ = (SSendFreeSelectTime)_o1_;
			if (goldselecttime != _o_.goldselecttime) return false;
			if (silverselecttime != _o_.silverselecttime) return false;
			if (bronzeselecttime != _o_.bronzeselecttime) return false;
			if (todaytimes != _o_.todaytimes) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)goldselecttime;
		_h_ += (int)silverselecttime;
		_h_ += (int)bronzeselecttime;
		_h_ += (int)todaytimes;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(goldselecttime).append(",");
		_sb_.append(silverselecttime).append(",");
		_sb_.append(bronzeselecttime).append(",");
		_sb_.append(todaytimes).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SSendFreeSelectTime _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(goldselecttime - _o_.goldselecttime);
		if (0 != _c_) return _c_;
		_c_ = Long.signum(silverselecttime - _o_.silverselecttime);
		if (0 != _c_) return _c_;
		_c_ = Long.signum(bronzeselecttime - _o_.bronzeselecttime);
		if (0 != _c_) return _c_;
		_c_ = todaytimes - _o_.todaytimes;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

