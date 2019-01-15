
package chuhan.gsp.task;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class HeroTimeTask implements Marshal , Comparable<HeroTimeTask>{
	public byte pos;
	public byte taskid;
	public long endtime;

	public HeroTimeTask() {
	}

	public HeroTimeTask(byte _pos_, byte _taskid_, long _endtime_) {
		this.pos = _pos_;
		this.taskid = _taskid_;
		this.endtime = _endtime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(pos);
		_os_.marshal(taskid);
		_os_.marshal(endtime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		pos = _os_.unmarshal_byte();
		taskid = _os_.unmarshal_byte();
		endtime = _os_.unmarshal_long();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof HeroTimeTask) {
			HeroTimeTask _o_ = (HeroTimeTask)_o1_;
			if (pos != _o_.pos) return false;
			if (taskid != _o_.taskid) return false;
			if (endtime != _o_.endtime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)pos;
		_h_ += (int)taskid;
		_h_ += (int)endtime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(pos).append(",");
		_sb_.append(taskid).append(",");
		_sb_.append(endtime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(HeroTimeTask _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = pos - _o_.pos;
		if (0 != _c_) return _c_;
		_c_ = taskid - _o_.taskid;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(endtime - _o_.endtime);
		if (0 != _c_) return _c_;
		return _c_;
	}

}

