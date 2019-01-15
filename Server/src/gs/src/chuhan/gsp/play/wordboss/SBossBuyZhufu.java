
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SBossBuyZhufu__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SBossBuyZhufu extends __SBossBuyZhufu__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788896;

	public int getType() {
		return 788896;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public int zhufunum; // 祝福次数
	public int shouwangzl; // 守望之灵
	public int bossid; // 值为1234，代表第几个boss

	public SBossBuyZhufu() {
	}

	public SBossBuyZhufu(int _result_, int _zhufunum_, int _shouwangzl_, int _bossid_) {
		this.result = _result_;
		this.zhufunum = _zhufunum_;
		this.shouwangzl = _shouwangzl_;
		this.bossid = _bossid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(zhufunum);
		_os_.marshal(shouwangzl);
		_os_.marshal(bossid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		zhufunum = _os_.unmarshal_int();
		shouwangzl = _os_.unmarshal_int();
		bossid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SBossBuyZhufu) {
			SBossBuyZhufu _o_ = (SBossBuyZhufu)_o1_;
			if (result != _o_.result) return false;
			if (zhufunum != _o_.zhufunum) return false;
			if (shouwangzl != _o_.shouwangzl) return false;
			if (bossid != _o_.bossid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += zhufunum;
		_h_ += shouwangzl;
		_h_ += bossid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(zhufunum).append(",");
		_sb_.append(shouwangzl).append(",");
		_sb_.append(bossid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SBossBuyZhufu _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = result - _o_.result;
		if (0 != _c_) return _c_;
		_c_ = zhufunum - _o_.zhufunum;
		if (0 != _c_) return _c_;
		_c_ = shouwangzl - _o_.shouwangzl;
		if (0 != _c_) return _c_;
		_c_ = bossid - _o_.bossid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

