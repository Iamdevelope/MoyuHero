
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CHeroLevelUpSpeed__ extends xio.Protocol { }

/** 英雄快速升级
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CHeroLevelUpSpeed extends __CHeroLevelUpSpeed__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PHeroLevelUpSpeed(roleId, this.herokey, this.levelnum,itemid,itemnum).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787783;

	public int getType() {
		return 787783;
	}

	public int herokey; // 英雄key
	public byte levelnum; // 升级数（根据策划案为1级和5级）填写0即为使用物品
	public int itemid; // 物品配表ID
	public int itemnum; // 物品使用数量

	public CHeroLevelUpSpeed() {
	}

	public CHeroLevelUpSpeed(int _herokey_, byte _levelnum_, int _itemid_, int _itemnum_) {
		this.herokey = _herokey_;
		this.levelnum = _levelnum_;
		this.itemid = _itemid_;
		this.itemnum = _itemnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.marshal(levelnum);
		_os_.marshal(itemid);
		_os_.marshal(itemnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		levelnum = _os_.unmarshal_byte();
		itemid = _os_.unmarshal_int();
		itemnum = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CHeroLevelUpSpeed) {
			CHeroLevelUpSpeed _o_ = (CHeroLevelUpSpeed)_o1_;
			if (herokey != _o_.herokey) return false;
			if (levelnum != _o_.levelnum) return false;
			if (itemid != _o_.itemid) return false;
			if (itemnum != _o_.itemnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += (int)levelnum;
		_h_ += itemid;
		_h_ += itemnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(levelnum).append(",");
		_sb_.append(itemid).append(",");
		_sb_.append(itemnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CHeroLevelUpSpeed _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		_c_ = levelnum - _o_.levelnum;
		if (0 != _c_) return _c_;
		_c_ = itemid - _o_.itemid;
		if (0 != _c_) return _c_;
		_c_ = itemnum - _o_.itemnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

