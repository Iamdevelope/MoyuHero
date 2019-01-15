package chuhan.gsp.main;


public class ModuleInfo implements mytools.ConvMain.Checkable ,Comparable<ModuleInfo>{

	public int compareTo(ModuleInfo o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ModuleInfo(){
		super();
	}
	public ModuleInfo(ModuleInfo arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.classname=arg.classname ;
		this.dep=arg.dep ;
	}
	public void checkValid(java.util.Map<String,java.util.Map<Integer,? extends Object> > objs){
	}
	/**
	 * id
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
	public String classname  = null  ;
	
	public String getClassname(){
		return this.classname;
	}
	
	public void setClassname(String v){
		this.classname=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<String> dep  ;
	
	public java.util.ArrayList<String> getDep(){
		return this.dep;
	}
	
	public void setDep(java.util.ArrayList<String> v){
		this.dep=v;
	}
	
	
};