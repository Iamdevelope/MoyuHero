package chuhan.gsp.task;


public class schargeconfig implements mytools.ConvMain.Checkable ,Comparable<schargeconfig>{

	public int compareTo(schargeconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public schargeconfig(){
		super();
	}
	public schargeconfig(schargeconfig arg){
		this.id=arg.id ;
		this.yuanbao=arg.yuanbao ;
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
	public java.util.ArrayList<Integer> yuanbao  ;
	
	public java.util.ArrayList<Integer> getYuanbao(){
		return this.yuanbao;
	}
	
	public void setYuanbao(java.util.ArrayList<Integer> v){
		this.yuanbao=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<Integer> items  ;
	
	public java.util.ArrayList<Integer> getItems(){
		return this.items;
	}
	
	public void setItems(java.util.ArrayList<Integer> v){
		this.items=v;
	}
	
	
};