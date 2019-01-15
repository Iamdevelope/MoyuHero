package chuhan.gsp.attr;


public class SBuffConflicts implements mytools.ConvMain.Checkable ,Comparable<SBuffConflicts>{

	public int compareTo(SBuffConflicts o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SBuffConflicts(){
		super();
	}
	public SBuffConflicts(SBuffConflicts arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.conflictbuffs=arg.conflictbuffs ;
		this.invalidbuffs=arg.invalidbuffs ;
		this.overridebuffs=arg.overridebuffs ;
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
	public String conflictbuffs  = null  ;
	
	public String getConflictbuffs(){
		return this.conflictbuffs;
	}
	
	public void setConflictbuffs(String v){
		this.conflictbuffs=v;
	}
	
	/**
	 * 
	 */
	public String invalidbuffs  = null  ;
	
	public String getInvalidbuffs(){
		return this.invalidbuffs;
	}
	
	public void setInvalidbuffs(String v){
		this.invalidbuffs=v;
	}
	
	/**
	 * 
	 */
	public String overridebuffs  = null  ;
	
	public String getOverridebuffs(){
		return this.overridebuffs;
	}
	
	public void setOverridebuffs(String v){
		this.overridebuffs=v;
	}
	
	
};