package chuhan.gsp.mail;


import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.util.DateUtil;


public class MailColumn {
		
	public static Logger logger = Logger.getLogger(MailColumn.class);
	public final static long DEFAULT_TIME = DateUtil.weekMills;
	public final static int MAIL_SIZE = 20;
	
	final public long roleId;
	final xbean.Mails xcolumn;
	final boolean readonly;
	
	
	public static MailColumn getMailColumn(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造MailColumn时，角色 "+roleId+" 不存在。");
		
		xbean.Mails mailcol = null;
		if(readonly)
			mailcol = xtable.Maillist.select(roleId);
		else
			mailcol = xtable.Maillist.get(roleId);
		if(mailcol == null)
		{
			if(readonly)
				mailcol = xbean.Pod.newMailsData();
			else
			{
				mailcol = xbean.Pod.newMails();
				xtable.Maillist.insert(roleId, mailcol);
			}
		}
		return new MailColumn(roleId, mailcol, readonly);
	}
	
	private MailColumn(long roleId, xbean.Mails xcolumn, boolean readonly) {
		this.roleId = roleId;
		this.xcolumn = xcolumn;
		this.readonly = readonly;
/*		//测试用
		if(xcolumn.getMails().size() == 0){
			test();
		}*/
	}
	
	public void sendSGetMailList(int mailsize){
		xdb.Procedure.psend(roleId, new SGetMailList(this.getProtocolMailList(mailsize),mailsize,
				this.xcolumn.getMails().size()));
	}
	
	/**
	 * 邮件列表转化成消息
	 * @param mailsize
	 * @return
	 */
	public LinkedList<chuhan.gsp.Mail> getProtocolMailList(int mailsize){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		LinkedList<chuhan.gsp.Mail> maillist = new LinkedList<chuhan.gsp.Mail>();
		LinkedList<Integer> delmailkey = new LinkedList<Integer>();
		int j = 0;
		for(int i = xcolumn.getMails().size() - 1; i >= 0 ; i--){
			if(j >= mailsize + MAIL_SIZE){
				break;
			}
			if( j < mailsize){
				j++;
				continue;
			}
			xbean.Mail xmail = xcolumn.getMails().get(i);
			if(xmail.getEndtime() < now){
				delmailkey.addFirst(xmail.getKey());
				continue;
			}
			j++;
			chuhan.gsp.Mail mail = this.getProtocolMail(xmail);
			maillist.addFirst(mail);	
		}		
		for(int delkey : delmailkey){
			for(xbean.Mail xmail : xcolumn.getMails()){
				if(xmail.getKey() == delkey){
					xcolumn.getMails().remove(xmail);
					break;
				}
			}
		}
		return maillist;
	}
	
	/**
	 * 转化成单个消息
	 * @param xmail
	 * @return
	 */
	public chuhan.gsp.Mail getProtocolMail(xbean.Mail xmail)
	{
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if(xmail == null)
			return null;
		chuhan.gsp.Mail mail = new chuhan.gsp.Mail();
		mail.key = xmail.getKey();
		mail.sender = xmail.getSender();
		mail.title = xmail.getTitle();
		mail.msg = xmail.getMsg();
		mail.innerdropidlist.addAll(xmail.getInnerdropidlist());
		for(xbean.MailItem xmailitem : xmail.getItems()){
			chuhan.gsp.MailItem item = new chuhan.gsp.MailItem();
			item.objectid = xmailitem.getObjectid();
			item.dropnum = xmailitem.getDropnum();
			item.dropparameter1 = xmailitem.getDropparameter1();
			item.dropparameter2 = xmailitem.getDropparameter2();
			mail.items.add(item);
		}
		mail.begintime = xmail.getEndtime() - DEFAULT_TIME;		//给客户端的是开始时间
		mail.isopen = xmail.getIsopen();
		mail.strlist.addAll(xmail.getStrlist());

		return mail;
	}
	
	// 获得下一个key
	public int getNextKey(){
		xcolumn.setNextkey(xcolumn.getNextkey() + 1);
		return xcolumn.getNextkey();
	}
	
	public xbean.Mails getxcolumn(){
		return xcolumn;
	}
	
	//增加一个mail
	public void addMail(xbean.Mail xmail,boolean notSend) {
		xmail.setKey(getNextKey());
		xcolumn.getMails().add(xmail);
		if(!notSend){
			this.sSendIsHaveNotOpen();
		}
	}
	
	/*// 删除一个邮件
	public boolean removeByKey(int mailKey) {
		java.util.LinkedList<Integer> removeList = new java.util.LinkedList<Integer>();
		removeList.add(mailKey);
		return removeByKeyList(removeList);
	}*/

	/**
	 * 删除多个邮件
	 * @param itemkeys
	 * @param mailsize
	 * @return
	 */
	public boolean removeByKeyList(java.util.LinkedList<Integer> itemkeys,int mailsize) {
		for(int i = xcolumn.getMails().size() - 1 ; i >= 0 ;i-- ){
			if(xcolumn.getMails().get(i).getIsopen() % 10 == 1){
				xcolumn.getMails().remove(i);
			}
		}
		this.sendSGetMailList(0);
		return true;
	}
	
	/**
	 * 创建一个邮件信息
	 * @param sender
	 * @param title
	 * @param msg
	 * @param innerdropidlist
	 * @param items
	 * @param endtime
	 * @return
	 */
	public xbean.Mail createMail(String sender,String title,String msg,
			java.util.List<Integer> innerdropidlist,
			java.util.List<xbean.MailItem> items,long endtime,List<String> strList){
		xbean.Mail mail = xbean.Pod.newMail();
		mail.setSender(sender);
		mail.setTitle(title);
		mail.setMsg(msg);
		if(innerdropidlist != null)
			mail.getInnerdropidlist().addAll(innerdropidlist);
		if(items != null)
			mail.getItems().addAll(items);
		mail.setEndtime(endtime);
		mail.setIsopen(0);
		if(strList != null){
			mail.getStrlist().addAll(strList);
		}
		return mail;
	}
	/**
	 * 创建一个邮件信息
	 * @param sender
	 * @param title
	 * @param msg
	 * @param innerdropidlist
	 * @param objectId
	 * @param objectNum
	 * @param endtime
	 * @param strList
	 * @return
	 */
	public xbean.Mail createMail(String sender,String title,String msg,
			java.util.List<Integer> innerdropidlist,
			int objectId,int objectNum,long endtime,List<String> strList){
		xbean.Mail mail = xbean.Pod.newMail();
		mail.setSender(sender);
		mail.setTitle(title);
		mail.setMsg(msg);
		if(innerdropidlist != null)
			mail.getInnerdropidlist().addAll(innerdropidlist);

		List<xbean.MailItem> items = new LinkedList<xbean.MailItem>();
		xbean.MailItem test = xbean.Pod.newMailItem();
		test.setObjectid(objectId);
		test.setDropnum(objectNum);
		test.setDropparameter1(0);
		test.setDropparameter2(0);
		items.add(test);
		mail.getItems().addAll(items);
		
		mail.setEndtime(endtime);
		mail.setIsopen(0);
		if(strList != null){
			mail.getStrlist().addAll(strList);
		}
		return mail;
	}
	
	public void test(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		for(int i = 0;i< 38;i++){
		List<Integer> innerdropidlist = new LinkedList<Integer>();
		innerdropidlist.add(1);
		innerdropidlist.add(11);
		innerdropidlist.add(21);
		List<xbean.MailItem> items = new LinkedList<xbean.MailItem>();
		xbean.MailItem test = xbean.Pod.newMailItem();
		test.setObjectid(1400000002);
		test.setDropnum(200);
		xbean.MailItem test2 = xbean.Pod.newMailItem();
		test2.setObjectid(1400000001);
		test.setDropnum(100);
		xbean.MailItem test3 = xbean.Pod.newMailItem();
		test3.setObjectid(1403100173);
		test3.setDropnum(1);
		items.add(test3);
		items.add(test2);
		items.add(test);
		int title = i%2+2;
		int msg = i%2+4;
		addMail(createMail("mail_tips1","mail_tips"+title,"mail_tips"+msg,innerdropidlist,items,now+DEFAULT_TIME,
				null),false);
		}

	}
	
	/**
	 * 通过邮件key获得邮件数据
	 * @param mailkey
	 * @return
	 */
	public xbean.Mail getMByMkey(int mailkey){
		for(xbean.Mail xmail : xcolumn.getMails()){
			if(xmail.getKey() == mailkey)
				return xmail;
		}
		return null;		
	}
	
	/**
	 * 点开邮件或者领取邮件
	 * @param mailkey
	 * @param isget
	 * @return
	 */
	public boolean ReceiveMail(int mailkey,int isget){
		xbean.Mail xmail = this.getMByMkey(mailkey);
		if(xmail == null){
			return false;
		}
		boolean result = true;
		boolean isRefresh = false;
		if(xmail.getIsopen() % 10 != 1){
			xmail.setIsopen(xmail.getIsopen() / 10 * 10 + 1);
			isRefresh = true;
		}
		if( isget == 1 ){
			if(xmail.getIsopen() / 10 != 0){
				result = false;
			}else{
				int innerNum[] = DropManager.getInstance().getNumByinnerIdList(xmail.getInnerdropidlist());
				int itemNum[] = DropManager.getInstance().getNumByMailItemList(xmail.getItems());
				int hNum = innerNum[0] + itemNum[0];
				int iNum = innerNum[1] + itemNum[1];
				if( DropManager.getInstance().getIsFull(hNum, iNum, roleId,false) != 0 ){
					result = false;
				}else{
					xmail.setIsopen(xmail.getIsopen() % 10 + 10);
					DropManager.getInstance().innerIdListToDrop(xmail.getInnerdropidlist(), roleId, LogBehavior.MAILGET,false,false);
					for(xbean.MailItem mailitem : xmail.getItems()){
						DropManager.getInstance().dropAddByOther(mailitem.getObjectid(), mailitem.getDropnum(),
								mailitem.getDropparameter1(), mailitem.getDropparameter2(), roleId, LogBehavior.MAILGET);
					}
					isRefresh = true;
				}
			}
				
		}
		if(isRefresh){
			sSRefreshMail(xmail);
			this.sSendIsHaveNotOpen();
		}
		return result;
	}
	/**
	 * 刷新单个邮件
	 * @param xmail
	 */
	public void sSRefreshMail(xbean.Mail xmail){
		SRefreshMail snd = new SRefreshMail();
		snd.mail = this.getProtocolMail(xmail);
		xdb.Procedure.psend(roleId,snd);
	}
	/**
	 * 发送是否有未读邮件
	 */
	public void sSendIsHaveNotOpen(){
		for(xbean.Mail xmail : xcolumn.getMails()){
			if(xmail.getIsopen() % 10 == 0){
				xdb.Procedure.psend(roleId, new SIsHaveNotOpen(1));
				return;
			}
		}
		xdb.Procedure.psend(roleId, new SIsHaveNotOpen(0));
	}
	
}
