
package chuhan.gsp.battle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class TargetDemo implements Marshal , Comparable<TargetDemo>{
	public byte targetid; // 受击者id
	public int hpchange; // 血变化
	public byte targetresult; // 参考ResultType

	public TargetDemo() {
	}

	public TargetDemo(byte _targetid_, int _hpchange_, byte _targetresult_) {
		this.targetid = _targetid_;
		this.hpchange = _hpchange_;
		this.targetresult = _targetresult_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(targetid);
		_os_.marshal(hpchange);
		_os_.marshal(targetresult);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		targetid = _os_.unmarshal_byte();
		hpchange = _os_.unmarshal_int();
		targetresult = _os_.unmarshal_byte();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof TargetDemo) {
			TargetDemo _o_ = (TargetDemo)_o1_;
			if (targetid != _o_.targetid) return false;
			if (hpchange != _o_.hpchange) return false;
			if (targetresult != _o_.targetresult) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)targetid;
		_h_ += hpchange;
		_h_ += (int)targetresult;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(targetid).append(",");
		_sb_.append(hpchange).append(",");
		_sb_.append(targetresult).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(TargetDemo _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = targetid - _o_.targetid;
		if (0 != _c_) return _c_;
		_c_ = hpchange - _o_.hpchange;
		if (0 != _c_) return _c_;
		_c_ = targetresult - _o_.targetresult;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

