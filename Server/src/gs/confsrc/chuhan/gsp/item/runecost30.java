package chuhan.gsp.item;


public class runecost30 implements mytools.ConvMain.Checkable ,Comparable<runecost30>{

	public int compareTo(runecost30 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public runecost30(){
		super();
	}
	public runecost30(runecost30 arg){
		this.id=arg.id ;
		this.bagId=arg.bagId ;
		this.level=arg.level ;
		this.attriType1=arg.attriType1 ;
		this.attriValue1=arg.attriValue1 ;
		this.attriType2=arg.attriType2 ;
		this.attriValue2=arg.attriValue2 ;
		this.returnType1=arg.returnType1 ;
		this.returnValue1=arg.returnValue1 ;
		this.returnType2=arg.returnType2 ;
		this.returnValue2=arg.returnValue2 ;
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
	public int attriType1  = 0  ;
	
	public int getAttriType1(){
		return this.attriType1;
	}
	
	public void setAttriType1(int v){
		this.attriType1=v;
	}
	
	/**
	 * 
	 */
	public int attriValue1  = 0  ;
	
	public int getAttriValue1(){
		return this.attriValue1;
	}
	
	public void setAttriValue1(int v){
		this.attriValue1=v;
	}
	
	/**
	 * 
	 */
	public int attriType2  = 0  ;
	
	public int getAttriType2(){
		return this.attriType2;
	}
	
	public void setAttriType2(int v){
		this.attriType2=v;
	}
	
	/**
	 * 
	 */
	public int attriValue2  = 0  ;
	
	public int getAttriValue2(){
		return this.attriValue2;
	}
	
	public void setAttriValue2(int v){
		this.attriValue2=v;
	}
	
	/**
	 * 
	 */
	public int returnType1  = 0  ;
	
	public int getReturnType1(){
		return this.returnType1;
	}
	
	public void setReturnType1(int v){
		this.returnType1=v;
	}
	
	/**
	 * 
	 */
	public int returnValue1  = 0  ;
	
	public int getReturnValue1(){
		return this.returnValue1;
	}
	
	public void setReturnValue1(int v){
		this.returnValue1=v;
	}
	
	/**
	 * 
	 */
	public int returnType2  = 0  ;
	
	public int getReturnType2(){
		return this.returnType2;
	}
	
	public void setReturnType2(int v){
		this.returnType2=v;
	}
	
	/**
	 * 
	 */
	public int returnValue2  = 0  ;
	
	public int getReturnValue2(){
		return this.returnValue2;
	}
	
	public void setReturnValue2(int v){
		this.returnValue2=v;
	}
	
	
};