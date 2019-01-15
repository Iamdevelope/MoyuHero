
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSwitchEquip__ extends xio.Protocol { }

/** 换装备
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSwitchEquip extends __CSwitchEquip__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PSwitchEquip(roleId, trooppos, itemkey).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787740;

	public int getType() {
		return 787740;
	}

	public byte trooppos; // 从1开始
	public int itemkey; // 在包裹中的key

	public CSwitchEquip() {
	}

	public CSwitchEquip(byte _trooppos_, int _itemkey_) {
		this.trooppos = _trooppos_;
		this.itemkey = _itemkey_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(trooppos);
		_os_.marshal(itemkey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		trooppos = _os_.unmarshal_byte();
		itemkey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSwitchEquip) {
			CSwitchEquip _o_ = (CSwitchEquip)_o1_;
			if (trooppos != _o_.trooppos) return false;
			if (itemkey != _o_.itemkey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)trooppos;
		_h_ += itemkey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(trooppos).append(",");
		_sb_.append(itemkey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CSwitchEquip _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = trooppos - _o_.trooppos;
		if (0 != _c_) return _c_;
		_c_ = itemkey - _o_.itemkey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

