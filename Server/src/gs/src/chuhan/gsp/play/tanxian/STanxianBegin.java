
package chuhan.gsp.play.tanxian;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __STanxianBegin__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class STanxianBegin extends __STanxianBegin__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788985;

	public int getType() {
		return 788985;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public java.util.LinkedList<Integer> team; // 小队英雄key列表
	public int tanxianid; // 探险id
	public long endtime; // 结束时间

	public STanxianBegin() {
		team = new java.util.LinkedList<Integer>();
	}

	public STanxianBegin(int _result_, java.util.LinkedList<Integer> _team_, int _tanxianid_, long _endtime_) {
		this.result = _result_;
		this.team = _team_;
		this.tanxianid = _tanxianid_;
		this.endtime = _endtime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.compact_uint32(team.size());
		for (Integer _v_ : team) {
			_os_.marshal(_v_);
		}
		_os_.marshal(tanxianid);
		_os_.marshal(endtime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			team.add(_v_);
		}
		tanxianid = _os_.unmarshal_int();
		endtime = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof STanxianBegin) {
			STanxianBegin _o_ = (STanxianBegin)_o1_;
			if (result != _o_.result) return false;
			if (!team.equals(_o_.team)) return false;
			if (tanxianid != _o_.tanxianid) return false;
			if (endtime != _o_.endtime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += team.hashCode();
		_h_ += tanxianid;
		_h_ += (int)endtime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(team).append(",");
		_sb_.append(tanxianid).append(",");
		_sb_.append(endtime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

