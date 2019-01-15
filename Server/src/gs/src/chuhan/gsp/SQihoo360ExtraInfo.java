
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SQihoo360ExtraInfo__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SQihoo360ExtraInfo extends __SQihoo360ExtraInfo__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786445;

	public int getType() {
		return 786445;
	}

	public java.lang.String uin;
	public java.lang.String token;
	public java.lang.String url;

	public SQihoo360ExtraInfo() {
		uin = "";
		token = "";
		url = "";
	}

	public SQihoo360ExtraInfo(java.lang.String _uin_, java.lang.String _token_, java.lang.String _url_) {
		this.uin = _uin_;
		this.token = _token_;
		this.url = _url_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(uin, "UTF-16LE");
		_os_.marshal(token, "UTF-16LE");
		_os_.marshal(url, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		uin = _os_.unmarshal_String("UTF-16LE");
		token = _os_.unmarshal_String("UTF-16LE");
		url = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SQihoo360ExtraInfo) {
			SQihoo360ExtraInfo _o_ = (SQihoo360ExtraInfo)_o1_;
			if (!uin.equals(_o_.uin)) return false;
			if (!token.equals(_o_.token)) return false;
			if (!url.equals(_o_.url)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += uin.hashCode();
		_h_ += token.hashCode();
		_h_ += url.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(uin.length()).append(",");
		_sb_.append("T").append(token.length()).append(",");
		_sb_.append("T").append(url.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

