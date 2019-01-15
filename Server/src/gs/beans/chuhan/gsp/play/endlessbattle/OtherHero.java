
package chuhan.gsp.play.endlessbattle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 他人查看英雄 by yanglk
*/
public class OtherHero implements Marshal , Comparable<OtherHero>{
	public int heroid; // 英雄配表ID
	public int exp; // 当前经验
	public int herolevel; // 英雄等级
	public int hp; // 血量
	public int physicalattack; // 物理攻击
	public int physicaldefence; // 物理防御
	public int magicattack; // 魔法攻击
	public int magicdefence; // 魔法防御
	public int skill1; // 技能1编号（未开通为0）
	public int skill2; // 技能2编号（未开通为0）
	public int skill3; // 技能3编号（未开通为0）
	public int heroviewid; // 英雄外观

	public OtherHero() {
	}

	public OtherHero(int _heroid_, int _exp_, int _herolevel_, int _hp_, int _physicalattack_, int _physicaldefence_, int _magicattack_, int _magicdefence_, int _skill1_, int _skill2_, int _skill3_, int _heroviewid_) {
		this.heroid = _heroid_;
		this.exp = _exp_;
		this.herolevel = _herolevel_;
		this.hp = _hp_;
		this.physicalattack = _physicalattack_;
		this.physicaldefence = _physicaldefence_;
		this.magicattack = _magicattack_;
		this.magicdefence = _magicdefence_;
		this.skill1 = _skill1_;
		this.skill2 = _skill2_;
		this.skill3 = _skill3_;
		this.heroviewid = _heroviewid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(heroid);
		_os_.marshal(exp);
		_os_.marshal(herolevel);
		_os_.marshal(hp);
		_os_.marshal(physicalattack);
		_os_.marshal(physicaldefence);
		_os_.marshal(magicattack);
		_os_.marshal(magicdefence);
		_os_.marshal(skill1);
		_os_.marshal(skill2);
		_os_.marshal(skill3);
		_os_.marshal(heroviewid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		heroid = _os_.unmarshal_int();
		exp = _os_.unmarshal_int();
		herolevel = _os_.unmarshal_int();
		hp = _os_.unmarshal_int();
		physicalattack = _os_.unmarshal_int();
		physicaldefence = _os_.unmarshal_int();
		magicattack = _os_.unmarshal_int();
		magicdefence = _os_.unmarshal_int();
		skill1 = _os_.unmarshal_int();
		skill2 = _os_.unmarshal_int();
		skill3 = _os_.unmarshal_int();
		heroviewid = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof OtherHero) {
			OtherHero _o_ = (OtherHero)_o1_;
			if (heroid != _o_.heroid) return false;
			if (exp != _o_.exp) return false;
			if (herolevel != _o_.herolevel) return false;
			if (hp != _o_.hp) return false;
			if (physicalattack != _o_.physicalattack) return false;
			if (physicaldefence != _o_.physicaldefence) return false;
			if (magicattack != _o_.magicattack) return false;
			if (magicdefence != _o_.magicdefence) return false;
			if (skill1 != _o_.skill1) return false;
			if (skill2 != _o_.skill2) return false;
			if (skill3 != _o_.skill3) return false;
			if (heroviewid != _o_.heroviewid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += heroid;
		_h_ += exp;
		_h_ += herolevel;
		_h_ += hp;
		_h_ += physicalattack;
		_h_ += physicaldefence;
		_h_ += magicattack;
		_h_ += magicdefence;
		_h_ += skill1;
		_h_ += skill2;
		_h_ += skill3;
		_h_ += heroviewid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroid).append(",");
		_sb_.append(exp).append(",");
		_sb_.append(herolevel).append(",");
		_sb_.append(hp).append(",");
		_sb_.append(physicalattack).append(",");
		_sb_.append(physicaldefence).append(",");
		_sb_.append(magicattack).append(",");
		_sb_.append(magicdefence).append(",");
		_sb_.append(skill1).append(",");
		_sb_.append(skill2).append(",");
		_sb_.append(skill3).append(",");
		_sb_.append(heroviewid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(OtherHero _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = heroid - _o_.heroid;
		if (0 != _c_) return _c_;
		_c_ = exp - _o_.exp;
		if (0 != _c_) return _c_;
		_c_ = herolevel - _o_.herolevel;
		if (0 != _c_) return _c_;
		_c_ = hp - _o_.hp;
		if (0 != _c_) return _c_;
		_c_ = physicalattack - _o_.physicalattack;
		if (0 != _c_) return _c_;
		_c_ = physicaldefence - _o_.physicaldefence;
		if (0 != _c_) return _c_;
		_c_ = magicattack - _o_.magicattack;
		if (0 != _c_) return _c_;
		_c_ = magicdefence - _o_.magicdefence;
		if (0 != _c_) return _c_;
		_c_ = skill1 - _o_.skill1;
		if (0 != _c_) return _c_;
		_c_ = skill2 - _o_.skill2;
		if (0 != _c_) return _c_;
		_c_ = skill3 - _o_.skill3;
		if (0 != _c_) return _c_;
		_c_ = heroviewid - _o_.heroviewid;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

