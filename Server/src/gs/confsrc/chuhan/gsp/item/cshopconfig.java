package chuhan.gsp.item;


public class cshopconfig implements mytools.ConvMain.Checkable ,Comparable<cshopconfig>{

	public int compareTo(cshopconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cshopconfig(){
		super();
	}
	public cshopconfig(cshopconfig arg){
		this.id=arg.id ;
		this.itemid=arg.itemid ;
		this.shoptype=arg.shoptype ;
		this.name=arg.name ;
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
	public int shoptype  = 0  ;
	
	public int getShoptype(){
		return this.shoptype;
	}
	
	public void setShoptype(int v){
		this.shoptype=v;
	}
	
	/**
	 * 
	 */
	public String name  = null  ;
	
	public String getName(){
		return this.name;
	}
	
	public void setName(String v){
		this.name=v;
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