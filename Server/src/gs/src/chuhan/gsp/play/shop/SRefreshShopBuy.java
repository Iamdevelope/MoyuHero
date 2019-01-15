
package chuhan.gsp.play.shop;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshShopBuy__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshShopBuy extends __SRefreshShopBuy__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788835;

	public int getType() {
		return 788835;
	}

	public chuhan.gsp.Shopbuy shopbuy;

	public SRefreshShopBuy() {
		shopbuy = new chuhan.gsp.Shopbuy();
	}

	public SRefreshShopBuy(chuhan.gsp.Shopbuy _shopbuy_) {
		this.shopbuy = _shopbuy_;
	}

	public final boolean _validator_() {
		if (!shopbuy._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(shopbuy);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		shopbuy.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshShopBuy) {
			SRefreshShopBuy _o_ = (SRefreshShopBuy)_o1_;
			if (!shopbuy.equals(_o_.shopbuy)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += shopbuy.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shopbuy).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshShopBuy _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = shopbuy.compareTo(_o_.shopbuy);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

