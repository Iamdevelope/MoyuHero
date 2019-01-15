
package chuhan.gsp.gm;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSendCommand__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSendCommand extends __CSendCommand__ {
	@Override
	protected void process() {
		final long gmroleid=gnet.link.Onlines.getInstance().findRoleid(this);
		final int userID=((gnet.link.Dispatch)this.getContext()).userid;
		final int localsid=((gnet.link.Dispatch)this.getContext()).linksid;
		GMInterface.execCommand(userID,gmroleid,localsid, cmd);
		//TODO:把ret告诉客户端
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787431;

	public int getType() {
		return 787431;
	}

	public java.lang.String cmd;

	public CSendCommand() {
		cmd = "";
	}

	public CSendCommand(java.lang.String _cmd_) {
		this.cmd = _cmd_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(cmd, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		cmd = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSendCommand) {
			CSendCommand _o_ = (CSendCommand)_o1_;
			if (!cmd.equals(_o_.cmd)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += cmd.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("T").append(cmd.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

