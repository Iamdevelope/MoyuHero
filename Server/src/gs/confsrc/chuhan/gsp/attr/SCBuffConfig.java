package chuhan.gsp.attr;


public class SCBuffConfig implements mytools.ConvMain.Checkable ,Comparable<SCBuffConfig>{

	public int compareTo(SCBuffConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SCBuffConfig(){
		super();
	}
	public SCBuffConfig(SCBuffConfig arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.classname=arg.classname ;
		this.clearType=arg.clearType ;
		this.buffclass=arg.buffclass ;
		this.Anticlass=arg.Anticlass ;
		this.sendtoclient=arg.sendtoclient ;
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
	public String classname  = null  ;
	
	public String getClassname(){
		return this.classname;
	}
	
	public void setClassname(String v){
		this.classname=v;
	}
	
	/**
	 * 
	 */
	public int clearType  = 0  ;
	
	public int getClearType(){
		return this.clearType;
	}
	
	public void setClearType(int v){
		this.clearType=v;
	}
	
	/**
	 * 
	 */
	public int buffclass  = 0  ;
	
	public int getBuffclass(){
		return this.buffclass;
	}
	
	public void setBuffclass(int v){
		this.buffclass=v;
	}
	
	/**
	 * 
	 */
	public String Anticlass  = null  ;
	
	public String getAnticlass(){
		return this.Anticlass;
	}
	
	public void setAnticlass(String v){
		this.Anticlass=v;
	}
	
	/**
	 * 
	 */
	public int sendtoclient  = 0  ;
	
	public int getSendtoclient(){
		return this.sendtoclient;
	}
	
	public void setSendtoclient(int v){
		this.sendtoclient=v;
	}
	
	
};