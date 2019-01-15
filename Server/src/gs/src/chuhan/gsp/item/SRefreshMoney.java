
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshMoney__ extends xio.Protocol { }

/** 刷新玩家的钱或者存款
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshMoney extends __SRefreshMoney__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787539;

	public int getType() {
		return 787539;
	}

	public byte bagid;
	public long money;

	public SRefreshMoney() {
	}

	public SRefreshMoney(byte _bagid_, long _money_) {
		this.bagid = _bagid_;
		this.money = _money_;
	}

	public final boolean _validator_() {
		if (bagid < 1 || bagid > 10) return false;
		if (money < 0) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bagid);
		_os_.marshal(money);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bagid = _os_.unmarshal_byte();
		money = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshMoney) {
			SRefreshMoney _o_ = (SRefreshMoney)_o1_;
			if (bagid != _o_.bagid) return false;
			if (money != _o_.money) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)bagid;
		_h_ += (int)money;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bagid).append(",");
		_sb_.append(money).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshMoney _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = bagid - _o_.bagid;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(money - _o_.money);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

