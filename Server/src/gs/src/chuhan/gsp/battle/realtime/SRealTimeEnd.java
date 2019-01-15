
package chuhan.gsp.battle.realtime;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRealTimeEnd__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRealTimeEnd extends xio.Protocol {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787895;

	public int getType() {
		return 787895;
	}

	public final static int WIN = 1; // 胜利
	public final static int FAIL = 0; // 失败

	public int endtype;

	public SRealTimeEnd() {
	}

	public SRealTimeEnd(int _endtype_) {
		this.endtype = _endtype_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(endtype);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		endtype = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRealTimeEnd) {
			SRealTimeEnd _o_ = (SRealTimeEnd)_o1_;
			if (endtype != _o_.endtype) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += endtype;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(endtype).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRealTimeEnd _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = endtype - _o_.endtype;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

