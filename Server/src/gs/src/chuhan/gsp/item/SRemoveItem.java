
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRemoveItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRemoveItem extends __SRemoveItem__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787535;

	public int getType() {
		return 787535;
	}

	public byte bagid;
	public java.util.LinkedList<Integer> itemkeys;

	public SRemoveItem() {
		itemkeys = new java.util.LinkedList<Integer>();
	}

	public SRemoveItem(byte _bagid_, java.util.LinkedList<Integer> _itemkeys_) {
		this.bagid = _bagid_;
		this.itemkeys = _itemkeys_;
	}

	public final boolean _validator_() {
		if (bagid < 1) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bagid);
		_os_.compact_uint32(itemkeys.size());
		for (Integer _v_ : itemkeys) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bagid = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			itemkeys.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRemoveItem) {
			SRemoveItem _o_ = (SRemoveItem)_o1_;
			if (bagid != _o_.bagid) return false;
			if (!itemkeys.equals(_o_.itemkeys)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)bagid;
		_h_ += itemkeys.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bagid).append(",");
		_sb_.append(itemkeys).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

