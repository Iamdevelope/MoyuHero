
package chuhan.gsp.play.tanxian;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class stagetanxian implements Marshal {
	public java.util.LinkedList<chuhan.gsp.play.tanxian.tanxian> stagetanxian; // 每章节探险列表

	public stagetanxian() {
		stagetanxian = new java.util.LinkedList<chuhan.gsp.play.tanxian.tanxian>();
	}

	public stagetanxian(java.util.LinkedList<chuhan.gsp.play.tanxian.tanxian> _stagetanxian_) {
		this.stagetanxian = _stagetanxian_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.tanxian.tanxian _v_ : stagetanxian)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.compact_uint32(stagetanxian.size());
		for (chuhan.gsp.play.tanxian.tanxian _v_ : stagetanxian) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.tanxian.tanxian _v_ = new chuhan.gsp.play.tanxian.tanxian();
			_v_.unmarshal(_os_);
			stagetanxian.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof stagetanxian) {
			stagetanxian _o_ = (stagetanxian)_o1_;
			if (!stagetanxian.equals(_o_.stagetanxian)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += stagetanxian.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(stagetanxian).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

