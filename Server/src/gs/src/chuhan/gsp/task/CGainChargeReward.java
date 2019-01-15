
package chuhan.gsp.task;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CGainChargeReward__ extends xio.Protocol { }

/** 领取充值奖励
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CGainChargeReward extends __CGainChargeReward__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788043;

	public int getType() {
		return 788043;
	}

	public int yuanbao; // 要领取的奖励

	public CGainChargeReward() {
	}

	public CGainChargeReward(int _yuanbao_) {
		this.yuanbao = _yuanbao_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(yuanbao);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		yuanbao = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CGainChargeReward) {
			CGainChargeReward _o_ = (CGainChargeReward)_o1_;
			if (yuanbao != _o_.yuanbao) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += yuanbao;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(yuanbao).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CGainChargeReward _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = yuanbao - _o_.yuanbao;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

