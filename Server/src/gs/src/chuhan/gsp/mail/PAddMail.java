package chuhan.gsp.mail;

import chuhan.gsp.Dictionary;

public class PAddMail extends xdb.Procedure{
	private final long roleid;
	private final String sender;
	private final String msg;
	private final int num;
	private final int numtype;
	private final java.util.LinkedList<Integer> heroidlist;
	private final java.util.LinkedList<Integer> equipidlist;
	private final int giftid;
	private long endtime;
	
	
	public PAddMail(long roleid, String sender,String msg,int num,int numtype
			,java.util.LinkedList<Integer> heroidlist,
			java.util.LinkedList<Integer> equipidlist,
			int giftid,long endtime) {
		this.roleid = roleid;
		this.sender = sender;
		this.msg = msg;
		this.num = num;
		this.numtype = numtype;
		this.heroidlist = heroidlist;
		this.equipidlist = equipidlist;
		this.giftid = giftid;
		this.endtime = endtime;
	}
	
	public PAddMail(long roleid,int num,int numtype
			,java.util.LinkedList<Integer> heroidlist,
			java.util.LinkedList<Integer> equipidlist,
			int giftid,long endtime) {
		this.roleid = roleid;
		this.sender = Dictionary.SYS_TITLE;
		this.msg = Dictionary.SYS_DEFAULT;
		this.num = num;
		this.numtype = numtype;
		this.heroidlist = heroidlist;
		this.equipidlist = equipidlist;
		this.giftid = giftid;
		this.endtime = endtime;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		/*xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null)
		{
//			ErrorManager.getInstance().SendError(roleid, ErrorType.ERR_NOT_ONLINE);
			return false;
		}
		MailColumn mailcol = MailColumn.getMailColumn(roleid, false);

		if(endtime == 0)
		{
			long now = chuhan.gsp.main.GameTime.currentTimeMillis();
			endtime = now + MailColumn.DEFAULT_TIME;
		}
		xbean.Mail xmail = mailcol.createMail(sender, msg,num,numtype,heroidlist,
				equipidlist,giftid,endtime);
		if(xmail == null)
			return false;
		
		mailcol.addMail(xmail);

		java.util.LinkedList<chuhan.gsp.Mail> maillist = new java.util.LinkedList<chuhan.gsp.Mail>();
		maillist.addFirst(mailcol.getProtocolMail(xmail));*/
		
		psendWhileCommit(roleid, new SIsHaveNotOpen(1));
				
		return true;
	}
	
}
