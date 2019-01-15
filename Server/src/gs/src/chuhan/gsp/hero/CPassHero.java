
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CPassHero__ extends xio.Protocol { }

/** 传功
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CPassHero extends __CPassHero__ {
	@Override
	protected void process() {
		/*final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PPassHero(roleId, herokey, costherokey).submit();*/
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787744;

	public int getType() {
		return 787744;
	}

	public int herokey; // 在包裹中的key
	public int costherokey; // 消耗的武将

	public CPassHero() {
	}

	public CPassHero(int _herokey_, int _costherokey_) {
		this.herokey = _herokey_;
		this.costherokey = _costherokey_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.marshal(costherokey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		costherokey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CPassHero) {
			CPassHero _o_ = (CPassHero)_o1_;
			if (herokey != _o_.herokey) return false;
			if (costherokey != _o_.costherokey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += costherokey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(costherokey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CPassHero _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		_c_ = costherokey - _o_.costherokey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

