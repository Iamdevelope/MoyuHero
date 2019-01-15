package chuhan.gsp.game;


public class svipstore implements mytools.ConvMain.Checkable ,Comparable<svipstore>{

	public int compareTo(svipstore o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public svipstore(){
		super();
	}
	public svipstore(svipstore arg){
		this.id=arg.id ;
		this.vip=arg.vip ;
		this.item=arg.item ;
		this.type=arg.type ;
		this.num=arg.num ;
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
	public int vip  = 0  ;
	
	public int getVip(){
		return this.vip;
	}
	
	public void setVip(int v){
		this.vip=v;
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
	public int num  = 0  ;
	
	public int getNum(){
		return this.num;
	}
	
	public void setNum(int v){
		this.num=v;
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