package chuhan.gsp.task;


public class cxuezhanxunhuan implements mytools.ConvMain.Checkable ,Comparable<cxuezhanxunhuan>{

	public int compareTo(cxuezhanxunhuan o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cxuezhanxunhuan(){
		super();
	}
	public cxuezhanxunhuan(cxuezhanxunhuan arg){
		this.id=arg.id ;
		this.item=arg.item ;
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
	public int item  = 0  ;
	
	public int getItem(){
		return this.item;
	}
	
	public void setItem(int v){
		this.item=v;
	}
	
	/**
	 * 
	 */
	public int price  = 0  ;
	
	public int getPrice(){
		return this.price;
	}
	
	public void setPrice(int v){
		this.price=v;
	}
	
	
};