package chuhan.gsp.task;


public class cpiclink implements mytools.ConvMain.Checkable ,Comparable<cpiclink>{

	public int compareTo(cpiclink o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cpiclink(){
		super();
	}
	public cpiclink(cpiclink arg){
		this.id=arg.id ;
		this.picid=arg.picid ;
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
	public String picid  = null  ;
	
	public String getPicid(){
		return this.picid;
	}
	
	public void setPicid(String v){
		this.picid=v;
	}
	
	
};