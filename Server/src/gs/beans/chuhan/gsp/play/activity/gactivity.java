
package chuhan.gsp.play.activity;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class gactivity implements Marshal , Comparable<gactivity>{
	public int id; // 活动id
	public long time; // 最近一次时间
	public int todaynum; // 今日次数
	public int allnum; // 累计次数
	public int cangetnum; // 可以领取次数（）
	public int activitynum; // 活动计数
	public int allactivitynum; // 累计计数
	public int issee; // 是否看过（提示用，0未看，1已看）

	public gactivity() {
	}

	public gactivity(int _id_, long _time_, int _todaynum_, int _allnum_, int _cangetnum_, int _activitynum_, int _allactivitynum_, int _issee_) {
		this.id = _id_;
		this.time = _time_;
		this.todaynum = _todaynum_;
		this.allnum = _allnum_;
		this.cangetnum = _cangetnum_;
		this.activitynum = _activitynum_;
		this.allactivitynum = _allactivitynum_;
		this.issee = _issee_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(time);
		_os_.marshal(todaynum);
		_os_.marshal(allnum);
		_os_.marshal(cangetnum);
		_os_.marshal(activitynum);
		_os_.marshal(allactivitynum);
		_os_.marshal(issee);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		time = _os_.unmarshal_long();
		todaynum = _os_.unmarshal_int();
		allnum = _os_.unmarshal_int();
		cangetnum = _os_.unmarshal_int();
		activitynum = _os_.unmarshal_int();
		allactivitynum = _os_.unmarshal_int();
		issee = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof gactivity) {
			gactivity _o_ = (gactivity)_o1_;
			if (id != _o_.id) return false;
			if (time != _o_.time) return false;
			if (todaynum != _o_.todaynum) return false;
			if (allnum != _o_.allnum) return false;
			if (cangetnum != _o_.cangetnum) return false;
			if (activitynum != _o_.activitynum) return false;
			if (allactivitynum != _o_.allactivitynum) return false;
			if (issee != _o_.issee) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += (int)time;
		_h_ += todaynum;
		_h_ += allnum;
		_h_ += cangetnum;
		_h_ += activitynum;
		_h_ += allactivitynum;
		_h_ += issee;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(time).append(",");
		_sb_.append(todaynum).append(",");
		_sb_.append(allnum).append(",");
		_sb_.append(cangetnum).append(",");
		_sb_.append(activitynum).append(",");
		_sb_.append(allactivitynum).append(",");
		_sb_.append(issee).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(gactivity _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(time - _o_.time);
		if (0 != _c_) return _c_;
		_c_ = todaynum - _o_.todaynum;
		if (0 != _c_) return _c_;
		_c_ = allnum - _o_.allnum;
		if (0 != _c_) return _c_;
		_c_ = cangetnum - _o_.cangetnum;
		if (0 != _c_) return _c_;
		_c_ = activitynum - _o_.activitynum;
		if (0 != _c_) return _c_;
		_c_ = allactivitynum - _o_.allactivitynum;
		if (0 != _c_) return _c_;
		_c_ = issee - _o_.issee;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

