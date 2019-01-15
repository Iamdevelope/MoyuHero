
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshTradeItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshTradeItem extends __SRefreshTradeItem__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787562;

	public int getType() {
		return 787562;
	}

	public chuhan.gsp.item.TradeInfo iteminfo;

	public SRefreshTradeItem() {
		iteminfo = new chuhan.gsp.item.TradeInfo();
	}

	public SRefreshTradeItem(chuhan.gsp.item.TradeInfo _iteminfo_) {
		this.iteminfo = _iteminfo_;
	}

	public final boolean _validator_() {
		if (!iteminfo._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(iteminfo);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		iteminfo.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshTradeItem) {
			SRefreshTradeItem _o_ = (SRefreshTradeItem)_o1_;
			if (!iteminfo.equals(_o_.iteminfo)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += iteminfo.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(iteminfo).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshTradeItem _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = iteminfo.compareTo(_o_.iteminfo);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

