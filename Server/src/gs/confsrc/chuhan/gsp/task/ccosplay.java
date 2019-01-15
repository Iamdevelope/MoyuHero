package chuhan.gsp.task;


public class ccosplay implements mytools.ConvMain.Checkable ,Comparable<ccosplay>{

	public int compareTo(ccosplay o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ccosplay(){
		super();
	}
	public ccosplay(ccosplay arg){
		this.id=arg.id ;
		this.number=arg.number ;
		this.name=arg.name ;
		this.firstpic=arg.firstpic ;
		this.secondpic=arg.secondpic ;
		this.thirdpic=arg.thirdpic ;
		this.pic3id=arg.pic3id ;
		this.fourthpic=arg.fourthpic ;
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
	public int number  = 0  ;
	
	public int getNumber(){
		return this.number;
	}
	
	public void setNumber(int v){
		this.number=v;
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
	public String firstpic  = null  ;
	
	public String getFirstpic(){
		return this.firstpic;
	}
	
	public void setFirstpic(String v){
		this.firstpic=v;
	}
	
	/**
	 * 
	 */
	public String secondpic  = null  ;
	
	public String getSecondpic(){
		return this.secondpic;
	}
	
	public void setSecondpic(String v){
		this.secondpic=v;
	}
	
	/**
	 * 
	 */
	public String thirdpic  = null  ;
	
	public String getThirdpic(){
		return this.thirdpic;
	}
	
	public void setThirdpic(String v){
		this.thirdpic=v;
	}
	
	/**
	 * 
	 */
	public int pic3id  = 0  ;
	
	public int getPic3id(){
		return this.pic3id;
	}
	
	public void setPic3id(int v){
		this.pic3id=v;
	}
	
	/**
	 * 
	 */
	public String fourthpic  = null  ;
	
	public String getFourthpic(){
		return this.fourthpic;
	}
	
	public void setFourthpic(String v){
		this.fourthpic=v;
	}
	
	
};