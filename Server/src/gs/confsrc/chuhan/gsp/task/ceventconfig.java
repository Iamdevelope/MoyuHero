package chuhan.gsp.task;


public class ceventconfig implements mytools.ConvMain.Checkable ,Comparable<ceventconfig>{

	public int compareTo(ceventconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ceventconfig(){
		super();
	}
	public ceventconfig(ceventconfig arg){
		this.id=arg.id ;
		this.type=arg.type ;
		this.icon=arg.icon ;
		this.color=arg.color ;
		this.sort=arg.sort ;
		this.start=arg.start ;
		this.end=arg.end ;
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
	public int icon  = 0  ;
	
	public int getIcon(){
		return this.icon;
	}
	
	public void setIcon(int v){
		this.icon=v;
	}
	
	/**
	 * 
	 */
	public int color  = 0  ;
	
	public int getColor(){
		return this.color;
	}
	
	public void setColor(int v){
		this.color=v;
	}
	
	/**
	 * 
	 */
	public int sort  = 0  ;
	
	public int getSort(){
		return this.sort;
	}
	
	public void setSort(int v){
		this.sort=v;
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
	
	
};