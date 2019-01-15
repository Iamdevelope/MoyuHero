
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SAddItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SAddItem extends __SAddItem__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787536;

	public int getType() {
		return 787536;
	}

	public byte bagid;
	public java.util.LinkedList<chuhan.gsp.Item> itemdata;

	public SAddItem() {
		itemdata = new java.util.LinkedList<chuhan.gsp.Item>();
	}

	public SAddItem(byte _bagid_, java.util.LinkedList<chuhan.gsp.Item> _itemdata_) {
		this.bagid = _bagid_;
		this.itemdata = _itemdata_;
	}

	public final boolean _validator_() {
		if (bagid < 1) return false;
		for (chuhan.gsp.Item _v_ : itemdata)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bagid);
		_os_.compact_uint32(itemdata.size());
		for (chuhan.gsp.Item _v_ : itemdata) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bagid = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.Item _v_ = new chuhan.gsp.Item();
			_v_.unmarshal(_os_);
			itemdata.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SAddItem) {
			SAddItem _o_ = (SAddItem)_o1_;
			if (bagid != _o_.bagid) return false;
			if (!itemdata.equals(_o_.itemdata)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)bagid;
		_h_ += itemdata.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bagid).append(",");
		_sb_.append(itemdata).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

