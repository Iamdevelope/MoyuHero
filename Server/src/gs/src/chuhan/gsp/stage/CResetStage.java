
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CResetStage__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CResetStage extends __CResetStage__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				StageRole stagerole = StageRole.getStageRole(roleId);
				xbean.StageBattleInfo xbattle = stagerole.getBattleInfo(battleid);
				boolean result;
				SResetStage snd = new SResetStage();
				if(xbattle != null){
					xbattle.setFightnum(0);
					snd.endtype = SResetStage.END_OK;
					stagerole.sendSRefreshStageBattle(xbattle, stagerole.getStageNumByBattleId(battleid));
					result = true;
				}else{
					snd.endtype = SResetStage.END_ERROR;
					result = false;
				}
				xdb.Procedure.psend(roleId, snd);
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787953;

	public int getType() {
		return 787953;
	}

	public int battleid; // 关卡ID

	public CResetStage() {
	}

	public CResetStage(int _battleid_) {
		this.battleid = _battleid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(battleid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		battleid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CResetStage) {
			CResetStage _o_ = (CResetStage)_o1_;
			if (battleid != _o_.battleid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += battleid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CResetStage _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = battleid - _o_.battleid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

