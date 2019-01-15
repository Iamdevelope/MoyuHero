
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CBossPass__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CBossPass extends __CBossPass__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				
				boolean result = Module.getInstance().bossPass(fightinfolist, roleId);
				if( !result ){
					SBossPass snd = new SBossPass();
					snd.result = SBossPass.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788889;

	public int getType() {
		return 788889;
	}

	public java.util.LinkedList<chuhan.gsp.fightInfo> fightinfolist; // 战斗信息

	public CBossPass() {
		fightinfolist = new java.util.LinkedList<chuhan.gsp.fightInfo>();
	}

	public CBossPass(java.util.LinkedList<chuhan.gsp.fightInfo> _fightinfolist_) {
		this.fightinfolist = _fightinfolist_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.fightInfo _v_ : fightinfolist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(fightinfolist.size());
		for (chuhan.gsp.fightInfo _v_ : fightinfolist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.fightInfo _v_ = new chuhan.gsp.fightInfo();
			_v_.unmarshal(_os_);
			fightinfolist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CBossPass) {
			CBossPass _o_ = (CBossPass)_o1_;
			if (!fightinfolist.equals(_o_.fightinfolist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += fightinfolist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(fightinfolist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

