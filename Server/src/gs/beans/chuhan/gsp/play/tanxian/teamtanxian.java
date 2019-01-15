
package chuhan.gsp.play.tanxian;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class teamtanxian implements Marshal {
	public int tanxianid; // 探险id
	public java.util.LinkedList<Integer> team; // 小队英雄key列表

	public teamtanxian() {
		team = new java.util.LinkedList<Integer>();
	}

	public teamtanxian(int _tanxianid_, java.util.LinkedList<Integer> _team_) {
		this.tanxianid = _tanxianid_;
		this.team = _team_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(tanxianid);
		_os_.compact_uint32(team.size());
		for (Integer _v_ : team) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		tanxianid = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			team.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof teamtanxian) {
			teamtanxian _o_ = (teamtanxian)_o1_;
			if (tanxianid != _o_.tanxianid) return false;
			if (!team.equals(_o_.team)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += tanxianid;
		_h_ += team.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(tanxianid).append(",");
		_sb_.append(team).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

