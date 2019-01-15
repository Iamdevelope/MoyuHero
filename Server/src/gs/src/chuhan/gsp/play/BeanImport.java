
package chuhan.gsp.play;

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
	public static final int PROTOCOL_TYPE = 788333;

	public int getType() {
		return 788333;
	}

	public chuhan.gsp.play.camp.CampType b1;
	public chuhan.gsp.play.PlayType b2;

	public BeanImport() {
		b1 = new chuhan.gsp.play.camp.CampType();
		b2 = new chuhan.gsp.play.PlayType();
	}

	public BeanImport(chuhan.gsp.play.camp.CampType _b1_, chuhan.gsp.play.PlayType _b2_) {
		this.b1 = _b1_;
		this.b2 = _b2_;
	}

	public final boolean _validator_() {
		if (!b1._validator_()) return false;
		if (!b2._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(b1);
		_os_.marshal(b2);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		b1.unmarshal(_os_);
		b2.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BeanImport) {
			BeanImport _o_ = (BeanImport)_o1_;
			if (!b1.equals(_o_.b1)) return false;
			if (!b2.equals(_o_.b2)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += b1.hashCode();
		_h_ += b2.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(b1).append(",");
		_sb_.append(b2).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(BeanImport _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = b1.compareTo(_o_.b1);
		if (0 != _c_) return _c_;
		_c_ = b2.compareTo(_o_.b2);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

