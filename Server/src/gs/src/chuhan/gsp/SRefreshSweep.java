
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshSweep__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshSweep extends __SRefreshSweep__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786456;

	public int getType() {
		return 786456;
	}

	public int sweephavenum; // 今日扫荡剩余次数
	public int sweepbuyhavenum; // 今日扫荡剩余购买次数
	public int mszqgetnum; // 缪斯奏曲
	public int qiyuannum; // 累计祈愿台次数
	public int qiyuanallnum; // 祈愿回合次数，第一次为3，完成后均为5
	public int isqiyuantoday; // 个位是今日是否祈愿，十位为是否断签，0是否，1为是

	public SRefreshSweep() {
	}

	public SRefreshSweep(int _sweephavenum_, int _sweepbuyhavenum_, int _mszqgetnum_, int _qiyuannum_, int _qiyuanallnum_, int _isqiyuantoday_) {
		this.sweephavenum = _sweephavenum_;
		this.sweepbuyhavenum = _sweepbuyhavenum_;
		this.mszqgetnum = _mszqgetnum_;
		this.qiyuannum = _qiyuannum_;
		this.qiyuanallnum = _qiyuanallnum_;
		this.isqiyuantoday = _isqiyuantoday_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(sweephavenum);
		_os_.marshal(sweepbuyhavenum);
		_os_.marshal(mszqgetnum);
		_os_.marshal(qiyuannum);
		_os_.marshal(qiyuanallnum);
		_os_.marshal(isqiyuantoday);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		sweephavenum = _os_.unmarshal_int();
		sweepbuyhavenum = _os_.unmarshal_int();
		mszqgetnum = _os_.unmarshal_int();
		qiyuannum = _os_.unmarshal_int();
		qiyuanallnum = _os_.unmarshal_int();
		isqiyuantoday = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshSweep) {
			SRefreshSweep _o_ = (SRefreshSweep)_o1_;
			if (sweephavenum != _o_.sweephavenum) return false;
			if (sweepbuyhavenum != _o_.sweepbuyhavenum) return false;
			if (mszqgetnum != _o_.mszqgetnum) return false;
			if (qiyuannum != _o_.qiyuannum) return false;
			if (qiyuanallnum != _o_.qiyuanallnum) return false;
			if (isqiyuantoday != _o_.isqiyuantoday) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += sweephavenum;
		_h_ += sweepbuyhavenum;
		_h_ += mszqgetnum;
		_h_ += qiyuannum;
		_h_ += qiyuanallnum;
		_h_ += isqiyuantoday;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(sweephavenum).append(",");
		_sb_.append(sweepbuyhavenum).append(",");
		_sb_.append(mszqgetnum).append(",");
		_sb_.append(qiyuannum).append(",");
		_sb_.append(qiyuanallnum).append(",");
		_sb_.append(isqiyuantoday).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshSweep _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = sweephavenum - _o_.sweephavenum;
		if (0 != _c_) return _c_;
		_c_ = sweepbuyhavenum - _o_.sweepbuyhavenum;
		if (0 != _c_) return _c_;
		_c_ = mszqgetnum - _o_.mszqgetnum;
		if (0 != _c_) return _c_;
		_c_ = qiyuannum - _o_.qiyuannum;
		if (0 != _c_) return _c_;
		_c_ = qiyuanallnum - _o_.qiyuanallnum;
		if (0 != _c_) return _c_;
		_c_ = isqiyuantoday - _o_.isqiyuantoday;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

