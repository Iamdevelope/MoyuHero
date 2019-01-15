package chuhan.gsp.game;


public class exchangecode38 implements mytools.ConvMain.Checkable ,Comparable<exchangecode38>{

	public int compareTo(exchangecode38 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public exchangecode38(){
		super();
	}
	public exchangecode38(exchangecode38 arg){
		this.id=arg.id ;
		this.startdate=arg.startdate ;
		this.deadline=arg.deadline ;
		this.exchangetimes=arg.exchangetimes ;
		this.normaldropid=arg.normaldropid ;
		this.typenum=arg.typenum ;
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
	public String startdate  = null  ;
	
	public String getStartdate(){
		return this.startdate;
	}
	
	public void setStartdate(String v){
		this.startdate=v;
	}
	
	/**
	 * 
	 */
	public String deadline  = null  ;
	
	public String getDeadline(){
		return this.deadline;
	}
	
	public void setDeadline(String v){
		this.deadline=v;
	}
	
	/**
	 * 
	 */
	public int exchangetimes  = 0  ;
	
	public int getExchangetimes(){
		return this.exchangetimes;
	}
	
	public void setExchangetimes(int v){
		this.exchangetimes=v;
	}
	
	/**
	 * 
	 */
	public int normaldropid  = 0  ;
	
	public int getNormaldropid(){
		return this.normaldropid;
	}
	
	public void setNormaldropid(int v){
		this.normaldropid=v;
	}
	
	/**
	 * 
	 */
	public int typenum  = 0  ;
	
	public int getTypenum(){
		return this.typenum;
	}
	
	public void setTypenum(int v){
		this.typenum=v;
	}
	
	
};