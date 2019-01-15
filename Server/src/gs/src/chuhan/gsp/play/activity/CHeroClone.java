
package chuhan.gsp.play.activity;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CHeroClone__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CHeroClone extends __CHeroClone__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				
				boolean result = ActivityManager.getInstance().heroCloneEntry(roleId, heroid);
				SHeroClone snd = new SHeroClone();
				snd.heroid = heroid;
				if(result){
					snd.result = SGetMSZQ.END_OK;
				}else{
					snd.result = SGetMSZQ.END_ERROR;
				}
				xdb.Procedure.psend(roleId, snd);
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 789033;

	public int getType() {
		return 789033;
	}

	public int heroid; // 英雄id

	public CHeroClone() {
	}

	public CHeroClone(int _heroid_) {
		this.heroid = _heroid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(heroid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		heroid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CHeroClone) {
			CHeroClone _o_ = (CHeroClone)_o1_;
			if (heroid != _o_.heroid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += heroid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CHeroClone _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = heroid - _o_.heroid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

