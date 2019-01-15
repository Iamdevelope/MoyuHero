
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CAddTroop__ extends xio.Protocol { }

/** 英雄出阵
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CAddTroop extends __CAddTroop__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PAddTroop(roleId,herokey,locationid).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787737;

	public int getType() {
		return 787737;
	}

	public int trooptype; // 阵型类型
	public int herokey;
	public int troopid;
	public int locationid;

	public CAddTroop() {
	}

	public CAddTroop(int _trooptype_, int _herokey_, int _troopid_, int _locationid_) {
		this.trooptype = _trooptype_;
		this.herokey = _herokey_;
		this.troopid = _troopid_;
		this.locationid = _locationid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(trooptype);
		_os_.marshal(herokey);
		_os_.marshal(troopid);
		_os_.marshal(locationid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		trooptype = _os_.unmarshal_int();
		herokey = _os_.unmarshal_int();
		troopid = _os_.unmarshal_int();
		locationid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CAddTroop) {
			CAddTroop _o_ = (CAddTroop)_o1_;
			if (trooptype != _o_.trooptype) return false;
			if (herokey != _o_.herokey) return false;
			if (troopid != _o_.troopid) return false;
			if (locationid != _o_.locationid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += trooptype;
		_h_ += herokey;
		_h_ += troopid;
		_h_ += locationid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(trooptype).append(",");
		_sb_.append(herokey).append(",");
		_sb_.append(troopid).append(",");
		_sb_.append(locationid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CAddTroop _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = trooptype - _o_.trooptype;
		if (0 != _c_) return _c_;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		_c_ = troopid - _o_.troopid;
		if (0 != _c_) return _c_;
		_c_ = locationid - _o_.locationid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

