
package chuhan.gsp.play.endlessbattle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class endlessAttr implements Marshal {
	public int dropnum; // 剩余勇者证明数量
	public int alldropnum; // 勇者证明总数
	public int add1; // 属性1购买次数
	public int add2; // 属性2购买次数
	public int add3; // 属性3购买次数
	public int add4; // 属性4购买次数（仅计数）
	public java.util.HashMap<Integer,Integer> herobloodlist; // 使用英雄的血量（key为位置，value为血量）

	public endlessAttr() {
		herobloodlist = new java.util.HashMap<Integer,Integer>();
	}

	public endlessAttr(int _dropnum_, int _alldropnum_, int _add1_, int _add2_, int _add3_, int _add4_, java.util.HashMap<Integer,Integer> _herobloodlist_) {
		this.dropnum = _dropnum_;
		this.alldropnum = _alldropnum_;
		this.add1 = _add1_;
		this.add2 = _add2_;
		this.add3 = _add3_;
		this.add4 = _add4_;
		this.herobloodlist = _herobloodlist_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(dropnum);
		_os_.marshal(alldropnum);
		_os_.marshal(add1);
		_os_.marshal(add2);
		_os_.marshal(add3);
		_os_.marshal(add4);
		_os_.compact_uint32(herobloodlist.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : herobloodlist.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		dropnum = _os_.unmarshal_int();
		alldropnum = _os_.unmarshal_int();
		add1 = _os_.unmarshal_int();
		add2 = _os_.unmarshal_int();
		add3 = _os_.unmarshal_int();
		add4 = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			int _v_;
			_v_ = _os_.unmarshal_int();
			herobloodlist.put(_k_, _v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof endlessAttr) {
			endlessAttr _o_ = (endlessAttr)_o1_;
			if (dropnum != _o_.dropnum) return false;
			if (alldropnum != _o_.alldropnum) return false;
			if (add1 != _o_.add1) return false;
			if (add2 != _o_.add2) return false;
			if (add3 != _o_.add3) return false;
			if (add4 != _o_.add4) return false;
			if (!herobloodlist.equals(_o_.herobloodlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += dropnum;
		_h_ += alldropnum;
		_h_ += add1;
		_h_ += add2;
		_h_ += add3;
		_h_ += add4;
		_h_ += herobloodlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(dropnum).append(",");
		_sb_.append(alldropnum).append(",");
		_sb_.append(add1).append(",");
		_sb_.append(add2).append(",");
		_sb_.append(add3).append(",");
		_sb_.append(add4).append(",");
		_sb_.append(herobloodlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

