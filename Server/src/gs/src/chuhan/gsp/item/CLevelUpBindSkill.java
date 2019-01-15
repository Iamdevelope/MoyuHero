
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CLevelUpBindSkill__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CLevelUpBindSkill extends __CLevelUpBindSkill__ {
	@Override
	protected void process() {
		// protocol handle
		long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PBindSkillLevelUp(roleId, herokey, consumeitemkeys).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787557;

	public int getType() {
		return 787557;
	}

	public int herokey;
	public java.util.LinkedList<Integer> consumeitemkeys;

	public CLevelUpBindSkill() {
		consumeitemkeys = new java.util.LinkedList<Integer>();
	}

	public CLevelUpBindSkill(int _herokey_, java.util.LinkedList<Integer> _consumeitemkeys_) {
		this.herokey = _herokey_;
		this.consumeitemkeys = _consumeitemkeys_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.compact_uint32(consumeitemkeys.size());
		for (Integer _v_ : consumeitemkeys) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			consumeitemkeys.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CLevelUpBindSkill) {
			CLevelUpBindSkill _o_ = (CLevelUpBindSkill)_o1_;
			if (herokey != _o_.herokey) return false;
			if (!consumeitemkeys.equals(_o_.consumeitemkeys)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += consumeitemkeys.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(consumeitemkeys).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

