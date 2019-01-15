
package chuhan.gsp.battle.realtime;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BDamageInfo implements Marshal , Comparable<BDamageInfo>{
	public int damagept; // 伤害
	public int iswtf;
	public int ismiss;
	public int isheal;

	public BDamageInfo() {
	}

	public BDamageInfo(int _damagept_, int _iswtf_, int _ismiss_, int _isheal_) {
		this.damagept = _damagept_;
		this.iswtf = _iswtf_;
		this.ismiss = _ismiss_;
		this.isheal = _isheal_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(damagept);
		_os_.marshal(iswtf);
		_os_.marshal(ismiss);
		_os_.marshal(isheal);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		damagept = _os_.unmarshal_int();
		iswtf = _os_.unmarshal_int();
		ismiss = _os_.unmarshal_int();
		isheal = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BDamageInfo) {
			BDamageInfo _o_ = (BDamageInfo)_o1_;
			if (damagept != _o_.damagept) return false;
			if (iswtf != _o_.iswtf) return false;
			if (ismiss != _o_.ismiss) return false;
			if (isheal != _o_.isheal) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += damagept;
		_h_ += iswtf;
		_h_ += ismiss;
		_h_ += isheal;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(damagept).append(",");
		_sb_.append(iswtf).append(",");
		_sb_.append(ismiss).append(",");
		_sb_.append(isheal).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(BDamageInfo _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = damagept - _o_.damagept;
		if (0 != _c_) return _c_;
		_c_ = iswtf - _o_.iswtf;
		if (0 != _c_) return _c_;
		_c_ = ismiss - _o_.ismiss;
		if (0 != _c_) return _c_;
		_c_ = isheal - _o_.isheal;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

