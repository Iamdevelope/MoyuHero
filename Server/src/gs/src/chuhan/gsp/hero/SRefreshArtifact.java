
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshArtifact__ extends xio.Protocol { }

/** 刷新神器属性
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshArtifact extends __SRefreshArtifact__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787774;

	public int getType() {
		return 787774;
	}

	public chuhan.gsp.Artifact artifactinfo;

	public SRefreshArtifact() {
		artifactinfo = new chuhan.gsp.Artifact();
	}

	public SRefreshArtifact(chuhan.gsp.Artifact _artifactinfo_) {
		this.artifactinfo = _artifactinfo_;
	}

	public final boolean _validator_() {
		if (!artifactinfo._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(artifactinfo);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		artifactinfo.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshArtifact) {
			SRefreshArtifact _o_ = (SRefreshArtifact)_o1_;
			if (!artifactinfo.equals(_o_.artifactinfo)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += artifactinfo.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(artifactinfo).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshArtifact _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = artifactinfo.compareTo(_o_.artifactinfo);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

