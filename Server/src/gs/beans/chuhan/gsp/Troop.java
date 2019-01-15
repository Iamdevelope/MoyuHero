
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 战队信息 by yanglk
*/
public class Troop implements Marshal , Comparable<Troop>{
	public int troopnum; // 战队编号
	public int trooptype; // 战队类型，1为前2后3，2为前3后2
	public int location1; // 0没装
	public int location2; // 0没装
	public int location3; // 0没装
	public int location4; // 0没装
	public int location5; // 0没装
	public int sh1; // 神魂1号，0没装
	public int sh2; // 神魂2号，0没装
	public int sh3; // 神魂3号，0没装
	public int sh4; // 神魂4号，0没装

	public Troop() {
	}

	public Troop(int _troopnum_, int _trooptype_, int _location1_, int _location2_, int _location3_, int _location4_, int _location5_, int _sh1_, int _sh2_, int _sh3_, int _sh4_) {
		this.troopnum = _troopnum_;
		this.trooptype = _trooptype_;
		this.location1 = _location1_;
		this.location2 = _location2_;
		this.location3 = _location3_;
		this.location4 = _location4_;
		this.location5 = _location5_;
		this.sh1 = _sh1_;
		this.sh2 = _sh2_;
		this.sh3 = _sh3_;
		this.sh4 = _sh4_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(troopnum);
		_os_.marshal(trooptype);
		_os_.marshal(location1);
		_os_.marshal(location2);
		_os_.marshal(location3);
		_os_.marshal(location4);
		_os_.marshal(location5);
		_os_.marshal(sh1);
		_os_.marshal(sh2);
		_os_.marshal(sh3);
		_os_.marshal(sh4);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		troopnum = _os_.unmarshal_int();
		trooptype = _os_.unmarshal_int();
		location1 = _os_.unmarshal_int();
		location2 = _os_.unmarshal_int();
		location3 = _os_.unmarshal_int();
		location4 = _os_.unmarshal_int();
		location5 = _os_.unmarshal_int();
		sh1 = _os_.unmarshal_int();
		sh2 = _os_.unmarshal_int();
		sh3 = _os_.unmarshal_int();
		sh4 = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Troop) {
			Troop _o_ = (Troop)_o1_;
			if (troopnum != _o_.troopnum) return false;
			if (trooptype != _o_.trooptype) return false;
			if (location1 != _o_.location1) return false;
			if (location2 != _o_.location2) return false;
			if (location3 != _o_.location3) return false;
			if (location4 != _o_.location4) return false;
			if (location5 != _o_.location5) return false;
			if (sh1 != _o_.sh1) return false;
			if (sh2 != _o_.sh2) return false;
			if (sh3 != _o_.sh3) return false;
			if (sh4 != _o_.sh4) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += troopnum;
		_h_ += trooptype;
		_h_ += location1;
		_h_ += location2;
		_h_ += location3;
		_h_ += location4;
		_h_ += location5;
		_h_ += sh1;
		_h_ += sh2;
		_h_ += sh3;
		_h_ += sh4;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(troopnum).append(",");
		_sb_.append(trooptype).append(",");
		_sb_.append(location1).append(",");
		_sb_.append(location2).append(",");
		_sb_.append(location3).append(",");
		_sb_.append(location4).append(",");
		_sb_.append(location5).append(",");
		_sb_.append(sh1).append(",");
		_sb_.append(sh2).append(",");
		_sb_.append(sh3).append(",");
		_sb_.append(sh4).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(Troop _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = troopnum - _o_.troopnum;
		if (0 != _c_) return _c_;
		_c_ = trooptype - _o_.trooptype;
		if (0 != _c_) return _c_;
		_c_ = location1 - _o_.location1;
		if (0 != _c_) return _c_;
		_c_ = location2 - _o_.location2;
		if (0 != _c_) return _c_;
		_c_ = location3 - _o_.location3;
		if (0 != _c_) return _c_;
		_c_ = location4 - _o_.location4;
		if (0 != _c_) return _c_;
		_c_ = location5 - _o_.location5;
		if (0 != _c_) return _c_;
		_c_ = sh1 - _o_.sh1;
		if (0 != _c_) return _c_;
		_c_ = sh2 - _o_.sh2;
		if (0 != _c_) return _c_;
		_c_ = sh3 - _o_.sh3;
		if (0 != _c_) return _c_;
		_c_ = sh4 - _o_.sh4;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

