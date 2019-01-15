package chuhan.gsp.game;


public class sgmconfig implements mytools.ConvMain.Checkable ,Comparable<sgmconfig>{

	public int compareTo(sgmconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sgmconfig(){
		super();
	}
	public sgmconfig(sgmconfig arg){
		this.id=arg.id ;
		this.gm=arg.gm ;
		this.pt=arg.pt ;
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
	public String gm  = null  ;
	
	public String getGm(){
		return this.gm;
	}
	
	public void setGm(String v){
		this.gm=v;
	}
	
	/**
	 * 
	 */
	public String pt  = null  ;
	
	public String getPt(){
		return this.pt;
	}
	
	public void setPt(String v){
		this.pt=v;
	}
	
	
};