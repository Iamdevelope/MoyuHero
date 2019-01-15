
package chuhan.gsp.task;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class LiangCaoActivity implements Marshal , Comparable<LiangCaoActivity>{
	public byte firstuse;
	public byte senconduse;

	public LiangCaoActivity() {
	}

	public LiangCaoActivity(byte _firstuse_, byte _senconduse_) {
		this.firstuse = _firstuse_;
		this.senconduse = _senconduse_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(firstuse);
		_os_.marshal(senconduse);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		firstuse = _os_.unmarshal_byte();
		senconduse = _os_.unmarshal_byte();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof LiangCaoActivity) {
			LiangCaoActivity _o_ = (LiangCaoActivity)_o1_;
			if (firstuse != _o_.firstuse) return false;
			if (senconduse != _o_.senconduse) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)firstuse;
		_h_ += (int)senconduse;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(firstuse).append(",");
		_sb_.append(senconduse).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(LiangCaoActivity _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = firstuse - _o_.firstuse;
		if (0 != _c_) return _c_;
		_c_ = senconduse - _o_.senconduse;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

