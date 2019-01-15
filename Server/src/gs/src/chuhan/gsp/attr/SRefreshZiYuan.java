
package chuhan.gsp.attr;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshZiYuan__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshZiYuan extends __SRefreshZiYuan__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 796449;

	public int getType() {
		return 796449;
	}

	public int shenglingzq; // 圣灵之泉
	public int ronglian; // 熔炼点
	public int huangjinxz; // 黄金勋章
	public int baijinxz; // 白金勋章
	public int qingtongxz; // 青铜勋章
	public int chitiexz; // 赤铁勋章
	public int jyjiejing; // 经验结晶

	public SRefreshZiYuan() {
	}

	public SRefreshZiYuan(int _shenglingzq_, int _ronglian_, int _huangjinxz_, int _baijinxz_, int _qingtongxz_, int _chitiexz_, int _jyjiejing_) {
		this.shenglingzq = _shenglingzq_;
		this.ronglian = _ronglian_;
		this.huangjinxz = _huangjinxz_;
		this.baijinxz = _baijinxz_;
		this.qingtongxz = _qingtongxz_;
		this.chitiexz = _chitiexz_;
		this.jyjiejing = _jyjiejing_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(shenglingzq);
		_os_.marshal(ronglian);
		_os_.marshal(huangjinxz);
		_os_.marshal(baijinxz);
		_os_.marshal(qingtongxz);
		_os_.marshal(chitiexz);
		_os_.marshal(jyjiejing);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		shenglingzq = _os_.unmarshal_int();
		ronglian = _os_.unmarshal_int();
		huangjinxz = _os_.unmarshal_int();
		baijinxz = _os_.unmarshal_int();
		qingtongxz = _os_.unmarshal_int();
		chitiexz = _os_.unmarshal_int();
		jyjiejing = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshZiYuan) {
			SRefreshZiYuan _o_ = (SRefreshZiYuan)_o1_;
			if (shenglingzq != _o_.shenglingzq) return false;
			if (ronglian != _o_.ronglian) return false;
			if (huangjinxz != _o_.huangjinxz) return false;
			if (baijinxz != _o_.baijinxz) return false;
			if (qingtongxz != _o_.qingtongxz) return false;
			if (chitiexz != _o_.chitiexz) return false;
			if (jyjiejing != _o_.jyjiejing) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += shenglingzq;
		_h_ += ronglian;
		_h_ += huangjinxz;
		_h_ += baijinxz;
		_h_ += qingtongxz;
		_h_ += chitiexz;
		_h_ += jyjiejing;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shenglingzq).append(",");
		_sb_.append(ronglian).append(",");
		_sb_.append(huangjinxz).append(",");
		_sb_.append(baijinxz).append(",");
		_sb_.append(qingtongxz).append(",");
		_sb_.append(chitiexz).append(",");
		_sb_.append(jyjiejing).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshZiYuan _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = shenglingzq - _o_.shenglingzq;
		if (0 != _c_) return _c_;
		_c_ = ronglian - _o_.ronglian;
		if (0 != _c_) return _c_;
		_c_ = huangjinxz - _o_.huangjinxz;
		if (0 != _c_) return _c_;
		_c_ = baijinxz - _o_.baijinxz;
		if (0 != _c_) return _c_;
		_c_ = qingtongxz - _o_.qingtongxz;
		if (0 != _c_) return _c_;
		_c_ = chitiexz - _o_.chitiexz;
		if (0 != _c_) return _c_;
		_c_ = jyjiejing - _o_.jyjiejing;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

