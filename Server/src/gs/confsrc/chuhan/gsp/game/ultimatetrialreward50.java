package chuhan.gsp.game;


public class ultimatetrialreward50 implements mytools.ConvMain.Checkable ,Comparable<ultimatetrialreward50>{

	public int compareTo(ultimatetrialreward50 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ultimatetrialreward50(){
		super();
	}
	public ultimatetrialreward50(ultimatetrialreward50 arg){
		this.id=arg.id ;
		this.levelrange=arg.levelrange ;
		this.rank1=arg.rank1 ;
		this.rank2=arg.rank2 ;
		this.reward_id=arg.reward_id ;
		this.reward_num=arg.reward_num ;
		this.rankdes=arg.rankdes ;
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
	public int levelrange  = 0  ;
	
	public int getLevelrange(){
		return this.levelrange;
	}
	
	public void setLevelrange(int v){
		this.levelrange=v;
	}
	
	/**
	 * 
	 */
	public int rank1  = 0  ;
	
	public int getRank1(){
		return this.rank1;
	}
	
	public void setRank1(int v){
		this.rank1=v;
	}
	
	/**
	 * 
	 */
	public int rank2  = 0  ;
	
	public int getRank2(){
		return this.rank2;
	}
	
	public void setRank2(int v){
		this.rank2=v;
	}
	
	/**
	 * 
	 */
	public String reward_id  = null  ;
	
	public String getReward_id(){
		return this.reward_id;
	}
	
	public void setReward_id(String v){
		this.reward_id=v;
	}
	
	/**
	 * 
	 */
	public String reward_num  = null  ;
	
	public String getReward_num(){
		return this.reward_num;
	}
	
	public void setReward_num(String v){
		this.reward_num=v;
	}
	
	/**
	 * 
	 */
	public String rankdes  = null  ;
	
	public String getRankdes(){
		return this.rankdes;
	}
	
	public void setRankdes(String v){
		this.rankdes=v;
	}
	
	
};