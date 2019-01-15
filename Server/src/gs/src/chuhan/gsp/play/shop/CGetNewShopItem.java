
package chuhan.gsp.play.shop;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CGetNewShopItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CGetNewShopItem extends __CGetNewShopItem__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				ShopBuyColumn shopcol = ShopBuyColumn.getShopBuyColumn(roleId, false);
				boolean result = shopcol.getNewShopItem(shopid);
				SGetNewShopItem snd = new SGetNewShopItem();
				snd.shopid = shopid;

				if(result){
					snd.result = SGetNewShopItem.END_OK;
				}else{
					snd.result = SGetNewShopItem.END_ERROR;
				}
				xdb.Procedure.psend(roleId, snd);
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788841;

	public int getType() {
		return 788841;
	}

	public int shopid; // 76表的ID

	public CGetNewShopItem() {
	}

	public CGetNewShopItem(int _shopid_) {
		this.shopid = _shopid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(shopid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		shopid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CGetNewShopItem) {
			CGetNewShopItem _o_ = (CGetNewShopItem)_o1_;
			if (shopid != _o_.shopid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += shopid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shopid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CGetNewShopItem _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = shopid - _o_.shopid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

