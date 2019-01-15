package chuhan.gsp.mail;

import chuhan.gsp.MsgType;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.msg.Message;

public class PReceiveMail extends xdb.Procedure{
	private final long roleid;
	private final int mailkey;
	private final int isget; // 是否领取附件

	
	
	public PReceiveMail(long roleid,int mailkey,int isget) {
		this.roleid = roleid;
		this.mailkey = mailkey;
		this.isget = isget;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
//			ErrorManager.getInstance().SendError(roleid, ErrorType.ERR_NOT_ONLINE);
			return false;
		}
		MailColumn mailcol = MailColumn.getMailColumn(roleid, false);		
		boolean result = mailcol.ReceiveMail(mailkey,isget);
		SReceiveMail send = new SReceiveMail();
		send.isget = isget;
		if(!result){
			send.result = SReceiveMail.END_ERROR;
		}else{
			send.result = SReceiveMail.END_OK;	
		}
		psend(roleid, send);			
		return result;
	}
	
}
