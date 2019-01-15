package chuhan.gsp.game;


public class cTurntableRull implements mytools.ConvMain.Checkable ,Comparable<cTurntableRull>{

	public int compareTo(cTurntableRull o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cTurntableRull(){
		super();
	}
	public cTurntableRull(cTurntableRull arg){
		this.id=arg.id ;
		this.tableid=arg.tableid ;
		this.type=arg.type ;
		this.times=arg.times ;
		this.cost=arg.cost ;
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
	public int times  = 0  ;
	
	public int getTimes(){
		return this.times;
	}
	
	public void setTimes(int v){
		this.times=v;
	}
	
	/**
	 * 
	 */
	public int cost  = 0  ;
	
	public int getCost(){
		return this.cost;
	}
	
	public void setCost(int v){
		this.cost=v;
	}
	
	
};