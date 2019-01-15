package chuhan.gsp.attr;


public class attributetrain32 implements mytools.ConvMain.Checkable ,Comparable<attributetrain32>{

	public int compareTo(attributetrain32 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public attributetrain32(){
		super();
	}
	public attributetrain32(attributetrain32 arg){
		this.id=arg.id ;
		this.bagId=arg.bagId ;
		this.times=arg.times ;
		this.attriType=arg.attriType ;
		this.attriValue=arg.attriValue ;
		this.costType=arg.costType ;
		this.cost=arg.cost ;
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
	public int bagId  = 0  ;
	
	public int getBagId(){
		return this.bagId;
	}
	
	public void setBagId(int v){
		this.bagId=v;
	}
	
	/**
	 * 
	 */
	public int times  = 0  ;
	
	public int getTimes(){
		return this.times;
	}
	
	public void setTimes(int v){
		this.times=v;
	}
	
	/**
	 * 
	 */
	public int attriType  = 0  ;
	
	public int getAttriType(){
		return this.attriType;
	}
	
	public void setAttriType(int v){
		this.attriType=v;
	}
	
	/**
	 * 
	 */
	public int attriValue  = 0  ;
	
	public int getAttriValue(){
		return this.attriValue;
	}
	
	public void setAttriValue(int v){
		this.attriValue=v;
	}
	
	/**
	 * 
	 */
	public int costType  = 0  ;
	
	public int getCostType(){
		return this.costType;
	}
	
	public void setCostType(int v){
		this.costType=v;
	}
	
	/**
	 * 
	 */
	public int cost  = 0  ;
	
	public int getCost(){
		return this.cost;
	}
	
	public void setCost(int v){
		this.cost=v;
	}
	
	
};