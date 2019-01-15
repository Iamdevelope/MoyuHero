
package chuhan.gsp.battle.realtime;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SAttackOther__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SAttackOther extends __SAttackOther__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787890;

	public int getType() {
		return 787890;
	}

	public int attackkey;
	public java.util.LinkedList<chuhan.gsp.battle.realtime.BHeroAttack> bherotypelist;
	public int iswin; // 是否赢了 0未赢，1赢了

	public SAttackOther() {
		bherotypelist = new java.util.LinkedList<chuhan.gsp.battle.realtime.BHeroAttack>();
	}

	public SAttackOther(int _attackkey_, java.util.LinkedList<chuhan.gsp.battle.realtime.BHeroAttack> _bherotypelist_, int _iswin_) {
		this.attackkey = _attackkey_;
		this.bherotypelist = _bherotypelist_;
		this.iswin = _iswin_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.battle.realtime.BHeroAttack _v_ : bherotypelist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(attackkey);
		_os_.compact_uint32(bherotypelist.size());
		for (chuhan.gsp.battle.realtime.BHeroAttack _v_ : bherotypelist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(iswin);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		attackkey = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.realtime.BHeroAttack _v_ = new chuhan.gsp.battle.realtime.BHeroAttack();
			_v_.unmarshal(_os_);
			bherotypelist.add(_v_);
		}
		iswin = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SAttackOther) {
			SAttackOther _o_ = (SAttackOther)_o1_;
			if (attackkey != _o_.attackkey) return false;
			if (!bherotypelist.equals(_o_.bherotypelist)) return false;
			if (iswin != _o_.iswin) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += attackkey;
		_h_ += bherotypelist.hashCode();
		_h_ += iswin;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(attackkey).append(",");
		_sb_.append(bherotypelist).append(",");
		_sb_.append(iswin).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

