
package chuhan.gsp.exchange;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class GoodInfo implements Marshal {
	public short goodid;
	public float price;
	public short yuanbao;
	public short present;

	public GoodInfo() {
	}

	public GoodInfo(short _goodid_, float _price_, short _yuanbao_, short _present_) {
		this.goodid = _goodid_;
		this.price = _price_;
		this.yuanbao = _yuanbao_;
		this.present = _present_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(goodid);
		_os_.marshal(price);
		_os_.marshal(yuanbao);
		_os_.marshal(present);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		goodid = _os_.unmarshal_short();
		price = _os_.unmarshal_float();
		yuanbao = _os_.unmarshal_short();
		present = _os_.unmarshal_short();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof GoodInfo) {
			GoodInfo _o_ = (GoodInfo)_o1_;
			if (goodid != _o_.goodid) return false;
			if (price != _o_.price) return false;
			if (yuanbao != _o_.yuanbao) return false;
			if (present != _o_.present) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += goodid;
		_h_ += Float.floatToIntBits(price);
		_h_ += yuanbao;
		_h_ += present;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(goodid).append(",");
		_sb_.append(price).append(",");
		_sb_.append(yuanbao).append(",");
		_sb_.append(present).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

