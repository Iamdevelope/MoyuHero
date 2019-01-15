
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CRoleList__ extends xio.Protocol { }

/** 客户端发给服务器，请求已有角色列表
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CRoleList extends __CRoleList__ {
	@Override
	protected void process() {
		// protocol handle
//		Formatter fo = new Formatter();
//		for(byte b : mac)
//		{
//			fo.format("%02X", b);
//		}
		gnet.link.User user = gnet.link.Onlines.getInstance().getOnlineUsers().online(this);
		if(user != null)
			user.setMac(mac);
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786433;

	public int getType() {
		return 786433;
	}

	public java.lang.String mac; // mac地址

	public CRoleList() {
		mac = "";
	}

	public CRoleList(java.lang.String _mac_) {
		this.mac = _mac_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(mac, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		mac = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CRoleList) {
			CRoleList _o_ = (CRoleList)_o1_;
			if (!mac.equals(_o_.mac)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += mac.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(mac.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

