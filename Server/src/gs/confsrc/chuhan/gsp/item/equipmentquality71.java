package chuhan.gsp.item;


public class equipmentquality71 implements mytools.ConvMain.Checkable ,Comparable<equipmentquality71>{

	public int compareTo(equipmentquality71 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public equipmentquality71(){
		super();
	}
	public equipmentquality71(equipmentquality71 arg){
		this.id=arg.id ;
		this.Name=arg.Name ;
		this.Icon=arg.Icon ;
		this.Qosition=arg.Qosition ;
		this.Parts=arg.Parts ;
		this.QialityColor=arg.QialityColor ;
		this.QualityLevel=arg.QualityLevel ;
		this.reqlevel=arg.reqlevel ;
		this.PropId=arg.PropId ;
		this.numbers=arg.numbers ;
		this.NextId=arg.NextId ;
		this.QualityAttribute=arg.QualityAttribute ;
		this.Numerical=arg.Numerical ;
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
	public String Name  = null  ;
	
	public String getName(){
		return this.Name;
	}
	
	public void setName(String v){
		this.Name=v;
	}
	
	/**
	 * 
	 */
	public String Icon  = null  ;
	
	public String getIcon(){
		return this.Icon;
	}
	
	public void setIcon(String v){
		this.Icon=v;
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
	public int QialityColor  = 0  ;
	
	public int getQialityColor(){
		return this.QialityColor;
	}
	
	public void setQialityColor(int v){
		this.QialityColor=v;
	}
	
	/**
	 * 
	 */
	public int QualityLevel  = 0  ;
	
	public int getQualityLevel(){
		return this.QualityLevel;
	}
	
	public void setQualityLevel(int v){
		this.QualityLevel=v;
	}
	
	/**
	 * 
	 */
	public int reqlevel  = 0  ;
	
	public int getReqlevel(){
		return this.reqlevel;
	}
	
	public void setReqlevel(int v){
		this.reqlevel=v;
	}
	
	/**
	 * 
	 */
	public String PropId  = null  ;
	
	public String getPropId(){
		return this.PropId;
	}
	
	public void setPropId(String v){
		this.PropId=v;
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
	public int NextId  = 0  ;
	
	public int getNextId(){
		return this.NextId;
	}
	
	public void setNextId(int v){
		this.NextId=v;
	}
	
	/**
	 * 
	 */
	public String QualityAttribute  = null  ;
	
	public String getQualityAttribute(){
		return this.QualityAttribute;
	}
	
	public void setQualityAttribute(String v){
		this.QualityAttribute=v;
	}
	
	/**
	 * 
	 */
	public String Numerical  = null  ;
	
	public String getNumerical(){
		return this.Numerical;
	}
	
	public void setNumerical(String v){
		this.Numerical=v;
	}
	
	
};