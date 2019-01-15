
package chuhan.gsp.battle.realtime;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class Battleroleinfo implements Marshal {
	public long roleid; // 人物roleid
	public int ranking; // 排名
	public java.lang.String name; // 名称
	public java.util.HashMap<Integer,Integer> useherokeylist; // 使用英雄id和位置（key为位置，value为herokey）
	public java.util.LinkedList<chuhan.gsp.Hero> heroes;

	public Battleroleinfo() {
		name = "";
		useherokeylist = new java.util.HashMap<Integer,Integer>();
		heroes = new java.util.LinkedList<chuhan.gsp.Hero>();
	}

	public Battleroleinfo(long _roleid_, int _ranking_, java.lang.String _name_, java.util.HashMap<Integer,Integer> _useherokeylist_, java.util.LinkedList<chuhan.gsp.Hero> _heroes_) {
		this.roleid = _roleid_;
		this.ranking = _ranking_;
		this.name = _name_;
		this.useherokeylist = _useherokeylist_;
		this.heroes = _heroes_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.Hero _v_ : heroes)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(ranking);
		_os_.marshal(name, "UTF-16LE");
		_os_.compact_uint32(useherokeylist.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : useherokeylist.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(heroes.size());
		for (chuhan.gsp.Hero _v_ : heroes) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		ranking = _os_.unmarshal_int();
		name = _os_.unmarshal_String("UTF-16LE");
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			int _v_;
			_v_ = _os_.unmarshal_int();
			useherokeylist.put(_k_, _v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.Hero _v_ = new chuhan.gsp.Hero();
			_v_.unmarshal(_os_);
			heroes.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Battleroleinfo) {
			Battleroleinfo _o_ = (Battleroleinfo)_o1_;
			if (roleid != _o_.roleid) return false;
			if (ranking != _o_.ranking) return false;
			if (!name.equals(_o_.name)) return false;
			if (!useherokeylist.equals(_o_.useherokeylist)) return false;
			if (!heroes.equals(_o_.heroes)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += ranking;
		_h_ += name.hashCode();
		_h_ += useherokeylist.hashCode();
		_h_ += heroes.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append(ranking).append(",");
		_sb_.append("T").append(name.length()).append(",");
		_sb_.append(useherokeylist).append(",");
		_sb_.append(heroes).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

