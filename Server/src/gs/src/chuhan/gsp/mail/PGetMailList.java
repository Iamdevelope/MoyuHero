package chuhan.gsp.mail;

import chuhan.gsp.MsgType;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.msg.Message;

public class PGetMailList extends xdb.Procedure{
	private final long roleid;	
	private final int mailsize; // 从第几个开始往后取20个
	
	public PGetMailList(long roleid,int mailsize) {
		this.roleid = roleid;
		this.mailsize = mailsize;

	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null)
		{
//			ErrorManager.getInstance().SendError(roleid, ErrorType.ERR_NOT_ONLINE);
			return false;
		}
		MailColumn mailcol = MailColumn.getMailColumn(roleid, false);
		mailcol.sendSGetMailList(mailsize);
//		psendWhileCommit(roleid, new SGetMailList(mailcol.getProtocolMailList(mailsize),mailsize));
				
		return true;
	}
	
}
