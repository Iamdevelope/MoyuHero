
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSettingView__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSettingView extends __SSettingView__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786447;

	public int getType() {
		return 786447;
	}

	public long invitename; // 邀请人 0-没有
	public byte isreward; // 是否可以领奖
	public short invitenum; // 邀请人数量

	public SSettingView() {
	}

	public SSettingView(long _invitename_, byte _isreward_, short _invitenum_) {
		this.invitename = _invitename_;
		this.isreward = _isreward_;
		this.invitenum = _invitenum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(invitename);
		_os_.marshal(isreward);
		_os_.marshal(invitenum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		invitename = _os_.unmarshal_long();
		isreward = _os_.unmarshal_byte();
		invitenum = _os_.unmarshal_short();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSettingView) {
			SSettingView _o_ = (SSettingView)_o1_;
			if (invitename != _o_.invitename) return false;
			if (isreward != _o_.isreward) return false;
			if (invitenum != _o_.invitenum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)invitename;
		_h_ += (int)isreward;
		_h_ += invitenum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(invitename).append(",");
		_sb_.append(isreward).append(",");
		_sb_.append(invitenum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SSettingView _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(invitename - _o_.invitename);
		if (0 != _c_) return _c_;
		_c_ = isreward - _o_.isreward;
		if (0 != _c_) return _c_;
		_c_ = invitenum - _o_.invitenum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

