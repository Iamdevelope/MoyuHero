package chuhan.gsp.game;


public class newbieguide60 implements mytools.ConvMain.Checkable ,Comparable<newbieguide60>{

	public int compareTo(newbieguide60 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public newbieguide60(){
		super();
	}
	public newbieguide60(newbieguide60 arg){
		this.id=arg.id ;
		this.reward=arg.reward ;
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
	public String reward  = null  ;
	
	public String getReward(){
		return this.reward;
	}
	
	public void setReward(String v){
		this.reward=v;
	}
	
	
};