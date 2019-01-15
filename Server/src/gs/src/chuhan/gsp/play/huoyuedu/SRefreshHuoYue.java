
package chuhan.gsp.play.huoyuedu;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshHuoYue__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshHuoYue extends __SRefreshHuoYue__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788784;

	public int getType() {
		return 788784;
	}

	public int huoyuenum; // 活跃度
	public int getnum; // 领取记录，个位第一个，十位第二个~~
	public java.util.LinkedList<chuhan.gsp.Huoyue> huoyuelist; // 活跃任务列表

	public SRefreshHuoYue() {
		huoyuelist = new java.util.LinkedList<chuhan.gsp.Huoyue>();
	}

	public SRefreshHuoYue(int _huoyuenum_, int _getnum_, java.util.LinkedList<chuhan.gsp.Huoyue> _huoyuelist_) {
		this.huoyuenum = _huoyuenum_;
		this.getnum = _getnum_;
		this.huoyuelist = _huoyuelist_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.Huoyue _v_ : huoyuelist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(huoyuenum);
		_os_.marshal(getnum);
		_os_.compact_uint32(huoyuelist.size());
		for (chuhan.gsp.Huoyue _v_ : huoyuelist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		huoyuenum = _os_.unmarshal_int();
		getnum = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.Huoyue _v_ = new chuhan.gsp.Huoyue();
			_v_.unmarshal(_os_);
			huoyuelist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshHuoYue) {
			SRefreshHuoYue _o_ = (SRefreshHuoYue)_o1_;
			if (huoyuenum != _o_.huoyuenum) return false;
			if (getnum != _o_.getnum) return false;
			if (!huoyuelist.equals(_o_.huoyuelist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += huoyuenum;
		_h_ += getnum;
		_h_ += huoyuelist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(huoyuenum).append(",");
		_sb_.append(getnum).append(",");
		_sb_.append(huoyuelist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

