
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CHeroEquipUp__ extends xio.Protocol { }

/** 英雄装备强化和升级
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CHeroEquipUp extends __CHeroEquipUp__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PHeroEquipUp(roleId, herokey,equiplocation,islevelup,isstrength).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787790;

	public int getType() {
		return 787790;
	}

	public int herokey; // 英雄key
	public int equiplocation; // 装备位置 从1开始
	public int islevelup; // 是否是升级，0为否（强化），1为升级
	public int isstrength; // 是否一键强化，0为否，1为是

	public CHeroEquipUp() {
	}

	public CHeroEquipUp(int _herokey_, int _equiplocation_, int _islevelup_, int _isstrength_) {
		this.herokey = _herokey_;
		this.equiplocation = _equiplocation_;
		this.islevelup = _islevelup_;
		this.isstrength = _isstrength_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.marshal(equiplocation);
		_os_.marshal(islevelup);
		_os_.marshal(isstrength);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		equiplocation = _os_.unmarshal_int();
		islevelup = _os_.unmarshal_int();
		isstrength = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CHeroEquipUp) {
			CHeroEquipUp _o_ = (CHeroEquipUp)_o1_;
			if (herokey != _o_.herokey) return false;
			if (equiplocation != _o_.equiplocation) return false;
			if (islevelup != _o_.islevelup) return false;
			if (isstrength != _o_.isstrength) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += equiplocation;
		_h_ += islevelup;
		_h_ += isstrength;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(equiplocation).append(",");
		_sb_.append(islevelup).append(",");
		_sb_.append(isstrength).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CHeroEquipUp _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		_c_ = equiplocation - _o_.equiplocation;
		if (0 != _c_) return _c_;
		_c_ = islevelup - _o_.islevelup;
		if (0 != _c_) return _c_;
		_c_ = isstrength - _o_.isstrength;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

