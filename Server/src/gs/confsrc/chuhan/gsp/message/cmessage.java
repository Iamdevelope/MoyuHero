package chuhan.gsp.message;


public class cmessage implements mytools.ConvMain.Checkable ,Comparable<cmessage>{

	public int compareTo(cmessage o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cmessage(){
		super();
	}
	public cmessage(cmessage arg){
		this.id=arg.id ;
		this.type=arg.type ;
		this.msg=arg.msg ;
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
	public int type  = 0  ;
	
	public int getType(){
		return this.type;
	}
	
	public void setType(int v){
		this.type=v;
	}
	
	/**
	 * 
	 */
	public String msg  = null  ;
	
	public String getMsg(){
		return this.msg;
	}
	
	public void setMsg(String v){
		this.msg=v;
	}
	
	
};