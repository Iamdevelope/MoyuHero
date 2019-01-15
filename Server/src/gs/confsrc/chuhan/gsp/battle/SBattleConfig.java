package chuhan.gsp.battle;


public class SBattleConfig implements mytools.ConvMain.Checkable ,Comparable<SBattleConfig>{

	public int compareTo(SBattleConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SBattleConfig(){
		super();
	}
	public SBattleConfig(SBattleConfig arg){
		this.id=arg.id ;
		this.type=arg.type ;
		this.spot=arg.spot ;
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
	public int type  = 0  ;
	
	public int getType(){
		return this.type;
	}
	
	public void setType(int v){
		this.type=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<Integer> spot  ;
	
	public java.util.ArrayList<Integer> getSpot(){
		return this.spot;
	}
	
	public void setSpot(java.util.ArrayList<Integer> v){
		this.spot=v;
	}
	
	
};