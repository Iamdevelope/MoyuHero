
package chuhan.gsp.msg;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CDeleteSysMsg__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CDeleteSysMsg extends __CDeleteSysMsg__ {
	@Override
	protected void process() {
		// protocol handle
		long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PRemoveSysMsg(roleId, verseindex).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787640;

	public int getType() {
		return 787640;
	}

	public short verseindex; // 列表内的倒数index，防止删的时候正好新加了

	public CDeleteSysMsg() {
	}

	public CDeleteSysMsg(short _verseindex_) {
		this.verseindex = _verseindex_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(verseindex);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		verseindex = _os_.unmarshal_short();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CDeleteSysMsg) {
			CDeleteSysMsg _o_ = (CDeleteSysMsg)_o1_;
			if (verseindex != _o_.verseindex) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += verseindex;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(verseindex).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CDeleteSysMsg _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = verseindex - _o_.verseindex;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

