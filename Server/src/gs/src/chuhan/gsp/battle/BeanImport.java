
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __BeanImport__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class BeanImport extends __BeanImport__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787833;

	public int getType() {
		return 787833;
	}

	public chuhan.gsp.battle.ResultType resulttype; // 1~10
	public chuhan.gsp.battle.BattleType battletype;

	public BeanImport() {
		resulttype = new chuhan.gsp.battle.ResultType();
		battletype = new chuhan.gsp.battle.BattleType();
	}

	public BeanImport(chuhan.gsp.battle.ResultType _resulttype_, chuhan.gsp.battle.BattleType _battletype_) {
		this.resulttype = _resulttype_;
		this.battletype = _battletype_;
	}

	public final boolean _validator_() {
		if (!resulttype._validator_()) return false;
		if (!battletype._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(resulttype);
		_os_.marshal(battletype);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		resulttype.unmarshal(_os_);
		battletype.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BeanImport) {
			BeanImport _o_ = (BeanImport)_o1_;
			if (!resulttype.equals(_o_.resulttype)) return false;
			if (!battletype.equals(_o_.battletype)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += resulttype.hashCode();
		_h_ += battletype.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(resulttype).append(",");
		_sb_.append(battletype).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(BeanImport _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = resulttype.compareTo(_o_.resulttype);
		if (0 != _c_) return _c_;
		_c_ = battletype.compareTo(_o_.battletype);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

