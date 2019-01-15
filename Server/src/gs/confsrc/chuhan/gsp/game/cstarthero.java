package chuhan.gsp.game;


public class cstarthero implements mytools.ConvMain.Checkable ,Comparable<cstarthero>{

	public int compareTo(cstarthero o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cstarthero(){
		super();
	}
	public cstarthero(cstarthero arg){
		this.id=arg.id ;
		this.heroid=arg.heroid ;
		this.name=arg.name ;
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
	public int heroid  = 0  ;
	
	public int getHeroid(){
		return this.heroid;
	}
	
	public void setHeroid(int v){
		this.heroid=v;
	}
	
	/**
	 * 
	 */
	public String name  = null  ;
	
	public String getName(){
		return this.name;
	}
	
	public void setName(String v){
		this.name=v;
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