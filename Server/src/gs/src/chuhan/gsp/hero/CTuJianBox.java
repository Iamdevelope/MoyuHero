
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CTuJianBox__ extends xio.Protocol { }

/** 图鉴宝箱
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CTuJianBox extends __CTuJianBox__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				
				xbean.TuJianHeros tjheros = xtable.Tujianheros.get(roleId);
				chuhan.gsp.game.medalexchange41 boxinit = chuhan.gsp.main.ConfigManager.getInstance().
						getConf(chuhan.gsp.game.medalexchange41.class).get(boxid);
				if(boxinit != null){
					if( !tjheros.getTujianbox().containsKey(boxinit.getId()) ){
						chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
						boolean isOk = false;
						switch(boxinit.getExchangeType()){
						case 1:
							if( prole.getProperties().getHuangjinxz() >= boxinit.getNeedNum() ){
								isOk = true;
							}
							break;
						case 2:
							if( prole.getProperties().getBaijinxz() >= boxinit.getNeedNum() ){
								isOk = true;
							}
							break;
						case 3:
							if( prole.getProperties().getQingtongxz() >= boxinit.getNeedNum() ){
								isOk = true;
							}
							break;
						case 4:
							if( prole.getProperties().getChitiexz() >= boxinit.getNeedNum() ){
								isOk = true;
							}
							break;
						}
						if(isOk){
//							chuhan.gsp.award.DropManager.getInstance().dropAddByOther(boxinit.getRewardId(), 
//									boxinit.getRewardNum(), 0, 0, roleId, "tjbox");
							chuhan.gsp.award.DropManager.getInstance().sendMailOrDropAdd(roleId, 
									boxinit.getRewardId(), boxinit.getRewardNum(), 0, chuhan.gsp.log.LogBehavior.TUJIANBOX); 
							tjheros.getTujianbox().put(boxinit.getId(), 1);
							
							STuJianHeros send = new STuJianHeros();
							send.isnew = STuJianHeros.IS_NEW;
							send.tujianbox.add(boxinit.getId());
							send.tjheromaxlevel.addAll(tjheros.getTjheromaxlevel());
							xdb.Procedure.psendWhileCommit(roleId, send);
							
							STuJianBox snd = new STuJianBox();
							snd.result = STuJianBox.END_OK;
							snd.boxid = boxid;
							xdb.Procedure.psendWhileCommit(roleId, snd);
							return true;
						}
					}
				}
				STuJianBox snd = new STuJianBox();
				snd.result = STuJianBox.END_ERROR;
				snd.boxid = boxid;
				xdb.Procedure.psend(roleId, snd);
				return false;
			};
		}.submit();

	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787763;

	public int getType() {
		return 787763;
	}

	public int boxid; // 宝箱ID

	public CTuJianBox() {
	}

	public CTuJianBox(int _boxid_) {
		this.boxid = _boxid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(boxid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		boxid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CTuJianBox) {
			CTuJianBox _o_ = (CTuJianBox)_o1_;
			if (boxid != _o_.boxid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += boxid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(boxid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CTuJianBox _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = boxid - _o_.boxid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

