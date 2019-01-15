
package chuhan.gsp.stage;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BattleInfo implements Marshal {
	public int battleid; // 关卡id
	public java.util.HashMap<Integer,Integer> useherokeylist; // 使用英雄id和位置（key为位置，value为herokey）
	public java.util.LinkedList<Integer> monstergroup; // 怪物组
	public int heroexp; // 英雄经验
	public int teamexp; // 玩家经验
	public int tili; // 消耗体力
	public int trooptype; // 战队类型
	public java.util.LinkedList<Integer> indroplist; // 掉落小包组

	public BattleInfo() {
		useherokeylist = new java.util.HashMap<Integer,Integer>();
		monstergroup = new java.util.LinkedList<Integer>();
		indroplist = new java.util.LinkedList<Integer>();
	}

	public BattleInfo(int _battleid_, java.util.HashMap<Integer,Integer> _useherokeylist_, java.util.LinkedList<Integer> _monstergroup_, int _heroexp_, int _teamexp_, int _tili_, int _trooptype_, java.util.LinkedList<Integer> _indroplist_) {
		this.battleid = _battleid_;
		this.useherokeylist = _useherokeylist_;
		this.monstergroup = _monstergroup_;
		this.heroexp = _heroexp_;
		this.teamexp = _teamexp_;
		this.tili = _tili_;
		this.trooptype = _trooptype_;
		this.indroplist = _indroplist_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(battleid);
		_os_.compact_uint32(useherokeylist.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : useherokeylist.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(monstergroup.size());
		for (Integer _v_ : monstergroup) {
			_os_.marshal(_v_);
		}
		_os_.marshal(heroexp);
		_os_.marshal(teamexp);
		_os_.marshal(tili);
		_os_.marshal(trooptype);
		_os_.compact_uint32(indroplist.size());
		for (Integer _v_ : indroplist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		battleid = _os_.unmarshal_int();
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
		heroexp = _os_.unmarshal_int();
		teamexp = _os_.unmarshal_int();
		tili = _os_.unmarshal_int();
		trooptype = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			indroplist.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BattleInfo) {
			BattleInfo _o_ = (BattleInfo)_o1_;
			if (battleid != _o_.battleid) return false;
			if (!useherokeylist.equals(_o_.useherokeylist)) return false;
			if (!monstergroup.equals(_o_.monstergroup)) return false;
			if (heroexp != _o_.heroexp) return false;
			if (teamexp != _o_.teamexp) return false;
			if (tili != _o_.tili) return false;
			if (trooptype != _o_.trooptype) return false;
			if (!indroplist.equals(_o_.indroplist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += battleid;
		_h_ += useherokeylist.hashCode();
		_h_ += monstergroup.hashCode();
		_h_ += heroexp;
		_h_ += teamexp;
		_h_ += tili;
		_h_ += trooptype;
		_h_ += indroplist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleid).append(",");
		_sb_.append(useherokeylist).append(",");
		_sb_.append(monstergroup).append(",");
		_sb_.append(heroexp).append(",");
		_sb_.append(teamexp).append(",");
		_sb_.append(tili).append(",");
		_sb_.append(trooptype).append(",");
		_sb_.append(indroplist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

