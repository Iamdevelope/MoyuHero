
package chuhan.gsp.play.camp;

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
	public static final int PROTOCOL_TYPE = 788634;

	public int getType() {
		return 788634;
	}

	public int gongxun;
	public int jifen;
	public int rank;
	public long endtime; // 活动结束时间
	public short mycamp; // 我的阵营
	public int cjifen; // 楚积分
	public int hjifen; // 汉积分
	public int qjifen; // 群积分
	public byte isbufftime; // 当前时间是否有加成 0-否 1-是

	public SMainView() {
	}

	public SMainView(int _gongxun_, int _jifen_, int _rank_, long _endtime_, short _mycamp_, int _cjifen_, int _hjifen_, int _qjifen_, byte _isbufftime_) {
		this.gongxun = _gongxun_;
		this.jifen = _jifen_;
		this.rank = _rank_;
		this.endtime = _endtime_;
		this.mycamp = _mycamp_;
		this.cjifen = _cjifen_;
		this.hjifen = _hjifen_;
		this.qjifen = _qjifen_;
		this.isbufftime = _isbufftime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(gongxun);
		_os_.marshal(jifen);
		_os_.marshal(rank);
		_os_.marshal(endtime);
		_os_.marshal(mycamp);
		_os_.marshal(cjifen);
		_os_.marshal(hjifen);
		_os_.marshal(qjifen);
		_os_.marshal(isbufftime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		gongxun = _os_.unmarshal_int();
		jifen = _os_.unmarshal_int();
		rank = _os_.unmarshal_int();
		endtime = _os_.unmarshal_long();
		mycamp = _os_.unmarshal_short();
		cjifen = _os_.unmarshal_int();
		hjifen = _os_.unmarshal_int();
		qjifen = _os_.unmarshal_int();
		isbufftime = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SMainView) {
			SMainView _o_ = (SMainView)_o1_;
			if (gongxun != _o_.gongxun) return false;
			if (jifen != _o_.jifen) return false;
			if (rank != _o_.rank) return false;
			if (endtime != _o_.endtime) return false;
			if (mycamp != _o_.mycamp) return false;
			if (cjifen != _o_.cjifen) return false;
			if (hjifen != _o_.hjifen) return false;
			if (qjifen != _o_.qjifen) return false;
			if (isbufftime != _o_.isbufftime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += gongxun;
		_h_ += jifen;
		_h_ += rank;
		_h_ += (int)endtime;
		_h_ += mycamp;
		_h_ += cjifen;
		_h_ += hjifen;
		_h_ += qjifen;
		_h_ += (int)isbufftime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(gongxun).append(",");
		_sb_.append(jifen).append(",");
		_sb_.append(rank).append(",");
		_sb_.append(endtime).append(",");
		_sb_.append(mycamp).append(",");
		_sb_.append(cjifen).append(",");
		_sb_.append(hjifen).append(",");
		_sb_.append(qjifen).append(",");
		_sb_.append(isbufftime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SMainView _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = gongxun - _o_.gongxun;
		if (0 != _c_) return _c_;
		_c_ = jifen - _o_.jifen;
		if (0 != _c_) return _c_;
		_c_ = rank - _o_.rank;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(endtime - _o_.endtime);
		if (0 != _c_) return _c_;
		_c_ = mycamp - _o_.mycamp;
		if (0 != _c_) return _c_;
		_c_ = cjifen - _o_.cjifen;
		if (0 != _c_) return _c_;
		_c_ = hjifen - _o_.hjifen;
		if (0 != _c_) return _c_;
		_c_ = qjifen - _o_.qjifen;
		if (0 != _c_) return _c_;
		_c_ = isbufftime - _o_.isbufftime;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

