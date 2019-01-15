
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SBeginBattle__ extends xio.Protocol { }

/** 开始关卡返回 by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SBeginBattle extends __SBeginBattle__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787854;

	public int getType() {
		return 787854;
	}

	public chuhan.gsp.battle.BattleInfo battleinfo;

	public SBeginBattle() {
		battleinfo = new chuhan.gsp.battle.BattleInfo();
	}

	public SBeginBattle(chuhan.gsp.battle.BattleInfo _battleinfo_) {
		this.battleinfo = _battleinfo_;
	}

	public final boolean _validator_() {
		if (!battleinfo._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(battleinfo);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		battleinfo.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SBeginBattle) {
			SBeginBattle _o_ = (SBeginBattle)_o1_;
			if (!battleinfo.equals(_o_.battleinfo)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += battleinfo.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleinfo).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SBeginBattle _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = battleinfo.compareTo(_o_.battleinfo);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

