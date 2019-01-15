
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CRequestTradeItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CRequestTradeItem extends __CRequestTradeItem__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
//				return chuhan.gsp.item.trade.TradeRole.getTradeRole(roleId, false)
//						.trade(id, useitemkey, useitembagtype);
				return true;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787561;

	public int getType() {
		return 787561;
	}

	public int id;
	public byte useitembagtype;
	public int useitemkey;

	public CRequestTradeItem() {
	}

	public CRequestTradeItem(int _id_, byte _useitembagtype_, int _useitemkey_) {
		this.id = _id_;
		this.useitembagtype = _useitembagtype_;
		this.useitemkey = _useitemkey_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(id);
		_os_.marshal(useitembagtype);
		_os_.marshal(useitemkey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		useitembagtype = _os_.unmarshal_byte();
		useitemkey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CRequestTradeItem) {
			CRequestTradeItem _o_ = (CRequestTradeItem)_o1_;
			if (id != _o_.id) return false;
			if (useitembagtype != _o_.useitembagtype) return false;
			if (useitemkey != _o_.useitemkey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += (int)useitembagtype;
		_h_ += useitemkey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(useitembagtype).append(",");
		_sb_.append(useitemkey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CRequestTradeItem _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = useitembagtype - _o_.useitembagtype;
		if (0 != _c_) return _c_;
		_c_ = useitemkey - _o_.useitemkey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

