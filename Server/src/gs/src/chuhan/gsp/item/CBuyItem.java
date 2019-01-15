
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CBuyItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CBuyItem extends __CBuyItem__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PBuyItem(roleId, shopkey, num).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787549;

	public int getType() {
		return 787549;
	}

	public byte shoptype; // ShopTypes
	public short shopkey; // 商场中商品key
	public short num; // 购买数量

	public CBuyItem() {
	}

	public CBuyItem(byte _shoptype_, short _shopkey_, short _num_) {
		this.shoptype = _shoptype_;
		this.shopkey = _shopkey_;
		this.num = _num_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(shoptype);
		_os_.marshal(shopkey);
		_os_.marshal(num);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		shoptype = _os_.unmarshal_byte();
		shopkey = _os_.unmarshal_short();
		num = _os_.unmarshal_short();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CBuyItem) {
			CBuyItem _o_ = (CBuyItem)_o1_;
			if (shoptype != _o_.shoptype) return false;
			if (shopkey != _o_.shopkey) return false;
			if (num != _o_.num) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)shoptype;
		_h_ += shopkey;
		_h_ += num;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shoptype).append(",");
		_sb_.append(shopkey).append(",");
		_sb_.append(num).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CBuyItem _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = shoptype - _o_.shoptype;
		if (0 != _c_) return _c_;
		_c_ = shopkey - _o_.shopkey;
		if (0 != _c_) return _c_;
		_c_ = num - _o_.num;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

