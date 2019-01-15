
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SShowAddItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SShowAddItem extends __SShowAddItem__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787546;

	public int getType() {
		return 787546;
	}

	public java.util.ArrayList<chuhan.gsp.item.ShowItemData> data;

	public SShowAddItem() {
		data = new java.util.ArrayList<chuhan.gsp.item.ShowItemData>();
	}

	public SShowAddItem(java.util.ArrayList<chuhan.gsp.item.ShowItemData> _data_) {
		this.data = _data_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.item.ShowItemData _v_ : data)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(data.size());
		for (chuhan.gsp.item.ShowItemData _v_ : data) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.item.ShowItemData _v_ = new chuhan.gsp.item.ShowItemData();
			_v_.unmarshal(_os_);
			data.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SShowAddItem) {
			SShowAddItem _o_ = (SShowAddItem)_o1_;
			if (!data.equals(_o_.data)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += data.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(data).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

