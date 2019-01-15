
package chuhan.gsp.item;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class TradeInfo implements Marshal , Comparable<TradeInfo>{
	public int id; // 交易id
	public long endtime; // 结束时间
	public byte usetimes; // 使用次数

	public TradeInfo() {
	}

	public TradeInfo(int _id_, long _endtime_, byte _usetimes_) {
		this.id = _id_;
		this.endtime = _endtime_;
		this.usetimes = _usetimes_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(endtime);
		_os_.marshal(usetimes);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		endtime = _os_.unmarshal_long();
		usetimes = _os_.unmarshal_byte();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof TradeInfo) {
			TradeInfo _o_ = (TradeInfo)_o1_;
			if (id != _o_.id) return false;
			if (endtime != _o_.endtime) return false;
			if (usetimes != _o_.usetimes) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += (int)endtime;
		_h_ += (int)usetimes;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(endtime).append(",");
		_sb_.append(usetimes).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(TradeInfo _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(endtime - _o_.endtime);
		if (0 != _c_) return _c_;
		_c_ = usetimes - _o_.usetimes;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

