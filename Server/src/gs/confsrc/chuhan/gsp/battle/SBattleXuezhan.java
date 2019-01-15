package chuhan.gsp.battle;


public class SBattleXuezhan implements mytools.ConvMain.Checkable ,Comparable<SBattleXuezhan>{

	public int compareTo(SBattleXuezhan o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SBattleXuezhan(){
		super();
	}
	public SBattleXuezhan(SBattleXuezhan arg){
		this.id=arg.id ;
		this.pic=arg.pic ;
		this.battle=arg.battle ;
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
	public int pic  = 0  ;
	
	public int getPic(){
		return this.pic;
	}
	
	public void setPic(int v){
		this.pic=v;
	}
	
	/**
	 * 
	 */
	public int battle  = 0  ;
	
	public int getBattle(){
		return this.battle;
	}
	
	public void setBattle(int v){
		this.battle=v;
	}
	
	
};