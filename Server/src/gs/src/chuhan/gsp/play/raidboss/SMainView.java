
package chuhan.gsp.play.raidboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SMainView__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SMainView extends __SMainView__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788534;

	public int getType() {
		return 788534;
	}

	public long endtime; // 活动结束时间
	public int rank; // 排名
	public int rongyao; // 荣耀
	public int bossid; // BOSS ID=0表示当前没BOSS
	public int bosslv;
	public long runtime; // 逃亡时间点

	public SMainView() {
	}

	public SMainView(long _endtime_, int _rank_, int _rongyao_, int _bossid_, int _bosslv_, long _runtime_) {
		this.endtime = _endtime_;
		this.rank = _rank_;
		this.rongyao = _rongyao_;
		this.bossid = _bossid_;
		this.bosslv = _bosslv_;
		this.runtime = _runtime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(endtime);
		_os_.marshal(rank);
		_os_.marshal(rongyao);
		_os_.marshal(bossid);
		_os_.marshal(bosslv);
		_os_.marshal(runtime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		endtime = _os_.unmarshal_long();
		rank = _os_.unmarshal_int();
		rongyao = _os_.unmarshal_int();
		bossid = _os_.unmarshal_int();
		bosslv = _os_.unmarshal_int();
		runtime = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SMainView) {
			SMainView _o_ = (SMainView)_o1_;
			if (endtime != _o_.endtime) return false;
			if (rank != _o_.rank) return false;
			if (rongyao != _o_.rongyao) return false;
			if (bossid != _o_.bossid) return false;
			if (bosslv != _o_.bosslv) return false;
			if (runtime != _o_.runtime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)endtime;
		_h_ += rank;
		_h_ += rongyao;
		_h_ += bossid;
		_h_ += bosslv;
		_h_ += (int)runtime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(endtime).append(",");
		_sb_.append(rank).append(",");
		_sb_.append(rongyao).append(",");
		_sb_.append(bossid).append(",");
		_sb_.append(bosslv).append(",");
		_sb_.append(runtime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SMainView _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(endtime - _o_.endtime);
		if (0 != _c_) return _c_;
		_c_ = rank - _o_.rank;
		if (0 != _c_) return _c_;
		_c_ = rongyao - _o_.rongyao;
		if (0 != _c_) return _c_;
		_c_ = bossid - _o_.bossid;
		if (0 != _c_) return _c_;
		_c_ = bosslv - _o_.bosslv;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(runtime - _o_.runtime);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

