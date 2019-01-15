
package chuhan.gsp.exchange;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSendGooglePlayRceipt__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSendGooglePlayRceipt extends __CSendGooglePlayRceipt__ {
	@Override
	protected void process() {
		// protocol handle
		long roleid = gnet.link.Onlines.getInstance().findRoleid(this);
		new PVerifyGoogleReceipt(roleid, billid, packagename, productid, token).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788148;

	public int getType() {
		return 788148;
	}

	public java.lang.String billid;
	public java.lang.String packagename;
	public java.lang.String productid;
	public java.lang.String token;

	public CSendGooglePlayRceipt() {
		billid = "";
		packagename = "";
		productid = "";
		token = "";
	}

	public CSendGooglePlayRceipt(java.lang.String _billid_, java.lang.String _packagename_, java.lang.String _productid_, java.lang.String _token_) {
		this.billid = _billid_;
		this.packagename = _packagename_;
		this.productid = _productid_;
		this.token = _token_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(billid, "UTF-16LE");
		_os_.marshal(packagename, "UTF-16LE");
		_os_.marshal(productid, "UTF-16LE");
		_os_.marshal(token, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		billid = _os_.unmarshal_String("UTF-16LE");
		packagename = _os_.unmarshal_String("UTF-16LE");
		productid = _os_.unmarshal_String("UTF-16LE");
		token = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSendGooglePlayRceipt) {
			CSendGooglePlayRceipt _o_ = (CSendGooglePlayRceipt)_o1_;
			if (!billid.equals(_o_.billid)) return false;
			if (!packagename.equals(_o_.packagename)) return false;
			if (!productid.equals(_o_.productid)) return false;
			if (!token.equals(_o_.token)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += billid.hashCode();
		_h_ += packagename.hashCode();
		_h_ += productid.hashCode();
		_h_ += token.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(billid.length()).append(",");
		_sb_.append("T").append(packagename.length()).append(",");
		_sb_.append("T").append(productid.length()).append(",");
		_sb_.append("T").append(token.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

