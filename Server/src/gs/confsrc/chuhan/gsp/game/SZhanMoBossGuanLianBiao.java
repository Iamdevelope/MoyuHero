package chuhan.gsp.game;


public class SZhanMoBossGuanLianBiao implements mytools.ConvMain.Checkable ,Comparable<SZhanMoBossGuanLianBiao>{

	public int compareTo(SZhanMoBossGuanLianBiao o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SZhanMoBossGuanLianBiao(){
		super();
	}
	public SZhanMoBossGuanLianBiao(SZhanMoBossGuanLianBiao arg){
		this.id=arg.id ;
		this.BattleNpcID=arg.BattleNpcID ;
		this.AwardID=arg.AwardID ;
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
	public int BattleNpcID  = 0  ;
	
	public int getBattleNpcID(){
		return this.BattleNpcID;
	}
	
	public void setBattleNpcID(int v){
		this.BattleNpcID=v;
	}
	
	/**
	 * 
	 */
	public int AwardID  = 0  ;
	
	public int getAwardID(){
		return this.AwardID;
	}
	
	public void setAwardID(int v){
		this.AwardID=v;
	}
	
	
};