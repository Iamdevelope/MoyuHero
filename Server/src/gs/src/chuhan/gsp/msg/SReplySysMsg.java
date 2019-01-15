
package chuhan.gsp.msg;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SReplySysMsg__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SReplySysMsg extends __SReplySysMsg__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787639;

	public int getType() {
		return 787639;
	}

	public java.util.LinkedList<chuhan.gsp.msg.SysMsg> msgs; // 只发这次上线后没发过的，旧的在前，新的在后

	public SReplySysMsg() {
		msgs = new java.util.LinkedList<chuhan.gsp.msg.SysMsg>();
	}

	public SReplySysMsg(java.util.LinkedList<chuhan.gsp.msg.SysMsg> _msgs_) {
		this.msgs = _msgs_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.msg.SysMsg _v_ : msgs)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(msgs.size());
		for (chuhan.gsp.msg.SysMsg _v_ : msgs) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.msg.SysMsg _v_ = new chuhan.gsp.msg.SysMsg();
			_v_.unmarshal(_os_);
			msgs.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SReplySysMsg) {
			SReplySysMsg _o_ = (SReplySysMsg)_o1_;
			if (!msgs.equals(_o_.msgs)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += msgs.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(msgs).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

