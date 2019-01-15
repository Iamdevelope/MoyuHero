package chuhan.gsp.game;


public class cTurntableAwardconfig implements mytools.ConvMain.Checkable ,Comparable<cTurntableAwardconfig>{

	public int compareTo(cTurntableAwardconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cTurntableAwardconfig(){
		super();
	}
	public cTurntableAwardconfig(cTurntableAwardconfig arg){
		this.id=arg.id ;
		this.tableid=arg.tableid ;
		this.itemcheck=arg.itemcheck ;
		this.itemid=arg.itemid ;
		this.itemnum=arg.itemnum ;
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
	public int itemid  = 0  ;
	
	public int getItemid(){
		return this.itemid;
	}
	
	public void setItemid(int v){
		this.itemid=v;
	}
	
	/**
	 * 
	 */
	public int itemnum  = 0  ;
	
	public int getItemnum(){
		return this.itemnum;
	}
	
	public void setItemnum(int v){
		this.itemnum=v;
	}
	
	
};