
package chuhan.gsp.play.lotteryitem;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class LotteryItemAll implements Marshal {
	public int mapkey; // 第几层
	public int mapvalue; // 第几个
	public java.util.LinkedList<Integer> superlist; // 遗迹宝藏特殊list
	public int ismonthfirsthave; // 是否有月卡首刷，0没有，1有
	public int ishavefree; // 是否有免费抽奖，0没有，1有
	public java.util.HashMap<Integer,chuhan.gsp.play.lotteryitem.LotteryItemlayer> lotteryitemmap; // 遗迹宝藏总信息

	public LotteryItemAll() {
		superlist = new java.util.LinkedList<Integer>();
		lotteryitemmap = new java.util.HashMap<Integer,chuhan.gsp.play.lotteryitem.LotteryItemlayer>();
	}

	public LotteryItemAll(int _mapkey_, int _mapvalue_, java.util.LinkedList<Integer> _superlist_, int _ismonthfirsthave_, int _ishavefree_, java.util.HashMap<Integer,chuhan.gsp.play.lotteryitem.LotteryItemlayer> _lotteryitemmap_) {
		this.mapkey = _mapkey_;
		this.mapvalue = _mapvalue_;
		this.superlist = _superlist_;
		this.ismonthfirsthave = _ismonthfirsthave_;
		this.ishavefree = _ishavefree_;
		this.lotteryitemmap = _lotteryitemmap_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.lotteryitem.LotteryItemlayer> _e_ : lotteryitemmap.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(mapkey);
		_os_.marshal(mapvalue);
		_os_.compact_uint32(superlist.size());
		for (Integer _v_ : superlist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(ismonthfirsthave);
		_os_.marshal(ishavefree);
		_os_.compact_uint32(lotteryitemmap.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.lotteryitem.LotteryItemlayer> _e_ : lotteryitemmap.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		mapkey = _os_.unmarshal_int();
		mapvalue = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			superlist.add(_v_);
		}
		ismonthfirsthave = _os_.unmarshal_int();
		ishavefree = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.play.lotteryitem.LotteryItemlayer _v_ = new chuhan.gsp.play.lotteryitem.LotteryItemlayer();
			_v_.unmarshal(_os_);
			lotteryitemmap.put(_k_, _v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof LotteryItemAll) {
			LotteryItemAll _o_ = (LotteryItemAll)_o1_;
			if (mapkey != _o_.mapkey) return false;
			if (mapvalue != _o_.mapvalue) return false;
			if (!superlist.equals(_o_.superlist)) return false;
			if (ismonthfirsthave != _o_.ismonthfirsthave) return false;
			if (ishavefree != _o_.ishavefree) return false;
			if (!lotteryitemmap.equals(_o_.lotteryitemmap)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += mapkey;
		_h_ += mapvalue;
		_h_ += superlist.hashCode();
		_h_ += ismonthfirsthave;
		_h_ += ishavefree;
		_h_ += lotteryitemmap.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(mapkey).append(",");
		_sb_.append(mapvalue).append(",");
		_sb_.append(superlist).append(",");
		_sb_.append(ismonthfirsthave).append(",");
		_sb_.append(ishavefree).append(",");
		_sb_.append(lotteryitemmap).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

