package chuhan.gsp.game;


public class SDianjiangQuanzhi implements mytools.ConvMain.Checkable ,Comparable<SDianjiangQuanzhi>{

	public int compareTo(SDianjiangQuanzhi o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SDianjiangQuanzhi(){
		super();
	}
	public SDianjiangQuanzhi(SDianjiangQuanzhi arg){
		this.id=arg.id ;
		this.quanzhi=arg.quanzhi ;
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
	public double quanzhi  = 0.0  ;
	
	public double getQuanzhi(){
		return this.quanzhi;
	}
	
	public void setQuanzhi(double v){
		this.quanzhi=v;
	}
	
	
};