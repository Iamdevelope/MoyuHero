package chuhan.gsp.game;


public class sstarthero implements mytools.ConvMain.Checkable ,Comparable<sstarthero>{

	public int compareTo(sstarthero o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sstarthero(){
		super();
	}
	public sstarthero(sstarthero arg){
		this.id=arg.id ;
		this.heroid=arg.heroid ;
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
	public int heroid  = 0  ;
	
	public int getHeroid(){
		return this.heroid;
	}
	
	public void setHeroid(int v){
		this.heroid=v;
	}
	
	
};