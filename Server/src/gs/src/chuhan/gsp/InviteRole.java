package chuhan.gsp;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;

import xdb.Transaction;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.AwardManager;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.task.syaoqingren;
import chuhan.gsp.util.Conv;

public class InviteRole {
	private long roleId;
//	private final xbean.InviteInfo inviteInfo;
	
/*	private InviteRole(long roleId, xbean.InviteInfo inviteInfo) {
		this.roleId = roleId;
		this.inviteInfo = inviteInfo;
	}*/
	
	public static InviteRole getInviteRole(long roleId, boolean readOnly) {
		/*xbean.InviteInfo inviteInfo;
		if(readOnly) {
			inviteInfo = xtable.Inviteinfo.select(roleId);
		} else {
			inviteInfo = xtable.Inviteinfo.get(roleId);
		}
		if(null == inviteInfo) {
			if(readOnly) {
				inviteInfo = xbean.Pod.newInviteInfoData();
			} else {
				inviteInfo = xbean.Pod.newInviteInfo();
				xtable.Inviteinfo.insert(roleId, inviteInfo);
			}
		}*/
		
		return null;
	}
	
	public void mainView() {
		SSettingView snd = new SSettingView();
//		snd.invitename = inviteInfo.getInviteme();
//		int inviteNum = inviteInfo.getAminvites().size();//我邀请的人数量
//		snd.invitenum = Conv.toShort(inviteNum);
//		if(inviteNum > inviteInfo.getAwardnum()) {
//			snd.isreward = 1;
//		} else {
//			snd.isreward = 0;
//		}
		if(Transaction.current() == null)
			gnet.link.Onlines.getInstance().send(roleId, snd);
		else
			xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	public boolean setInvite(long inviteId) {
		if(inviteId == roleId) {
			Message.psendMsgNotify(roleId, 341);
			return false;
		}
//		if(inviteInfo.getInviteme() > 0) {//已经填写过了
//			return false;
//		}
		PropRole prole = PropRole.getPropRole(inviteId, true);
		if(prole == null) {
			Message.psendMsgNotify(roleId, 341);
			return false;
		}
		PropRole self = PropRole.getPropRole(roleId, true);
		if(self.getLevel() < 20) {//等级不够
			Message.psendMsgNotify(roleId, 342);
			return false;
		}
//		inviteInfo.setInviteme(inviteId);
//		getInviteRole(inviteId, false).inviteInfo.getAminvites().add(roleId);
		//发奖
		AwardManager.getInstance().distributeAllAward(roleId, 101526, null, true);
		SInviteName snd = new SInviteName();
		snd.invitename = inviteId;
		if(Transaction.current() == null)
			gnet.link.Onlines.getInstance().send(roleId, snd);
		else
			xdb.Procedure.psendWhileCommit(roleId, snd);
		
		return true;
	}
	
	public boolean reward() {
//		int inviteNum = inviteInfo.getAminvites().size();//我邀请的人数量
//		if(inviteInfo.getAwardnum() == inviteNum) {//已经领取过了
//			Message.psendMsgNotify(roleId, 344, inviteNum);
//			return false;
//		}
		Map<Integer, syaoqingren> awards = ConfigManager.getInstance().getConf(syaoqingren.class);
		List<Integer> keys = new ArrayList<Integer>();
		for(int id : awards.keySet()) {
			keys.add(id);
		}
		Collections.sort(keys);
		int awardId = 0;
		for(int id : keys) {
//			if(inviteInfo.getAwardnum() < id && inviteNum >= id) {
//				awardId = id;
//				break;
//			}
		}
		if(0 == awardId) {
//			Message.psendMsgNotify(roleId, 344, inviteNum);
			return false;
		}
		//发奖
		AwardManager.getInstance().distributeAllAward(roleId, awards.get(awardId).items, null, true);
//		inviteInfo.setAwardnum(awardId);
		SInviteReward snd = new SInviteReward();
//		snd.invitenum = Conv.toShort(inviteNum);
//		if(inviteNum > inviteInfo.getAwardnum()) {
//			snd.isreward = 1;
//		} else {
//			snd.isreward = 0;
//		}
		if(Transaction.current() == null)
			gnet.link.Onlines.getInstance().send(roleId, snd);
		else
			xdb.Procedure.psendWhileCommit(roleId, snd);
		
		return true;
	}
	
}
