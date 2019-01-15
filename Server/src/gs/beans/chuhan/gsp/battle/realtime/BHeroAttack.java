
package chuhan.gsp.battle.realtime;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BHeroAttack implements Marshal {
	public int herokey; // 英雄key（包括召唤物）
	public int skillid;
	public java.util.LinkedList<Integer> skillherolist;
	public java.util.HashMap<Integer,chuhan.gsp.battle.realtime.BDamageInfo> hurt;
	public java.util.LinkedList<Integer> killlist;

	public BHeroAttack() {
		skillherolist = new java.util.LinkedList<Integer>();
		hurt = new java.util.HashMap<Integer,chuhan.gsp.battle.realtime.BDamageInfo>();
		killlist = new java.util.LinkedList<Integer>();
	}

	public BHeroAttack(int _herokey_, int _skillid_, java.util.LinkedList<Integer> _skillherolist_, java.util.HashMap<Integer,chuhan.gsp.battle.realtime.BDamageInfo> _hurt_, java.util.LinkedList<Integer> _killlist_) {
		this.herokey = _herokey_;
		this.skillid = _skillid_;
		this.skillherolist = _skillherolist_;
		this.hurt = _hurt_;
		this.killlist = _killlist_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.battle.realtime.BDamageInfo> _e_ : hurt.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(herokey);
		_os_.marshal(skillid);
		_os_.compact_uint32(skillherolist.size());
		for (Integer _v_ : skillherolist) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(hurt.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.battle.realtime.BDamageInfo> _e_ : hurt.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(killlist.size());
		for (Integer _v_ : killlist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		skillid = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			skillherolist.add(_v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.battle.realtime.BDamageInfo _v_ = new chuhan.gsp.battle.realtime.BDamageInfo();
			_v_.unmarshal(_os_);
			hurt.put(_k_, _v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			killlist.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BHeroAttack) {
			BHeroAttack _o_ = (BHeroAttack)_o1_;
			if (herokey != _o_.herokey) return false;
			if (skillid != _o_.skillid) return false;
			if (!skillherolist.equals(_o_.skillherolist)) return false;
			if (!hurt.equals(_o_.hurt)) return false;
			if (!killlist.equals(_o_.killlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += skillid;
		_h_ += skillherolist.hashCode();
		_h_ += hurt.hashCode();
		_h_ += killlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(skillid).append(",");
		_sb_.append(skillherolist).append(",");
		_sb_.append(hurt).append(",");
		_sb_.append(killlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

