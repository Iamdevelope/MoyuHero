
package chuhan.gsp.play.activity;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CGetGameAct__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CGetGameAct extends __CGetGameAct__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				
				boolean result = ActivityGameManager.getInstance().getGameActEntry(roleId, actid);
				SGetGameAct snd = new SGetGameAct();
				snd.actid = actid;
				if(result){
					snd.result = SGetGameAct.END_OK;
				}else{
					snd.result = SGetGameAct.END_ERROR;
				}
				xdb.Procedure.psend(roleId, snd);
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 789050;

	public int getType() {
		return 789050;
	}

	public int actid; // 活动ID

	public CGetGameAct() {
	}

	public CGetGameAct(int _actid_) {
		this.actid = _actid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(actid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		actid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CGetGameAct) {
			CGetGameAct _o_ = (CGetGameAct)_o1_;
			if (actid != _o_.actid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += actid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(actid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CGetGameAct _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = actid - _o_.actid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

