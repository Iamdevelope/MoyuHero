package chuhan.gsp.para;


public class cparaconfig implements mytools.ConvMain.Checkable ,Comparable<cparaconfig>{

	public int compareTo(cparaconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cparaconfig(){
		super();
	}
	public cparaconfig(cparaconfig arg){
		this.id=arg.id ;
		this.para=arg.para ;
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
	public String para  = null  ;
	
	public String getPara(){
		return this.para;
	}
	
	public void setPara(String v){
		this.para=v;
	}
	
	
};