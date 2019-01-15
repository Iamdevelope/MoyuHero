package chuhan.gsp.friends;

import java.util.Map.Entry;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.battle.BloodRole;
import chuhan.gsp.battle.LadderRole;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.util.CollectionUtil;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.StringValidateUtil;

import xbean.FriendInfo;
import xbean.FriendReqs;
import xbean.Friends;
import xdb.Transaction;

public class FriendRole {
	private final long roleId;
	private final xbean.FriendReqs friendReqs;
	private final xbean.Friends friends;
	
	private FriendRole(long roleId, FriendReqs friendReqs, Friends friends) {
		this.roleId = roleId;
		this.friendReqs = friendReqs;
		this.friends = friends;
		processData();
	}
	
	private void processData() {
		if(friends.getMine().isEmpty()) {
			return;
		}
		long now = GameTime.currentTimeMillis();
		int nowday = DateUtil.getCurrentDay(now);
		for(xbean.FriendInfo friendInfo : friends.getMine().values()) {
			if(friendInfo.getLastdaychangetime() == 0) {
				friendInfo.setLastdaychangetime(now);
				continue;
			}
			int oldday = DateUtil.getCurrentDay(friendInfo.getLastdaychangetime());
			if(nowday != oldday) {
				friendInfo.setGivetilinum(0);
				friendInfo.setTotilinum(0);
				friendInfo.setLastdaychangetime(now);
			}
		}
		
	}
	
	public static FriendRole getFriendRole(long roleId, boolean readOnly) {
		if(null == xtable.Properties.select(roleId))
			return null;
		
		xbean.FriendReqs reqs;
		xbean.Friends _friends;
		if(readOnly) {
			reqs = xtable.Friendreqs.select(roleId);
			_friends = xtable.Friends.select(roleId);
		} else {
			reqs = xtable.Friendreqs.get(roleId);
			_friends = xtable.Friends.get(roleId);
		}
		if(null == reqs) {
			if(readOnly) {
				reqs = xbean.Pod.newFriendReqsData();
			} else {
				reqs = xbean.Pod.newFriendReqs();
				xtable.Friendreqs.insert(roleId, reqs);
			}
		}
		
		if(null == _friends) {
			if(readOnly) {
				_friends = xbean.Pod.newFriendsData();
			} else {
				_friends = xbean.Pod.newFriends();
				xtable.Friends.insert(roleId, _friends);
			}
		}
		
		return new FriendRole(roleId, reqs, _friends);
	}
	
	/**
	 * 发送好友请求 锁2人
	 * @param byReqRoleId
	 * @return
	 */
	public boolean request(long byReqRoleId) {
		if(roleId == byReqRoleId) {
			return false;
		}
		PropRole proRole = PropRole.getPropRole(roleId, true);
		if(proRole.friendIsFull()) {
			Message.psendMsgNotify(roleId, 285);
			return false;
		}
		
		chuhan.gsp.attr.PropRole byPropRole = chuhan.gsp.attr.PropRole.getPropRole(byReqRoleId, true);
		Message.psendMsgNotify(roleId, 273, byPropRole.getProperties().getRolename());
		
		if(friendReqs.getByme().contains(byReqRoleId)) {//已经邀请过了
			return false;
		}
		if(isFriend(byReqRoleId)) {
			return false;
		}
		FriendRole byRole = FriendRole.getFriendRole(byReqRoleId, false);
		//他也邀请过我，则加为好友
		if(byRole.isRequest(roleId)) {
			if(!genFriends(byRole, byReqRoleId)) {
				return false;
			}
			return true;
		}
		//生成邀请记录
		friendReqs.getByme().add(byReqRoleId);//加到我的对象邀请过的人记录里
		byRole.getFriendReqs().getImby().add(roleId);//加到他的对象邀请他的人的记录里
		
		return true;
	}
	
	/**
	 * 同意好友邀请 锁2人
	 * @param byRoleId
	 * @return
	 */
	public boolean agree(long byRoleId) {
		if(roleId == byRoleId) {
			return false;
		}
		FriendRole byRole = FriendRole.getFriendRole(byRoleId, false);
		if(!byRole.isRequest(roleId)) {//他没有邀请我
			return false;
		}
		//已经是好友
		if(isFriend(byRoleId)) {
			friendReqs.getImby().remove(byRoleId);//删除我的对象里邀请我的人数据
			return true;
		}
		
		if(!genFriends(byRole, byRoleId)) {
			return false;
		}
		SAgree snd = new SAgree();
		snd.byroleid = byRoleId;
		if(Transaction.current() == null)
			gnet.link.Onlines.getInstance().send(roleId, snd);
		else
			xdb.Procedure.psendWhileCommit(roleId, snd);
		
		return true;
	}
	
	private boolean genFriends(FriendRole byRole, long byReqRoleId) {
		PropRole proRole = PropRole.getPropRole(roleId, false);
		if(proRole.friendIsFull()) {
			Message.psendMsgNotify(roleId, 285);
			return false;
		}
		PropRole byProRole = PropRole.getPropRole(byReqRoleId, false);
		if(byProRole.friendIsFull()) {
			Message.psendMsgNotify(roleId, 286);
			return false;
		}
		
		friendReqs.getImby().remove(byReqRoleId);//删除我的对象里邀请我的人数据
		byRole.getFriendReqs().getByme().remove(roleId);//删除他的对象邀请过的人的数据
		//成为好友
		xbean.FriendInfo friendInfo = xbean.Pod.newFriendInfo();
		xbean.FriendInfo friendInfo1 = xbean.Pod.newFriendInfo();
		friends.getMine().put(byReqRoleId, friendInfo);
		byRole.getFriends().getMine().put(roleId, friendInfo1);
		
//		proRole.getProperties().setFriendnum(proRole.getProperties().getFriendnum() + 1);
//		byProRole.getProperties().setFriendnum(byProRole.getProperties().getFriendnum() + 1);
		
		return true;
	}
	
	/**
	 * 这个人是否被我邀请过
	 * @param byRoleId
	 * @return
	 */
	public boolean isRequest(long byRoleId) {
		return friendReqs.getByme().contains(byRoleId);
	}
	
	/**
	 * 是否是好友
	 * @param byRoleId
	 * @return
	 */
	public boolean isFriend(long byRoleId) {
		return friends.getMine().containsKey(byRoleId);
	}
	
	/**
	 * 拒绝好友邀请 锁2人
	 * @param byRoleId
	 * @return
	 */
	public boolean refuse(long byRoleId) {
		FriendRole byRole = FriendRole.getFriendRole(byRoleId, false);
		if(!byRole.isRequest(roleId)) {
			return false;
		}
		
		friendReqs.getImby().remove(byRoleId);//删除我的对象里邀请我的人数据
		byRole.getFriendReqs().getByme().remove(roleId);//删除他的对象邀请过的人的数据
		
		SRefuse snd = new SRefuse();
		snd.byroleid = byRoleId;
		if(Transaction.current() == null)
			gnet.link.Onlines.getInstance().send(roleId, snd);
		else
			xdb.Procedure.psendWhileCommit(roleId, snd);
		
		return true;
	}
	
	/**
	 * 删除好友 锁2人
	 * @param byRoleId
	 * @return
	 */
	public boolean delete(long byRoleId) {
		FriendRole byRole = FriendRole.getFriendRole(byRoleId, false);
		if(!isFriend(byRoleId)) {//不是好友
			return false;
		}
		friends.getMine().remove(byRoleId);
		byRole.getFriends().getMine().remove(roleId);
		PropRole proRole = PropRole.getPropRole(roleId, false);
//		proRole.getProperties().setFriendnum(proRole.getProperties().getFriendnum() - 1);
		PropRole byProRole = PropRole.getPropRole(byRoleId, false);
//		byProRole.getProperties().setFriendnum(byProRole.getProperties().getFriendnum() - 1);
		xdb.Procedure.psendWhileCommit(roleId, new SDelFriend(byRoleId));
		return true;
	}
	
	public void sendFriendList() {
		SFriendList snd = new SFriendList();
		for(Entry<Long, FriendInfo> entry : friends.getMine().entrySet()) {
			chuhan.gsp.friends.FriendInfo friendInfo = new chuhan.gsp.friends.FriendInfo();
			long roleId = entry.getKey();
			friendInfo.roleid = roleId;
			if(entry.getValue().getGivetilinum() > 0) {
				friendInfo.istili = 1;
			} else {
				friendInfo.istili = 0;
			}
			chuhan.gsp.attr.PropRole proRole = chuhan.gsp.attr.PropRole.getPropRole(roleId, true);
			friendInfo.level = proRole.getLevel();
			friendInfo.name = proRole.getProperties().getRolename();
			friendInfo.lastlogintime = proRole.getProperties().getOnlinetime();
			//天梯
			LadderRole ladderRole = LadderRole.getLadderRole(roleId, true);
			friendInfo.ladderrankid = ladderRole.getMyRank();
			//血战
			BloodRole bloodRole = BloodRole.getBloodRole(roleId, true);
//			friendInfo.bloodlv = bloodRole.getData().getMaxlevel();
			
			snd.friends.add(friendInfo);
		}
		
		if(Transaction.current() == null)
			gnet.link.Onlines.getInstance().send(roleId, snd);
		else
			xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 给好友发消息，锁好友
	 * @param toRoleId
	 * @param msg
	 * @return
	 */
	public boolean sendMsg(long toRoleId, String msg) {
		if(CollectionUtil.isEmpty(msg)) {
			return false;
		}
		if(!isFriend(toRoleId)) {
			return false;
		}
		msg = StringValidateUtil.checkAndReplaceIllegalWord(msg);
		MsgRole msgRole = MsgRole.getMsgRole(toRoleId, false);
		xbean.Properties xprop = xtable.Properties.get(roleId);
		String title = Message.getMessage(9)+xprop.getRolename()+Message.getMessage(10);
		if(!msgRole.addSysMsgWithSP(0, null, title+msg, roleId, MsgRole.MST_TYPE_FRIEND)) {
			return false;
		}
		
		return true;
	}
	
	/**
	 * 送体力 锁2人
	 * @param toRoleId
	 * @return
	 */
	public boolean toFriTi(long toRoleId) {
		if(!isFriend(toRoleId)) {
			return false;
		}
		FriendInfo myInfo = friends.getMine().get(toRoleId);
		if(myInfo.getTotilinum() > 0) {//已经给过
			Message.psendMsgNotify(roleId, 283);
			return false;
		}
		myInfo.setTotilinum(1);
		FriendInfo friInfo = FriendRole.getFriendRole(toRoleId, false).getFriends().getMine().get(roleId);
		friInfo.setGivetilinum(1);
		Message.psendMsgNotifyWhileCommit(roleId, 282);
		
		return true;
	}
	
	/**
	 * 领取体力 锁自己
	 * @param friRoleId
	 * @return
	 */
	public boolean gainFriTi(long friRoleId) {
		xbean.FriendInfo friendInfo = friends.getMine().get(friRoleId);
		if(friendInfo.getGivetilinum() <= 0) {//没有给我
			return false;
		}
		PropRole propRole = PropRole.getPropRole(roleId, false);
		//还有次数才加体力
		int useNum = 0;//propRole.getProperties().getGetfritilinum();
		int canNum = 6 + propRole.getVipLevel();
		if(useNum < canNum) {
			propRole.addTili(1);
			Message.psendMsgNotifyWhileCommit(roleId, 280, (canNum - useNum - 1));
		} else {
			Message.psendMsgNotify(roleId, 281);
		}
		friendInfo.setGivetilinum(0);//设为0表示已经领取他的体力了
//		propRole.getProperties().setGetfritilinum(useNum + 1);
		
		SGainTi snd = new SGainTi(friRoleId);
		if(Transaction.current() == null)
			gnet.link.Onlines.getInstance().send(roleId, snd);
		else
			xdb.Procedure.psendWhileCommit(roleId, snd);
		
		return true;
	}
	
	public xbean.FriendReqs getFriendReqs() {
		return friendReqs;
	}

	public xbean.Friends getFriends() {
		return friends;
	}
}
