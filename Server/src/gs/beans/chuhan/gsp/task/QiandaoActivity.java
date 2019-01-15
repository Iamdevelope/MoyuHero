
package chuhan.gsp.task;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class QiandaoActivity implements Marshal , Comparable<QiandaoActivity>{
	public byte qiandaodays; // 已连续签到天数
	public byte getreward; // 是否领取奖励

	public QiandaoActivity() {
	}

	public QiandaoActivity(byte _qiandaodays_, byte _getreward_) {
		this.qiandaodays = _qiandaodays_;
		this.getreward = _getreward_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(qiandaodays);
		_os_.marshal(getreward);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		qiandaodays = _os_.unmarshal_byte();
		getreward = _os_.unmarshal_byte();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof QiandaoActivity) {
			QiandaoActivity _o_ = (QiandaoActivity)_o1_;
			if (qiandaodays != _o_.qiandaodays) return false;
			if (getreward != _o_.getreward) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)qiandaodays;
		_h_ += (int)getreward;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(qiandaodays).append(",");
		_sb_.append(getreward).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(QiandaoActivity _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = qiandaodays - _o_.qiandaodays;
		if (0 != _c_) return _c_;
		_c_ = getreward - _o_.getreward;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

