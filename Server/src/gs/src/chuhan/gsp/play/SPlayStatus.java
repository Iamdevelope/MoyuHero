
package chuhan.gsp.play;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SPlayStatus__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SPlayStatus extends __SPlayStatus__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788335;

	public int getType() {
		return 788335;
	}

	public byte playid; // 玩法ID 参见PlayType
	public byte status; // 每个玩法自定义
	public long nexttime;

	public SPlayStatus() {
	}

	public SPlayStatus(byte _playid_, byte _status_, long _nexttime_) {
		this.playid = _playid_;
		this.status = _status_;
		this.nexttime = _nexttime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(playid);
		_os_.marshal(status);
		_os_.marshal(nexttime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		playid = _os_.unmarshal_byte();
		status = _os_.unmarshal_byte();
		nexttime = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SPlayStatus) {
			SPlayStatus _o_ = (SPlayStatus)_o1_;
			if (playid != _o_.playid) return false;
			if (status != _o_.status) return false;
			if (nexttime != _o_.nexttime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)playid;
		_h_ += (int)status;
		_h_ += (int)nexttime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(playid).append(",");
		_sb_.append(status).append(",");
		_sb_.append(nexttime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SPlayStatus _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = playid - _o_.playid;
		if (0 != _c_) return _c_;
		_c_ = status - _o_.status;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(nexttime - _o_.nexttime);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

