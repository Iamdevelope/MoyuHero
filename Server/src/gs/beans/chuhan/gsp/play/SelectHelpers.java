
package chuhan.gsp.play;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class SelectHelpers implements Marshal , Comparable<SelectHelpers>{
	public long roleid;
	public short herolv;
	public int heroid;
	public byte colorgrade;
	public byte weapon;
	public byte armor;
	public byte horse;

	public SelectHelpers() {
	}

	public SelectHelpers(long _roleid_, short _herolv_, int _heroid_, byte _colorgrade_, byte _weapon_, byte _armor_, byte _horse_) {
		this.roleid = _roleid_;
		this.herolv = _herolv_;
		this.heroid = _heroid_;
		this.colorgrade = _colorgrade_;
		this.weapon = _weapon_;
		this.armor = _armor_;
		this.horse = _horse_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(herolv);
		_os_.marshal(heroid);
		_os_.marshal(colorgrade);
		_os_.marshal(weapon);
		_os_.marshal(armor);
		_os_.marshal(horse);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		herolv = _os_.unmarshal_short();
		heroid = _os_.unmarshal_int();
		colorgrade = _os_.unmarshal_byte();
		weapon = _os_.unmarshal_byte();
		armor = _os_.unmarshal_byte();
		horse = _os_.unmarshal_byte();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SelectHelpers) {
			SelectHelpers _o_ = (SelectHelpers)_o1_;
			if (roleid != _o_.roleid) return false;
			if (herolv != _o_.herolv) return false;
			if (heroid != _o_.heroid) return false;
			if (colorgrade != _o_.colorgrade) return false;
			if (weapon != _o_.weapon) return false;
			if (armor != _o_.armor) return false;
			if (horse != _o_.horse) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += herolv;
		_h_ += heroid;
		_h_ += (int)colorgrade;
		_h_ += (int)weapon;
		_h_ += (int)armor;
		_h_ += (int)horse;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append(herolv).append(",");
		_sb_.append(heroid).append(",");
		_sb_.append(colorgrade).append(",");
		_sb_.append(weapon).append(",");
		_sb_.append(armor).append(",");
		_sb_.append(horse).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SelectHelpers _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(roleid - _o_.roleid);
		if (0 != _c_) return _c_;
		_c_ = herolv - _o_.herolv;
		if (0 != _c_) return _c_;
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
		return _c_;
	}

}

