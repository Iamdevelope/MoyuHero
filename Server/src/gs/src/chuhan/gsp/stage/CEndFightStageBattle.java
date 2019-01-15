
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CEndFightStageBattle__ extends xio.Protocol { }

/** 通关返回C by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CEndFightStageBattle extends __CEndFightStageBattle__ {
	@Override
	protected void process() {
		// protocol handle
		long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		if(roleId <= 0)
			return;
		new PEndFightStageBattle(roleId, pass).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787942;

	public int getType() {
		return 787942;
	}

	public int pass; // 0未通过，1通过1，2通过2，3全通

	public CEndFightStageBattle() {
	}

	public CEndFightStageBattle(int _pass_) {
		this.pass = _pass_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(pass);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		pass = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CEndFightStageBattle) {
			CEndFightStageBattle _o_ = (CEndFightStageBattle)_o1_;
			if (pass != _o_.pass) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += pass;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(pass).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CEndFightStageBattle _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = pass - _o_.pass;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

