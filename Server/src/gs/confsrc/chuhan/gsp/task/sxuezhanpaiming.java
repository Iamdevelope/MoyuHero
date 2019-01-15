package chuhan.gsp.task;


public class sxuezhanpaiming implements mytools.ConvMain.Checkable ,Comparable<sxuezhanpaiming>{

	public int compareTo(sxuezhanpaiming o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sxuezhanpaiming(){
		super();
	}
	public sxuezhanpaiming(sxuezhanpaiming arg){
		this.id=arg.id ;
		this.reward=arg.reward ;
		this.describe=arg.describe ;
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
	public int reward  = 0  ;
	
	public int getReward(){
		return this.reward;
	}
	
	public void setReward(int v){
		this.reward=v;
	}
	
	/**
	 * 
	 */
	public String describe  = null  ;
	
	public String getDescribe(){
		return this.describe;
	}
	
	public void setDescribe(String v){
		this.describe=v;
	}
	
	
};