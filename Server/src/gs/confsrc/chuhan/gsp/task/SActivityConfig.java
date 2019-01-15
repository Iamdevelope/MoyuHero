package chuhan.gsp.task;


public class SActivityConfig implements mytools.ConvMain.Checkable ,Comparable<SActivityConfig>{

	public int compareTo(SActivityConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SActivityConfig(){
		super();
	}
	public SActivityConfig(SActivityConfig arg){
		this.id=arg.id ;
		this.type=arg.type ;
		this.start=arg.start ;
		this.end=arg.end ;
		this.levelmin=arg.levelmin ;
		this.levelmax=arg.levelmax ;
		this.createmin=arg.createmin ;
		this.createmax=arg.createmax ;
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
	public String start  = null  ;
	
	public String getStart(){
		return this.start;
	}
	
	public void setStart(String v){
		this.start=v;
	}
	
	/**
	 * 
	 */
	public String end  = null  ;
	
	public String getEnd(){
		return this.end;
	}
	
	public void setEnd(String v){
		this.end=v;
	}
	
	/**
	 * 
	 */
	public int levelmin  = 0  ;
	
	public int getLevelmin(){
		return this.levelmin;
	}
	
	public void setLevelmin(int v){
		this.levelmin=v;
	}
	
	/**
	 * 
	 */
	public int levelmax  = 0  ;
	
	public int getLevelmax(){
		return this.levelmax;
	}
	
	public void setLevelmax(int v){
		this.levelmax=v;
	}
	
	/**
	 * 
	 */
	public int createmin  = 0  ;
	
	public int getCreatemin(){
		return this.createmin;
	}
	
	public void setCreatemin(int v){
		this.createmin=v;
	}
	
	/**
	 * 
	 */
	public int createmax  = 0  ;
	
	public int getCreatemax(){
		return this.createmax;
	}
	
	public void setCreatemax(int v){
		this.createmax=v;
	}
	
	
};