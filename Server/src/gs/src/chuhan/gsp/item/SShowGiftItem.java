
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SShowGiftItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SShowGiftItem extends __SShowGiftItem__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787564;

	public int getType() {
		return 787564;
	}

	public java.util.LinkedList<Integer> giftitems;

	public SShowGiftItem() {
		giftitems = new java.util.LinkedList<Integer>();
	}

	public SShowGiftItem(java.util.LinkedList<Integer> _giftitems_) {
		this.giftitems = _giftitems_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(giftitems.size());
		for (Integer _v_ : giftitems) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			giftitems.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SShowGiftItem) {
			SShowGiftItem _o_ = (SShowGiftItem)_o1_;
			if (!giftitems.equals(_o_.giftitems)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += giftitems.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(giftitems).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

