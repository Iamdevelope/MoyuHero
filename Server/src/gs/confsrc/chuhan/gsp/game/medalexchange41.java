package chuhan.gsp.game;


public class medalexchange41 implements mytools.ConvMain.Checkable ,Comparable<medalexchange41>{

	public int compareTo(medalexchange41 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public medalexchange41(){
		super();
	}
	public medalexchange41(medalexchange41 arg){
		this.id=arg.id ;
		this.exchangeType=arg.exchangeType ;
		this.needNum=arg.needNum ;
		this.rewardId=arg.rewardId ;
		this.rewardNum=arg.rewardNum ;
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
	public int exchangeType  = 0  ;
	
	public int getExchangeType(){
		return this.exchangeType;
	}
	
	public void setExchangeType(int v){
		this.exchangeType=v;
	}
	
	/**
	 * 
	 */
	public int needNum  = 0  ;
	
	public int getNeedNum(){
		return this.needNum;
	}
	
	public void setNeedNum(int v){
		this.needNum=v;
	}
	
	/**
	 * 
	 */
	public int rewardId  = 0  ;
	
	public int getRewardId(){
		return this.rewardId;
	}
	
	public void setRewardId(int v){
		this.rewardId=v;
	}
	
	/**
	 * 
	 */
	public int rewardNum  = 0  ;
	
	public int getRewardNum(){
		return this.rewardNum;
	}
	
	public void setRewardNum(int v){
		this.rewardNum=v;
	}
	
	
};