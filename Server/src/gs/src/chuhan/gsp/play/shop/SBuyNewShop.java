
package chuhan.gsp.play.shop;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SBuyNewShop__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SBuyNewShop extends __SBuyNewShop__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788840;

	public int getType() {
		return 788840;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public int shopid; // 76表的ID
	public int itemid; // 77表的道具ID
	public int costtype; // 消耗资源
	public int price; // 价格
	public int num; // 数量

	public SBuyNewShop() {
	}

	public SBuyNewShop(int _result_, int _shopid_, int _itemid_, int _costtype_, int _price_, int _num_) {
		this.result = _result_;
		this.shopid = _shopid_;
		this.itemid = _itemid_;
		this.costtype = _costtype_;
		this.price = _price_;
		this.num = _num_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(shopid);
		_os_.marshal(itemid);
		_os_.marshal(costtype);
		_os_.marshal(price);
		_os_.marshal(num);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		shopid = _os_.unmarshal_int();
		itemid = _os_.unmarshal_int();
		costtype = _os_.unmarshal_int();
		price = _os_.unmarshal_int();
		num = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SBuyNewShop) {
			SBuyNewShop _o_ = (SBuyNewShop)_o1_;
			if (result != _o_.result) return false;
			if (shopid != _o_.shopid) return false;
			if (itemid != _o_.itemid) return false;
			if (costtype != _o_.costtype) return false;
			if (price != _o_.price) return false;
			if (num != _o_.num) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += shopid;
		_h_ += itemid;
		_h_ += costtype;
		_h_ += price;
		_h_ += num;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(shopid).append(",");
		_sb_.append(itemid).append(",");
		_sb_.append(costtype).append(",");
		_sb_.append(price).append(",");
		_sb_.append(num).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SBuyNewShop _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = result - _o_.result;
		if (0 != _c_) return _c_;
		_c_ = shopid - _o_.shopid;
		if (0 != _c_) return _c_;
		_c_ = itemid - _o_.itemid;
		if (0 != _c_) return _c_;
		_c_ = costtype - _o_.costtype;
		if (0 != _c_) return _c_;
		_c_ = price - _o_.price;
		if (0 != _c_) return _c_;
		_c_ = num - _o_.num;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

