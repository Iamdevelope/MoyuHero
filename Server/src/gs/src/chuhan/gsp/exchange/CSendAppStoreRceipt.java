
package chuhan.gsp.exchange;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSendAppStoreRceipt__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSendAppStoreRceipt extends __CSendAppStoreRceipt__ {
	@Override
	protected void process() {
		// protocol handle
		long roleid = gnet.link.Onlines.getInstance().findRoleid(this);
		new PVerifyAppReceipt(roleid, transactionid, receipt).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788146;

	public int getType() {
		return 788146;
	}

	public java.lang.String transactionid;
	public java.lang.String receipt;

	public CSendAppStoreRceipt() {
		transactionid = "";
		receipt = "";
	}

	public CSendAppStoreRceipt(java.lang.String _transactionid_, java.lang.String _receipt_) {
		this.transactionid = _transactionid_;
		this.receipt = _receipt_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(transactionid, "UTF-16LE");
		_os_.marshal(receipt, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		transactionid = _os_.unmarshal_String("UTF-16LE");
		receipt = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSendAppStoreRceipt) {
			CSendAppStoreRceipt _o_ = (CSendAppStoreRceipt)_o1_;
			if (!transactionid.equals(_o_.transactionid)) return false;
			if (!receipt.equals(_o_.receipt)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += transactionid.hashCode();
		_h_ += receipt.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(transactionid.length()).append(",");
		_sb_.append("T").append(receipt.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

