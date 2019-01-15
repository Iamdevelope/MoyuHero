
package chuhan.gsp.item;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class EquipItemData implements Marshal , Comparable<EquipItemData>{
	public int init1; // 基础属性1，默认-1
	public int init2; // 基础属性2，默认-1
	public int init3; // 基础属性3，默认-1
	public int attr1; // 附属属性1，默认-1
	public int attr2; // 附属属性2，默认-1
	public int attr3; // 附属属性3，默认-1
	public int attr4; // 附属属性4，默认-1

	public EquipItemData() {
	}

	public EquipItemData(int _init1_, int _init2_, int _init3_, int _attr1_, int _attr2_, int _attr3_, int _attr4_) {
		this.init1 = _init1_;
		this.init2 = _init2_;
		this.init3 = _init3_;
		this.attr1 = _attr1_;
		this.attr2 = _attr2_;
		this.attr3 = _attr3_;
		this.attr4 = _attr4_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(init1);
		_os_.marshal(init2);
		_os_.marshal(init3);
		_os_.marshal(attr1);
		_os_.marshal(attr2);
		_os_.marshal(attr3);
		_os_.marshal(attr4);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		init1 = _os_.unmarshal_int();
		init2 = _os_.unmarshal_int();
		init3 = _os_.unmarshal_int();
		attr1 = _os_.unmarshal_int();
		attr2 = _os_.unmarshal_int();
		attr3 = _os_.unmarshal_int();
		attr4 = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof EquipItemData) {
			EquipItemData _o_ = (EquipItemData)_o1_;
			if (init1 != _o_.init1) return false;
			if (init2 != _o_.init2) return false;
			if (init3 != _o_.init3) return false;
			if (attr1 != _o_.attr1) return false;
			if (attr2 != _o_.attr2) return false;
			if (attr3 != _o_.attr3) return false;
			if (attr4 != _o_.attr4) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += init1;
		_h_ += init2;
		_h_ += init3;
		_h_ += attr1;
		_h_ += attr2;
		_h_ += attr3;
		_h_ += attr4;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(init1).append(",");
		_sb_.append(init2).append(",");
		_sb_.append(init3).append(",");
		_sb_.append(attr1).append(",");
		_sb_.append(attr2).append(",");
		_sb_.append(attr3).append(",");
		_sb_.append(attr4).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(EquipItemData _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = init1 - _o_.init1;
		if (0 != _c_) return _c_;
		_c_ = init2 - _o_.init2;
		if (0 != _c_) return _c_;
		_c_ = init3 - _o_.init3;
		if (0 != _c_) return _c_;
		_c_ = attr1 - _o_.attr1;
		if (0 != _c_) return _c_;
		_c_ = attr2 - _o_.attr2;
		if (0 != _c_) return _c_;
		_c_ = attr3 - _o_.attr3;
		if (0 != _c_) return _c_;
		_c_ = attr4 - _o_.attr4;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

