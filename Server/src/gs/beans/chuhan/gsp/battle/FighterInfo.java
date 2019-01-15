
package chuhan.gsp.battle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class FighterInfo implements Marshal , Comparable<FighterInfo>{
	public byte fighterid; // 1~10
	public int hp; // 初始血
	public int heroid; // 武将id
	public byte colorgrade;
	public byte weapon;
	public byte armor;
	public byte horse;
	public short shape;

	public FighterInfo() {
	}

	public FighterInfo(byte _fighterid_, int _hp_, int _heroid_, byte _colorgrade_, byte _weapon_, byte _armor_, byte _horse_, short _shape_) {
		this.fighterid = _fighterid_;
		this.hp = _hp_;
		this.heroid = _heroid_;
		this.colorgrade = _colorgrade_;
		this.weapon = _weapon_;
		this.armor = _armor_;
		this.horse = _horse_;
		this.shape = _shape_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(fighterid);
		_os_.marshal(hp);
		_os_.marshal(heroid);
		_os_.marshal(colorgrade);
		_os_.marshal(weapon);
		_os_.marshal(armor);
		_os_.marshal(horse);
		_os_.marshal(shape);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		fighterid = _os_.unmarshal_byte();
		hp = _os_.unmarshal_int();
		heroid = _os_.unmarshal_int();
		colorgrade = _os_.unmarshal_byte();
		weapon = _os_.unmarshal_byte();
		armor = _os_.unmarshal_byte();
		horse = _os_.unmarshal_byte();
		shape = _os_.unmarshal_short();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof FighterInfo) {
			FighterInfo _o_ = (FighterInfo)_o1_;
			if (fighterid != _o_.fighterid) return false;
			if (hp != _o_.hp) return false;
			if (heroid != _o_.heroid) return false;
			if (colorgrade != _o_.colorgrade) return false;
			if (weapon != _o_.weapon) return false;
			if (armor != _o_.armor) return false;
			if (horse != _o_.horse) return false;
			if (shape != _o_.shape) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)fighterid;
		_h_ += hp;
		_h_ += heroid;
		_h_ += (int)colorgrade;
		_h_ += (int)weapon;
		_h_ += (int)armor;
		_h_ += (int)horse;
		_h_ += shape;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(fighterid).append(",");
		_sb_.append(hp).append(",");
		_sb_.append(heroid).append(",");
		_sb_.append(colorgrade).append(",");
		_sb_.append(weapon).append(",");
		_sb_.append(armor).append(",");
		_sb_.append(horse).append(",");
		_sb_.append(shape).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(FighterInfo _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = fighterid - _o_.fighterid;
		if (0 != _c_) return _c_;
		_c_ = hp - _o_.hp;
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
		_c_ = shape - _o_.shape;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

