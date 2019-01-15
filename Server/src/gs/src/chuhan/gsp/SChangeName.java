
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SChangeName__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SChangeName extends __SChangeName__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786453;

	public int getType() {
		return 786453;
	}

	public final static int OK = 1; // 成功
	public final static int ERROR = 2; // 失败
	public final static int INVALID = 3; // 名称不合法
	public final static int DUPLICATED = 4; // 重名
	public final static int NO_ITEM = 5; // 没有道具
	public final static int OVERLEN = 6; // 角色名过长
	public final static int SHORTLEN = 7; // 角色名过短
	public final static int ERRORCHAR = 8; // 特殊符号
	public final static int HAVESPACE = 9; // 有空格

	public byte error;
	public java.lang.String newname;

	public SChangeName() {
		newname = "";
	}

	public SChangeName(byte _error_, java.lang.String _newname_) {
		this.error = _error_;
		this.newname = _newname_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(error);
		_os_.marshal(newname, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		error = _os_.unmarshal_byte();
		newname = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SChangeName) {
			SChangeName _o_ = (SChangeName)_o1_;
			if (error != _o_.error) return false;
			if (!newname.equals(_o_.newname)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)error;
		_h_ += newname.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(error).append(",");
		_sb_.append("T").append(newname.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

