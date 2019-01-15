
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 商城购买记录 by yanglk
*/
public class Shopbuy implements Marshal , Comparable<Shopbuy>{
	public int shopid; // 商城ID（key）
	public int todaynum; // 今日已购买次数
	public int buyallnum; // 总共购买次数

	public Shopbuy() {
	}

	public Shopbuy(int _shopid_, int _todaynum_, int _buyallnum_) {
		this.shopid = _shopid_;
		this.todaynum = _todaynum_;
		this.buyallnum = _buyallnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(shopid);
		_os_.marshal(todaynum);
		_os_.marshal(buyallnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		shopid = _os_.unmarshal_int();
		todaynum = _os_.unmarshal_int();
		buyallnum = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Shopbuy) {
			Shopbuy _o_ = (Shopbuy)_o1_;
			if (shopid != _o_.shopid) return false;
			if (todaynum != _o_.todaynum) return false;
			if (buyallnum != _o_.buyallnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += shopid;
		_h_ += todaynum;
		_h_ += buyallnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shopid).append(",");
		_sb_.append(todaynum).append(",");
		_sb_.append(buyallnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(Shopbuy _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = shopid - _o_.shopid;
		if (0 != _c_) return _c_;
		_c_ = todaynum - _o_.todaynum;
		if (0 != _c_) return _c_;
		_c_ = buyallnum - _o_.buyallnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

