
package chuhan.gsp.battle.realtime;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class FacePlace implements Marshal {
	public float x;
	public float y;
	public float z;
	public float w;

	public FacePlace() {
	}

	public FacePlace(float _x_, float _y_, float _z_, float _w_) {
		this.x = _x_;
		this.y = _y_;
		this.z = _z_;
		this.w = _w_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(x);
		_os_.marshal(y);
		_os_.marshal(z);
		_os_.marshal(w);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		x = _os_.unmarshal_float();
		y = _os_.unmarshal_float();
		z = _os_.unmarshal_float();
		w = _os_.unmarshal_float();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof FacePlace) {
			FacePlace _o_ = (FacePlace)_o1_;
			if (x != _o_.x) return false;
			if (y != _o_.y) return false;
			if (z != _o_.z) return false;
			if (w != _o_.w) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += Float.floatToIntBits(x);
		_h_ += Float.floatToIntBits(y);
		_h_ += Float.floatToIntBits(z);
		_h_ += Float.floatToIntBits(w);
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(x).append(",");
		_sb_.append(y).append(",");
		_sb_.append(z).append(",");
		_sb_.append(w).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

