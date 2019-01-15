
package chuhan.gsp.play.lottery;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SLottery__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SLottery extends __SLottery__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788734;

	public int getType() {
		return 788734;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public int lotterytype;
	public java.util.LinkedList<Integer> herolist; // 英雄列表（已废弃）
	public java.util.LinkedList<chuhan.gsp.play.lottery.Items> items; // 抽中物列表列表

	public SLottery() {
		herolist = new java.util.LinkedList<Integer>();
		items = new java.util.LinkedList<chuhan.gsp.play.lottery.Items>();
	}

	public SLottery(int _result_, int _lotterytype_, java.util.LinkedList<Integer> _herolist_, java.util.LinkedList<chuhan.gsp.play.lottery.Items> _items_) {
		this.result = _result_;
		this.lotterytype = _lotterytype_;
		this.herolist = _herolist_;
		this.items = _items_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.lottery.Items _v_ : items)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(lotterytype);
		_os_.compact_uint32(herolist.size());
		for (Integer _v_ : herolist) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(items.size());
		for (chuhan.gsp.play.lottery.Items _v_ : items) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		lotterytype = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			herolist.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.lottery.Items _v_ = new chuhan.gsp.play.lottery.Items();
			_v_.unmarshal(_os_);
			items.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SLottery) {
			SLottery _o_ = (SLottery)_o1_;
			if (result != _o_.result) return false;
			if (lotterytype != _o_.lotterytype) return false;
			if (!herolist.equals(_o_.herolist)) return false;
			if (!items.equals(_o_.items)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += lotterytype;
		_h_ += herolist.hashCode();
		_h_ += items.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(lotterytype).append(",");
		_sb_.append(herolist).append(",");
		_sb_.append(items).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

