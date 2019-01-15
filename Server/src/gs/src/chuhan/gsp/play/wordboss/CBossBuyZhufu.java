
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CBossBuyZhufu__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CBossBuyZhufu extends __CBossBuyZhufu__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				
				boolean result = Module.getInstance().bossBuyZhufuEntry(roleId, bossid);
				if( !result ){
					SBossBuyZhufu snd = new SBossBuyZhufu();
					snd.result = SBossBuyZhufu.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788895;

	public int getType() {
		return 788895;
	}

	public int bossid; // 值为1234，代表第几个boss

	public CBossBuyZhufu() {
	}

	public CBossBuyZhufu(int _bossid_) {
		this.bossid = _bossid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bossid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bossid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CBossBuyZhufu) {
			CBossBuyZhufu _o_ = (CBossBuyZhufu)_o1_;
			if (bossid != _o_.bossid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += bossid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bossid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CBossBuyZhufu _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = bossid - _o_.bossid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

