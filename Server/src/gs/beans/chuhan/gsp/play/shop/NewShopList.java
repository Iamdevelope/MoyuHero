
package chuhan.gsp.play.shop;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class NewShopList implements Marshal {
	public java.util.LinkedList<chuhan.gsp.play.shop.NewShop> shoplist; // 单个商城列表
	public long lasttime; // 刷新时间
	public int refreshnum; // 刷新次数

	public NewShopList() {
		shoplist = new java.util.LinkedList<chuhan.gsp.play.shop.NewShop>();
	}

	public NewShopList(java.util.LinkedList<chuhan.gsp.play.shop.NewShop> _shoplist_, long _lasttime_, int _refreshnum_) {
		this.shoplist = _shoplist_;
		this.lasttime = _lasttime_;
		this.refreshnum = _refreshnum_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.shop.NewShop _v_ : shoplist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.compact_uint32(shoplist.size());
		for (chuhan.gsp.play.shop.NewShop _v_ : shoplist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(lasttime);
		_os_.marshal(refreshnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.shop.NewShop _v_ = new chuhan.gsp.play.shop.NewShop();
			_v_.unmarshal(_os_);
			shoplist.add(_v_);
		}
		lasttime = _os_.unmarshal_long();
		refreshnum = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof NewShopList) {
			NewShopList _o_ = (NewShopList)_o1_;
			if (!shoplist.equals(_o_.shoplist)) return false;
			if (lasttime != _o_.lasttime) return false;
			if (refreshnum != _o_.refreshnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += shoplist.hashCode();
		_h_ += (int)lasttime;
		_h_ += refreshnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shoplist).append(",");
		_sb_.append(lasttime).append(",");
		_sb_.append(refreshnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

