
package chuhan.gsp.play.endlessbattle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class endlessBattleInfo implements Marshal {
	public int battleid; // 关卡id
	public int groupnum; // 第几轮
	public java.util.HashMap<Integer,Integer> useherokeylist; // 使用英雄id和位置（key为位置，value为herokey）
	public java.util.LinkedList<Integer> monstergroup; // 怪物组
	public int trooptype; // 战队类型
	public int monstertrooptype; // 怪物战队类型
	public int pact; // 强者之约（没有则为-1）

	public endlessBattleInfo() {
		useherokeylist = new java.util.HashMap<Integer,Integer>();
		monstergroup = new java.util.LinkedList<Integer>();
	}

	public endlessBattleInfo(int _battleid_, int _groupnum_, java.util.HashMap<Integer,Integer> _useherokeylist_, java.util.LinkedList<Integer> _monstergroup_, int _trooptype_, int _monstertrooptype_, int _pact_) {
		this.battleid = _battleid_;
		this.groupnum = _groupnum_;
		this.useherokeylist = _useherokeylist_;
		this.monstergroup = _monstergroup_;
		this.trooptype = _trooptype_;
		this.monstertrooptype = _monstertrooptype_;
		this.pact = _pact_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(battleid);
		_os_.marshal(groupnum);
		_os_.compact_uint32(useherokeylist.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : useherokeylist.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(monstergroup.size());
		for (Integer _v_ : monstergroup) {
			_os_.marshal(_v_);
		}
		_os_.marshal(trooptype);
		_os_.marshal(monstertrooptype);
		_os_.marshal(pact);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		battleid = _os_.unmarshal_int();
		groupnum = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			int _v_;
			_v_ = _os_.unmarshal_int();
			useherokeylist.put(_k_, _v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			monstergroup.add(_v_);
		}
		trooptype = _os_.unmarshal_int();
		monstertrooptype = _os_.unmarshal_int();
		pact = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof endlessBattleInfo) {
			endlessBattleInfo _o_ = (endlessBattleInfo)_o1_;
			if (battleid != _o_.battleid) return false;
			if (groupnum != _o_.groupnum) return false;
			if (!useherokeylist.equals(_o_.useherokeylist)) return false;
			if (!monstergroup.equals(_o_.monstergroup)) return false;
			if (trooptype != _o_.trooptype) return false;
			if (monstertrooptype != _o_.monstertrooptype) return false;
			if (pact != _o_.pact) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += battleid;
		_h_ += groupnum;
		_h_ += useherokeylist.hashCode();
		_h_ += monstergroup.hashCode();
		_h_ += trooptype;
		_h_ += monstertrooptype;
		_h_ += pact;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleid).append(",");
		_sb_.append(groupnum).append(",");
		_sb_.append(useherokeylist).append(",");
		_sb_.append(monstergroup).append(",");
		_sb_.append(trooptype).append(",");
		_sb_.append(monstertrooptype).append(",");
		_sb_.append(pact).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

