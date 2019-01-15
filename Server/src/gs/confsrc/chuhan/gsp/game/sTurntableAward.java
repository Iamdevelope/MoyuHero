package chuhan.gsp.game;


public class sTurntableAward implements mytools.ConvMain.Checkable ,Comparable<sTurntableAward>{

	public int compareTo(sTurntableAward o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sTurntableAward(){
		super();
	}
	public sTurntableAward(sTurntableAward arg){
		this.id=arg.id ;
		this.tableid=arg.tableid ;
		this.itemcheck=arg.itemcheck ;
		this.post=arg.post ;
		this.award=arg.award ;
		this.postname=arg.postname ;
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
	public int tableid  = 0  ;
	
	public int getTableid(){
		return this.tableid;
	}
	
	public void setTableid(int v){
		this.tableid=v;
	}
	
	/**
	 * 
	 */
	public int itemcheck  = 0  ;
	
	public int getItemcheck(){
		return this.itemcheck;
	}
	
	public void setItemcheck(int v){
		this.itemcheck=v;
	}
	
	/**
	 * 
	 */
	public int post  = 0  ;
	
	public int getPost(){
		return this.post;
	}
	
	public void setPost(int v){
		this.post=v;
	}
	
	/**
	 * 
	 */
	public int award  = 0  ;
	
	public int getAward(){
		return this.award;
	}
	
	public void setAward(int v){
		this.award=v;
	}
	
	/**
	 * 
	 */
	public String postname  = null  ;
	
	public String getPostname(){
		return this.postname;
	}
	
	public void setPostname(String v){
		this.postname=v;
	}
	
	
};