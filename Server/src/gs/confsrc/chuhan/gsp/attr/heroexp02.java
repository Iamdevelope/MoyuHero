package chuhan.gsp.attr;


public class heroexp02 implements mytools.ConvMain.Checkable ,Comparable<heroexp02>{

	public int compareTo(heroexp02 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public heroexp02(){
		super();
	}
	public heroexp02(heroexp02 arg){
		this.id=arg.id ;
		this.exp=arg.exp ;
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
	public String exp  = null  ;
	
	public String getExp(){
		return this.exp;
	}
	
	public void setExp(String v){
		this.exp=v;
	}
	
	
};