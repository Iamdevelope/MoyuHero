package chuhan.gsp.task;


public class scosplay implements mytools.ConvMain.Checkable ,Comparable<scosplay>{

	public int compareTo(scosplay o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public scosplay(){
		super();
	}
	public scosplay(scosplay arg){
		this.id=arg.id ;
		this.number=arg.number ;
		this.name=arg.name ;
		this.pic3id=arg.pic3id ;
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
	public int pic3id  = 0  ;
	
	public int getPic3id(){
		return this.pic3id;
	}
	
	public void setPic3id(int v){
		this.pic3id=v;
	}
	
	
};