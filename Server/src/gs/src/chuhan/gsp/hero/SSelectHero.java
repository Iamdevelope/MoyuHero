
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSelectHero__ extends xio.Protocol { }

/** 点将结果
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSelectHero extends __SSelectHero__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787752;

	public int getType() {
		return 787752;
	}

	public int herokey; // 选到的武将
	public long nextfreetime; // 下次免费时间
	public byte todaytimes; // 为-1表示刷新金牌的时间，为-2刷新银牌时间，为[0,5]则刷新铜牌时间和已免费领取次数

	public SSelectHero() {
	}

	public SSelectHero(int _herokey_, long _nextfreetime_, byte _todaytimes_) {
		this.herokey = _herokey_;
		this.nextfreetime = _nextfreetime_;
		this.todaytimes = _todaytimes_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.marshal(nextfreetime);
		_os_.marshal(todaytimes);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		nextfreetime = _os_.unmarshal_long();
		todaytimes = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSelectHero) {
			SSelectHero _o_ = (SSelectHero)_o1_;
			if (herokey != _o_.herokey) return false;
			if (nextfreetime != _o_.nextfreetime) return false;
			if (todaytimes != _o_.todaytimes) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += (int)nextfreetime;
		_h_ += (int)todaytimes;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(nextfreetime).append(",");
		_sb_.append(todaytimes).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SSelectHero _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(nextfreetime - _o_.nextfreetime);
		if (0 != _c_) return _c_;
		_c_ = todaytimes - _o_.todaytimes;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

