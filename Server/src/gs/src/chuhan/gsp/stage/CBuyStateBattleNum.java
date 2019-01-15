
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CBuyStateBattleNum__ extends xio.Protocol { }

/** 购买关卡或扫荡 by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CBuyStateBattleNum extends __CBuyStateBattleNum__ {
	@Override
	protected void process() {
		// protocol handle
		long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		if(roleId <= 0)
			return;
		new PBuyStateBattleNum(roleId, buytype,battleid).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787948;

	public int getType() {
		return 787948;
	}

	public int buytype; // 购买类型：1为扫荡，2为关卡（需要关卡id）
	public int battleid; // 关卡ID

	public CBuyStateBattleNum() {
	}

	public CBuyStateBattleNum(int _buytype_, int _battleid_) {
		this.buytype = _buytype_;
		this.battleid = _battleid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(buytype);
		_os_.marshal(battleid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		buytype = _os_.unmarshal_int();
		battleid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CBuyStateBattleNum) {
			CBuyStateBattleNum _o_ = (CBuyStateBattleNum)_o1_;
			if (buytype != _o_.buytype) return false;
			if (battleid != _o_.battleid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += buytype;
		_h_ += battleid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(buytype).append(",");
		_sb_.append(battleid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CBuyStateBattleNum _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = buytype - _o_.buytype;
		if (0 != _c_) return _c_;
		_c_ = battleid - _o_.battleid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

