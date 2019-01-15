
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CHeroJinjie__ extends xio.Protocol { }

/** 英雄进阶
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CHeroJinjie extends __CHeroJinjie__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PHeroJinjie(roleId, herokey).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787786;

	public int getType() {
		return 787786;
	}

	public int herokey; // 英雄key

	public CHeroJinjie() {
	}

	public CHeroJinjie(int _herokey_) {
		this.herokey = _herokey_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CHeroJinjie) {
			CHeroJinjie _o_ = (CHeroJinjie)_o1_;
			if (herokey != _o_.herokey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CHeroJinjie _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

