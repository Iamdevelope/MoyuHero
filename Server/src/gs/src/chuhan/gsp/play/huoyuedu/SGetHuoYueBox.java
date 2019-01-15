
package chuhan.gsp.play.huoyuedu;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SGetHuoYueBox__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SGetHuoYueBox extends __SGetHuoYueBox__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788786;

	public int getType() {
		return 788786;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public java.util.LinkedList<Integer> droplist; // 掉落包id

	public SGetHuoYueBox() {
		droplist = new java.util.LinkedList<Integer>();
	}

	public SGetHuoYueBox(int _result_, java.util.LinkedList<Integer> _droplist_) {
		this.result = _result_;
		this.droplist = _droplist_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.compact_uint32(droplist.size());
		for (Integer _v_ : droplist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			droplist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SGetHuoYueBox) {
			SGetHuoYueBox _o_ = (SGetHuoYueBox)_o1_;
			if (result != _o_.result) return false;
			if (!droplist.equals(_o_.droplist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += droplist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(droplist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

