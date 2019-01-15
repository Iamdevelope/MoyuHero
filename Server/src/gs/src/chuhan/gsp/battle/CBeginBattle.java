
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CBeginBattle__ extends xio.Protocol { }

/** 开始关卡 by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CBeginBattle extends __CBeginBattle__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PBeginBattle(roleId, battleid,troopid).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787853;

	public int getType() {
		return 787853;
	}

	public int battleid; // 关卡ID
	public int troopid; // 战队ID

	public CBeginBattle() {
	}

	public CBeginBattle(int _battleid_, int _troopid_) {
		this.battleid = _battleid_;
		this.troopid = _troopid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(battleid);
		_os_.marshal(troopid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		battleid = _os_.unmarshal_int();
		troopid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CBeginBattle) {
			CBeginBattle _o_ = (CBeginBattle)_o1_;
			if (battleid != _o_.battleid) return false;
			if (troopid != _o_.troopid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += battleid;
		_h_ += troopid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleid).append(",");
		_sb_.append(troopid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CBeginBattle _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = battleid - _o_.battleid;
		if (0 != _c_) return _c_;
		_c_ = troopid - _o_.troopid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

