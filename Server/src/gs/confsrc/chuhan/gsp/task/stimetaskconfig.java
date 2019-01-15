package chuhan.gsp.task;


public class stimetaskconfig implements mytools.ConvMain.Checkable ,Comparable<stimetaskconfig>{

	public int compareTo(stimetaskconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public stimetaskconfig(){
		super();
	}
	public stimetaskconfig(stimetaskconfig arg){
		this.id=arg.id ;
		this.vip=arg.vip ;
		this.time=arg.time ;
		this.grow=arg.grow ;
		this.pricetype=arg.pricetype ;
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
	public int time  = 0  ;
	
	public int getTime(){
		return this.time;
	}
	
	public void setTime(int v){
		this.time=v;
	}
	
	/**
	 * 
	 */
	public int grow  = 0  ;
	
	public int getGrow(){
		return this.grow;
	}
	
	public void setGrow(int v){
		this.grow=v;
	}
	
	/**
	 * 
	 */
	public int pricetype  = 0  ;
	
	public int getPricetype(){
		return this.pricetype;
	}
	
	public void setPricetype(int v){
		this.pricetype=v;
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