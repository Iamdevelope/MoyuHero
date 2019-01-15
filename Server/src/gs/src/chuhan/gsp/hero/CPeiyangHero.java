
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CPeiyangHero__ extends xio.Protocol { }

/** 培养英雄
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CPeiyangHero extends __CPeiyangHero__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PPeiYangHero(roleId, this.herokey, this.slotnum,this.isreset).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787779;

	public int getType() {
		return 787779;
	}

	public int herokey; // 英雄key
	public byte slotnum; // 培养位置
	public byte isreset; // 是否重置（0为非重置，1为重置）

	public CPeiyangHero() {
	}

	public CPeiyangHero(int _herokey_, byte _slotnum_, byte _isreset_) {
		this.herokey = _herokey_;
		this.slotnum = _slotnum_;
		this.isreset = _isreset_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.marshal(slotnum);
		_os_.marshal(isreset);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		slotnum = _os_.unmarshal_byte();
		isreset = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CPeiyangHero) {
			CPeiyangHero _o_ = (CPeiyangHero)_o1_;
			if (herokey != _o_.herokey) return false;
			if (slotnum != _o_.slotnum) return false;
			if (isreset != _o_.isreset) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += (int)slotnum;
		_h_ += (int)isreset;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(slotnum).append(",");
		_sb_.append(isreset).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CPeiyangHero _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		_c_ = slotnum - _o_.slotnum;
		if (0 != _c_) return _c_;
		_c_ = isreset - _o_.isreset;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

