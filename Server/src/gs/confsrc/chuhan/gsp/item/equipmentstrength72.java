package chuhan.gsp.item;


public class equipmentstrength72 implements mytools.ConvMain.Checkable ,Comparable<equipmentstrength72>{

	public int compareTo(equipmentstrength72 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public equipmentstrength72(){
		super();
	}
	public equipmentstrength72(equipmentstrength72 arg){
		this.id=arg.id ;
		this.Qosition=arg.Qosition ;
		this.Parts=arg.Parts ;
		this.Level=arg.Level ;
		this.sthequipmentlevel=arg.sthequipmentlevel ;
		this.propid=arg.propid ;
		this.numbers=arg.numbers ;
		this.propid2=arg.propid2 ;
		this.numbers2=arg.numbers2 ;
		this.attribute=arg.attribute ;
		this.value=arg.value ;
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
	public int Parts  = 0  ;
	
	public int getParts(){
		return this.Parts;
	}
	
	public void setParts(int v){
		this.Parts=v;
	}
	
	/**
	 * 
	 */
	public int Level  = 0  ;
	
	public int getLevel(){
		return this.Level;
	}
	
	public void setLevel(int v){
		this.Level=v;
	}
	
	/**
	 * 
	 */
	public int sthequipmentlevel  = 0  ;
	
	public int getSthequipmentlevel(){
		return this.sthequipmentlevel;
	}
	
	public void setSthequipmentlevel(int v){
		this.sthequipmentlevel=v;
	}
	
	/**
	 * 
	 */
	public String propid  = null  ;
	
	public String getPropid(){
		return this.propid;
	}
	
	public void setPropid(String v){
		this.propid=v;
	}
	
	/**
	 * 
	 */
	public String numbers  = null  ;
	
	public String getNumbers(){
		return this.numbers;
	}
	
	public void setNumbers(String v){
		this.numbers=v;
	}
	
	/**
	 * 
	 */
	public String propid2  = null  ;
	
	public String getPropid2(){
		return this.propid2;
	}
	
	public void setPropid2(String v){
		this.propid2=v;
	}
	
	/**
	 * 
	 */
	public String numbers2  = null  ;
	
	public String getNumbers2(){
		return this.numbers2;
	}
	
	public void setNumbers2(String v){
		this.numbers2=v;
	}
	
	/**
	 * 
	 */
	public String attribute  = null  ;
	
	public String getAttribute(){
		return this.attribute;
	}
	
	public void setAttribute(String v){
		this.attribute=v;
	}
	
	/**
	 * 
	 */
	public String value  = null  ;
	
	public String getValue(){
		return this.value;
	}
	
	public void setValue(String v){
		this.value=v;
	}
	
	
};