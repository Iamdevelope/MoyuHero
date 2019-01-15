package chuhan.gsp.task;


public class cweiboconfig implements mytools.ConvMain.Checkable ,Comparable<cweiboconfig>{

	public int compareTo(cweiboconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cweiboconfig(){
		super();
	}
	public cweiboconfig(cweiboconfig arg){
		this.id=arg.id ;
		this.msgid=arg.msgid ;
		this.msgtype=arg.msgtype ;
		this.forwardid=arg.forwardid ;
	}
	public void checkValid(java.util.Map<String,java.util.Map<Integer,? extends Object> > objs){
	}
	/**
	 * 
	 */
	public int id  = 0  ;
	
	public int getId(){
		return this.id;
	}
	
	public void setId(int v){
		this.id=v;
	}
	
	/**
	 * 
	 */
	public int msgid  = 0  ;
	
	public int getMsgid(){
		return this.msgid;
	}
	
	public void setMsgid(int v){
		this.msgid=v;
	}
	
	/**
	 * 
	 */
	public int msgtype  = 0  ;
	
	public int getMsgtype(){
		return this.msgtype;
	}
	
	public void setMsgtype(int v){
		this.msgtype=v;
	}
	
	/**
	 * 
	 */
	public String forwardid  = null  ;
	
	public String getForwardid(){
		return this.forwardid;
	}
	
	public void setForwardid(String v){
		this.forwardid=v;
	}
	
	
};