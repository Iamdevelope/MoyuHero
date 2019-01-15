package chuhan.gsp.item;


public class innerdrop16 implements mytools.ConvMain.Checkable ,Comparable<innerdrop16>{

	public int compareTo(innerdrop16 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public innerdrop16(){
		super();
	}
	public innerdrop16(innerdrop16 arg){
		this.id=arg.id ;
		this.innerdropid=arg.innerdropid ;
		this.innerdroptype=arg.innerdroptype ;
		this.innerdroptime=arg.innerdroptime ;
		this.dropwight=arg.dropwight ;
		this.objectid=arg.objectid ;
		this.dropnum=arg.dropnum ;
		this.dropparameter1=arg.dropparameter1 ;
		this.dropparameter2=arg.dropparameter2 ;
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
	public int innerdropid  = 0  ;
	
	public int getInnerdropid(){
		return this.innerdropid;
	}
	
	public void setInnerdropid(int v){
		this.innerdropid=v;
	}
	
	/**
	 * 
	 */
	public int innerdroptype  = 0  ;
	
	public int getInnerdroptype(){
		return this.innerdroptype;
	}
	
	public void setInnerdroptype(int v){
		this.innerdroptype=v;
	}
	
	/**
	 * 
	 */
	public int innerdroptime  = 0  ;
	
	public int getInnerdroptime(){
		return this.innerdroptime;
	}
	
	public void setInnerdroptime(int v){
		this.innerdroptime=v;
	}
	
	/**
	 * 
	 */
	public int dropwight  = 0  ;
	
	public int getDropwight(){
		return this.dropwight;
	}
	
	public void setDropwight(int v){
		this.dropwight=v;
	}
	
	/**
	 * 
	 */
	public int objectid  = 0  ;
	
	public int getObjectid(){
		return this.objectid;
	}
	
	public void setObjectid(int v){
		this.objectid=v;
	}
	
	/**
	 * 
	 */
	public int dropnum  = 0  ;
	
	public int getDropnum(){
		return this.dropnum;
	}
	
	public void setDropnum(int v){
		this.dropnum=v;
	}
	
	/**
	 * 
	 */
	public int dropparameter1  = 0  ;
	
	public int getDropparameter1(){
		return this.dropparameter1;
	}
	
	public void setDropparameter1(int v){
		this.dropparameter1=v;
	}
	
	/**
	 * 
	 */
	public int dropparameter2  = 0  ;
	
	public int getDropparameter2(){
		return this.dropparameter2;
	}
	
	public void setDropparameter2(int v){
		this.dropparameter2=v;
	}
	
	
};