
package chuhan.gsp.task;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class ShengjiActivity implements Marshal , Comparable<ShengjiActivity>{
	public int lastgetrewardlevel; // 已经领取奖励等级

	public ShengjiActivity() {
	}

	public ShengjiActivity(int _lastgetrewardlevel_) {
		this.lastgetrewardlevel = _lastgetrewardlevel_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(lastgetrewardlevel);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		lastgetrewardlevel = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof ShengjiActivity) {
			ShengjiActivity _o_ = (ShengjiActivity)_o1_;
			if (lastgetrewardlevel != _o_.lastgetrewardlevel) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += lastgetrewardlevel;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lastgetrewardlevel).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(ShengjiActivity _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = lastgetrewardlevel - _o_.lastgetrewardlevel;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

