
package chuhan.gsp.play.endlessbattle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class EndlessRankInfo implements Marshal {
	public long roleid; // 玩家guid
	public java.lang.String rolename; // 玩家名称
	public int level; // 玩家等级
	public int groupnum; // 第几轮
	public int trooptype; // 战队类型
	public int alldropnum; // 勇者证明总数量
	public java.util.HashMap<Integer,chuhan.gsp.play.endlessbattle.OtherHero> heroattribute; // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
	public int onranknum; // 连续在榜次数

	public EndlessRankInfo() {
		rolename = "";
		heroattribute = new java.util.HashMap<Integer,chuhan.gsp.play.endlessbattle.OtherHero>();
	}

	public EndlessRankInfo(long _roleid_, java.lang.String _rolename_, int _level_, int _groupnum_, int _trooptype_, int _alldropnum_, java.util.HashMap<Integer,chuhan.gsp.play.endlessbattle.OtherHero> _heroattribute_, int _onranknum_) {
		this.roleid = _roleid_;
		this.rolename = _rolename_;
		this.level = _level_;
		this.groupnum = _groupnum_;
		this.trooptype = _trooptype_;
		this.alldropnum = _alldropnum_;
		this.heroattribute = _heroattribute_;
		this.onranknum = _onranknum_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.endlessbattle.OtherHero> _e_ : heroattribute.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(rolename, "UTF-16LE");
		_os_.marshal(level);
		_os_.marshal(groupnum);
		_os_.marshal(trooptype);
		_os_.marshal(alldropnum);
		_os_.compact_uint32(heroattribute.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.endlessbattle.OtherHero> _e_ : heroattribute.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(onranknum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		rolename = _os_.unmarshal_String("UTF-16LE");
		level = _os_.unmarshal_int();
		groupnum = _os_.unmarshal_int();
		trooptype = _os_.unmarshal_int();
		alldropnum = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.play.endlessbattle.OtherHero _v_ = new chuhan.gsp.play.endlessbattle.OtherHero();
			_v_.unmarshal(_os_);
			heroattribute.put(_k_, _v_);
		}
		onranknum = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof EndlessRankInfo) {
			EndlessRankInfo _o_ = (EndlessRankInfo)_o1_;
			if (roleid != _o_.roleid) return false;
			if (!rolename.equals(_o_.rolename)) return false;
			if (level != _o_.level) return false;
			if (groupnum != _o_.groupnum) return false;
			if (trooptype != _o_.trooptype) return false;
			if (alldropnum != _o_.alldropnum) return false;
			if (!heroattribute.equals(_o_.heroattribute)) return false;
			if (onranknum != _o_.onranknum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += rolename.hashCode();
		_h_ += level;
		_h_ += groupnum;
		_h_ += trooptype;
		_h_ += alldropnum;
		_h_ += heroattribute.hashCode();
		_h_ += onranknum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append("T").append(rolename.length()).append(",");
		_sb_.append(level).append(",");
		_sb_.append(groupnum).append(",");
		_sb_.append(trooptype).append(",");
		_sb_.append(alldropnum).append(",");
		_sb_.append(heroattribute).append(",");
		_sb_.append(onranknum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

