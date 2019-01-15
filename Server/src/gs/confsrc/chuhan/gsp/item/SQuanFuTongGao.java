package chuhan.gsp.item;


public class SQuanFuTongGao implements mytools.ConvMain.Checkable ,Comparable<SQuanFuTongGao>{

	public int compareTo(SQuanFuTongGao o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SQuanFuTongGao(){
		super();
	}
	public SQuanFuTongGao(SQuanFuTongGao arg){
		this.id=arg.id ;
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
	
	
};