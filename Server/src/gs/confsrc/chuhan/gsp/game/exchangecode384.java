package chuhan.gsp.game;


public class exchangecode384 implements mytools.ConvMain.Checkable ,Comparable<exchangecode384>{

	public int compareTo(exchangecode384 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public exchangecode384(){
		super();
	}
	public exchangecode384(exchangecode384 arg){
		this.id=arg.id ;
		this.code=arg.code ;
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
	public String code  = null  ;
	
	public String getCode(){
		return this.code;
	}
	
	public void setCode(String v){
		this.code=v;
	}
	
	
};