
package chuhan.gsp.task;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class ActivityInfo implements Marshal {
	public byte id;
	public com.goldhuman.Common.Octets data;

	public ActivityInfo() {
		data = new com.goldhuman.Common.Octets();
	}

	public ActivityInfo(byte _id_, com.goldhuman.Common.Octets _data_) {
		this.id = _id_;
		this.data = _data_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(data);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_byte();
		data = _os_.unmarshal_Octets();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof ActivityInfo) {
			ActivityInfo _o_ = (ActivityInfo)_o1_;
			if (id != _o_.id) return false;
			if (!data.equals(_o_.data)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)id;
		_h_ += data.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append("B").append(data.size()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

