
package chuhan.gsp.play.endlessbattle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SGetEndlessRank__ extends xio.Protocol { }

/** 获取极限试炼排行榜返回
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SGetEndlessRank extends __SGetEndlessRank__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788945;

	public int getType() {
		return 788945;
	}

	public java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo> rank1_50;
	public java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo> rank51_100;
	public java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo> rank101_;

	public SGetEndlessRank() {
		rank1_50 = new java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo>();
		rank51_100 = new java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo>();
		rank101_ = new java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo>();
	}

	public SGetEndlessRank(java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo> _rank1_50_, java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo> _rank51_100_, java.util.LinkedList<chuhan.gsp.play.endlessbattle.EndlessRankInfo> _rank101__) {
		this.rank1_50 = _rank1_50_;
		this.rank51_100 = _rank51_100_;
		this.rank101_ = _rank101__;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ : rank1_50)
			if (!_v_._validator_()) return false;
		for (chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ : rank51_100)
			if (!_v_._validator_()) return false;
		for (chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ : rank101_)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(rank1_50.size());
		for (chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ : rank1_50) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(rank51_100.size());
		for (chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ : rank51_100) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(rank101_.size());
		for (chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ : rank101_) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ = new chuhan.gsp.play.endlessbattle.EndlessRankInfo();
			_v_.unmarshal(_os_);
			rank1_50.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ = new chuhan.gsp.play.endlessbattle.EndlessRankInfo();
			_v_.unmarshal(_os_);
			rank51_100.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.endlessbattle.EndlessRankInfo _v_ = new chuhan.gsp.play.endlessbattle.EndlessRankInfo();
			_v_.unmarshal(_os_);
			rank101_.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SGetEndlessRank) {
			SGetEndlessRank _o_ = (SGetEndlessRank)_o1_;
			if (!rank1_50.equals(_o_.rank1_50)) return false;
			if (!rank51_100.equals(_o_.rank51_100)) return false;
			if (!rank101_.equals(_o_.rank101_)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += rank1_50.hashCode();
		_h_ += rank51_100.hashCode();
		_h_ += rank101_.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(rank1_50).append(",");
		_sb_.append(rank51_100).append(",");
		_sb_.append(rank101_).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

