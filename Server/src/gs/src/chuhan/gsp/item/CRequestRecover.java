
package chuhan.gsp.item;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.SRefreshBiwu;
import chuhan.gsp.attr.YuanBaoConsumeType;
import chuhan.gsp.battle.LadderRole;
import chuhan.gsp.game.svipconfig;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.task.shuifuconfig;
import chuhan.gsp.util.Conv;
// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CRequestRecover__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CRequestRecover extends __CRequestRecover__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				
				long now = GameTime.currentTimeMillis();
				PropRole prole = PropRole.getPropRole(roleId, false);
				
				if(recovertype == 1)
				{
					int oldtimes = 0;//prole.getProperties().getRecoverti();
					if(oldtimes >= getMaxTime(prole))
						return false;
					
					shuifuconfig cfg = ConfigManager.getInstance().getConf(shuifuconfig.class).get(3001);
					int price = cfg.price.get(oldtimes);
					if(prole.delYuanBao(-price,YuanBaoConsumeType.BUY_ITEM) != -price)
					{
						Message.psendMsgNotify(roleId, 27);
						return false;
					}
//					prole.setRecoverTi(oldtimes+1,now);
					prole.addTili(10);
					Message.psendMsgNotifyWhileCommit(roleId, 93);
				}
				else if(recovertype == 2)
				{
					/*
					int oldtimes = prole.getProperties().getRecoverhuo();
					if(oldtimes >= getMaxTime(prole))
						return false;
					
					shuifuconfig cfg = ConfigManager.getInstance().getConf(shuifuconfig.class).get(3002);
					int price = cfg.price.get(oldtimes);
					if(prole.delYuanBao(-price,YuanBaoConsumeType.BUY_ITEM) != -price)
					{
						Message.psendMsgNotify(roleId, 27);
						return false;
					}
//					prole.setRecoverHuo(oldtimes+1,now);
//					prole.addHuoli(10);
					Message.psendMsgNotifyWhileCommit(roleId, 94);
					*/
				}
				else if(recovertype == 3)
				{					
					int price = 28;
					if(prole.delYuanBao(-price,YuanBaoConsumeType.BUY_ITEM) != -price)
					{
						Message.psendMsgNotify(roleId, 27);
						return false;
					}
/*					LadderRole role = LadderRole.getLadderRole(roleId, false);
					if(role.getData().getFighttimes() == 0)
						return false;
					role.getData().setFighttimes(Math.max(0, role.getData().getFighttimes() - 10));
					xdb.Procedure.psendWhileCommit(roleId, new SRefreshBiwu(role.getData().getFighttimes()));
					Message.psendMsgNotifyWhileCommit(roleId, 129);*/
					return true;
				}
				else
					return false;
				
				SSendTodayRecoverd snd = new SSendTodayRecoverd();
//				snd.huoli = Conv.toByte(prole.getProperties().getRecoverhuo());
//				snd.tili = Conv.toByte(prole.getProperties().getRecoverti());
				xdb.Procedure.psendWhileCommit(roleId, snd);
				return true;
			};
		}.submit();
	}
	
	public static int getMaxTime(PropRole prole)
	{
		svipconfig svipconfig = ConfigManager.getInstance().getConf(svipconfig.class).get(prole.getVipLevel());
		if(null != svipconfig) {
			return svipconfig.buytimes;
		}
		return 0;
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787554;

	public int getType() {
		return 787554;
	}

	public byte recovertype; // 1:体力，2：活力

	public CRequestRecover() {
	}

	public CRequestRecover(byte _recovertype_) {
		this.recovertype = _recovertype_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(recovertype);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		recovertype = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CRequestRecover) {
			CRequestRecover _o_ = (CRequestRecover)_o1_;
			if (recovertype != _o_.recovertype) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)recovertype;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(recovertype).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CRequestRecover _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = recovertype - _o_.recovertype;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

