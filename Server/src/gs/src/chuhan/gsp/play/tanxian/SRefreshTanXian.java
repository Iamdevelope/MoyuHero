
package chuhan.gsp.play.tanxian;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshTanXian__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshTanXian extends __SRefreshTanXian__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788983;

	public int getType() {
		return 788983;
	}

	public chuhan.gsp.play.tanxian.stagetxall tanxianinfo;

	public SRefreshTanXian() {
		tanxianinfo = new chuhan.gsp.play.tanxian.stagetxall();
	}

	public SRefreshTanXian(chuhan.gsp.play.tanxian.stagetxall _tanxianinfo_) {
		this.tanxianinfo = _tanxianinfo_;
	}

	public final boolean _validator_() {
		if (!tanxianinfo._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(tanxianinfo);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		tanxianinfo.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshTanXian) {
			SRefreshTanXian _o_ = (SRefreshTanXian)_o1_;
			if (!tanxianinfo.equals(_o_.tanxianinfo)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += tanxianinfo.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(tanxianinfo).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

