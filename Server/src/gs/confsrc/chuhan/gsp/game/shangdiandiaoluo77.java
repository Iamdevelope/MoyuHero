package chuhan.gsp.game;


public class shangdiandiaoluo77 implements mytools.ConvMain.Checkable ,Comparable<shangdiandiaoluo77>{

	public int compareTo(shangdiandiaoluo77 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public shangdiandiaoluo77(){
		super();
	}
	public shangdiandiaoluo77(shangdiandiaoluo77 arg){
		this.id=arg.id ;
		this.DropId=arg.DropId ;
		this.SmallLeve=arg.SmallLeve ;
		this.BigLeve=arg.BigLeve ;
		this.ItemId=arg.ItemId ;
		this.Weight=arg.Weight ;
		this.consumerzy=arg.consumerzy ;
		this.Price=arg.Price ;
		this.Number=arg.Number ;
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
	public int DropId  = 0  ;
	
	public int getDropId(){
		return this.DropId;
	}
	
	public void setDropId(int v){
		this.DropId=v;
	}
	
	/**
	 * 
	 */
	public int SmallLeve  = 0  ;
	
	public int getSmallLeve(){
		return this.SmallLeve;
	}
	
	public void setSmallLeve(int v){
		this.SmallLeve=v;
	}
	
	/**
	 * 
	 */
	public int BigLeve  = 0  ;
	
	public int getBigLeve(){
		return this.BigLeve;
	}
	
	public void setBigLeve(int v){
		this.BigLeve=v;
	}
	
	/**
	 * 
	 */
	public String ItemId  = null  ;
	
	public String getItemId(){
		return this.ItemId;
	}
	
	public void setItemId(String v){
		this.ItemId=v;
	}
	
	/**
	 * 
	 */
	public String Weight  = null  ;
	
	public String getWeight(){
		return this.Weight;
	}
	
	public void setWeight(String v){
		this.Weight=v;
	}
	
	/**
	 * 
	 */
	public String consumerzy  = null  ;
	
	public String getConsumerzy(){
		return this.consumerzy;
	}
	
	public void setConsumerzy(String v){
		this.consumerzy=v;
	}
	
	/**
	 * 
	 */
	public String Price  = null  ;
	
	public String getPrice(){
		return this.Price;
	}
	
	public void setPrice(String v){
		this.Price=v;
	}
	
	/**
	 * 
	 */
	public String Number  = null  ;
	
	public String getNumber(){
		return this.Number;
	}
	
	public void setNumber(String v){
		this.Number=v;
	}
	
	
};