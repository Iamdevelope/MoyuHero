package chuhan.gsp.item;


public class heroculture70 implements mytools.ConvMain.Checkable ,Comparable<heroculture70>{

	public int compareTo(heroculture70 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public heroculture70(){
		super();
	}
	public heroculture70(heroculture70 arg){
		this.id=arg.id ;
		this.Born=arg.Born ;
		this.Qosition=arg.Qosition ;
		this.Element=arg.Element ;
		this.ElementLeve=arg.ElementLeve ;
		this.Consumption=arg.Consumption ;
		this.Number=arg.Number ;
		this.Attribute=arg.Attribute ;
		this.Value=arg.Value ;
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
	public int Born  = 0  ;
	
	public int getBorn(){
		return this.Born;
	}
	
	public void setBorn(int v){
		this.Born=v;
	}
	
	/**
	 * 
	 */
	public int Qosition  = 0  ;
	
	public int getQosition(){
		return this.Qosition;
	}
	
	public void setQosition(int v){
		this.Qosition=v;
	}
	
	/**
	 * 
	 */
	public int Element  = 0  ;
	
	public int getElement(){
		return this.Element;
	}
	
	public void setElement(int v){
		this.Element=v;
	}
	
	/**
	 * 
	 */
	public int ElementLeve  = 0  ;
	
	public int getElementLeve(){
		return this.ElementLeve;
	}
	
	public void setElementLeve(int v){
		this.ElementLeve=v;
	}
	
	/**
	 * 
	 */
	public int Consumption  = 0  ;
	
	public int getConsumption(){
		return this.Consumption;
	}
	
	public void setConsumption(int v){
		this.Consumption=v;
	}
	
	/**
	 * 
	 */
	public int Number  = 0  ;
	
	public int getNumber(){
		return this.Number;
	}
	
	public void setNumber(int v){
		this.Number=v;
	}
	
	/**
	 * 
	 */
	public String Attribute  = null  ;
	
	public String getAttribute(){
		return this.Attribute;
	}
	
	public void setAttribute(String v){
		this.Attribute=v;
	}
	
	/**
	 * 
	 */
	public String Value  = null  ;
	
	public String getValue(){
		return this.Value;
	}
	
	public void setValue(String v){
		this.Value=v;
	}
	
	
};