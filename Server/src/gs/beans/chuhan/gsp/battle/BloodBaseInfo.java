
package chuhan.gsp.battle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BloodBaseInfo implements Marshal {
	public short maxlevel; // 最大关
	public short curlevel; // 当前关
	public float army;
	public float attack;
	public float defend;
	public float wisdom;

	public BloodBaseInfo() {
	}

	public BloodBaseInfo(short _maxlevel_, short _curlevel_, float _army_, float _attack_, float _defend_, float _wisdom_) {
		this.maxlevel = _maxlevel_;
		this.curlevel = _curlevel_;
		this.army = _army_;
		this.attack = _attack_;
		this.defend = _defend_;
		this.wisdom = _wisdom_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(maxlevel);
		_os_.marshal(curlevel);
		_os_.marshal(army);
		_os_.marshal(attack);
		_os_.marshal(defend);
		_os_.marshal(wisdom);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		maxlevel = _os_.unmarshal_short();
		curlevel = _os_.unmarshal_short();
		army = _os_.unmarshal_float();
		attack = _os_.unmarshal_float();
		defend = _os_.unmarshal_float();
		wisdom = _os_.unmarshal_float();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BloodBaseInfo) {
			BloodBaseInfo _o_ = (BloodBaseInfo)_o1_;
			if (maxlevel != _o_.maxlevel) return false;
			if (curlevel != _o_.curlevel) return false;
			if (army != _o_.army) return false;
			if (attack != _o_.attack) return false;
			if (defend != _o_.defend) return false;
			if (wisdom != _o_.wisdom) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += maxlevel;
		_h_ += curlevel;
		_h_ += Float.floatToIntBits(army);
		_h_ += Float.floatToIntBits(attack);
		_h_ += Float.floatToIntBits(defend);
		_h_ += Float.floatToIntBits(wisdom);
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(maxlevel).append(",");
		_sb_.append(curlevel).append(",");
		_sb_.append(army).append(",");
		_sb_.append(attack).append(",");
		_sb_.append(defend).append(",");
		_sb_.append(wisdom).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

