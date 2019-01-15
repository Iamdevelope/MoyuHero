
package chuhan.gsp.exchange;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CResponseExchangeBill__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CResponseExchangeBill extends __CResponseExchangeBill__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		final long billuid = Long.valueOf(billid);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				ChargeRole crole = ChargeRole.getChargeRole(roleId, false);
				return crole.responseCharge(billuid, status);
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788145;

	public int getType() {
		return 788145;
	}

	public java.lang.String billid;
	public byte status;

	public CResponseExchangeBill() {
		billid = "";
	}

	public CResponseExchangeBill(java.lang.String _billid_, byte _status_) {
		this.billid = _billid_;
		this.status = _status_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(billid, "UTF-16LE");
		_os_.marshal(status);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		billid = _os_.unmarshal_String("UTF-16LE");
		status = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CResponseExchangeBill) {
			CResponseExchangeBill _o_ = (CResponseExchangeBill)_o1_;
			if (!billid.equals(_o_.billid)) return false;
			if (status != _o_.status) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += billid.hashCode();
		_h_ += (int)status;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(billid.length()).append(",");
		_sb_.append(status).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

