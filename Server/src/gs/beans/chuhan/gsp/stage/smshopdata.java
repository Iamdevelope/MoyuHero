
package chuhan.gsp.stage;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 神秘商店信息 by yanglk
*/
public class smshopdata implements Marshal , Comparable<smshopdata>{
	public int id; // id
	public int isopen; // 是否购买（1购买，0未购买）
	public int price; // 价格

	public smshopdata() {
	}

	public smshopdata(int _id_, int _isopen_, int _price_) {
		this.id = _id_;
		this.isopen = _isopen_;
		this.price = _price_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(isopen);
		_os_.marshal(price);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		isopen = _os_.unmarshal_int();
		price = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof smshopdata) {
			smshopdata _o_ = (smshopdata)_o1_;
			if (id != _o_.id) return false;
			if (isopen != _o_.isopen) return false;
			if (price != _o_.price) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += isopen;
		_h_ += price;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(isopen).append(",");
		_sb_.append(price).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(smshopdata _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = isopen - _o_.isopen;
		if (0 != _c_) return _c_;
		_c_ = price - _o_.price;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

