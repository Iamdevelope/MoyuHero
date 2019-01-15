
package chuhan.gsp.play.turntable;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CTurnTable__ extends xio.Protocol { }

/** 转(抽奖)
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CTurnTable extends __CTurnTable__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788437;

	public int getType() {
		return 788437;
	}

	public byte turntype; // 抽奖类型(1-普通 2-精品)

	public CTurnTable() {
	}

	public CTurnTable(byte _turntype_) {
		this.turntype = _turntype_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(turntype);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		turntype = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CTurnTable) {
			CTurnTable _o_ = (CTurnTable)_o1_;
			if (turntype != _o_.turntype) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)turntype;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(turntype).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CTurnTable _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = turntype - _o_.turntype;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

