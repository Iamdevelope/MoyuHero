
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SArtifactLevelUp__ extends xio.Protocol { }

/** 神器升级通知
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SArtifactLevelUp extends __SArtifactLevelUp__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787785;

	public int getType() {
		return 787785;
	}

	public int artifacttype; // 神器类型（key）
	public int artifactid; // 神器ID

	public SArtifactLevelUp() {
	}

	public SArtifactLevelUp(int _artifacttype_, int _artifactid_) {
		this.artifacttype = _artifacttype_;
		this.artifactid = _artifactid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(artifacttype);
		_os_.marshal(artifactid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		artifacttype = _os_.unmarshal_int();
		artifactid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SArtifactLevelUp) {
			SArtifactLevelUp _o_ = (SArtifactLevelUp)_o1_;
			if (artifacttype != _o_.artifacttype) return false;
			if (artifactid != _o_.artifactid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += artifacttype;
		_h_ += artifactid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(artifacttype).append(",");
		_sb_.append(artifactid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SArtifactLevelUp _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = artifacttype - _o_.artifacttype;
		if (0 != _c_) return _c_;
		_c_ = artifactid - _o_.artifactid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

