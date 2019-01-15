
package chuhan.gsp.battle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BattleDemo implements Marshal {
	public byte attacker; // 攻击者id
	public short skillid; // 技能id，0为普攻
	public java.util.LinkedList<chuhan.gsp.battle.TargetDemo> targets;

	public BattleDemo() {
		targets = new java.util.LinkedList<chuhan.gsp.battle.TargetDemo>();
	}

	public BattleDemo(byte _attacker_, short _skillid_, java.util.LinkedList<chuhan.gsp.battle.TargetDemo> _targets_) {
		this.attacker = _attacker_;
		this.skillid = _skillid_;
		this.targets = _targets_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.battle.TargetDemo _v_ : targets)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(attacker);
		_os_.marshal(skillid);
		_os_.compact_uint32(targets.size());
		for (chuhan.gsp.battle.TargetDemo _v_ : targets) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		attacker = _os_.unmarshal_byte();
		skillid = _os_.unmarshal_short();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.TargetDemo _v_ = new chuhan.gsp.battle.TargetDemo();
			_v_.unmarshal(_os_);
			targets.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BattleDemo) {
			BattleDemo _o_ = (BattleDemo)_o1_;
			if (attacker != _o_.attacker) return false;
			if (skillid != _o_.skillid) return false;
			if (!targets.equals(_o_.targets)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)attacker;
		_h_ += skillid;
		_h_ += targets.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(attacker).append(",");
		_sb_.append(skillid).append(",");
		_sb_.append(targets).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

