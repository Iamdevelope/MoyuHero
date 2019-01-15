
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SInviteReward__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SInviteReward extends __SInviteReward__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786451;

	public int getType() {
		return 786451;
	}

	public byte isreward; // 是否可以领奖
	public short invitenum; // 邀请人数量

	public SInviteReward() {
	}

	public SInviteReward(byte _isreward_, short _invitenum_) {
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
		_os_.marshal(isreward);
		_os_.marshal(invitenum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		isreward = _os_.unmarshal_byte();
		invitenum = _os_.unmarshal_short();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SInviteReward) {
			SInviteReward _o_ = (SInviteReward)_o1_;
			if (isreward != _o_.isreward) return false;
			if (invitenum != _o_.invitenum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)isreward;
		_h_ += invitenum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(isreward).append(",");
		_sb_.append(invitenum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SInviteReward _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = isreward - _o_.isreward;
		if (0 != _c_) return _c_;
		_c_ = invitenum - _o_.invitenum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

