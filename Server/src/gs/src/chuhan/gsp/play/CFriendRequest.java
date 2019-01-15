
package chuhan.gsp.play;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CFriendRequest__ extends xio.Protocol { }

/** 向助战的陌生人发送好友邀请
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CFriendRequest extends __CFriendRequest__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788339;

	public int getType() {
		return 788339;
	}

	public byte isadd; // 是否添加陌生人 0-否 1-是
	public byte playtype;

	public CFriendRequest() {
	}

	public CFriendRequest(byte _isadd_, byte _playtype_) {
		this.isadd = _isadd_;
		this.playtype = _playtype_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(isadd);
		_os_.marshal(playtype);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		isadd = _os_.unmarshal_byte();
		playtype = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CFriendRequest) {
			CFriendRequest _o_ = (CFriendRequest)_o1_;
			if (isadd != _o_.isadd) return false;
			if (playtype != _o_.playtype) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)isadd;
		_h_ += (int)playtype;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(isadd).append(",");
		_sb_.append(playtype).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CFriendRequest _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = isadd - _o_.isadd;
		if (0 != _c_) return _c_;
		_c_ = playtype - _o_.playtype;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

