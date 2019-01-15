package chuhan.gsp.game;


public class ultimatetrialmonster49 implements mytools.ConvMain.Checkable ,Comparable<ultimatetrialmonster49>{

	public int compareTo(ultimatetrialmonster49 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ultimatetrialmonster49(){
		super();
	}
	public ultimatetrialmonster49(ultimatetrialmonster49 arg){
		this.id=arg.id ;
		this.Probability=arg.Probability ;
		this.MaxHPCoefficient=arg.MaxHPCoefficient ;
		this.PhysicalAttackCoefficient=arg.PhysicalAttackCoefficient ;
		this.PhysicalDefenceCoefficient=arg.PhysicalDefenceCoefficient ;
		this.MagicAttackCoefficient=arg.MagicAttackCoefficient ;
		this.MagicDefenceCoefficient=arg.MagicDefenceCoefficient ;
		this.HitCoefficient=arg.HitCoefficient ;
		this.DodgeCoefficient=arg.DodgeCoefficient ;
		this.CriticalCoefficient=arg.CriticalCoefficient ;
		this.TenacityCoefficient=arg.TenacityCoefficient ;
		this.AdditionalMaxHP=arg.AdditionalMaxHP ;
		this.AdditionalPhysicalAttack=arg.AdditionalPhysicalAttack ;
		this.AdditionalPhysicalDefence=arg.AdditionalPhysicalDefence ;
		this.AdditionalMagicAttack=arg.AdditionalMagicAttack ;
		this.AdditionalMagicDefence=arg.AdditionalMagicDefence ;
		this.AdditionalHit=arg.AdditionalHit ;
		this.AdditionalDodge=arg.AdditionalDodge ;
		this.AdditionalCritical=arg.AdditionalCritical ;
		this.AdditionalTenacity=arg.AdditionalTenacity ;
		this.AdditionalSpeed=arg.AdditionalSpeed ;
		this.AdditionalBaseHit=arg.AdditionalBaseHit ;
		this.AdditionalBaseDodge=arg.AdditionalBaseDodge ;
		this.AdditionalBaseCritical=arg.AdditionalBaseCritical ;
		this.AdditionalBaseTenacity=arg.AdditionalBaseTenacity ;
		this.AdditionalBasePhyDamageIncrease=arg.AdditionalBasePhyDamageIncrease ;
		this.AdditionalBasePhyDamageDecrease=arg.AdditionalBasePhyDamageDecrease ;
		this.AdditionalBaseMagDamageIncrease=arg.AdditionalBaseMagDamageIncrease ;
		this.AdditionalBaseMagDamageDecrease=arg.AdditionalBaseMagDamageDecrease ;
		this.AdditionalBaseCriticalDamage=arg.AdditionalBaseCriticalDamage ;
		this.AdditionalDamageIncrease=arg.AdditionalDamageIncrease ;
		this.AdditionallifeRestoringForce=arg.AdditionallifeRestoringForce ;
		this.AdditionalDamageDecrease=arg.AdditionalDamageDecrease ;
		this.AdditionalLevel=arg.AdditionalLevel ;
		this.AdditionalSkill=arg.AdditionalSkill ;
		this.wavereward=arg.wavereward ;
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
	public String Probability  = null  ;
	
	public String getProbability(){
		return this.Probability;
	}
	
	public void setProbability(String v){
		this.Probability=v;
	}
	
	/**
	 * 
	 */
	public String MaxHPCoefficient  = null  ;
	
	public String getMaxHPCoefficient(){
		return this.MaxHPCoefficient;
	}
	
	public void setMaxHPCoefficient(String v){
		this.MaxHPCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String PhysicalAttackCoefficient  = null  ;
	
	public String getPhysicalAttackCoefficient(){
		return this.PhysicalAttackCoefficient;
	}
	
	public void setPhysicalAttackCoefficient(String v){
		this.PhysicalAttackCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String PhysicalDefenceCoefficient  = null  ;
	
	public String getPhysicalDefenceCoefficient(){
		return this.PhysicalDefenceCoefficient;
	}
	
	public void setPhysicalDefenceCoefficient(String v){
		this.PhysicalDefenceCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String MagicAttackCoefficient  = null  ;
	
	public String getMagicAttackCoefficient(){
		return this.MagicAttackCoefficient;
	}
	
	public void setMagicAttackCoefficient(String v){
		this.MagicAttackCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String MagicDefenceCoefficient  = null  ;
	
	public String getMagicDefenceCoefficient(){
		return this.MagicDefenceCoefficient;
	}
	
	public void setMagicDefenceCoefficient(String v){
		this.MagicDefenceCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String HitCoefficient  = null  ;
	
	public String getHitCoefficient(){
		return this.HitCoefficient;
	}
	
	public void setHitCoefficient(String v){
		this.HitCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String DodgeCoefficient  = null  ;
	
	public String getDodgeCoefficient(){
		return this.DodgeCoefficient;
	}
	
	public void setDodgeCoefficient(String v){
		this.DodgeCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String CriticalCoefficient  = null  ;
	
	public String getCriticalCoefficient(){
		return this.CriticalCoefficient;
	}
	
	public void setCriticalCoefficient(String v){
		this.CriticalCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String TenacityCoefficient  = null  ;
	
	public String getTenacityCoefficient(){
		return this.TenacityCoefficient;
	}
	
	public void setTenacityCoefficient(String v){
		this.TenacityCoefficient=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalMaxHP  = null  ;
	
	public String getAdditionalMaxHP(){
		return this.AdditionalMaxHP;
	}
	
	public void setAdditionalMaxHP(String v){
		this.AdditionalMaxHP=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalPhysicalAttack  = null  ;
	
	public String getAdditionalPhysicalAttack(){
		return this.AdditionalPhysicalAttack;
	}
	
	public void setAdditionalPhysicalAttack(String v){
		this.AdditionalPhysicalAttack=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalPhysicalDefence  = null  ;
	
	public String getAdditionalPhysicalDefence(){
		return this.AdditionalPhysicalDefence;
	}
	
	public void setAdditionalPhysicalDefence(String v){
		this.AdditionalPhysicalDefence=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalMagicAttack  = null  ;
	
	public String getAdditionalMagicAttack(){
		return this.AdditionalMagicAttack;
	}
	
	public void setAdditionalMagicAttack(String v){
		this.AdditionalMagicAttack=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalMagicDefence  = null  ;
	
	public String getAdditionalMagicDefence(){
		return this.AdditionalMagicDefence;
	}
	
	public void setAdditionalMagicDefence(String v){
		this.AdditionalMagicDefence=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalHit  = null  ;
	
	public String getAdditionalHit(){
		return this.AdditionalHit;
	}
	
	public void setAdditionalHit(String v){
		this.AdditionalHit=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalDodge  = null  ;
	
	public String getAdditionalDodge(){
		return this.AdditionalDodge;
	}
	
	public void setAdditionalDodge(String v){
		this.AdditionalDodge=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalCritical  = null  ;
	
	public String getAdditionalCritical(){
		return this.AdditionalCritical;
	}
	
	public void setAdditionalCritical(String v){
		this.AdditionalCritical=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalTenacity  = null  ;
	
	public String getAdditionalTenacity(){
		return this.AdditionalTenacity;
	}
	
	public void setAdditionalTenacity(String v){
		this.AdditionalTenacity=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalSpeed  = null  ;
	
	public String getAdditionalSpeed(){
		return this.AdditionalSpeed;
	}
	
	public void setAdditionalSpeed(String v){
		this.AdditionalSpeed=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBaseHit  = null  ;
	
	public String getAdditionalBaseHit(){
		return this.AdditionalBaseHit;
	}
	
	public void setAdditionalBaseHit(String v){
		this.AdditionalBaseHit=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBaseDodge  = null  ;
	
	public String getAdditionalBaseDodge(){
		return this.AdditionalBaseDodge;
	}
	
	public void setAdditionalBaseDodge(String v){
		this.AdditionalBaseDodge=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBaseCritical  = null  ;
	
	public String getAdditionalBaseCritical(){
		return this.AdditionalBaseCritical;
	}
	
	public void setAdditionalBaseCritical(String v){
		this.AdditionalBaseCritical=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBaseTenacity  = null  ;
	
	public String getAdditionalBaseTenacity(){
		return this.AdditionalBaseTenacity;
	}
	
	public void setAdditionalBaseTenacity(String v){
		this.AdditionalBaseTenacity=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBasePhyDamageIncrease  = null  ;
	
	public String getAdditionalBasePhyDamageIncrease(){
		return this.AdditionalBasePhyDamageIncrease;
	}
	
	public void setAdditionalBasePhyDamageIncrease(String v){
		this.AdditionalBasePhyDamageIncrease=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBasePhyDamageDecrease  = null  ;
	
	public String getAdditionalBasePhyDamageDecrease(){
		return this.AdditionalBasePhyDamageDecrease;
	}
	
	public void setAdditionalBasePhyDamageDecrease(String v){
		this.AdditionalBasePhyDamageDecrease=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBaseMagDamageIncrease  = null  ;
	
	public String getAdditionalBaseMagDamageIncrease(){
		return this.AdditionalBaseMagDamageIncrease;
	}
	
	public void setAdditionalBaseMagDamageIncrease(String v){
		this.AdditionalBaseMagDamageIncrease=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBaseMagDamageDecrease  = null  ;
	
	public String getAdditionalBaseMagDamageDecrease(){
		return this.AdditionalBaseMagDamageDecrease;
	}
	
	public void setAdditionalBaseMagDamageDecrease(String v){
		this.AdditionalBaseMagDamageDecrease=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalBaseCriticalDamage  = null  ;
	
	public String getAdditionalBaseCriticalDamage(){
		return this.AdditionalBaseCriticalDamage;
	}
	
	public void setAdditionalBaseCriticalDamage(String v){
		this.AdditionalBaseCriticalDamage=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalDamageIncrease  = null  ;
	
	public String getAdditionalDamageIncrease(){
		return this.AdditionalDamageIncrease;
	}
	
	public void setAdditionalDamageIncrease(String v){
		this.AdditionalDamageIncrease=v;
	}
	
	/**
	 * 
	 */
	public String AdditionallifeRestoringForce  = null  ;
	
	public String getAdditionallifeRestoringForce(){
		return this.AdditionallifeRestoringForce;
	}
	
	public void setAdditionallifeRestoringForce(String v){
		this.AdditionallifeRestoringForce=v;
	}
	
	/**
	 * 
	 */
	public String AdditionalDamageDecrease  = null  ;
	
	public String getAdditionalDamageDecrease(){
		return this.AdditionalDamageDecrease;
	}
	
	public void setAdditionalDamageDecrease(String v){
		this.AdditionalDamageDecrease=v;
	}
	
	/**
	 * 
	 */
	public int AdditionalLevel  = 0  ;
	
	public int getAdditionalLevel(){
		return this.AdditionalLevel;
	}
	
	public void setAdditionalLevel(int v){
		this.AdditionalLevel=v;
	}
	
	/**
	 * 
	 */
	public int AdditionalSkill  = 0  ;
	
	public int getAdditionalSkill(){
		return this.AdditionalSkill;
	}
	
	public void setAdditionalSkill(int v){
		this.AdditionalSkill=v;
	}
	
	/**
	 * 
	 */
	public int wavereward  = 0  ;
	
	public int getWavereward(){
		return this.wavereward;
	}
	
	public void setWavereward(int v){
		this.wavereward=v;
	}
	
	
};