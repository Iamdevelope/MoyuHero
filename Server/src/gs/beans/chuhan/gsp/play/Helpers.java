
package chuhan.gsp.play;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 协议号1901-2000
*/
public class Helpers implements Marshal {
	public long roleid;
	public short rolelv;
	public java.lang.String rolename;
	public byte isfriend; // 是否好友 0-否 1-是
	public byte isselect; // 是否选中 0-否 1-是
	public int heroid;
	public byte colorgrade;
	public short herolv;
	public int score;

	public Helpers() {
		rolename = "";
	}

	public Helpers(long _roleid_, short _rolelv_, java.lang.String _rolename_, byte _isfriend_, byte _isselect_, int _heroid_, byte _colorgrade_, short _herolv_, int _score_) {
		this.roleid = _roleid_;
		this.rolelv = _rolelv_;
		this.rolename = _rolename_;
		this.isfriend = _isfriend_;
		this.isselect = _isselect_;
		this.heroid = _heroid_;
		this.colorgrade = _colorgrade_;
		this.herolv = _herolv_;
		this.score = _score_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(rolelv);
		_os_.marshal(rolename, "UTF-16LE");
		_os_.marshal(isfriend);
		_os_.marshal(isselect);
		_os_.marshal(heroid);
		_os_.marshal(colorgrade);
		_os_.marshal(herolv);
		_os_.marshal(score);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		rolelv = _os_.unmarshal_short();
		rolename = _os_.unmarshal_String("UTF-16LE");
		isfriend = _os_.unmarshal_byte();
		isselect = _os_.unmarshal_byte();
		heroid = _os_.unmarshal_int();
		colorgrade = _os_.unmarshal_byte();
		herolv = _os_.unmarshal_short();
		score = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Helpers) {
			Helpers _o_ = (Helpers)_o1_;
			if (roleid != _o_.roleid) return false;
			if (rolelv != _o_.rolelv) return false;
			if (!rolename.equals(_o_.rolename)) return false;
			if (isfriend != _o_.isfriend) return false;
			if (isselect != _o_.isselect) return false;
			if (heroid != _o_.heroid) return false;
			if (colorgrade != _o_.colorgrade) return false;
			if (herolv != _o_.herolv) return false;
			if (score != _o_.score) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += rolelv;
		_h_ += rolename.hashCode();
		_h_ += (int)isfriend;
		_h_ += (int)isselect;
		_h_ += heroid;
		_h_ += (int)colorgrade;
		_h_ += herolv;
		_h_ += score;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append(rolelv).append(",");
		_sb_.append("T").append(rolename.length()).append(",");
		_sb_.append(isfriend).append(",");
		_sb_.append(isselect).append(",");
		_sb_.append(heroid).append(",");
		_sb_.append(colorgrade).append(",");
		_sb_.append(herolv).append(",");
		_sb_.append(score).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

