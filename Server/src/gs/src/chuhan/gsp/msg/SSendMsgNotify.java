
package chuhan.gsp.msg;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendMsgNotify__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendMsgNotify extends __SSendMsgNotify__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787633;

	public int getType() {
		return 787633;
	}

	public int msgid; // 消息提示ID
	public java.util.ArrayList<com.goldhuman.Common.Octets> parameters; // 参数

	public SSendMsgNotify() {
		parameters = new java.util.ArrayList<com.goldhuman.Common.Octets>();
	}

	public SSendMsgNotify(int _msgid_, java.util.ArrayList<com.goldhuman.Common.Octets> _parameters_) {
		this.msgid = _msgid_;
		this.parameters = _parameters_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(msgid);
		_os_.compact_uint32(parameters.size());
		for (com.goldhuman.Common.Octets _v_ : parameters) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		msgid = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			com.goldhuman.Common.Octets _v_;
			_v_ = _os_.unmarshal_Octets();
			parameters.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendMsgNotify) {
			SSendMsgNotify _o_ = (SSendMsgNotify)_o1_;
			if (msgid != _o_.msgid) return false;
			if (!parameters.equals(_o_.parameters)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += msgid;
		_h_ += parameters.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(msgid).append(",");
		_sb_.append(parameters).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

