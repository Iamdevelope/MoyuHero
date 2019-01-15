
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class Bag implements Marshal {
	public java.util.ArrayList<chuhan.gsp.Item> items;

	public Bag() {
		items = new java.util.ArrayList<chuhan.gsp.Item>();
	}

	public Bag(java.util.ArrayList<chuhan.gsp.Item> _items_) {
		this.items = _items_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.Item _v_ : items)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.compact_uint32(items.size());
		for (chuhan.gsp.Item _v_ : items) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.Item _v_ = new chuhan.gsp.Item();
			_v_.unmarshal(_os_);
			items.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Bag) {
			Bag _o_ = (Bag)_o1_;
			if (!items.equals(_o_.items)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += items.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(items).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

