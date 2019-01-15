
package chuhan.gsp.mail;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CReceiveMail__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CReceiveMail extends __CReceiveMail__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PReceiveMail(roleId,this.mailkey,isget).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786938;

	public int getType() {
		return 786938;
	}

	public int mailkey; // 邮件key
	public int isget; // 是否领取附件

	public CReceiveMail() {
	}

	public CReceiveMail(int _mailkey_, int _isget_) {
		this.mailkey = _mailkey_;
		this.isget = _isget_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(mailkey);
		_os_.marshal(isget);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		mailkey = _os_.unmarshal_int();
		isget = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CReceiveMail) {
			CReceiveMail _o_ = (CReceiveMail)_o1_;
			if (mailkey != _o_.mailkey) return false;
			if (isget != _o_.isget) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += mailkey;
		_h_ += isget;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(mailkey).append(",");
		_sb_.append(isget).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CReceiveMail _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = mailkey - _o_.mailkey;
		if (0 != _c_) return _c_;
		_c_ = isget - _o_.isget;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

