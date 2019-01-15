package chuhan.gsp.task;


public class monster13 implements mytools.ConvMain.Checkable ,Comparable<monster13>{

	public int compareTo(monster13 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public monster13(){
		super();
	}
	public monster13(monster13 arg){
		this.id=arg.id ;
		this.monstername=arg.monstername ;
		this.artresources=arg.artresources ;
		this.monstertype=arg.monstertype ;
		this.normalattack=arg.normalattack ;
		this.monsterskill=arg.monsterskill ;
		this.MaxHP=arg.MaxHP ;
		this.PhysicalAttack=arg.PhysicalAttack ;
		this.PhysicalDefence=arg.PhysicalDefence ;
		this.MagicAttack=arg.MagicAttack ;
		this.MagicDefence=arg.MagicDefence ;
		this.Hit=arg.Hit ;
		this.Dodge=arg.Dodge ;
		this.Critical=arg.Critical ;
		this.Tenacity=arg.Tenacity ;
		this.Speed=arg.Speed ;
		this.BaseHit=arg.BaseHit ;
		this.BaseDodge=arg.BaseDodge ;
		this.BaseCritical=arg.BaseCritical ;
		this.BaseTenacity=arg.BaseTenacity ;
		this.BasePhyDamageIncrease=arg.BasePhyDamageIncrease ;
		this.BasePhyDamageDecrease=arg.BasePhyDamageDecrease ;
		this.BaseMagDamageIncrease=arg.BaseMagDamageIncrease ;
		this.BaseMagDamageDecrease=arg.BaseMagDamageDecrease ;
		this.BaseCriticalDamage=arg.BaseCriticalDamage ;
		this.DamageIncrease=arg.DamageIncrease ;
		this.lifeRestoringForce=arg.lifeRestoringForce ;
		this.DamageDecrease=arg.DamageDecrease ;
		this.clientSignType=arg.clientSignType ;
		this.HPLengthMultiple=arg.HPLengthMultiple ;
		this.HPWidthMultiple=arg.HPWidthMultiple ;
		this.damageTextShow=arg.damageTextShow ;
		this.levelBorderMultiple=arg.levelBorderMultiple ;
		this.HPTransformFury=arg.HPTransformFury ;
		this.camp=arg.camp ;
		this.fadeCondition=arg.fadeCondition ;
		this.deathSlowTime=arg.deathSlowTime ;
		this.monsterlevel=arg.monsterlevel ;
		this.monsterstar=arg.monsterstar ;
		this.monsterEnlarge=arg.monsterEnlarge ;
		this.monsterPercentMaxHp=arg.monsterPercentMaxHp ;
		this.deathSkillType=arg.deathSkillType ;
		this.deathSkillProb=arg.deathSkillProb ;
		this.monstermaxstar=arg.monstermaxstar ;
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
	public String monstername  = null  ;
	
	public String getMonstername(){
		return this.monstername;
	}
	
	public void setMonstername(String v){
		this.monstername=v;
	}
	
	/**
	 * 
	 */
	public int artresources  = 0  ;
	
	public int getArtresources(){
		return this.artresources;
	}
	
	public void setArtresources(int v){
		this.artresources=v;
	}
	
	/**
	 * 
	 */
	public int monstertype  = 0  ;
	
	public int getMonstertype(){
		return this.monstertype;
	}
	
	public void setMonstertype(int v){
		this.monstertype=v;
	}
	
	/**
	 * 
	 */
	public int normalattack  = 0  ;
	
	public int getNormalattack(){
		return this.normalattack;
	}
	
	public void setNormalattack(int v){
		this.normalattack=v;
	}
	
	/**
	 * 
	 */
	public String monsterskill  = null  ;
	
	public String getMonsterskill(){
		return this.monsterskill;
	}
	
	public void setMonsterskill(String v){
		this.monsterskill=v;
	}
	
	/**
	 * 
	 */
	public int MaxHP  = 0  ;
	
	public int getMaxHP(){
		return this.MaxHP;
	}
	
	public void setMaxHP(int v){
		this.MaxHP=v;
	}
	
	/**
	 * 
	 */
	public int PhysicalAttack  = 0  ;
	
	public int getPhysicalAttack(){
		return this.PhysicalAttack;
	}
	
	public void setPhysicalAttack(int v){
		this.PhysicalAttack=v;
	}
	
	/**
	 * 
	 */
	public int PhysicalDefence  = 0  ;
	
	public int getPhysicalDefence(){
		return this.PhysicalDefence;
	}
	
	public void setPhysicalDefence(int v){
		this.PhysicalDefence=v;
	}
	
	/**
	 * 
	 */
	public int MagicAttack  = 0  ;
	
	public int getMagicAttack(){
		return this.MagicAttack;
	}
	
	public void setMagicAttack(int v){
		this.MagicAttack=v;
	}
	
	/**
	 * 
	 */
	public int MagicDefence  = 0  ;
	
	public int getMagicDefence(){
		return this.MagicDefence;
	}
	
	public void setMagicDefence(int v){
		this.MagicDefence=v;
	}
	
	/**
	 * 
	 */
	public int Hit  = 0  ;
	
	public int getHit(){
		return this.Hit;
	}
	
	public void setHit(int v){
		this.Hit=v;
	}
	
	/**
	 * 
	 */
	public int Dodge  = 0  ;
	
	public int getDodge(){
		return this.Dodge;
	}
	
	public void setDodge(int v){
		this.Dodge=v;
	}
	
	/**
	 * 
	 */
	public int Critical  = 0  ;
	
	public int getCritical(){
		return this.Critical;
	}
	
	public void setCritical(int v){
		this.Critical=v;
	}
	
	/**
	 * 
	 */
	public int Tenacity  = 0  ;
	
	public int getTenacity(){
		return this.Tenacity;
	}
	
	public void setTenacity(int v){
		this.Tenacity=v;
	}
	
	/**
	 * 
	 */
	public int Speed  = 0  ;
	
	public int getSpeed(){
		return this.Speed;
	}
	
	public void setSpeed(int v){
		this.Speed=v;
	}
	
	/**
	 * 
	 */
	public int BaseHit  = 0  ;
	
	public int getBaseHit(){
		return this.BaseHit;
	}
	
	public void setBaseHit(int v){
		this.BaseHit=v;
	}
	
	/**
	 * 
	 */
	public int BaseDodge  = 0  ;
	
	public int getBaseDodge(){
		return this.BaseDodge;
	}
	
	public void setBaseDodge(int v){
		this.BaseDodge=v;
	}
	
	/**
	 * 
	 */
	public int BaseCritical  = 0  ;
	
	public int getBaseCritical(){
		return this.BaseCritical;
	}
	
	public void setBaseCritical(int v){
		this.BaseCritical=v;
	}
	
	/**
	 * 
	 */
	public int BaseTenacity  = 0  ;
	
	public int getBaseTenacity(){
		return this.BaseTenacity;
	}
	
	public void setBaseTenacity(int v){
		this.BaseTenacity=v;
	}
	
	/**
	 * 
	 */
	public int BasePhyDamageIncrease  = 0  ;
	
	public int getBasePhyDamageIncrease(){
		return this.BasePhyDamageIncrease;
	}
	
	public void setBasePhyDamageIncrease(int v){
		this.BasePhyDamageIncrease=v;
	}
	
	/**
	 * 
	 */
	public int BasePhyDamageDecrease  = 0  ;
	
	public int getBasePhyDamageDecrease(){
		return this.BasePhyDamageDecrease;
	}
	
	public void setBasePhyDamageDecrease(int v){
		this.BasePhyDamageDecrease=v;
	}
	
	/**
	 * 
	 */
	public int BaseMagDamageIncrease  = 0  ;
	
	public int getBaseMagDamageIncrease(){
		return this.BaseMagDamageIncrease;
	}
	
	public void setBaseMagDamageIncrease(int v){
		this.BaseMagDamageIncrease=v;
	}
	
	/**
	 * 
	 */
	public int BaseMagDamageDecrease  = 0  ;
	
	public int getBaseMagDamageDecrease(){
		return this.BaseMagDamageDecrease;
	}
	
	public void setBaseMagDamageDecrease(int v){
		this.BaseMagDamageDecrease=v;
	}
	
	/**
	 * 
	 */
	public int BaseCriticalDamage  = 0  ;
	
	public int getBaseCriticalDamage(){
		return this.BaseCriticalDamage;
	}
	
	public void setBaseCriticalDamage(int v){
		this.BaseCriticalDamage=v;
	}
	
	/**
	 * 
	 */
	public int DamageIncrease  = 0  ;
	
	public int getDamageIncrease(){
		return this.DamageIncrease;
	}
	
	public void setDamageIncrease(int v){
		this.DamageIncrease=v;
	}
	
	/**
	 * 
	 */
	public int lifeRestoringForce  = 0  ;
	
	public int getLifeRestoringForce(){
		return this.lifeRestoringForce;
	}
	
	public void setLifeRestoringForce(int v){
		this.lifeRestoringForce=v;
	}
	
	/**
	 * 
	 */
	public int DamageDecrease  = 0  ;
	
	public int getDamageDecrease(){
		return this.DamageDecrease;
	}
	
	public void setDamageDecrease(int v){
		this.DamageDecrease=v;
	}
	
	/**
	 * 
	 */
	public String clientSignType  = null  ;
	
	public String getClientSignType(){
		return this.clientSignType;
	}
	
	public void setClientSignType(String v){
		this.clientSignType=v;
	}
	
	/**
	 * 
	 */
	public int HPLengthMultiple  = 0  ;
	
	public int getHPLengthMultiple(){
		return this.HPLengthMultiple;
	}
	
	public void setHPLengthMultiple(int v){
		this.HPLengthMultiple=v;
	}
	
	/**
	 * 
	 */
	public int HPWidthMultiple  = 0  ;
	
	public int getHPWidthMultiple(){
		return this.HPWidthMultiple;
	}
	
	public void setHPWidthMultiple(int v){
		this.HPWidthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String damageTextShow  = null  ;
	
	public String getDamageTextShow(){
		return this.damageTextShow;
	}
	
	public void setDamageTextShow(String v){
		this.damageTextShow=v;
	}
	
	/**
	 * 
	 */
	public int levelBorderMultiple  = 0  ;
	
	public int getLevelBorderMultiple(){
		return this.levelBorderMultiple;
	}
	
	public void setLevelBorderMultiple(int v){
		this.levelBorderMultiple=v;
	}
	
	/**
	 * 
	 */
	public int HPTransformFury  = 0  ;
	
	public int getHPTransformFury(){
		return this.HPTransformFury;
	}
	
	public void setHPTransformFury(int v){
		this.HPTransformFury=v;
	}
	
	/**
	 * 
	 */
	public int camp  = 0  ;
	
	public int getCamp(){
		return this.camp;
	}
	
	public void setCamp(int v){
		this.camp=v;
	}
	
	/**
	 * 
	 */
	public int fadeCondition  = 0  ;
	
	public int getFadeCondition(){
		return this.fadeCondition;
	}
	
	public void setFadeCondition(int v){
		this.fadeCondition=v;
	}
	
	/**
	 * 
	 */
	public String deathSlowTime  = null  ;
	
	public String getDeathSlowTime(){
		return this.deathSlowTime;
	}
	
	public void setDeathSlowTime(String v){
		this.deathSlowTime=v;
	}
	
	/**
	 * 
	 */
	public int monsterlevel  = 0  ;
	
	public int getMonsterlevel(){
		return this.monsterlevel;
	}
	
	public void setMonsterlevel(int v){
		this.monsterlevel=v;
	}
	
	/**
	 * 
	 */
	public int monsterstar  = 0  ;
	
	public int getMonsterstar(){
		return this.monsterstar;
	}
	
	public void setMonsterstar(int v){
		this.monsterstar=v;
	}
	
	/**
	 * 
	 */
	public String monsterEnlarge  = null  ;
	
	public String getMonsterEnlarge(){
		return this.monsterEnlarge;
	}
	
	public void setMonsterEnlarge(String v){
		this.monsterEnlarge=v;
	}
	
	/**
	 * 
	 */
	public int monsterPercentMaxHp  = 0  ;
	
	public int getMonsterPercentMaxHp(){
		return this.monsterPercentMaxHp;
	}
	
	public void setMonsterPercentMaxHp(int v){
		this.monsterPercentMaxHp=v;
	}
	
	/**
	 * 
	 */
	public String deathSkillType  = null  ;
	
	public String getDeathSkillType(){
		return this.deathSkillType;
	}
	
	public void setDeathSkillType(String v){
		this.deathSkillType=v;
	}
	
	/**
	 * 
	 */
	public String deathSkillProb  = null  ;
	
	public String getDeathSkillProb(){
		return this.deathSkillProb;
	}
	
	public void setDeathSkillProb(String v){
		this.deathSkillProb=v;
	}
	
	/**
	 * 
	 */
	public int monstermaxstar  = 0  ;
	
	public int getMonstermaxstar(){
		return this.monstermaxstar;
	}
	
	public void setMonstermaxstar(int v){
		this.monstermaxstar=v;
	}
	
	
};