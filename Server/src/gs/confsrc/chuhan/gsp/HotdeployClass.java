package chuhan.gsp;


public class HotdeployClass implements mytools.ConvMain.Checkable ,Comparable<HotdeployClass>{

	public int compareTo(HotdeployClass o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public HotdeployClass(){
		super();
	}
	public HotdeployClass(HotdeployClass arg){
		this.id=arg.id ;
		this.oldClassName=arg.oldClassName ;
		this.newClassName=arg.newClassName ;
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
	public String oldClassName  = null  ;
	
	public String getOldClassName(){
		return this.oldClassName;
	}
	
	public void setOldClassName(String v){
		this.oldClassName=v;
	}
	
	/**
	 * 
	 */
	public String newClassName  = null  ;
	
	public String getNewClassName(){
		return this.newClassName;
	}
	
	public void setNewClassName(String v){
		this.newClassName=v;
	}
	
	
};