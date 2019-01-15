
package chuhan.gsp.play.lotteryitem;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshLottyItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshLottyItem extends __SRefreshLottyItem__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788765;

	public int getType() {
		return 788765;
	}

	public chuhan.gsp.play.lotteryitem.LotteryItemAll lotteryitem;

	public SRefreshLottyItem() {
		lotteryitem = new chuhan.gsp.play.lotteryitem.LotteryItemAll();
	}

	public SRefreshLottyItem(chuhan.gsp.play.lotteryitem.LotteryItemAll _lotteryitem_) {
		this.lotteryitem = _lotteryitem_;
	}

	public final boolean _validator_() {
		if (!lotteryitem._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(lotteryitem);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		lotteryitem.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshLottyItem) {
			SRefreshLottyItem _o_ = (SRefreshLottyItem)_o1_;
			if (!lotteryitem.equals(_o_.lotteryitem)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += lotteryitem.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lotteryitem).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

