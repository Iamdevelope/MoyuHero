
package chuhan;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __RemoteLogVital__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class RemoteLogVital extends __RemoteLogVital__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 61;

	public int getType() {
		return 61;
	}

	public int priority;
	public java.lang.String msg;
	public java.lang.String hostname;
	public java.lang.String servicename;

	public RemoteLogVital() {
		msg = "";
		hostname = "";
		servicename = "";
	}

	public RemoteLogVital(int _priority_, java.lang.String _msg_, java.lang.String _hostname_, java.lang.String _servicename_) {
		this.priority = _priority_;
		this.msg = _msg_;
		this.hostname = _hostname_;
		this.servicename = _servicename_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(priority);
		_os_.marshal(msg, "UTF-16LE");
		_os_.marshal(hostname, "UTF-16LE");
		_os_.marshal(servicename, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		priority = _os_.unmarshal_int();
		msg = _os_.unmarshal_String("UTF-16LE");
		hostname = _os_.unmarshal_String("UTF-16LE");
		servicename = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof RemoteLogVital) {
			RemoteLogVital _o_ = (RemoteLogVital)_o1_;
			if (priority != _o_.priority) return false;
			if (!msg.equals(_o_.msg)) return false;
			if (!hostname.equals(_o_.hostname)) return false;
			if (!servicename.equals(_o_.servicename)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += priority;
		_h_ += msg.hashCode();
		_h_ += hostname.hashCode();
		_h_ += servicename.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(priority).append(",");
		_sb_.append("T").append(msg.length()).append(",");
		_sb_.append("T").append(hostname.length()).append(",");
		_sb_.append("T").append(servicename.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

