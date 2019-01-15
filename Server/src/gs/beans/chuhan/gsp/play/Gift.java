
package chuhan.gsp.play;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class Gift implements Marshal {
	public int gold;
	public int yb;
	public int ti;
	public int hammer;
	public java.util.LinkedList<Integer> itemidlist;
	public java.util.LinkedList<Integer> heroidlist;

	public Gift() {
		itemidlist = new java.util.LinkedList<Integer>();
		heroidlist = new java.util.LinkedList<Integer>();
	}

	public Gift(int _gold_, int _yb_, int _ti_, int _hammer_, java.util.LinkedList<Integer> _itemidlist_, java.util.LinkedList<Integer> _heroidlist_) {
		this.gold = _gold_;
		this.yb = _yb_;
		this.ti = _ti_;
		this.hammer = _hammer_;
		this.itemidlist = _itemidlist_;
		this.heroidlist = _heroidlist_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(gold);
		_os_.marshal(yb);
		_os_.marshal(ti);
		_os_.marshal(hammer);
		_os_.compact_uint32(itemidlist.size());
		for (Integer _v_ : itemidlist) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(heroidlist.size());
		for (Integer _v_ : heroidlist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		gold = _os_.unmarshal_int();
		yb = _os_.unmarshal_int();
		ti = _os_.unmarshal_int();
		hammer = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			itemidlist.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			heroidlist.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Gift) {
			Gift _o_ = (Gift)_o1_;
			if (gold != _o_.gold) return false;
			if (yb != _o_.yb) return false;
			if (ti != _o_.ti) return false;
			if (hammer != _o_.hammer) return false;
			if (!itemidlist.equals(_o_.itemidlist)) return false;
			if (!heroidlist.equals(_o_.heroidlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += gold;
		_h_ += yb;
		_h_ += ti;
		_h_ += hammer;
		_h_ += itemidlist.hashCode();
		_h_ += heroidlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(gold).append(",");
		_sb_.append(yb).append(",");
		_sb_.append(ti).append(",");
		_sb_.append(hammer).append(",");
		_sb_.append(itemidlist).append(",");
		_sb_.append(heroidlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

