
package chuhan.gsp.play.lotteryitem;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class LotteryItem implements Marshal , Comparable<LotteryItem>{
	public int id; // 遗迹宝藏ID
	public int isget; // 是否领取
	public int viewnum; // 显示位置
	public int superid; // 激活的特殊事件

	public LotteryItem() {
	}

	public LotteryItem(int _id_, int _isget_, int _viewnum_, int _superid_) {
		this.id = _id_;
		this.isget = _isget_;
		this.viewnum = _viewnum_;
		this.superid = _superid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(isget);
		_os_.marshal(viewnum);
		_os_.marshal(superid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		isget = _os_.unmarshal_int();
		viewnum = _os_.unmarshal_int();
		superid = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof LotteryItem) {
			LotteryItem _o_ = (LotteryItem)_o1_;
			if (id != _o_.id) return false;
			if (isget != _o_.isget) return false;
			if (viewnum != _o_.viewnum) return false;
			if (superid != _o_.superid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += isget;
		_h_ += viewnum;
		_h_ += superid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(isget).append(",");
		_sb_.append(viewnum).append(",");
		_sb_.append(superid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(LotteryItem _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = isget - _o_.isget;
		if (0 != _c_) return _c_;
		_c_ = viewnum - _o_.viewnum;
		if (0 != _c_) return _c_;
		_c_ = superid - _o_.superid;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

