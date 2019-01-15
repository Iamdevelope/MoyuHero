package chuhan.gsp.gm;

import org.apache.log4j.Logger;

import chuhan.gsp.msg.Message;

public abstract class GMCommand {
	static class FormatError extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	protected final static String Prefix = "<T t=\"";
	protected final static String Suffix = "\"></T><B></B>";
	protected static Logger logger = Logger.getLogger(GMCommand.class);

	private long gmroleid;
	
	private int gmUserid;
	private int localsid;
	
	public void setGmroleid(long gmroleid) {
		this.gmroleid = gmroleid;
	}

	public long getGmroleid() {
		return gmroleid;
	}

	abstract boolean exec(String[] args);
	abstract String usage();
	
	public boolean sendToGM(String str){
		if(gmroleid<=0)
			return false;
		Message.sendMsgNotify(gmroleid, Message.EMPTY_MSG_ID,str);
		return true;
	}

	
	public int getGmUserid() {
	
		return gmUserid;
	}

	
	public void setGmUserid(int gmUserid) {
	
		this.gmUserid = gmUserid;
	}
	
	public void setGmLocalsid(int localsid){
		this.localsid = localsid;
	}
	
	public int getGmLocalsid(){
		return localsid;
	}
}
