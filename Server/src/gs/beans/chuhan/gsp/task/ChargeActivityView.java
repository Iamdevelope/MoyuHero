
package chuhan.gsp.task;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class ChargeActivityView implements Marshal {
	public int totalcharge; // 充值的元宝总数
	public long endtime; // 活动结束时间
	public java.util.ArrayList<Integer> isgain; // 已经领取过的奖励

	public ChargeActivityView() {
		isgain = new java.util.ArrayList<Integer>();
	}

	public ChargeActivityView(int _totalcharge_, long _endtime_, java.util.ArrayList<Integer> _isgain_) {
		this.totalcharge = _totalcharge_;
		this.endtime = _endtime_;
		this.isgain = _isgain_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(totalcharge);
		_os_.marshal(endtime);
		_os_.compact_uint32(isgain.size());
		for (Integer _v_ : isgain) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		totalcharge = _os_.unmarshal_int();
		endtime = _os_.unmarshal_long();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			isgain.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof ChargeActivityView) {
			ChargeActivityView _o_ = (ChargeActivityView)_o1_;
			if (totalcharge != _o_.totalcharge) return false;
			if (endtime != _o_.endtime) return false;
			if (!isgain.equals(_o_.isgain)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += totalcharge;
		_h_ += (int)endtime;
		_h_ += isgain.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(totalcharge).append(",");
		_sb_.append(endtime).append(",");
		_sb_.append(isgain).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

