
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 活跃度信息 by yanglk
*/
public class Huoyue implements Marshal , Comparable<Huoyue>{
	public int huoyueid; // 活跃id
	public int num; // 发生次数
	public int numall; // 总次数
	public int huoyuetype; // 任务类型
	public int isok; // 是否完成

	public Huoyue() {
	}

	public Huoyue(int _huoyueid_, int _num_, int _numall_, int _huoyuetype_, int _isok_) {
		this.huoyueid = _huoyueid_;
		this.num = _num_;
		this.numall = _numall_;
		this.huoyuetype = _huoyuetype_;
		this.isok = _isok_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(huoyueid);
		_os_.marshal(num);
		_os_.marshal(numall);
		_os_.marshal(huoyuetype);
		_os_.marshal(isok);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		huoyueid = _os_.unmarshal_int();
		num = _os_.unmarshal_int();
		numall = _os_.unmarshal_int();
		huoyuetype = _os_.unmarshal_int();
		isok = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Huoyue) {
			Huoyue _o_ = (Huoyue)_o1_;
			if (huoyueid != _o_.huoyueid) return false;
			if (num != _o_.num) return false;
			if (numall != _o_.numall) return false;
			if (huoyuetype != _o_.huoyuetype) return false;
			if (isok != _o_.isok) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += huoyueid;
		_h_ += num;
		_h_ += numall;
		_h_ += huoyuetype;
		_h_ += isok;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(huoyueid).append(",");
		_sb_.append(num).append(",");
		_sb_.append(numall).append(",");
		_sb_.append(huoyuetype).append(",");
		_sb_.append(isok).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(Huoyue _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = huoyueid - _o_.huoyueid;
		if (0 != _c_) return _c_;
		_c_ = num - _o_.num;
		if (0 != _c_) return _c_;
		_c_ = numall - _o_.numall;
		if (0 != _c_) return _c_;
		_c_ = huoyuetype - _o_.huoyuetype;
		if (0 != _c_) return _c_;
		_c_ = isok - _o_.isok;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

