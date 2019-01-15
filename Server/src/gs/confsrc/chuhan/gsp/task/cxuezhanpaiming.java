package chuhan.gsp.task;


public class cxuezhanpaiming implements mytools.ConvMain.Checkable ,Comparable<cxuezhanpaiming>{

	public int compareTo(cxuezhanpaiming o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cxuezhanpaiming(){
		super();
	}
	public cxuezhanpaiming(cxuezhanpaiming arg){
		this.id=arg.id ;
		this.describe=arg.describe ;
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
	public String describe  = null  ;
	
	public String getDescribe(){
		return this.describe;
	}
	
	public void setDescribe(String v){
		this.describe=v;
	}
	
	
};