
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SGetMyWordBoss__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SGetMyWordBoss extends __SGetMyWordBoss__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788886;

	public int getType() {
		return 788886;
	}

	public int bossid; // 值为1234，代表第几个boss
	public chuhan.gsp.play.wordboss.bossrole mywordboss;

	public SGetMyWordBoss() {
		mywordboss = new chuhan.gsp.play.wordboss.bossrole();
	}

	public SGetMyWordBoss(int _bossid_, chuhan.gsp.play.wordboss.bossrole _mywordboss_) {
		this.bossid = _bossid_;
		this.mywordboss = _mywordboss_;
	}

	public final boolean _validator_() {
		if (!mywordboss._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bossid);
		_os_.marshal(mywordboss);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bossid = _os_.unmarshal_int();
		mywordboss.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SGetMyWordBoss) {
			SGetMyWordBoss _o_ = (SGetMyWordBoss)_o1_;
			if (bossid != _o_.bossid) return false;
			if (!mywordboss.equals(_o_.mywordboss)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += bossid;
		_h_ += mywordboss.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bossid).append(",");
		_sb_.append(mywordboss).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SGetMyWordBoss _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = bossid - _o_.bossid;
		if (0 != _c_) return _c_;
		_c_ = mywordboss.compareTo(_o_.mywordboss);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

