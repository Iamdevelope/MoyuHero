package chuhan.gsp.task;


public class shuifuconfig implements mytools.ConvMain.Checkable ,Comparable<shuifuconfig>{

	public int compareTo(shuifuconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public shuifuconfig(){
		super();
	}
	public shuifuconfig(shuifuconfig arg){
		this.id=arg.id ;
		this.price=arg.price ;
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
	public java.util.ArrayList<Integer> price  ;
	
	public java.util.ArrayList<Integer> getPrice(){
		return this.price;
	}
	
	public void setPrice(java.util.ArrayList<Integer> v){
		this.price=v;
	}
	
	
};