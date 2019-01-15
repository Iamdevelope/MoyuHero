package chuhan.gsp.game;


public class SBchestconfig implements mytools.ConvMain.Checkable ,Comparable<SBchestconfig>{

	public int compareTo(SBchestconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SBchestconfig(){
		super();
	}
	public SBchestconfig(SBchestconfig arg){
		this.id=arg.id ;
		this.min=arg.min ;
		this.max=arg.max ;
		this.pro=arg.pro ;
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
	public int min  = 0  ;
	
	public int getMin(){
		return this.min;
	}
	
	public void setMin(int v){
		this.min=v;
	}
	
	/**
	 * 
	 */
	public int max  = 0  ;
	
	public int getMax(){
		return this.max;
	}
	
	public void setMax(int v){
		this.max=v;
	}
	
	/**
	 * 
	 */
	public double pro  = 0.0  ;
	
	public double getPro(){
		return this.pro;
	}
	
	public void setPro(double v){
		this.pro=v;
	}
	
	/**
	 * 
	 */
	public int reward  = 0  ;
	
	public int getReward(){
		return this.reward;
	}
	
	public void setReward(int v){
		this.reward=v;
	}
	
	
};