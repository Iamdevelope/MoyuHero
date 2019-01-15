
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SReplyXiulianResult__ extends xio.Protocol { }

/** 返回修炼结果
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SReplyXiulianResult extends __SReplyXiulianResult__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787749;

	public int getType() {
		return 787749;
	}

	public byte hp;
	public byte attack;
	public byte defend;
	public byte wisdom;

	public SReplyXiulianResult() {
	}

	public SReplyXiulianResult(byte _hp_, byte _attack_, byte _defend_, byte _wisdom_) {
		this.hp = _hp_;
		this.attack = _attack_;
		this.defend = _defend_;
		this.wisdom = _wisdom_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(hp);
		_os_.marshal(attack);
		_os_.marshal(defend);
		_os_.marshal(wisdom);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		hp = _os_.unmarshal_byte();
		attack = _os_.unmarshal_byte();
		defend = _os_.unmarshal_byte();
		wisdom = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SReplyXiulianResult) {
			SReplyXiulianResult _o_ = (SReplyXiulianResult)_o1_;
			if (hp != _o_.hp) return false;
			if (attack != _o_.attack) return false;
			if (defend != _o_.defend) return false;
			if (wisdom != _o_.wisdom) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)hp;
		_h_ += (int)attack;
		_h_ += (int)defend;
		_h_ += (int)wisdom;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(hp).append(",");
		_sb_.append(attack).append(",");
		_sb_.append(defend).append(",");
		_sb_.append(wisdom).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SReplyXiulianResult _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = hp - _o_.hp;
		if (0 != _c_) return _c_;
		_c_ = attack - _o_.attack;
		if (0 != _c_) return _c_;
		_c_ = defend - _o_.defend;
		if (0 != _c_) return _c_;
		_c_ = wisdom - _o_.wisdom;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

