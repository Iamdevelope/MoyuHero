package chuhan.gsp.item;


public class baseruneattribute28 implements mytools.ConvMain.Checkable ,Comparable<baseruneattribute28>{

	public int compareTo(baseruneattribute28 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public baseruneattribute28(){
		super();
	}
	public baseruneattribute28(baseruneattribute28 arg){
		this.id=arg.id ;
		this.bagId=arg.bagId ;
		this.level=arg.level ;
		this.attriType=arg.attriType ;
		this.attriValue=arg.attriValue ;
		this.attriDes=arg.attriDes ;
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
	public String attriDes  = null  ;
	
	public String getAttriDes(){
		return this.attriDes;
	}
	
	public void setAttriDes(String v){
		this.attriDes=v;
	}
	
	
};