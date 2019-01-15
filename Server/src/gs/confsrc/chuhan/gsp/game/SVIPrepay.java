package chuhan.gsp.game;


public class SVIPrepay implements mytools.ConvMain.Checkable ,Comparable<SVIPrepay>{

	public int compareTo(SVIPrepay o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SVIPrepay(){
		super();
	}
	public SVIPrepay(SVIPrepay arg){
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
	public int reward  = 0  ;
	
	public int getReward(){
		return this.reward;
	}
	
	public void setReward(int v){
		this.reward=v;
	}
	
	
};