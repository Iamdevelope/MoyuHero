package chuhan.gsp.task;


public class stage11 implements mytools.ConvMain.Checkable ,Comparable<stage11>{

	public int compareTo(stage11 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public stage11(){
		super();
	}
	public stage11(stage11 arg){
		this.id=arg.id ;
		this.stagename=arg.stagename ;
		this.stageinfo=arg.stageinfo ;
		this.premissionid=arg.premissionid ;
		this.playerlevel=arg.playerlevel ;
		this.stagetype=arg.stagetype ;
		this.stagemap=arg.stagemap ;
		this.stageevent=arg.stageevent ;
		this.extraloadresource=arg.extraloadresource ;
		this.playerexp=arg.playerexp ;
		this.goldreward=arg.goldreward ;
		this.heroexp=arg.heroexp ;
		this.expcrystal=arg.expcrystal ;
		this.cost=arg.cost ;
		this.stageicon=arg.stageicon ;
		this.monstergroup=arg.monstergroup ;
		this.stagedrop=arg.stagedrop ;
		this.specialcondition=arg.specialcondition ;
		this.specialstagedrop=arg.specialstagedrop ;
		this.dropcheck=arg.dropcheck ;
		this.limittime=arg.limittime ;
		this.displaydrop=arg.displaydrop ;
		this.displayMonster=arg.displayMonster ;
		this.bossbox=arg.bossbox ;
		this.winCondition=arg.winCondition ;
		this.waveFury=arg.waveFury ;
		this.aiCheck=arg.aiCheck ;
		this.mysteriousStage=arg.mysteriousStage ;
		this.mysteriousShop=arg.mysteriousShop ;
		this.fightTime=arg.fightTime ;
		this.isBoss=arg.isBoss ;
		this.resetCost=arg.resetCost ;
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
	public String stagename  = null  ;
	
	public String getStagename(){
		return this.stagename;
	}
	
	public void setStagename(String v){
		this.stagename=v;
	}
	
	/**
	 * 
	 */
	public String stageinfo  = null  ;
	
	public String getStageinfo(){
		return this.stageinfo;
	}
	
	public void setStageinfo(String v){
		this.stageinfo=v;
	}
	
	/**
	 * 
	 */
	public int premissionid  = 0  ;
	
	public int getPremissionid(){
		return this.premissionid;
	}
	
	public void setPremissionid(int v){
		this.premissionid=v;
	}
	
	/**
	 * 
	 */
	public int playerlevel  = 0  ;
	
	public int getPlayerlevel(){
		return this.playerlevel;
	}
	
	public void setPlayerlevel(int v){
		this.playerlevel=v;
	}
	
	/**
	 * 
	 */
	public int stagetype  = 0  ;
	
	public int getStagetype(){
		return this.stagetype;
	}
	
	public void setStagetype(int v){
		this.stagetype=v;
	}
	
	/**
	 * 
	 */
	public String stagemap  = null  ;
	
	public String getStagemap(){
		return this.stagemap;
	}
	
	public void setStagemap(String v){
		this.stagemap=v;
	}
	
	/**
	 * 
	 */
	public String stageevent  = null  ;
	
	public String getStageevent(){
		return this.stageevent;
	}
	
	public void setStageevent(String v){
		this.stageevent=v;
	}
	
	/**
	 * 
	 */
	public String extraloadresource  = null  ;
	
	public String getExtraloadresource(){
		return this.extraloadresource;
	}
	
	public void setExtraloadresource(String v){
		this.extraloadresource=v;
	}
	
	/**
	 * 
	 */
	public int playerexp  = 0  ;
	
	public int getPlayerexp(){
		return this.playerexp;
	}
	
	public void setPlayerexp(int v){
		this.playerexp=v;
	}
	
	/**
	 * 
	 */
	public int goldreward  = 0  ;
	
	public int getGoldreward(){
		return this.goldreward;
	}
	
	public void setGoldreward(int v){
		this.goldreward=v;
	}
	
	/**
	 * 
	 */
	public int heroexp  = 0  ;
	
	public int getHeroexp(){
		return this.heroexp;
	}
	
	public void setHeroexp(int v){
		this.heroexp=v;
	}
	
	/**
	 * 
	 */
	public int expcrystal  = 0  ;
	
	public int getExpcrystal(){
		return this.expcrystal;
	}
	
	public void setExpcrystal(int v){
		this.expcrystal=v;
	}
	
	/**
	 * 
	 */
	public int cost  = 0  ;
	
	public int getCost(){
		return this.cost;
	}
	
	public void setCost(int v){
		this.cost=v;
	}
	
	/**
	 * 
	 */
	public String stageicon  = null  ;
	
	public String getStageicon(){
		return this.stageicon;
	}
	
	public void setStageicon(String v){
		this.stageicon=v;
	}
	
	/**
	 * 
	 */
	public String monstergroup  = null  ;
	
	public String getMonstergroup(){
		return this.monstergroup;
	}
	
	public void setMonstergroup(String v){
		this.monstergroup=v;
	}
	
	/**
	 * 
	 */
	public String stagedrop  = null  ;
	
	public String getStagedrop(){
		return this.stagedrop;
	}
	
	public void setStagedrop(String v){
		this.stagedrop=v;
	}
	
	/**
	 * 
	 */
	public int specialcondition  = 0  ;
	
	public int getSpecialcondition(){
		return this.specialcondition;
	}
	
	public void setSpecialcondition(int v){
		this.specialcondition=v;
	}
	
	/**
	 * 
	 */
	public String specialstagedrop  = null  ;
	
	public String getSpecialstagedrop(){
		return this.specialstagedrop;
	}
	
	public void setSpecialstagedrop(String v){
		this.specialstagedrop=v;
	}
	
	/**
	 * 
	 */
	public int dropcheck  = 0  ;
	
	public int getDropcheck(){
		return this.dropcheck;
	}
	
	public void setDropcheck(int v){
		this.dropcheck=v;
	}
	
	/**
	 * 
	 */
	public int limittime  = 0  ;
	
	public int getLimittime(){
		return this.limittime;
	}
	
	public void setLimittime(int v){
		this.limittime=v;
	}
	
	/**
	 * 
	 */
	public String displaydrop  = null  ;
	
	public String getDisplaydrop(){
		return this.displaydrop;
	}
	
	public void setDisplaydrop(String v){
		this.displaydrop=v;
	}
	
	/**
	 * 
	 */
	public String displayMonster  = null  ;
	
	public String getDisplayMonster(){
		return this.displayMonster;
	}
	
	public void setDisplayMonster(String v){
		this.displayMonster=v;
	}
	
	/**
	 * 
	 */
	public int bossbox  = 0  ;
	
	public int getBossbox(){
		return this.bossbox;
	}
	
	public void setBossbox(int v){
		this.bossbox=v;
	}
	
	/**
	 * 
	 */
	public int winCondition  = 0  ;
	
	public int getWinCondition(){
		return this.winCondition;
	}
	
	public void setWinCondition(int v){
		this.winCondition=v;
	}
	
	/**
	 * 
	 */
	public String waveFury  = null  ;
	
	public String getWaveFury(){
		return this.waveFury;
	}
	
	public void setWaveFury(String v){
		this.waveFury=v;
	}
	
	/**
	 * 
	 */
	public int aiCheck  = 0  ;
	
	public int getAiCheck(){
		return this.aiCheck;
	}
	
	public void setAiCheck(int v){
		this.aiCheck=v;
	}
	
	/**
	 * 
	 */
	public int mysteriousStage  = 0  ;
	
	public int getMysteriousStage(){
		return this.mysteriousStage;
	}
	
	public void setMysteriousStage(int v){
		this.mysteriousStage=v;
	}
	
	/**
	 * 
	 */
	public int mysteriousShop  = 0  ;
	
	public int getMysteriousShop(){
		return this.mysteriousShop;
	}
	
	public void setMysteriousShop(int v){
		this.mysteriousShop=v;
	}
	
	/**
	 * 
	 */
	public int fightTime  = 0  ;
	
	public int getFightTime(){
		return this.fightTime;
	}
	
	public void setFightTime(int v){
		this.fightTime=v;
	}
	
	/**
	 * 
	 */
	public int isBoss  = 0  ;
	
	public int getIsBoss(){
		return this.isBoss;
	}
	
	public void setIsBoss(int v){
		this.isBoss=v;
	}
	
	/**
	 * 
	 */
	public String resetCost  = null  ;
	
	public String getResetCost(){
		return this.resetCost;
	}
	
	public void setResetCost(String v){
		this.resetCost=v;
	}
	
	
};