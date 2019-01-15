
package chuhan.gsp.mail;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SGetMailList__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SGetMailList extends __SGetMailList__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786934;

	public int getType() {
		return 786934;
	}

	public java.util.LinkedList<chuhan.gsp.Mail> maillist; // 邮件列表
	public int mailsize; // 从第几个开始往后取20个
	public int mailallsize; // 邮件总数

	public SGetMailList() {
		maillist = new java.util.LinkedList<chuhan.gsp.Mail>();
	}

	public SGetMailList(java.util.LinkedList<chuhan.gsp.Mail> _maillist_, int _mailsize_, int _mailallsize_) {
		this.maillist = _maillist_;
		this.mailsize = _mailsize_;
		this.mailallsize = _mailallsize_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.Mail _v_ : maillist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(maillist.size());
		for (chuhan.gsp.Mail _v_ : maillist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(mailsize);
		_os_.marshal(mailallsize);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.Mail _v_ = new chuhan.gsp.Mail();
			_v_.unmarshal(_os_);
			maillist.add(_v_);
		}
		mailsize = _os_.unmarshal_int();
		mailallsize = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SGetMailList) {
			SGetMailList _o_ = (SGetMailList)_o1_;
			if (!maillist.equals(_o_.maillist)) return false;
			if (mailsize != _o_.mailsize) return false;
			if (mailallsize != _o_.mailallsize) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += maillist.hashCode();
		_h_ += mailsize;
		_h_ += mailallsize;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(maillist).append(",");
		_sb_.append(mailsize).append(",");
		_sb_.append(mailallsize).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

