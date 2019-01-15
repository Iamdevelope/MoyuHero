package chuhan.gsp.task;


public class cchargeconfig implements mytools.ConvMain.Checkable ,Comparable<cchargeconfig>{

	public int compareTo(cchargeconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cchargeconfig(){
		super();
	}
	public cchargeconfig(cchargeconfig arg){
		this.id=arg.id ;
		this.yuanbao=arg.yuanbao ;
		this.items=arg.items ;
		this.text=arg.text ;
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
	
	/**
	 * 
	 */
	public String text  = null  ;
	
	public String getText(){
		return this.text;
	}
	
	public void setText(String v){
		this.text=v;
	}
	
	
};