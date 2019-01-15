package chuhan.gsp.attr;


public class player03 implements mytools.ConvMain.Checkable ,Comparable<player03>{

	public int compareTo(player03 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public player03(){
		super();
	}
	public player03(player03 arg){
		this.id=arg.id ;
		this.exp=arg.exp ;
		this.entranceFury=arg.entranceFury ;
		this.waveFury=arg.waveFury ;
		this.MaxFury=arg.MaxFury ;
		this.extraAp=arg.extraAp ;
		this.extraHeroPackset=arg.extraHeroPackset ;
		this.extraCommonItemPackset=arg.extraCommonItemPackset ;
		this.apRecover=arg.apRecover ;
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
	public int exp  = 0  ;
	
	public int getExp(){
		return this.exp;
	}
	
	public void setExp(int v){
		this.exp=v;
	}
	
	/**
	 * 
	 */
	public int entranceFury  = 0  ;
	
	public int getEntranceFury(){
		return this.entranceFury;
	}
	
	public void setEntranceFury(int v){
		this.entranceFury=v;
	}
	
	/**
	 * 
	 */
	public int waveFury  = 0  ;
	
	public int getWaveFury(){
		return this.waveFury;
	}
	
	public void setWaveFury(int v){
		this.waveFury=v;
	}
	
	/**
	 * 
	 */
	public int MaxFury  = 0  ;
	
	public int getMaxFury(){
		return this.MaxFury;
	}
	
	public void setMaxFury(int v){
		this.MaxFury=v;
	}
	
	/**
	 * 
	 */
	public int extraAp  = 0  ;
	
	public int getExtraAp(){
		return this.extraAp;
	}
	
	public void setExtraAp(int v){
		this.extraAp=v;
	}
	
	/**
	 * 
	 */
	public int extraHeroPackset  = 0  ;
	
	public int getExtraHeroPackset(){
		return this.extraHeroPackset;
	}
	
	public void setExtraHeroPackset(int v){
		this.extraHeroPackset=v;
	}
	
	/**
	 * 
	 */
	public int extraCommonItemPackset  = 0  ;
	
	public int getExtraCommonItemPackset(){
		return this.extraCommonItemPackset;
	}
	
	public void setExtraCommonItemPackset(int v){
		this.extraCommonItemPackset=v;
	}
	
	/**
	 * 
	 */
	public int apRecover  = 0  ;
	
	public int getApRecover(){
		return this.apRecover;
	}
	
	public void setApRecover(int v){
		this.apRecover=v;
	}
	
	
};