
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CConsumeHero__ extends xio.Protocol { }

/** 祭天
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CConsumeHero extends __CConsumeHero__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PConsumeHero(roleId, herokeys, (heroorsoul == 1)).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787742;

	public int getType() {
		return 787742;
	}

	public byte heroorsoul; // 1:hero,0:soul
	public java.util.LinkedList<Integer> herokeys; // 在包裹中的key

	public CConsumeHero() {
		herokeys = new java.util.LinkedList<Integer>();
	}

	public CConsumeHero(byte _heroorsoul_, java.util.LinkedList<Integer> _herokeys_) {
		this.heroorsoul = _heroorsoul_;
		this.herokeys = _herokeys_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(heroorsoul);
		_os_.compact_uint32(herokeys.size());
		for (Integer _v_ : herokeys) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		heroorsoul = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			herokeys.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CConsumeHero) {
			CConsumeHero _o_ = (CConsumeHero)_o1_;
			if (heroorsoul != _o_.heroorsoul) return false;
			if (!herokeys.equals(_o_.herokeys)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)heroorsoul;
		_h_ += herokeys.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroorsoul).append(",");
		_sb_.append(herokeys).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

