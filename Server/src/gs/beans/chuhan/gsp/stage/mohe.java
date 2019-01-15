
package chuhan.gsp.stage;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class mohe implements Marshal , Comparable<mohe>{
	public int id; // id
	public int isopen; // 是否开启（1开启，0未开启）
	public int place; // 排序（0为随机排序，123为正常排序）

	public mohe() {
	}

	public mohe(int _id_, int _isopen_, int _place_) {
		this.id = _id_;
		this.isopen = _isopen_;
		this.place = _place_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(isopen);
		_os_.marshal(place);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		isopen = _os_.unmarshal_int();
		place = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof mohe) {
			mohe _o_ = (mohe)_o1_;
			if (id != _o_.id) return false;
			if (isopen != _o_.isopen) return false;
			if (place != _o_.place) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += isopen;
		_h_ += place;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(isopen).append(",");
		_sb_.append(place).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(mohe _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = isopen - _o_.isopen;
		if (0 != _c_) return _c_;
		_c_ = place - _o_.place;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

