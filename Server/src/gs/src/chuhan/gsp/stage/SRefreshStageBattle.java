
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshStageBattle__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshStageBattle extends __SRefreshStageBattle__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787939;

	public int getType() {
		return 787939;
	}

	public int stageid; // 章节id
	public chuhan.gsp.stage.StageBattle stagebattle;

	public SRefreshStageBattle() {
		stagebattle = new chuhan.gsp.stage.StageBattle();
	}

	public SRefreshStageBattle(int _stageid_, chuhan.gsp.stage.StageBattle _stagebattle_) {
		this.stageid = _stageid_;
		this.stagebattle = _stagebattle_;
	}

	public final boolean _validator_() {
		if (!stagebattle._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(stageid);
		_os_.marshal(stagebattle);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		stageid = _os_.unmarshal_int();
		stagebattle.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshStageBattle) {
			SRefreshStageBattle _o_ = (SRefreshStageBattle)_o1_;
			if (stageid != _o_.stageid) return false;
			if (!stagebattle.equals(_o_.stagebattle)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += stageid;
		_h_ += stagebattle.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(stageid).append(",");
		_sb_.append(stagebattle).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshStageBattle _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = stageid - _o_.stageid;
		if (0 != _c_) return _c_;
		_c_ = stagebattle.compareTo(_o_.stagebattle);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

