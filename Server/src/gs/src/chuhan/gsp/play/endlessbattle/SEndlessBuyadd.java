
package chuhan.gsp.play.endlessbattle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEndlessBuyadd__ extends xio.Protocol { }

/** 购买属性增加返回
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEndlessBuyadd extends __SEndlessBuyadd__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788943;

	public int getType() {
		return 788943;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public int add1; // 属性1购买次数
	public int add2; // 属性2购买次数
	public int add3; // 属性3购买次数
	public int add4; // 属性4购买次数（仅计数）

	public SEndlessBuyadd() {
	}

	public SEndlessBuyadd(int _result_, int _add1_, int _add2_, int _add3_, int _add4_) {
		this.result = _result_;
		this.add1 = _add1_;
		this.add2 = _add2_;
		this.add3 = _add3_;
		this.add4 = _add4_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(add1);
		_os_.marshal(add2);
		_os_.marshal(add3);
		_os_.marshal(add4);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		add1 = _os_.unmarshal_int();
		add2 = _os_.unmarshal_int();
		add3 = _os_.unmarshal_int();
		add4 = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEndlessBuyadd) {
			SEndlessBuyadd _o_ = (SEndlessBuyadd)_o1_;
			if (result != _o_.result) return false;
			if (add1 != _o_.add1) return false;
			if (add2 != _o_.add2) return false;
			if (add3 != _o_.add3) return false;
			if (add4 != _o_.add4) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += add1;
		_h_ += add2;
		_h_ += add3;
		_h_ += add4;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(add1).append(",");
		_sb_.append(add2).append(",");
		_sb_.append(add3).append(",");
		_sb_.append(add4).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SEndlessBuyadd _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = result - _o_.result;
		if (0 != _c_) return _c_;
		_c_ = add1 - _o_.add1;
		if (0 != _c_) return _c_;
		_c_ = add2 - _o_.add2;
		if (0 != _c_) return _c_;
		_c_ = add3 - _o_.add3;
		if (0 != _c_) return _c_;
		_c_ = add4 - _o_.add4;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

