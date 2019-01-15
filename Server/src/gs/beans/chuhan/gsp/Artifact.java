
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 神器记录 by yanglk
*/
public class Artifact implements Marshal , Comparable<Artifact>{
	public int artifacttype; // 神器类型（key）
	public int artifactid; // 神器ID
	public int heronum1; // 英雄数量1
	public int heronum2; // 英雄数量2
	public int heronum3; // 英雄数量3
	public int heronum4; // 英雄数量4
	public int heronum5; // 英雄数量5

	public Artifact() {
	}

	public Artifact(int _artifacttype_, int _artifactid_, int _heronum1_, int _heronum2_, int _heronum3_, int _heronum4_, int _heronum5_) {
		this.artifacttype = _artifacttype_;
		this.artifactid = _artifactid_;
		this.heronum1 = _heronum1_;
		this.heronum2 = _heronum2_;
		this.heronum3 = _heronum3_;
		this.heronum4 = _heronum4_;
		this.heronum5 = _heronum5_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(artifacttype);
		_os_.marshal(artifactid);
		_os_.marshal(heronum1);
		_os_.marshal(heronum2);
		_os_.marshal(heronum3);
		_os_.marshal(heronum4);
		_os_.marshal(heronum5);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		artifacttype = _os_.unmarshal_int();
		artifactid = _os_.unmarshal_int();
		heronum1 = _os_.unmarshal_int();
		heronum2 = _os_.unmarshal_int();
		heronum3 = _os_.unmarshal_int();
		heronum4 = _os_.unmarshal_int();
		heronum5 = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Artifact) {
			Artifact _o_ = (Artifact)_o1_;
			if (artifacttype != _o_.artifacttype) return false;
			if (artifactid != _o_.artifactid) return false;
			if (heronum1 != _o_.heronum1) return false;
			if (heronum2 != _o_.heronum2) return false;
			if (heronum3 != _o_.heronum3) return false;
			if (heronum4 != _o_.heronum4) return false;
			if (heronum5 != _o_.heronum5) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += artifacttype;
		_h_ += artifactid;
		_h_ += heronum1;
		_h_ += heronum2;
		_h_ += heronum3;
		_h_ += heronum4;
		_h_ += heronum5;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(artifacttype).append(",");
		_sb_.append(artifactid).append(",");
		_sb_.append(heronum1).append(",");
		_sb_.append(heronum2).append(",");
		_sb_.append(heronum3).append(",");
		_sb_.append(heronum4).append(",");
		_sb_.append(heronum5).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(Artifact _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = artifacttype - _o_.artifacttype;
		if (0 != _c_) return _c_;
		_c_ = artifactid - _o_.artifactid;
		if (0 != _c_) return _c_;
		_c_ = heronum1 - _o_.heronum1;
		if (0 != _c_) return _c_;
		_c_ = heronum2 - _o_.heronum2;
		if (0 != _c_) return _c_;
		_c_ = heronum3 - _o_.heronum3;
		if (0 != _c_) return _c_;
		_c_ = heronum4 - _o_.heronum4;
		if (0 != _c_) return _c_;
		_c_ = heronum5 - _o_.heronum5;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

