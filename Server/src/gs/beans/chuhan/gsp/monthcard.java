
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 月卡 by yanglk
*/
public class monthcard implements Marshal , Comparable<monthcard>{
	public int monthcardid; // 月卡id
	public long overtime; // 到期时间
	public int istodayget; // 今天是否领取：0未领取，1已领取

	public monthcard() {
	}

	public monthcard(int _monthcardid_, long _overtime_, int _istodayget_) {
		this.monthcardid = _monthcardid_;
		this.overtime = _overtime_;
		this.istodayget = _istodayget_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(monthcardid);
		_os_.marshal(overtime);
		_os_.marshal(istodayget);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		monthcardid = _os_.unmarshal_int();
		overtime = _os_.unmarshal_long();
		istodayget = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof monthcard) {
			monthcard _o_ = (monthcard)_o1_;
			if (monthcardid != _o_.monthcardid) return false;
			if (overtime != _o_.overtime) return false;
			if (istodayget != _o_.istodayget) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += monthcardid;
		_h_ += (int)overtime;
		_h_ += istodayget;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(monthcardid).append(",");
		_sb_.append(overtime).append(",");
		_sb_.append(istodayget).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(monthcard _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = monthcardid - _o_.monthcardid;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(overtime - _o_.overtime);
		if (0 != _c_) return _c_;
		_c_ = istodayget - _o_.istodayget;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

