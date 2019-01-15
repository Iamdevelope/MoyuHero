package chuhan.gsp.attr;


public class SAttributeDesConfig implements mytools.ConvMain.Checkable ,Comparable<SAttributeDesConfig>{

	public int compareTo(SAttributeDesConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SAttributeDesConfig(){
		super();
	}
	public SAttributeDesConfig(SAttributeDesConfig arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.init=arg.init ;
		this.numid=arg.numid ;
		this.numName=arg.numName ;
		this.percentid=arg.percentid ;
		this.pctName=arg.pctName ;
	}
	public void checkValid(java.util.Map<String,java.util.Map<Integer,? extends Object> > objs){
	}
	/**
	 * id
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
	public int init  = 0  ;
	
	public int getInit(){
		return this.init;
	}
	
	public void setInit(int v){
		this.init=v;
	}
	
	/**
	 * 
	 */
	public int numid  = 0  ;
	
	public int getNumid(){
		return this.numid;
	}
	
	public void setNumid(int v){
		this.numid=v;
	}
	
	/**
	 * 
	 */
	public String numName  = null  ;
	
	public String getNumName(){
		return this.numName;
	}
	
	public void setNumName(String v){
		this.numName=v;
	}
	
	/**
	 * 
	 */
	public int percentid  = 0  ;
	
	public int getPercentid(){
		return this.percentid;
	}
	
	public void setPercentid(int v){
		this.percentid=v;
	}
	
	/**
	 * 
	 */
	public String pctName  = null  ;
	
	public String getPctName(){
		return this.pctName;
	}
	
	public void setPctName(String v){
		this.pctName=v;
	}
	
	
};