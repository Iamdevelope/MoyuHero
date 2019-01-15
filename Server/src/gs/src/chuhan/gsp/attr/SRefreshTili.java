
package chuhan.gsp.attr;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshTili__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshTili extends __SRefreshTili__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787441;

	public int getType() {
		return 787441;
	}

	public int tili; // 体力值
	public int titime; // 体力更新剩余时间
	public int xingdongti; // 行动力值
	public int xingdongtitime; // 行动力更新剩余时间
	public int jineng; // 技能点值
	public int jinengtime; // 技能点更新剩余时间

	public SRefreshTili() {
	}

	public SRefreshTili(int _tili_, int _titime_, int _xingdongti_, int _xingdongtitime_, int _jineng_, int _jinengtime_) {
		this.tili = _tili_;
		this.titime = _titime_;
		this.xingdongti = _xingdongti_;
		this.xingdongtitime = _xingdongtitime_;
		this.jineng = _jineng_;
		this.jinengtime = _jinengtime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(tili);
		_os_.marshal(titime);
		_os_.marshal(xingdongti);
		_os_.marshal(xingdongtitime);
		_os_.marshal(jineng);
		_os_.marshal(jinengtime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		tili = _os_.unmarshal_int();
		titime = _os_.unmarshal_int();
		xingdongti = _os_.unmarshal_int();
		xingdongtitime = _os_.unmarshal_int();
		jineng = _os_.unmarshal_int();
		jinengtime = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshTili) {
			SRefreshTili _o_ = (SRefreshTili)_o1_;
			if (tili != _o_.tili) return false;
			if (titime != _o_.titime) return false;
			if (xingdongti != _o_.xingdongti) return false;
			if (xingdongtitime != _o_.xingdongtitime) return false;
			if (jineng != _o_.jineng) return false;
			if (jinengtime != _o_.jinengtime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += tili;
		_h_ += titime;
		_h_ += xingdongti;
		_h_ += xingdongtitime;
		_h_ += jineng;
		_h_ += jinengtime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(tili).append(",");
		_sb_.append(titime).append(",");
		_sb_.append(xingdongti).append(",");
		_sb_.append(xingdongtitime).append(",");
		_sb_.append(jineng).append(",");
		_sb_.append(jinengtime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshTili _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = tili - _o_.tili;
		if (0 != _c_) return _c_;
		_c_ = titime - _o_.titime;
		if (0 != _c_) return _c_;
		_c_ = xingdongti - _o_.xingdongti;
		if (0 != _c_) return _c_;
		_c_ = xingdongtitime - _o_.xingdongtitime;
		if (0 != _c_) return _c_;
		_c_ = jineng - _o_.jineng;
		if (0 != _c_) return _c_;
		_c_ = jinengtime - _o_.jinengtime;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

