
package chuhan.gsp.play.tanxian;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __STanXianOther__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class STanXianOther extends __STanXianOther__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788987;

	public int getType() {
		return 788987;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public int endtype;
	public int tanxianid; // 探险id
	public java.util.LinkedList<Integer> dropidlist; // 掉落小包ID

	public STanXianOther() {
		dropidlist = new java.util.LinkedList<Integer>();
	}

	public STanXianOther(int _result_, int _endtype_, int _tanxianid_, java.util.LinkedList<Integer> _dropidlist_) {
		this.result = _result_;
		this.endtype = _endtype_;
		this.tanxianid = _tanxianid_;
		this.dropidlist = _dropidlist_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(endtype);
		_os_.marshal(tanxianid);
		_os_.compact_uint32(dropidlist.size());
		for (Integer _v_ : dropidlist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		endtype = _os_.unmarshal_int();
		tanxianid = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			dropidlist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof STanXianOther) {
			STanXianOther _o_ = (STanXianOther)_o1_;
			if (result != _o_.result) return false;
			if (endtype != _o_.endtype) return false;
			if (tanxianid != _o_.tanxianid) return false;
			if (!dropidlist.equals(_o_.dropidlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += endtype;
		_h_ += tanxianid;
		_h_ += dropidlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(endtype).append(",");
		_sb_.append(tanxianid).append(",");
		_sb_.append(dropidlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

