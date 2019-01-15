
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSwitchSkill__ extends xio.Protocol { }

/** 换技能
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSwitchSkill extends __CSwitchSkill__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PSwitchSkill(roleId, trooppos, skillpos, itemkey).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787741;

	public int getType() {
		return 787741;
	}

	public byte trooppos; // 从1开始
	public byte skillpos; // 从1开始
	public int itemkey; // 在包裹中的key

	public CSwitchSkill() {
	}

	public CSwitchSkill(byte _trooppos_, byte _skillpos_, int _itemkey_) {
		this.trooppos = _trooppos_;
		this.skillpos = _skillpos_;
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
		_os_.marshal(skillpos);
		_os_.marshal(itemkey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		trooppos = _os_.unmarshal_byte();
		skillpos = _os_.unmarshal_byte();
		itemkey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSwitchSkill) {
			CSwitchSkill _o_ = (CSwitchSkill)_o1_;
			if (trooppos != _o_.trooppos) return false;
			if (skillpos != _o_.skillpos) return false;
			if (itemkey != _o_.itemkey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)trooppos;
		_h_ += (int)skillpos;
		_h_ += itemkey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(trooppos).append(",");
		_sb_.append(skillpos).append(",");
		_sb_.append(itemkey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CSwitchSkill _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = trooppos - _o_.trooppos;
		if (0 != _c_) return _c_;
		_c_ = skillpos - _o_.skillpos;
		if (0 != _c_) return _c_;
		_c_ = itemkey - _o_.itemkey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

