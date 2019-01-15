
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SConsumeHero__ extends xio.Protocol { }

/** 祭天结果
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SConsumeHero extends __SConsumeHero__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787743;

	public int getType() {
		return 787743;
	}

	public int addsoul;

	public SConsumeHero() {
	}

	public SConsumeHero(int _addsoul_) {
		this.addsoul = _addsoul_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(addsoul);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		addsoul = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SConsumeHero) {
			SConsumeHero _o_ = (SConsumeHero)_o1_;
			if (addsoul != _o_.addsoul) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += addsoul;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(addsoul).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SConsumeHero _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = addsoul - _o_.addsoul;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

