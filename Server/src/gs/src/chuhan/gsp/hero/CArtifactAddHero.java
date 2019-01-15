
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CArtifactAddHero__ extends xio.Protocol { }

/** 神器注入英雄
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CArtifactAddHero extends __CArtifactAddHero__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PArtifactAddHero(roleId, this.artifacttype, this.herokeylist).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787772;

	public int getType() {
		return 787772;
	}

	public int artifacttype; // 神器类型（key）
	public java.util.LinkedList<Integer> herokeylist;

	public CArtifactAddHero() {
		herokeylist = new java.util.LinkedList<Integer>();
	}

	public CArtifactAddHero(int _artifacttype_, java.util.LinkedList<Integer> _herokeylist_) {
		this.artifacttype = _artifacttype_;
		this.herokeylist = _herokeylist_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(artifacttype);
		_os_.compact_uint32(herokeylist.size());
		for (Integer _v_ : herokeylist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		artifacttype = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			herokeylist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CArtifactAddHero) {
			CArtifactAddHero _o_ = (CArtifactAddHero)_o1_;
			if (artifacttype != _o_.artifacttype) return false;
			if (!herokeylist.equals(_o_.herokeylist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += artifacttype;
		_h_ += herokeylist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(artifacttype).append(",");
		_sb_.append(herokeylist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

