
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshVipBuyInfo__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshVipBuyInfo extends __SRefreshVipBuyInfo__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787555;

	public int getType() {
		return 787555;
	}

	public java.util.HashMap<Integer,Byte> boughtitems; // 已经购买的vip物品数量

	public SRefreshVipBuyInfo() {
		boughtitems = new java.util.HashMap<Integer,Byte>();
	}

	public SRefreshVipBuyInfo(java.util.HashMap<Integer,Byte> _boughtitems_) {
		this.boughtitems = _boughtitems_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(boughtitems.size());
		for (java.util.Map.Entry<Integer, Byte> _e_ : boughtitems.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			byte _v_;
			_v_ = _os_.unmarshal_byte();
			boughtitems.put(_k_, _v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshVipBuyInfo) {
			SRefreshVipBuyInfo _o_ = (SRefreshVipBuyInfo)_o1_;
			if (!boughtitems.equals(_o_.boughtitems)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += boughtitems.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(boughtitems).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

