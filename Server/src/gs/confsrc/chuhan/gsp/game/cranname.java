package chuhan.gsp.game;


public class cranname implements mytools.ConvMain.Checkable ,Comparable<cranname>{

	public int compareTo(cranname o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cranname(){
		super();
	}
	public cranname(cranname arg){
		this.id=arg.id ;
		this.first=arg.first ;
		this.middle=arg.middle ;
		this.last=arg.last ;
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
	public String first  = null  ;
	
	public String getFirst(){
		return this.first;
	}
	
	public void setFirst(String v){
		this.first=v;
	}
	
	/**
	 * 
	 */
	public String middle  = null  ;
	
	public String getMiddle(){
		return this.middle;
	}
	
	public void setMiddle(String v){
		this.middle=v;
	}
	
	/**
	 * 
	 */
	public String last  = null  ;
	
	public String getLast(){
		return this.last;
	}
	
	public void setLast(String v){
		this.last=v;
	}
	
	
};