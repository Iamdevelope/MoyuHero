package chuhan.gsp.game;


public class exchangecode388 implements mytools.ConvMain.Checkable ,Comparable<exchangecode388>{

	public int compareTo(exchangecode388 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public exchangecode388(){
		super();
	}
	public exchangecode388(exchangecode388 arg){
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