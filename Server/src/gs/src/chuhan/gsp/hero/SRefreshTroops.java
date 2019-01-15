
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshTroops__ extends xio.Protocol { }

/** 刷新整个阵容
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshTroops extends __SRefreshTroops__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787734;

	public int getType() {
		return 787734;
	}

	public java.util.LinkedList<chuhan.gsp.Troop> troops;

	public SRefreshTroops() {
		troops = new java.util.LinkedList<chuhan.gsp.Troop>();
	}

	public SRefreshTroops(java.util.LinkedList<chuhan.gsp.Troop> _troops_) {
		this.troops = _troops_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.Troop _v_ : troops)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(troops.size());
		for (chuhan.gsp.Troop _v_ : troops) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.Troop _v_ = new chuhan.gsp.Troop();
			_v_.unmarshal(_os_);
			troops.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshTroops) {
			SRefreshTroops _o_ = (SRefreshTroops)_o1_;
			if (!troops.equals(_o_.troops)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += troops.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(troops).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

