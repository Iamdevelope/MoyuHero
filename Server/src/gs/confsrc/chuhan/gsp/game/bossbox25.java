package chuhan.gsp.game;


public class bossbox25 implements mytools.ConvMain.Checkable ,Comparable<bossbox25>{

	public int compareTo(bossbox25 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public bossbox25(){
		super();
	}
	public bossbox25(bossbox25 arg){
		this.id=arg.id ;
		this.bossboxid=arg.bossboxid ;
		this.dorplevel=arg.dorplevel ;
		this.rewardid=arg.rewardid ;
		this.rewardnum=arg.rewardnum ;
		this.dropwight1=arg.dropwight1 ;
		this.dropwight1plus=arg.dropwight1plus ;
		this.dropwight2=arg.dropwight2 ;
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
	public int bossboxid  = 0  ;
	
	public int getBossboxid(){
		return this.bossboxid;
	}
	
	public void setBossboxid(int v){
		this.bossboxid=v;
	}
	
	/**
	 * 
	 */
	public int dorplevel  = 0  ;
	
	public int getDorplevel(){
		return this.dorplevel;
	}
	
	public void setDorplevel(int v){
		this.dorplevel=v;
	}
	
	/**
	 * 
	 */
	public int rewardid  = 0  ;
	
	public int getRewardid(){
		return this.rewardid;
	}
	
	public void setRewardid(int v){
		this.rewardid=v;
	}
	
	/**
	 * 
	 */
	public int rewardnum  = 0  ;
	
	public int getRewardnum(){
		return this.rewardnum;
	}
	
	public void setRewardnum(int v){
		this.rewardnum=v;
	}
	
	/**
	 * 
	 */
	public int dropwight1  = 0  ;
	
	public int getDropwight1(){
		return this.dropwight1;
	}
	
	public void setDropwight1(int v){
		this.dropwight1=v;
	}
	
	/**
	 * 
	 */
	public int dropwight1plus  = 0  ;
	
	public int getDropwight1plus(){
		return this.dropwight1plus;
	}
	
	public void setDropwight1plus(int v){
		this.dropwight1plus=v;
	}
	
	/**
	 * 
	 */
	public int dropwight2  = 0  ;
	
	public int getDropwight2(){
		return this.dropwight2;
	}
	
	public void setDropwight2(int v){
		this.dropwight2=v;
	}
	
	
};