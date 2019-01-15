
package chuhan.gsp.play.lotteryitem;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class LotteryItemlayer implements Marshal {
	public java.util.LinkedList<chuhan.gsp.play.lotteryitem.LotteryItem> lotteryitemlist; // 遗迹宝藏每层list

	public LotteryItemlayer() {
		lotteryitemlist = new java.util.LinkedList<chuhan.gsp.play.lotteryitem.LotteryItem>();
	}

	public LotteryItemlayer(java.util.LinkedList<chuhan.gsp.play.lotteryitem.LotteryItem> _lotteryitemlist_) {
		this.lotteryitemlist = _lotteryitemlist_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.lotteryitem.LotteryItem _v_ : lotteryitemlist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.compact_uint32(lotteryitemlist.size());
		for (chuhan.gsp.play.lotteryitem.LotteryItem _v_ : lotteryitemlist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.lotteryitem.LotteryItem _v_ = new chuhan.gsp.play.lotteryitem.LotteryItem();
			_v_.unmarshal(_os_);
			lotteryitemlist.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof LotteryItemlayer) {
			LotteryItemlayer _o_ = (LotteryItemlayer)_o1_;
			if (!lotteryitemlist.equals(_o_.lotteryitemlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += lotteryitemlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lotteryitemlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

