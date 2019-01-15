
package chuhan.gsp.play.tanxian;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class tanxian implements Marshal , Comparable<tanxian>{
	public int tanxianid; // 探险id
	public int tanxiantype; // 状态，0未开启，1进行中，2已完成
	public long endtime; // 结束时间
	public int teamnum; // 队伍号

	public tanxian() {
	}

	public tanxian(int _tanxianid_, int _tanxiantype_, long _endtime_, int _teamnum_) {
		this.tanxianid = _tanxianid_;
		this.tanxiantype = _tanxiantype_;
		this.endtime = _endtime_;
		this.teamnum = _teamnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(tanxianid);
		_os_.marshal(tanxiantype);
		_os_.marshal(endtime);
		_os_.marshal(teamnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		tanxianid = _os_.unmarshal_int();
		tanxiantype = _os_.unmarshal_int();
		endtime = _os_.unmarshal_long();
		teamnum = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof tanxian) {
			tanxian _o_ = (tanxian)_o1_;
			if (tanxianid != _o_.tanxianid) return false;
			if (tanxiantype != _o_.tanxiantype) return false;
			if (endtime != _o_.endtime) return false;
			if (teamnum != _o_.teamnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += tanxianid;
		_h_ += tanxiantype;
		_h_ += (int)endtime;
		_h_ += teamnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(tanxianid).append(",");
		_sb_.append(tanxiantype).append(",");
		_sb_.append(endtime).append(",");
		_sb_.append(teamnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(tanxian _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = tanxianid - _o_.tanxianid;
		if (0 != _c_) return _c_;
		_c_ = tanxiantype - _o_.tanxiantype;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(endtime - _o_.endtime);
		if (0 != _c_) return _c_;
		_c_ = teamnum - _o_.teamnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

