
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SGetBossRank__ extends xio.Protocol { }

/** 获取BOSS战排行榜返回
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SGetBossRank extends __SGetBossRank__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788900;

	public int getType() {
		return 788900;
	}

	public java.util.LinkedList<chuhan.gsp.play.wordboss.BossRankInfo> rank;
	public int ranknum; // 排名
	public long num; // 伤害

	public SGetBossRank() {
		rank = new java.util.LinkedList<chuhan.gsp.play.wordboss.BossRankInfo>();
	}

	public SGetBossRank(java.util.LinkedList<chuhan.gsp.play.wordboss.BossRankInfo> _rank_, int _ranknum_, long _num_) {
		this.rank = _rank_;
		this.ranknum = _ranknum_;
		this.num = _num_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.play.wordboss.BossRankInfo _v_ : rank)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(rank.size());
		for (chuhan.gsp.play.wordboss.BossRankInfo _v_ : rank) {
			_os_.marshal(_v_);
		}
		_os_.marshal(ranknum);
		_os_.marshal(num);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.play.wordboss.BossRankInfo _v_ = new chuhan.gsp.play.wordboss.BossRankInfo();
			_v_.unmarshal(_os_);
			rank.add(_v_);
		}
		ranknum = _os_.unmarshal_int();
		num = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SGetBossRank) {
			SGetBossRank _o_ = (SGetBossRank)_o1_;
			if (!rank.equals(_o_.rank)) return false;
			if (ranknum != _o_.ranknum) return false;
			if (num != _o_.num) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += rank.hashCode();
		_h_ += ranknum;
		_h_ += (int)num;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(rank).append(",");
		_sb_.append(ranknum).append(",");
		_sb_.append(num).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

