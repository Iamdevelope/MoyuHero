package chuhan.gsp.game;


public class sturntable implements mytools.ConvMain.Checkable ,Comparable<sturntable>{

	public int compareTo(sturntable o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sturntable(){
		super();
	}
	public sturntable(sturntable arg){
		this.id=arg.id ;
		this.tableid=arg.tableid ;
		this.tablepro=arg.tablepro ;
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
	public double tablepro  = 0.0  ;
	
	public double getTablepro(){
		return this.tablepro;
	}
	
	public void setTablepro(double v){
		this.tablepro=v;
	}
	
	
};