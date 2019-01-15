
package chuhan.gsp.play.raidboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRankView__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRankView extends __SRankView__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788542;

	public int getType() {
		return 788542;
	}

	public int rank; // 排名
	public int rongyao; // 荣耀
	public java.util.LinkedList<chuhan.gsp.play.RankInfo> rankers;

	public SRankView() {
		rankers = new java.util.LinkedList<chuhan.gsp.play.RankInfo>();
	}

	public SRankView(int _rank_, int _rongyao_, java.util.LinkedList<chuhan.gsp.play.RankInfo> _rankers_) {
		this.rank = _rank_;
		this.rongyao = _rongyao_;
		this.rankers = _rankers_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.RankInfo _v_ : rankers)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(rank);
		_os_.marshal(rongyao);
		_os_.compact_uint32(rankers.size());
		for (chuhan.gsp.play.RankInfo _v_ : rankers) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		rank = _os_.unmarshal_int();
		rongyao = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.RankInfo _v_ = new chuhan.gsp.play.RankInfo();
			_v_.unmarshal(_os_);
			rankers.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRankView) {
			SRankView _o_ = (SRankView)_o1_;
			if (rank != _o_.rank) return false;
			if (rongyao != _o_.rongyao) return false;
			if (!rankers.equals(_o_.rankers)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += rank;
		_h_ += rongyao;
		_h_ += rankers.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(rank).append(",");
		_sb_.append(rongyao).append(",");
		_sb_.append(rankers).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

