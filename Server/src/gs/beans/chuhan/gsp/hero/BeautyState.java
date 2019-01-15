
package chuhan.gsp.hero;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 美人状态
*/
public class BeautyState implements Marshal , Comparable<BeautyState>{
	public byte beautyid;
	public byte alreadytimes;
	public byte maxtimes;

	public BeautyState() {
	}

	public BeautyState(byte _beautyid_, byte _alreadytimes_, byte _maxtimes_) {
		this.beautyid = _beautyid_;
		this.alreadytimes = _alreadytimes_;
		this.maxtimes = _maxtimes_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(beautyid);
		_os_.marshal(alreadytimes);
		_os_.marshal(maxtimes);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		beautyid = _os_.unmarshal_byte();
		alreadytimes = _os_.unmarshal_byte();
		maxtimes = _os_.unmarshal_byte();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BeautyState) {
			BeautyState _o_ = (BeautyState)_o1_;
			if (beautyid != _o_.beautyid) return false;
			if (alreadytimes != _o_.alreadytimes) return false;
			if (maxtimes != _o_.maxtimes) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)beautyid;
		_h_ += (int)alreadytimes;
		_h_ += (int)maxtimes;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(beautyid).append(",");
		_sb_.append(alreadytimes).append(",");
		_sb_.append(maxtimes).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(BeautyState _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = beautyid - _o_.beautyid;
		if (0 != _c_) return _c_;
		_c_ = alreadytimes - _o_.alreadytimes;
		if (0 != _c_) return _c_;
		_c_ = maxtimes - _o_.maxtimes;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

