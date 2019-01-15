
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CRequestXiulian__ extends xio.Protocol { }

/** 请求修炼
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CRequestXiulian extends __CRequestXiulian__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PRequestXiulian(roleId, herokey, costtype).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787748;

	public int getType() {
		return 787748;
	}

	public int herokey; // 在包裹中的key
	public byte costtype; // 1：普通，2：元宝

	public CRequestXiulian() {
	}

	public CRequestXiulian(int _herokey_, byte _costtype_) {
		this.herokey = _herokey_;
		this.costtype = _costtype_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.marshal(costtype);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		costtype = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CRequestXiulian) {
			CRequestXiulian _o_ = (CRequestXiulian)_o1_;
			if (herokey != _o_.herokey) return false;
			if (costtype != _o_.costtype) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += (int)costtype;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(costtype).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CRequestXiulian _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		_c_ = costtype - _o_.costtype;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

