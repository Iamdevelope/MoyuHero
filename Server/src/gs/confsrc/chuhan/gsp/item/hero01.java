package chuhan.gsp.item;


public class hero01 implements mytools.ConvMain.Checkable ,Comparable<hero01>{

	public int compareTo(hero01 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public hero01(){
		super();
	}
	public hero01(hero01 arg){
		this.id=arg.id ;
		this.comment=arg.comment ;
		this.NameID=arg.NameID ;
		this.DescriptionID=arg.DescriptionID ;
		this.Quality=arg.Quality ;
		this.herotype=arg.herotype ;
		this.InitMaxHP=arg.InitMaxHP ;
		this.InitPhysicalAttack=arg.InitPhysicalAttack ;
		this.InitPhysicalDefence=arg.InitPhysicalDefence ;
		this.InitMagicAttack=arg.InitMagicAttack ;
		this.InitMagicDefence=arg.InitMagicDefence ;
		this.InitHit=arg.InitHit ;
		this.InitDodge=arg.InitDodge ;
		this.InitCritical=arg.InitCritical ;
		this.InitTenacity=arg.InitTenacity ;
		this.InitSpeed=arg.InitSpeed ;
		this.HPGrowth=arg.HPGrowth ;
		this.HPGrowthMultiple=arg.HPGrowthMultiple ;
		this.PhysicalAttackGrowth=arg.PhysicalAttackGrowth ;
		this.PhysicalAttackGrowthMultiple=arg.PhysicalAttackGrowthMultiple ;
		this.PhysicalDefenceGrowth=arg.PhysicalDefenceGrowth ;
		this.PhysicalDefenceGrowthMultiple=arg.PhysicalDefenceGrowthMultiple ;
		this.MagicAttackGrowth=arg.MagicAttackGrowth ;
		this.MagicAttackGrowthMultiple=arg.MagicAttackGrowthMultiple ;
		this.MagicDefenceGrowth=arg.MagicDefenceGrowth ;
		this.MagicDefenceGrowthMultiple=arg.MagicDefenceGrowthMultiple ;
		this.HitGrowth=arg.HitGrowth ;
		this.HitGrowthMultiple=arg.HitGrowthMultiple ;
		this.DodgeGrowth=arg.DodgeGrowth ;
		this.DodgeGrowthMultiple=arg.DodgeGrowthMultiple ;
		this.CriticalGrowth=arg.CriticalGrowth ;
		this.CriticalGrowthMultiple=arg.CriticalGrowthMultiple ;
		this.TenacityGrowth=arg.TenacityGrowth ;
		this.TenacityGrowthMultiple=arg.TenacityGrowthMultiple ;
		this.SpeedGrowth=arg.SpeedGrowth ;
		this.SpeedGrowthMultiple=arg.SpeedGrowthMultiple ;
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
		this.MaxLevel=arg.MaxLevel ;
		this.ExpNum=arg.ExpNum ;
		this.normalskill=arg.normalskill ;
		this.skill1ID=arg.skill1ID ;
		this.skill2ID=arg.skill2ID ;
		this.skill3ID=arg.skill3ID ;
		this.artresources=arg.artresources ;
		this.movespeed=arg.movespeed ;
		this.HPTransformFury=arg.HPTransformFury ;
		this.entranceFury=arg.entranceFury ;
		this.waveFury=arg.waveFury ;
		this.clientSignType=arg.clientSignType ;
		this.camp=arg.camp ;
		this.fadeCondition=arg.fadeCondition ;
		this.titleID=arg.titleID ;
		this.skillPair=arg.skillPair ;
		this.runePair1=arg.runePair1 ;
		this.runePair2=arg.runePair2 ;
		this.runePair3=arg.runePair3 ;
		this.runePair4=arg.runePair4 ;
		this.runePassive=arg.runePassive ;
		this.stageUpCostType1=arg.stageUpCostType1 ;
		this.stageUpCost1=arg.stageUpCost1 ;
		this.stageUpCostType2=arg.stageUpCostType2 ;
		this.stageUpCost2=arg.stageUpCost2 ;
		this.useableArtresource=arg.useableArtresource ;
		this.trainSlot1=arg.trainSlot1 ;
		this.trainMaximum1=arg.trainMaximum1 ;
		this.trainSlot2=arg.trainSlot2 ;
		this.trainMaximum2=arg.trainMaximum2 ;
		this.trainSlot3=arg.trainSlot3 ;
		this.trainMaximum3=arg.trainMaximum3 ;
		this.trainSlot4=arg.trainSlot4 ;
		this.trainMaximum4=arg.trainMaximum4 ;
		this.stageUpTargetID=arg.stageUpTargetID ;
		this.maxQuality=arg.maxQuality ;
		this.returnBack=arg.returnBack ;
		this.heroDes=arg.heroDes ;
		this.skillMaxLevel=arg.skillMaxLevel ;
		this.accessMethod=arg.accessMethod ;
		this.systemBroadcasts=arg.systemBroadcasts ;
		this.Born=arg.Born ;
		this.Qosition=arg.Qosition ;
		this.BlockHit=arg.BlockHit ;
		this.SabotageHit=arg.SabotageHit ;
		this.DamageBonusHit=arg.DamageBonusHit ;
		this.DamageReductionHit=arg.DamageReductionHit ;
		this.FuryId=arg.FuryId ;
		this.Gold=arg.Gold ;
		this.Stuff=arg.Stuff ;
		this.Numbers=arg.Numbers ;
		this.Paxid=arg.Paxid ;
		this.SyntheticItemid=arg.SyntheticItemid ;
		this.SyntheticCount=arg.SyntheticCount ;
		this.VampireRate=arg.VampireRate ;
		this.msid=arg.msid ;
		this.equipmentid=arg.equipmentid ;
		this.totalskill=arg.totalskill ;
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
	public String comment  = null  ;
	
	public String getComment(){
		return this.comment;
	}
	
	public void setComment(String v){
		this.comment=v;
	}
	
	/**
	 * 
	 */
	public String NameID  = null  ;
	
	public String getNameID(){
		return this.NameID;
	}
	
	public void setNameID(String v){
		this.NameID=v;
	}
	
	/**
	 * 
	 */
	public String DescriptionID  = null  ;
	
	public String getDescriptionID(){
		return this.DescriptionID;
	}
	
	public void setDescriptionID(String v){
		this.DescriptionID=v;
	}
	
	/**
	 * 
	 */
	public int Quality  = 0  ;
	
	public int getQuality(){
		return this.Quality;
	}
	
	public void setQuality(int v){
		this.Quality=v;
	}
	
	/**
	 * 
	 */
	public int herotype  = 0  ;
	
	public int getHerotype(){
		return this.herotype;
	}
	
	public void setHerotype(int v){
		this.herotype=v;
	}
	
	/**
	 * 
	 */
	public int InitMaxHP  = 0  ;
	
	public int getInitMaxHP(){
		return this.InitMaxHP;
	}
	
	public void setInitMaxHP(int v){
		this.InitMaxHP=v;
	}
	
	/**
	 * 
	 */
	public int InitPhysicalAttack  = 0  ;
	
	public int getInitPhysicalAttack(){
		return this.InitPhysicalAttack;
	}
	
	public void setInitPhysicalAttack(int v){
		this.InitPhysicalAttack=v;
	}
	
	/**
	 * 
	 */
	public int InitPhysicalDefence  = 0  ;
	
	public int getInitPhysicalDefence(){
		return this.InitPhysicalDefence;
	}
	
	public void setInitPhysicalDefence(int v){
		this.InitPhysicalDefence=v;
	}
	
	/**
	 * 
	 */
	public int InitMagicAttack  = 0  ;
	
	public int getInitMagicAttack(){
		return this.InitMagicAttack;
	}
	
	public void setInitMagicAttack(int v){
		this.InitMagicAttack=v;
	}
	
	/**
	 * 
	 */
	public int InitMagicDefence  = 0  ;
	
	public int getInitMagicDefence(){
		return this.InitMagicDefence;
	}
	
	public void setInitMagicDefence(int v){
		this.InitMagicDefence=v;
	}
	
	/**
	 * 
	 */
	public int InitHit  = 0  ;
	
	public int getInitHit(){
		return this.InitHit;
	}
	
	public void setInitHit(int v){
		this.InitHit=v;
	}
	
	/**
	 * 
	 */
	public int InitDodge  = 0  ;
	
	public int getInitDodge(){
		return this.InitDodge;
	}
	
	public void setInitDodge(int v){
		this.InitDodge=v;
	}
	
	/**
	 * 
	 */
	public int InitCritical  = 0  ;
	
	public int getInitCritical(){
		return this.InitCritical;
	}
	
	public void setInitCritical(int v){
		this.InitCritical=v;
	}
	
	/**
	 * 
	 */
	public int InitTenacity  = 0  ;
	
	public int getInitTenacity(){
		return this.InitTenacity;
	}
	
	public void setInitTenacity(int v){
		this.InitTenacity=v;
	}
	
	/**
	 * 
	 */
	public int InitSpeed  = 0  ;
	
	public int getInitSpeed(){
		return this.InitSpeed;
	}
	
	public void setInitSpeed(int v){
		this.InitSpeed=v;
	}
	
	/**
	 * 
	 */
	public String HPGrowth  = null  ;
	
	public String getHPGrowth(){
		return this.HPGrowth;
	}
	
	public void setHPGrowth(String v){
		this.HPGrowth=v;
	}
	
	/**
	 * 
	 */
	public int HPGrowthMultiple  = 0  ;
	
	public int getHPGrowthMultiple(){
		return this.HPGrowthMultiple;
	}
	
	public void setHPGrowthMultiple(int v){
		this.HPGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String PhysicalAttackGrowth  = null  ;
	
	public String getPhysicalAttackGrowth(){
		return this.PhysicalAttackGrowth;
	}
	
	public void setPhysicalAttackGrowth(String v){
		this.PhysicalAttackGrowth=v;
	}
	
	/**
	 * 
	 */
	public int PhysicalAttackGrowthMultiple  = 0  ;
	
	public int getPhysicalAttackGrowthMultiple(){
		return this.PhysicalAttackGrowthMultiple;
	}
	
	public void setPhysicalAttackGrowthMultiple(int v){
		this.PhysicalAttackGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String PhysicalDefenceGrowth  = null  ;
	
	public String getPhysicalDefenceGrowth(){
		return this.PhysicalDefenceGrowth;
	}
	
	public void setPhysicalDefenceGrowth(String v){
		this.PhysicalDefenceGrowth=v;
	}
	
	/**
	 * 
	 */
	public int PhysicalDefenceGrowthMultiple  = 0  ;
	
	public int getPhysicalDefenceGrowthMultiple(){
		return this.PhysicalDefenceGrowthMultiple;
	}
	
	public void setPhysicalDefenceGrowthMultiple(int v){
		this.PhysicalDefenceGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String MagicAttackGrowth  = null  ;
	
	public String getMagicAttackGrowth(){
		return this.MagicAttackGrowth;
	}
	
	public void setMagicAttackGrowth(String v){
		this.MagicAttackGrowth=v;
	}
	
	/**
	 * 
	 */
	public int MagicAttackGrowthMultiple  = 0  ;
	
	public int getMagicAttackGrowthMultiple(){
		return this.MagicAttackGrowthMultiple;
	}
	
	public void setMagicAttackGrowthMultiple(int v){
		this.MagicAttackGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String MagicDefenceGrowth  = null  ;
	
	public String getMagicDefenceGrowth(){
		return this.MagicDefenceGrowth;
	}
	
	public void setMagicDefenceGrowth(String v){
		this.MagicDefenceGrowth=v;
	}
	
	/**
	 * 
	 */
	public int MagicDefenceGrowthMultiple  = 0  ;
	
	public int getMagicDefenceGrowthMultiple(){
		return this.MagicDefenceGrowthMultiple;
	}
	
	public void setMagicDefenceGrowthMultiple(int v){
		this.MagicDefenceGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String HitGrowth  = null  ;
	
	public String getHitGrowth(){
		return this.HitGrowth;
	}
	
	public void setHitGrowth(String v){
		this.HitGrowth=v;
	}
	
	/**
	 * 
	 */
	public int HitGrowthMultiple  = 0  ;
	
	public int getHitGrowthMultiple(){
		return this.HitGrowthMultiple;
	}
	
	public void setHitGrowthMultiple(int v){
		this.HitGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String DodgeGrowth  = null  ;
	
	public String getDodgeGrowth(){
		return this.DodgeGrowth;
	}
	
	public void setDodgeGrowth(String v){
		this.DodgeGrowth=v;
	}
	
	/**
	 * 
	 */
	public int DodgeGrowthMultiple  = 0  ;
	
	public int getDodgeGrowthMultiple(){
		return this.DodgeGrowthMultiple;
	}
	
	public void setDodgeGrowthMultiple(int v){
		this.DodgeGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String CriticalGrowth  = null  ;
	
	public String getCriticalGrowth(){
		return this.CriticalGrowth;
	}
	
	public void setCriticalGrowth(String v){
		this.CriticalGrowth=v;
	}
	
	/**
	 * 
	 */
	public int CriticalGrowthMultiple  = 0  ;
	
	public int getCriticalGrowthMultiple(){
		return this.CriticalGrowthMultiple;
	}
	
	public void setCriticalGrowthMultiple(int v){
		this.CriticalGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String TenacityGrowth  = null  ;
	
	public String getTenacityGrowth(){
		return this.TenacityGrowth;
	}
	
	public void setTenacityGrowth(String v){
		this.TenacityGrowth=v;
	}
	
	/**
	 * 
	 */
	public int TenacityGrowthMultiple  = 0  ;
	
	public int getTenacityGrowthMultiple(){
		return this.TenacityGrowthMultiple;
	}
	
	public void setTenacityGrowthMultiple(int v){
		this.TenacityGrowthMultiple=v;
	}
	
	/**
	 * 
	 */
	public String SpeedGrowth  = null  ;
	
	public String getSpeedGrowth(){
		return this.SpeedGrowth;
	}
	
	public void setSpeedGrowth(String v){
		this.SpeedGrowth=v;
	}
	
	/**
	 * 
	 */
	public int SpeedGrowthMultiple  = 0  ;
	
	public int getSpeedGrowthMultiple(){
		return this.SpeedGrowthMultiple;
	}
	
	public void setSpeedGrowthMultiple(int v){
		this.SpeedGrowthMultiple=v;
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
	public int MaxLevel  = 0  ;
	
	public int getMaxLevel(){
		return this.MaxLevel;
	}
	
	public void setMaxLevel(int v){
		this.MaxLevel=v;
	}
	
	/**
	 * 
	 */
	public int ExpNum  = 0  ;
	
	public int getExpNum(){
		return this.ExpNum;
	}
	
	public void setExpNum(int v){
		this.ExpNum=v;
	}
	
	/**
	 * 
	 */
	public int normalskill  = 0  ;
	
	public int getNormalskill(){
		return this.normalskill;
	}
	
	public void setNormalskill(int v){
		this.normalskill=v;
	}
	
	/**
	 * 
	 */
	public int skill1ID  = 0  ;
	
	public int getSkill1ID(){
		return this.skill1ID;
	}
	
	public void setSkill1ID(int v){
		this.skill1ID=v;
	}
	
	/**
	 * 
	 */
	public int skill2ID  = 0  ;
	
	public int getSkill2ID(){
		return this.skill2ID;
	}
	
	public void setSkill2ID(int v){
		this.skill2ID=v;
	}
	
	/**
	 * 
	 */
	public int skill3ID  = 0  ;
	
	public int getSkill3ID(){
		return this.skill3ID;
	}
	
	public void setSkill3ID(int v){
		this.skill3ID=v;
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
	public String movespeed  = null  ;
	
	public String getMovespeed(){
		return this.movespeed;
	}
	
	public void setMovespeed(String v){
		this.movespeed=v;
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
	public String titleID  = null  ;
	
	public String getTitleID(){
		return this.titleID;
	}
	
	public void setTitleID(String v){
		this.titleID=v;
	}
	
	/**
	 * 
	 */
	public String skillPair  = null  ;
	
	public String getSkillPair(){
		return this.skillPair;
	}
	
	public void setSkillPair(String v){
		this.skillPair=v;
	}
	
	/**
	 * 
	 */
	public int runePair1  = 0  ;
	
	public int getRunePair1(){
		return this.runePair1;
	}
	
	public void setRunePair1(int v){
		this.runePair1=v;
	}
	
	/**
	 * 
	 */
	public int runePair2  = 0  ;
	
	public int getRunePair2(){
		return this.runePair2;
	}
	
	public void setRunePair2(int v){
		this.runePair2=v;
	}
	
	/**
	 * 
	 */
	public int runePair3  = 0  ;
	
	public int getRunePair3(){
		return this.runePair3;
	}
	
	public void setRunePair3(int v){
		this.runePair3=v;
	}
	
	/**
	 * 
	 */
	public int runePair4  = 0  ;
	
	public int getRunePair4(){
		return this.runePair4;
	}
	
	public void setRunePair4(int v){
		this.runePair4=v;
	}
	
	/**
	 * 
	 */
	public int runePassive  = 0  ;
	
	public int getRunePassive(){
		return this.runePassive;
	}
	
	public void setRunePassive(int v){
		this.runePassive=v;
	}
	
	/**
	 * 
	 */
	public int stageUpCostType1  = 0  ;
	
	public int getStageUpCostType1(){
		return this.stageUpCostType1;
	}
	
	public void setStageUpCostType1(int v){
		this.stageUpCostType1=v;
	}
	
	/**
	 * 
	 */
	public int stageUpCost1  = 0  ;
	
	public int getStageUpCost1(){
		return this.stageUpCost1;
	}
	
	public void setStageUpCost1(int v){
		this.stageUpCost1=v;
	}
	
	/**
	 * 
	 */
	public int stageUpCostType2  = 0  ;
	
	public int getStageUpCostType2(){
		return this.stageUpCostType2;
	}
	
	public void setStageUpCostType2(int v){
		this.stageUpCostType2=v;
	}
	
	/**
	 * 
	 */
	public int stageUpCost2  = 0  ;
	
	public int getStageUpCost2(){
		return this.stageUpCost2;
	}
	
	public void setStageUpCost2(int v){
		this.stageUpCost2=v;
	}
	
	/**
	 * 
	 */
	public String useableArtresource  = null  ;
	
	public String getUseableArtresource(){
		return this.useableArtresource;
	}
	
	public void setUseableArtresource(String v){
		this.useableArtresource=v;
	}
	
	/**
	 * 
	 */
	public int trainSlot1  = 0  ;
	
	public int getTrainSlot1(){
		return this.trainSlot1;
	}
	
	public void setTrainSlot1(int v){
		this.trainSlot1=v;
	}
	
	/**
	 * 
	 */
	public int trainMaximum1  = 0  ;
	
	public int getTrainMaximum1(){
		return this.trainMaximum1;
	}
	
	public void setTrainMaximum1(int v){
		this.trainMaximum1=v;
	}
	
	/**
	 * 
	 */
	public int trainSlot2  = 0  ;
	
	public int getTrainSlot2(){
		return this.trainSlot2;
	}
	
	public void setTrainSlot2(int v){
		this.trainSlot2=v;
	}
	
	/**
	 * 
	 */
	public int trainMaximum2  = 0  ;
	
	public int getTrainMaximum2(){
		return this.trainMaximum2;
	}
	
	public void setTrainMaximum2(int v){
		this.trainMaximum2=v;
	}
	
	/**
	 * 
	 */
	public int trainSlot3  = 0  ;
	
	public int getTrainSlot3(){
		return this.trainSlot3;
	}
	
	public void setTrainSlot3(int v){
		this.trainSlot3=v;
	}
	
	/**
	 * 
	 */
	public int trainMaximum3  = 0  ;
	
	public int getTrainMaximum3(){
		return this.trainMaximum3;
	}
	
	public void setTrainMaximum3(int v){
		this.trainMaximum3=v;
	}
	
	/**
	 * 
	 */
	public int trainSlot4  = 0  ;
	
	public int getTrainSlot4(){
		return this.trainSlot4;
	}
	
	public void setTrainSlot4(int v){
		this.trainSlot4=v;
	}
	
	/**
	 * 
	 */
	public int trainMaximum4  = 0  ;
	
	public int getTrainMaximum4(){
		return this.trainMaximum4;
	}
	
	public void setTrainMaximum4(int v){
		this.trainMaximum4=v;
	}
	
	/**
	 * 
	 */
	public int stageUpTargetID  = 0  ;
	
	public int getStageUpTargetID(){
		return this.stageUpTargetID;
	}
	
	public void setStageUpTargetID(int v){
		this.stageUpTargetID=v;
	}
	
	/**
	 * 
	 */
	public int maxQuality  = 0  ;
	
	public int getMaxQuality(){
		return this.maxQuality;
	}
	
	public void setMaxQuality(int v){
		this.maxQuality=v;
	}
	
	/**
	 * 
	 */
	public int returnBack  = 0  ;
	
	public int getReturnBack(){
		return this.returnBack;
	}
	
	public void setReturnBack(int v){
		this.returnBack=v;
	}
	
	/**
	 * 
	 */
	public String heroDes  = null  ;
	
	public String getHeroDes(){
		return this.heroDes;
	}
	
	public void setHeroDes(String v){
		this.heroDes=v;
	}
	
	/**
	 * 
	 */
	public int skillMaxLevel  = 0  ;
	
	public int getSkillMaxLevel(){
		return this.skillMaxLevel;
	}
	
	public void setSkillMaxLevel(int v){
		this.skillMaxLevel=v;
	}
	
	/**
	 * 
	 */
	public String accessMethod  = null  ;
	
	public String getAccessMethod(){
		return this.accessMethod;
	}
	
	public void setAccessMethod(String v){
		this.accessMethod=v;
	}
	
	/**
	 * 
	 */
	public int systemBroadcasts  = 0  ;
	
	public int getSystemBroadcasts(){
		return this.systemBroadcasts;
	}
	
	public void setSystemBroadcasts(int v){
		this.systemBroadcasts=v;
	}
	
	/**
	 * 
	 */
	public int Born  = 0  ;
	
	public int getBorn(){
		return this.Born;
	}
	
	public void setBorn(int v){
		this.Born=v;
	}
	
	/**
	 * 
	 */
	public int Qosition  = 0  ;
	
	public int getQosition(){
		return this.Qosition;
	}
	
	public void setQosition(int v){
		this.Qosition=v;
	}
	
	/**
	 * 
	 */
	public int BlockHit  = 0  ;
	
	public int getBlockHit(){
		return this.BlockHit;
	}
	
	public void setBlockHit(int v){
		this.BlockHit=v;
	}
	
	/**
	 * 
	 */
	public int SabotageHit  = 0  ;
	
	public int getSabotageHit(){
		return this.SabotageHit;
	}
	
	public void setSabotageHit(int v){
		this.SabotageHit=v;
	}
	
	/**
	 * 
	 */
	public int DamageBonusHit  = 0  ;
	
	public int getDamageBonusHit(){
		return this.DamageBonusHit;
	}
	
	public void setDamageBonusHit(int v){
		this.DamageBonusHit=v;
	}
	
	/**
	 * 
	 */
	public int DamageReductionHit  = 0  ;
	
	public int getDamageReductionHit(){
		return this.DamageReductionHit;
	}
	
	public void setDamageReductionHit(int v){
		this.DamageReductionHit=v;
	}
	
	/**
	 * 
	 */
	public int FuryId  = 0  ;
	
	public int getFuryId(){
		return this.FuryId;
	}
	
	public void setFuryId(int v){
		this.FuryId=v;
	}
	
	/**
	 * 
	 */
	public int Gold  = 0  ;
	
	public int getGold(){
		return this.Gold;
	}
	
	public void setGold(int v){
		this.Gold=v;
	}
	
	/**
	 * 
	 */
	public int Stuff  = 0  ;
	
	public int getStuff(){
		return this.Stuff;
	}
	
	public void setStuff(int v){
		this.Stuff=v;
	}
	
	/**
	 * 
	 */
	public int Numbers  = 0  ;
	
	public int getNumbers(){
		return this.Numbers;
	}
	
	public void setNumbers(int v){
		this.Numbers=v;
	}
	
	/**
	 * 
	 */
	public int Paxid  = 0  ;
	
	public int getPaxid(){
		return this.Paxid;
	}
	
	public void setPaxid(int v){
		this.Paxid=v;
	}
	
	/**
	 * 
	 */
	public int SyntheticItemid  = 0  ;
	
	public int getSyntheticItemid(){
		return this.SyntheticItemid;
	}
	
	public void setSyntheticItemid(int v){
		this.SyntheticItemid=v;
	}
	
	/**
	 * 
	 */
	public int SyntheticCount  = 0  ;
	
	public int getSyntheticCount(){
		return this.SyntheticCount;
	}
	
	public void setSyntheticCount(int v){
		this.SyntheticCount=v;
	}
	
	/**
	 * 
	 */
	public int VampireRate  = 0  ;
	
	public int getVampireRate(){
		return this.VampireRate;
	}
	
	public void setVampireRate(int v){
		this.VampireRate=v;
	}
	
	/**
	 * 
	 */
	public String msid  = null  ;
	
	public String getMsid(){
		return this.msid;
	}
	
	public void setMsid(String v){
		this.msid=v;
	}
	
	/**
	 * 
	 */
	public String equipmentid  = null  ;
	
	public String getEquipmentid(){
		return this.equipmentid;
	}
	
	public void setEquipmentid(String v){
		this.equipmentid=v;
	}
	
	/**
	 * 
	 */
	public String totalskill  = null  ;
	
	public String getTotalskill(){
		return this.totalskill;
	}
	
	public void setTotalskill(String v){
		this.totalskill=v;
	}
	
	
};