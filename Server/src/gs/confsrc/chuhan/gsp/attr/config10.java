package chuhan.gsp.attr;


public class config10 implements mytools.ConvMain.Checkable ,Comparable<config10>{

	public int compareTo(config10 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public config10(){
		super();
	}
	public config10(config10 arg){
		this.id=arg.id ;
		this.configname=arg.configname ;
		this.configtype=arg.configtype ;
		this.configvalue=arg.configvalue ;
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
	public String configname  = null  ;
	
	public String getConfigname(){
		return this.configname;
	}
	
	public void setConfigname(String v){
		this.configname=v;
	}
	
	/**
	 * 
	 */
	public String configtype  = null  ;
	
	public String getConfigtype(){
		return this.configtype;
	}
	
	public void setConfigtype(String v){
		this.configtype=v;
	}
	
	/**
	 * 
	 */
	public String configvalue  = null  ;
	
	public String getConfigvalue(){
		return this.configvalue;
	}
	
	public void setConfigvalue(String v){
		this.configvalue=v;
	}
	
	
};