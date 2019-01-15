
package chuhan.gsp.exchange;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SReplyExchangeBill__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SReplyExchangeBill extends __SReplyExchangeBill__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788143;

	public int getType() {
		return 788143;
	}

	public java.lang.String billid;
	public int goodid;
	public java.lang.String goodname;
	public int goodnum;
	public java.lang.String price;
	public int serverid;

	public SReplyExchangeBill() {
		billid = "";
		goodname = "";
		price = "";
	}

	public SReplyExchangeBill(java.lang.String _billid_, int _goodid_, java.lang.String _goodname_, int _goodnum_, java.lang.String _price_, int _serverid_) {
		this.billid = _billid_;
		this.goodid = _goodid_;
		this.goodname = _goodname_;
		this.goodnum = _goodnum_;
		this.price = _price_;
		this.serverid = _serverid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(billid, "UTF-16LE");
		_os_.marshal(goodid);
		_os_.marshal(goodname, "UTF-16LE");
		_os_.marshal(goodnum);
		_os_.marshal(price, "UTF-16LE");
		_os_.marshal(serverid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		billid = _os_.unmarshal_String("UTF-16LE");
		goodid = _os_.unmarshal_int();
		goodname = _os_.unmarshal_String("UTF-16LE");
		goodnum = _os_.unmarshal_int();
		price = _os_.unmarshal_String("UTF-16LE");
		serverid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SReplyExchangeBill) {
			SReplyExchangeBill _o_ = (SReplyExchangeBill)_o1_;
			if (!billid.equals(_o_.billid)) return false;
			if (goodid != _o_.goodid) return false;
			if (!goodname.equals(_o_.goodname)) return false;
			if (goodnum != _o_.goodnum) return false;
			if (!price.equals(_o_.price)) return false;
			if (serverid != _o_.serverid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += billid.hashCode();
		_h_ += goodid;
		_h_ += goodname.hashCode();
		_h_ += goodnum;
		_h_ += price.hashCode();
		_h_ += serverid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(billid.length()).append(",");
		_sb_.append(goodid).append(",");
		_sb_.append("T").append(goodname.length()).append(",");
		_sb_.append(goodnum).append(",");
		_sb_.append("T").append(price.length()).append(",");
		_sb_.append(serverid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

