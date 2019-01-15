package chuhan.gsp.item;


public class artifact33 implements mytools.ConvMain.Checkable ,Comparable<artifact33>{

	public int compareTo(artifact33 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public artifact33(){
		super();
	}
	public artifact33(artifact33 arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.level=arg.level ;
		this.resourceName=arg.resourceName ;
		this.type=arg.type ;
		this.playerLevel=arg.playerLevel ;
		this.attriType=arg.attriType ;
		this.attriValue=arg.attriValue ;
		this.heroID=arg.heroID ;
		this.heroNum=arg.heroNum ;
		this.weight=arg.weight ;
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
	public int level  = 0  ;
	
	public int getLevel(){
		return this.level;
	}
	
	public void setLevel(int v){
		this.level=v;
	}
	
	/**
	 * 
	 */
	public String resourceName  = null  ;
	
	public String getResourceName(){
		return this.resourceName;
	}
	
	public void setResourceName(String v){
		this.resourceName=v;
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
	public int playerLevel  = 0  ;
	
	public int getPlayerLevel(){
		return this.playerLevel;
	}
	
	public void setPlayerLevel(int v){
		this.playerLevel=v;
	}
	
	/**
	 * 
	 */
	public String attriType  = null  ;
	
	public String getAttriType(){
		return this.attriType;
	}
	
	public void setAttriType(String v){
		this.attriType=v;
	}
	
	/**
	 * 
	 */
	public String attriValue  = null  ;
	
	public String getAttriValue(){
		return this.attriValue;
	}
	
	public void setAttriValue(String v){
		this.attriValue=v;
	}
	
	/**
	 * 
	 */
	public String heroID  = null  ;
	
	public String getHeroID(){
		return this.heroID;
	}
	
	public void setHeroID(String v){
		this.heroID=v;
	}
	
	/**
	 * 
	 */
	public String heroNum  = null  ;
	
	public String getHeroNum(){
		return this.heroNum;
	}
	
	public void setHeroNum(String v){
		this.heroNum=v;
	}
	
	/**
	 * 
	 */
	public String weight  = null  ;
	
	public String getWeight(){
		return this.weight;
	}
	
	public void setWeight(String v){
		this.weight=v;
	}
	
	
};