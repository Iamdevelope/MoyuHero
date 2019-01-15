
package chuhan.gsp.play.shop;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CShopBuy__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CShopBuy extends __CShopBuy__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PShopBuy(roleId, shopid,num,isdiscount).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788833;

	public int getType() {
		return 788833;
	}

	public int shopid; // 商店物品id
	public int num; // 购买数量
	public byte isdiscount; // 是否打折期间（1是，0不是）

	public CShopBuy() {
	}

	public CShopBuy(int _shopid_, int _num_, byte _isdiscount_) {
		this.shopid = _shopid_;
		this.num = _num_;
		this.isdiscount = _isdiscount_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(shopid);
		_os_.marshal(num);
		_os_.marshal(isdiscount);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		shopid = _os_.unmarshal_int();
		num = _os_.unmarshal_int();
		isdiscount = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CShopBuy) {
			CShopBuy _o_ = (CShopBuy)_o1_;
			if (shopid != _o_.shopid) return false;
			if (num != _o_.num) return false;
			if (isdiscount != _o_.isdiscount) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += shopid;
		_h_ += num;
		_h_ += (int)isdiscount;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shopid).append(",");
		_sb_.append(num).append(",");
		_sb_.append(isdiscount).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CShopBuy _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = shopid - _o_.shopid;
		if (0 != _c_) return _c_;
		_c_ = num - _o_.num;
		if (0 != _c_) return _c_;
		_c_ = isdiscount - _o_.isdiscount;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

