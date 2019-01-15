
package chuhan.gsp.play.lotteryitem;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SLotteryItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SLotteryItem extends __SLotteryItem__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788764;

	public int getType() {
		return 788764;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public int lotterytype;
	public java.util.LinkedList<chuhan.gsp.play.lotteryitem.LotteryItemget> itemlist; // 遗迹宝藏列表

	public SLotteryItem() {
		itemlist = new java.util.LinkedList<chuhan.gsp.play.lotteryitem.LotteryItemget>();
	}

	public SLotteryItem(int _result_, int _lotterytype_, java.util.LinkedList<chuhan.gsp.play.lotteryitem.LotteryItemget> _itemlist_) {
		this.result = _result_;
		this.lotterytype = _lotterytype_;
		this.itemlist = _itemlist_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.lotteryitem.LotteryItemget _v_ : itemlist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(lotterytype);
		_os_.compact_uint32(itemlist.size());
		for (chuhan.gsp.play.lotteryitem.LotteryItemget _v_ : itemlist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		lotterytype = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.lotteryitem.LotteryItemget _v_ = new chuhan.gsp.play.lotteryitem.LotteryItemget();
			_v_.unmarshal(_os_);
			itemlist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SLotteryItem) {
			SLotteryItem _o_ = (SLotteryItem)_o1_;
			if (result != _o_.result) return false;
			if (lotterytype != _o_.lotterytype) return false;
			if (!itemlist.equals(_o_.itemlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += lotterytype;
		_h_ += itemlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(lotterytype).append(",");
		_sb_.append(itemlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

