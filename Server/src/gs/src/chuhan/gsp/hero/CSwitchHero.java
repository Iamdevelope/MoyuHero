
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSwitchHero__ extends xio.Protocol { }

/** 更换英雄
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSwitchHero extends __CSwitchHero__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PSwitchHero(roleId, trooppos, herokey).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787738;

	public int getType() {
		return 787738;
	}

	public byte trooppos; // 从1开始
	public int herokey; // 英雄栏里的key，不能是已出阵的英雄

	public CSwitchHero() {
	}

	public CSwitchHero(byte _trooppos_, int _herokey_) {
		this.trooppos = _trooppos_;
		this.herokey = _herokey_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(trooppos);
		_os_.marshal(herokey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		trooppos = _os_.unmarshal_byte();
		herokey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSwitchHero) {
			CSwitchHero _o_ = (CSwitchHero)_o1_;
			if (trooppos != _o_.trooppos) return false;
			if (herokey != _o_.herokey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)trooppos;
		_h_ += herokey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(trooppos).append(",");
		_sb_.append(herokey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CSwitchHero _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = trooppos - _o_.trooppos;
		if (0 != _c_) return _c_;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

