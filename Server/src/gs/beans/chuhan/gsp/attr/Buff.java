
package chuhan.gsp.attr;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class Buff implements Marshal {
	public long time; // 剩余的时间
	public int count; // 剩余的回合数/次数
	public java.util.LinkedList<com.goldhuman.Common.Octets> tipargs; // 显示Buff Tip的参数

	public Buff() {
		tipargs = new java.util.LinkedList<com.goldhuman.Common.Octets>();
	}

	public Buff(long _time_, int _count_, java.util.LinkedList<com.goldhuman.Common.Octets> _tipargs_) {
		this.time = _time_;
		this.count = _count_;
		this.tipargs = _tipargs_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(time);
		_os_.marshal(count);
		_os_.compact_uint32(tipargs.size());
		for (com.goldhuman.Common.Octets _v_ : tipargs) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		time = _os_.unmarshal_long();
		count = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			com.goldhuman.Common.Octets _v_;
			_v_ = _os_.unmarshal_Octets();
			tipargs.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Buff) {
			Buff _o_ = (Buff)_o1_;
			if (time != _o_.time) return false;
			if (count != _o_.count) return false;
			if (!tipargs.equals(_o_.tipargs)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)time;
		_h_ += count;
		_h_ += tipargs.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(time).append(",");
		_sb_.append(count).append(",");
		_sb_.append(tipargs).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

