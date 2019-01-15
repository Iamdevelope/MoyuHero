
package chuhan.gsp.play.shop;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshNewShop__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshNewShop extends __SRefreshNewShop__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788838;

	public int getType() {
		return 788838;
	}

	public java.util.HashMap<Integer,chuhan.gsp.play.shop.NewShopList> shopmap; // 整个商城map，key为76表的序列号

	public SRefreshNewShop() {
		shopmap = new java.util.HashMap<Integer,chuhan.gsp.play.shop.NewShopList>();
	}

	public SRefreshNewShop(java.util.HashMap<Integer,chuhan.gsp.play.shop.NewShopList> _shopmap_) {
		this.shopmap = _shopmap_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.shop.NewShopList> _e_ : shopmap.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(shopmap.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.shop.NewShopList> _e_ : shopmap.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.play.shop.NewShopList _v_ = new chuhan.gsp.play.shop.NewShopList();
			_v_.unmarshal(_os_);
			shopmap.put(_k_, _v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshNewShop) {
			SRefreshNewShop _o_ = (SRefreshNewShop)_o1_;
			if (!shopmap.equals(_o_.shopmap)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += shopmap.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shopmap).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

