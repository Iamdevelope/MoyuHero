
package chuhan.gsp.exchange;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CRequestExchangeBill__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CRequestExchangeBill extends __CRequestExchangeBill__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				ChargeRole crole = ChargeRole.getChargeRole(roleId, false);
				return crole.requestCharge(goodid, goodnum);
			};
		}.submit();
		
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788142;

	public int getType() {
		return 788142;
	}

	public int goodid;
	public int goodnum;

	public CRequestExchangeBill() {
	}

	public CRequestExchangeBill(int _goodid_, int _goodnum_) {
		this.goodid = _goodid_;
		this.goodnum = _goodnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(goodid);
		_os_.marshal(goodnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		goodid = _os_.unmarshal_int();
		goodnum = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CRequestExchangeBill) {
			CRequestExchangeBill _o_ = (CRequestExchangeBill)_o1_;
			if (goodid != _o_.goodid) return false;
			if (goodnum != _o_.goodnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += goodid;
		_h_ += goodnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(goodid).append(",");
		_sb_.append(goodnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CRequestExchangeBill _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = goodid - _o_.goodid;
		if (0 != _c_) return _c_;
		_c_ = goodnum - _o_.goodnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

