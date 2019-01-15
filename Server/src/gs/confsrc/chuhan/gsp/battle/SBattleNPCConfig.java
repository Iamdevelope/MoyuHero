package chuhan.gsp.battle;


public class SBattleNPCConfig implements mytools.ConvMain.Checkable ,Comparable<SBattleNPCConfig>{

	public int compareTo(SBattleNPCConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SBattleNPCConfig(){
		super();
	}
	public SBattleNPCConfig(SBattleNPCConfig arg){
		this.id=arg.id ;
		this.chuhan=arg.chuhan ;
		this.name=arg.name ;
		this.color=arg.color ;
		this.icon=arg.icon ;
		this.pic=arg.pic ;
		this.levtype=arg.levtype ;
		this.lev=arg.lev ;
		this.army=arg.army ;
		this.attack=arg.attack ;
		this.defend=arg.defend ;
		this.wise=arg.wise ;
		this.speed=arg.speed ;
		this.skill1=arg.skill1 ;
		this.prob1=arg.prob1 ;
		this.skill2=arg.skill2 ;
		this.prob2=arg.prob2 ;
		this.skill3=arg.skill3 ;
		this.prob3=arg.prob3 ;
		this.battletype=arg.battletype ;
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
	public int chuhan  = 0  ;
	
	public int getChuhan(){
		return this.chuhan;
	}
	
	public void setChuhan(int v){
		this.chuhan=v;
	}
	
	/**
	 * 
	 */
	public String name  = null  ;
	
	public String getName(){
		return this.name;
	}
	
	public void setName(String v){
		this.name=v;
	}
	
	/**
	 * 
	 */
	public int color  = 0  ;
	
	public int getColor(){
		return this.color;
	}
	
	public void setColor(int v){
		this.color=v;
	}
	
	/**
	 * 
	 */
	public int icon  = 0  ;
	
	public int getIcon(){
		return this.icon;
	}
	
	public void setIcon(int v){
		this.icon=v;
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
	public int levtype  = 0  ;
	
	public int getLevtype(){
		return this.levtype;
	}
	
	public void setLevtype(int v){
		this.levtype=v;
	}
	
	/**
	 * 
	 */
	public int lev  = 0  ;
	
	public int getLev(){
		return this.lev;
	}
	
	public void setLev(int v){
		this.lev=v;
	}
	
	/**
	 * 
	 */
	public String army  = null  ;
	
	public String getArmy(){
		return this.army;
	}
	
	public void setArmy(String v){
		this.army=v;
	}
	
	/**
	 * 
	 */
	public String attack  = null  ;
	
	public String getAttack(){
		return this.attack;
	}
	
	public void setAttack(String v){
		this.attack=v;
	}
	
	/**
	 * 
	 */
	public String defend  = null  ;
	
	public String getDefend(){
		return this.defend;
	}
	
	public void setDefend(String v){
		this.defend=v;
	}
	
	/**
	 * 
	 */
	public String wise  = null  ;
	
	public String getWise(){
		return this.wise;
	}
	
	public void setWise(String v){
		this.wise=v;
	}
	
	/**
	 * 
	 */
	public String speed  = null  ;
	
	public String getSpeed(){
		return this.speed;
	}
	
	public void setSpeed(String v){
		this.speed=v;
	}
	
	/**
	 * 
	 */
	public int skill1  = 0  ;
	
	public int getSkill1(){
		return this.skill1;
	}
	
	public void setSkill1(int v){
		this.skill1=v;
	}
	
	/**
	 * 
	 */
	public int prob1  = 0  ;
	
	public int getProb1(){
		return this.prob1;
	}
	
	public void setProb1(int v){
		this.prob1=v;
	}
	
	/**
	 * 
	 */
	public int skill2  = 0  ;
	
	public int getSkill2(){
		return this.skill2;
	}
	
	public void setSkill2(int v){
		this.skill2=v;
	}
	
	/**
	 * 
	 */
	public int prob2  = 0  ;
	
	public int getProb2(){
		return this.prob2;
	}
	
	public void setProb2(int v){
		this.prob2=v;
	}
	
	/**
	 * 
	 */
	public int skill3  = 0  ;
	
	public int getSkill3(){
		return this.skill3;
	}
	
	public void setSkill3(int v){
		this.skill3=v;
	}
	
	/**
	 * 
	 */
	public int prob3  = 0  ;
	
	public int getProb3(){
		return this.prob3;
	}
	
	public void setProb3(int v){
		this.prob3=v;
	}
	
	/**
	 * 
	 */
	public int battletype  = 0  ;
	
	public int getBattletype(){
		return this.battletype;
	}
	
	public void setBattletype(int v){
		this.battletype=v;
	}
	
	
};