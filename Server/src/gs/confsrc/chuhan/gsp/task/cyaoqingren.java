package chuhan.gsp.task;


public class cyaoqingren implements mytools.ConvMain.Checkable ,Comparable<cyaoqingren>{

	public int compareTo(cyaoqingren o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cyaoqingren(){
		super();
	}
	public cyaoqingren(cyaoqingren arg){
		this.id=arg.id ;
		this.items=arg.items ;
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
	public int items  = 0  ;
	
	public int getItems(){
		return this.items;
	}
	
	public void setItems(int v){
		this.items=v;
	}
	
	
};