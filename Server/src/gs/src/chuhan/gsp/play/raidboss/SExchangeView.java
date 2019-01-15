
package chuhan.gsp.play.raidboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SExchangeView__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SExchangeView extends __SExchangeView__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788544;

	public int getType() {
		return 788544;
	}

	public int rank; // 排名
	public int jifen; // 积分
	public java.util.LinkedList<Integer> exchangeids; // 还可兑换的奖励ID

	public SExchangeView() {
		exchangeids = new java.util.LinkedList<Integer>();
	}

	public SExchangeView(int _rank_, int _jifen_, java.util.LinkedList<Integer> _exchangeids_) {
		this.rank = _rank_;
		this.jifen = _jifen_;
		this.exchangeids = _exchangeids_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(rank);
		_os_.marshal(jifen);
		_os_.compact_uint32(exchangeids.size());
		for (Integer _v_ : exchangeids) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		rank = _os_.unmarshal_int();
		jifen = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			exchangeids.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SExchangeView) {
			SExchangeView _o_ = (SExchangeView)_o1_;
			if (rank != _o_.rank) return false;
			if (jifen != _o_.jifen) return false;
			if (!exchangeids.equals(_o_.exchangeids)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += rank;
		_h_ += jifen;
		_h_ += exchangeids.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(rank).append(",");
		_sb_.append(jifen).append(",");
		_sb_.append(exchangeids).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

