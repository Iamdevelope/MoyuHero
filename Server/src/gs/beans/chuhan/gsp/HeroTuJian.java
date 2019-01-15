
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class HeroTuJian implements Marshal , Comparable<HeroTuJian>{
	public int heroid; // 英雄ID
	public int flag; // 是否满级，0未满，1满级

	public HeroTuJian() {
	}

	public HeroTuJian(int _heroid_, int _flag_) {
		this.heroid = _heroid_;
		this.flag = _flag_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(heroid);
		_os_.marshal(flag);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		heroid = _os_.unmarshal_int();
		flag = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof HeroTuJian) {
			HeroTuJian _o_ = (HeroTuJian)_o1_;
			if (heroid != _o_.heroid) return false;
			if (flag != _o_.flag) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += heroid;
		_h_ += flag;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroid).append(",");
		_sb_.append(flag).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(HeroTuJian _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = heroid - _o_.heroid;
		if (0 != _c_) return _c_;
		_c_ = flag - _o_.flag;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

