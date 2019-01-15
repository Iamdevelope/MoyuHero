package chuhan.gsp.msg;

import java.util.Iterator;
import java.util.List;

import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.Conv;

public class MsgRole {
	
	public static final int MAX_STORE_MSG = 20; 
	public static final byte MST_TYPE_SYS = 0;//系统消息
	public static final byte MST_TYPE_FRIEND = 1;//好友消息
	public static final byte MST_TYPE_DEFEAT = 2;//天梯战败消息
	public static final byte MSG_MAX_SIZE = 30;
	
	public static MsgRole getMsgRole(long roleId, boolean readonly)
	{
		xbean.MsgRole xrole = null;
		if(readonly)
			xrole = xtable.Msgroles.select(roleId);
		else
			xrole = xtable.Msgroles.get(roleId);
		if(xrole == null)
		{
			if(xtable.Properties.select(roleId) == null)
				throw new IllegalArgumentException("unknown roleid : " + roleId);
			if(readonly)
				xrole = xbean.Pod.newMsgRoleData();
			else
			{
				xrole = xbean.Pod.newMsgRole();
				xtable.Msgroles.insert(roleId, xrole);
			}
		}
		return new MsgRole(roleId, xrole, readonly);
	}
	
	
	public final long roleId;
	public final xbean.MsgRole xrole;
	public final boolean readonly;
	private MsgRole(long roleId, xbean.MsgRole xrole, boolean readonly) {
		this.roleId = roleId;
		this.xrole = xrole;
		this.readonly = readonly;
	}
	
	public void processWhileOnline()
	{
		int i = 0;
		for(Iterator<xbean.SysMsg> it = xrole.getSysmsgs().iterator();it.hasNext();)
		{
			xbean.SysMsg xmsg = it.next();
			if(i <= MAX_STORE_MSG)
				xmsg.setSended(false);
			else
				it.remove();
			i++;
		}
		notifySysNewNum();
	}
	
	public void sendSysMsgs()
	{
		SReplySysMsg sysmsgs = new SReplySysMsg();
		for(xbean.SysMsg xmsg : xrole.getSysmsgs())
		{
			if(xmsg.getSended())
				continue;
			SysMsg promsg = new SysMsg();
			promsg.msgid = xmsg.getMsgid();
			promsg.text = xmsg.getText();
			promsg.msgtype = Conv.toByte(xmsg.getMsgtype());
			promsg.sendroleid = xmsg.getSendroleid();
			promsg.sendtime = xmsg.getTime();
			if(promsg.sendroleid > 0) {
				chuhan.gsp.attr.PropRole propRole = chuhan.gsp.attr.PropRole.getPropRole(promsg.sendroleid, true);
				if(null != propRole) {
					promsg.sendname = propRole.getProperties().getRolename();
				}
			} else {
				promsg.sendname = "";
			}
			for(String str : xmsg.getParams())
			{
				promsg.params.add(Message.convertString2Octets(str));
			}
			sysmsgs.msgs.add(promsg);
			xmsg.setIsnew(false);
			xmsg.setSended(true);
		}
		xdb.Procedure.psendWhileCommit(roleId, sysmsgs);
	}
	
	
	public boolean addSysMsg(int msgId, List<String> params, String text, long sendRoleId, int msgType)
	{
		if(msgId <= 0 && (text == null || text.isEmpty()))
			return false;
		xbean.SysMsg sysmsg = xbean.Pod.newSysMsg();
		sysmsg.setMsgid(msgId);
		if(params != null)
			sysmsg.getParams().addAll(params);
		if(text != null)
			sysmsg.setText(text);
		sysmsg.setTime(GameTime.currentTimeMillis());
		sysmsg.setIsnew(true);
		sysmsg.setSended(false);
		sysmsg.setSendroleid(sendRoleId);
		sysmsg.setMsgtype(msgType);
		xrole.getSysmsgs().add(sysmsg);
		if(xrole.getSysmsgs().size() > MSG_MAX_SIZE) {
			int removeNum = xrole.getSysmsgs().size() - MSG_MAX_SIZE;
			for(int i = 0; i < removeNum; i ++) {
				xrole.getSysmsgs().remove(0);
			}
		}
		return true;
	}
	
	public void notifySysNewNum()
	{
		int i = 0;
		for(xbean.SysMsg xmsg : xrole.getSysmsgs())
		{
			if(xmsg.getIsnew())
				i++;
		}
		if(i > 0)
		{
			if(xdb.Transaction.current() != null)
				xdb.Procedure.psendWhileCommit(roleId, new SNotifyNewSysMsg(Conv.toByte(i)));
			else
				gnet.link.Onlines.getInstance().send(roleId, new SNotifyNewSysMsg(Conv.toByte(i)));
		}	
	}
	
	public boolean addSysMsgWithSP(int msgId, List<String> params, String text, long sendRoleId, int msgType)
	{
		boolean succ = addSysMsg(msgId, params, text, sendRoleId, msgType);
		if(succ)
			notifySysNewNum();
		return succ;
	}
	
	public boolean removeSysMsg(int verseindex)
	{
		if(verseindex >= xrole.getSysmsgs().size())
			return false;
		xrole.getSysmsgs().remove(xrole.getSysmsgs().size() - verseindex - 1);
		return true;
	}
	
	/**
	 * 清空全部消息
	 * @return
	 */
	public boolean clearSysMsg() {
		xrole.getSysmsgs().clear();
		SClearSysMsgResult sClearSysMsgResult = new SClearSysMsgResult();
		xdb.Procedure.psendWhileCommit(roleId, sClearSysMsgResult);
		return true;
	}
	
}
