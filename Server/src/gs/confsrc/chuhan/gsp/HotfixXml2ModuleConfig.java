package chuhan.gsp;


public class HotfixXml2ModuleConfig implements mytools.ConvMain.Checkable ,Comparable<HotfixXml2ModuleConfig>{

	public int compareTo(HotfixXml2ModuleConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public HotfixXml2ModuleConfig(){
		super();
	}
	public HotfixXml2ModuleConfig(HotfixXml2ModuleConfig arg){
		this.id=arg.id ;
		this.filename=arg.filename ;
		this.canreload=arg.canreload ;
		this.module=arg.module ;
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
	public String filename  = null  ;
	
	public String getFilename(){
		return this.filename;
	}
	
	public void setFilename(String v){
		this.filename=v;
	}
	
	/**
	 * 
	 */
	public boolean canreload  = false  ;
	
	public boolean getCanreload(){
		return this.canreload;
	}
	
	public void setCanreload(boolean v){
		this.canreload=v;
	}
	
	/**
	 * 
	 */
	public String module  = null  ;
	
	public String getModule(){
		return this.module;
	}
	
	public void setModule(String v){
		this.module=v;
	}
	
	
};