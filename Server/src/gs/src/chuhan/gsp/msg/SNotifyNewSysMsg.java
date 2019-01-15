
package chuhan.gsp.msg;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SNotifyNewSysMsg__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SNotifyNewSysMsg extends __SNotifyNewSysMsg__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787637;

	public int getType() {
		return 787637;
	}

	public byte newmsgnum; // 新消息个数

	public SNotifyNewSysMsg() {
	}

	public SNotifyNewSysMsg(byte _newmsgnum_) {
		this.newmsgnum = _newmsgnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(newmsgnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		newmsgnum = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SNotifyNewSysMsg) {
			SNotifyNewSysMsg _o_ = (SNotifyNewSysMsg)_o1_;
			if (newmsgnum != _o_.newmsgnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)newmsgnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(newmsgnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SNotifyNewSysMsg _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = newmsgnum - _o_.newmsgnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

