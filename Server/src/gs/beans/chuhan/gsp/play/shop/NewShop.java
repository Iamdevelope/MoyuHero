
package chuhan.gsp.play.shop;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class NewShop implements Marshal , Comparable<NewShop>{
	public int itemid; // 77表的道具ID
	public int costtype; // 消耗资源
	public int price; // 价格
	public int num; // 数量
	public int isbuy; // 0未购买，1为已购买

	public NewShop() {
	}

	public NewShop(int _itemid_, int _costtype_, int _price_, int _num_, int _isbuy_) {
		this.itemid = _itemid_;
		this.costtype = _costtype_;
		this.price = _price_;
		this.num = _num_;
		this.isbuy = _isbuy_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(itemid);
		_os_.marshal(costtype);
		_os_.marshal(price);
		_os_.marshal(num);
		_os_.marshal(isbuy);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		itemid = _os_.unmarshal_int();
		costtype = _os_.unmarshal_int();
		price = _os_.unmarshal_int();
		num = _os_.unmarshal_int();
		isbuy = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof NewShop) {
			NewShop _o_ = (NewShop)_o1_;
			if (itemid != _o_.itemid) return false;
			if (costtype != _o_.costtype) return false;
			if (price != _o_.price) return false;
			if (num != _o_.num) return false;
			if (isbuy != _o_.isbuy) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += itemid;
		_h_ += costtype;
		_h_ += price;
		_h_ += num;
		_h_ += isbuy;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(itemid).append(",");
		_sb_.append(costtype).append(",");
		_sb_.append(price).append(",");
		_sb_.append(num).append(",");
		_sb_.append(isbuy).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(NewShop _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = itemid - _o_.itemid;
		if (0 != _c_) return _c_;
		_c_ = costtype - _o_.costtype;
		if (0 != _c_) return _c_;
		_c_ = price - _o_.price;
		if (0 != _c_) return _c_;
		_c_ = num - _o_.num;
		if (0 != _c_) return _c_;
		_c_ = isbuy - _o_.isbuy;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

