
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class WatchTroopInfo implements Marshal , Comparable<WatchTroopInfo>{
	public short heroid;
	public byte colorgrade;
	public byte weapon;
	public byte armor;
	public byte horse;
	public short vicehero1;
	public byte vhero1color;
	public short vicehero2;
	public byte vhero2color;
	public short shape;

	public WatchTroopInfo() {
	}

	public WatchTroopInfo(short _heroid_, byte _colorgrade_, byte _weapon_, byte _armor_, byte _horse_, short _vicehero1_, byte _vhero1color_, short _vicehero2_, byte _vhero2color_, short _shape_) {
		this.heroid = _heroid_;
		this.colorgrade = _colorgrade_;
		this.weapon = _weapon_;
		this.armor = _armor_;
		this.horse = _horse_;
		this.vicehero1 = _vicehero1_;
		this.vhero1color = _vhero1color_;
		this.vicehero2 = _vicehero2_;
		this.vhero2color = _vhero2color_;
		this.shape = _shape_;
	}

	public final boolean _validator_() {
		if (heroid < 1) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(heroid);
		_os_.marshal(colorgrade);
		_os_.marshal(weapon);
		_os_.marshal(armor);
		_os_.marshal(horse);
		_os_.marshal(vicehero1);
		_os_.marshal(vhero1color);
		_os_.marshal(vicehero2);
		_os_.marshal(vhero2color);
		_os_.marshal(shape);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		heroid = _os_.unmarshal_short();
		colorgrade = _os_.unmarshal_byte();
		weapon = _os_.unmarshal_byte();
		armor = _os_.unmarshal_byte();
		horse = _os_.unmarshal_byte();
		vicehero1 = _os_.unmarshal_short();
		vhero1color = _os_.unmarshal_byte();
		vicehero2 = _os_.unmarshal_short();
		vhero2color = _os_.unmarshal_byte();
		shape = _os_.unmarshal_short();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof WatchTroopInfo) {
			WatchTroopInfo _o_ = (WatchTroopInfo)_o1_;
			if (heroid != _o_.heroid) return false;
			if (colorgrade != _o_.colorgrade) return false;
			if (weapon != _o_.weapon) return false;
			if (armor != _o_.armor) return false;
			if (horse != _o_.horse) return false;
			if (vicehero1 != _o_.vicehero1) return false;
			if (vhero1color != _o_.vhero1color) return false;
			if (vicehero2 != _o_.vicehero2) return false;
			if (vhero2color != _o_.vhero2color) return false;
			if (shape != _o_.shape) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += heroid;
		_h_ += (int)colorgrade;
		_h_ += (int)weapon;
		_h_ += (int)armor;
		_h_ += (int)horse;
		_h_ += vicehero1;
		_h_ += (int)vhero1color;
		_h_ += vicehero2;
		_h_ += (int)vhero2color;
		_h_ += shape;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroid).append(",");
		_sb_.append(colorgrade).append(",");
		_sb_.append(weapon).append(",");
		_sb_.append(armor).append(",");
		_sb_.append(horse).append(",");
		_sb_.append(vicehero1).append(",");
		_sb_.append(vhero1color).append(",");
		_sb_.append(vicehero2).append(",");
		_sb_.append(vhero2color).append(",");
		_sb_.append(shape).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(WatchTroopInfo _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = heroid - _o_.heroid;
		if (0 != _c_) return _c_;
		_c_ = colorgrade - _o_.colorgrade;
		if (0 != _c_) return _c_;
		_c_ = weapon - _o_.weapon;
		if (0 != _c_) return _c_;
		_c_ = armor - _o_.armor;
		if (0 != _c_) return _c_;
		_c_ = horse - _o_.horse;
		if (0 != _c_) return _c_;
		_c_ = vicehero1 - _o_.vicehero1;
		if (0 != _c_) return _c_;
		_c_ = vhero1color - _o_.vhero1color;
		if (0 != _c_) return _c_;
		_c_ = vicehero2 - _o_.vicehero2;
		if (0 != _c_) return _c_;
		_c_ = vhero2color - _o_.vhero2color;
		if (0 != _c_) return _c_;
		_c_ = shape - _o_.shape;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

