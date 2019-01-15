package chuhan.gsp.item;


public class skill06 implements mytools.ConvMain.Checkable ,Comparable<skill06>{

	public int compareTo(skill06 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public skill06(){
		super();
	}
	public skill06(skill06 arg){
		this.id=arg.id ;
		this.skillName=arg.skillName ;
		this.skillNameRes=arg.skillNameRes ;
		this.skillIcon=arg.skillIcon ;
		this.skillDes=arg.skillDes ;
		this.skillLevel=arg.skillLevel ;
		this.skillType=arg.skillType ;
		this.skillNo=arg.skillNo ;
		this.skillCostType1=arg.skillCostType1 ;
		this.skillCostId1=arg.skillCostId1 ;
		this.skillCostNum1=arg.skillCostNum1 ;
		this.skillCostType2=arg.skillCostType2 ;
		this.skillCostId2=arg.skillCostId2 ;
		this.skillCostNum2=arg.skillCostNum2 ;
		this.skillCostType3=arg.skillCostType3 ;
		this.skillCostId3=arg.skillCostId3 ;
		this.skillCostNum3=arg.skillCostNum3 ;
		this.hpConditionType=arg.hpConditionType ;
		this.hpConditionNum=arg.hpConditionNum ;
		this.rpConditionType=arg.rpConditionType ;
		this.rpConditionNum=arg.rpConditionNum ;
		this.normalTemplate=arg.normalTemplate ;
		this.normalpriority=arg.normalpriority ;
		this.attFirstTemplate=arg.attFirstTemplate ;
		this.attFirstpriority=arg.attFirstpriority ;
		this.defFirstTemplate=arg.defFirstTemplate ;
		this.defFirstpriority=arg.defFirstpriority ;
		this.cooldown=arg.cooldown ;
		this.cooldownAlike=arg.cooldownAlike ;
		this.damageInterrupt=arg.damageInterrupt ;
		this.damageInterruptType=arg.damageInterruptType ;
		this.interruptSkill=arg.interruptSkill ;
		this.target=arg.target ;
		this.attDistance=arg.attDistance ;
		this.coverage=arg.coverage ;
		this.targetNum=arg.targetNum ;
		this.temporarySelfBuff=arg.temporarySelfBuff ;
		this.temporaryTargetBuff=arg.temporaryTargetBuff ;
		this.buffList=arg.buffList ;
		this.attackSound=arg.attackSound ;
		this.underAttackEffID=arg.underAttackEffID ;
		this.underAttackEffLink=arg.underAttackEffLink ;
		this.underAttackSound=arg.underAttackSound ;
		this.ballIsticEffID=arg.ballIsticEffID ;
		this.bullEffectPoint=arg.bullEffectPoint ;
		this.effectHitPoint=arg.effectHitPoint ;
		this.bulletsFiredSound=arg.bulletsFiredSound ;
		this.ballIsticSpeed=arg.ballIsticSpeed ;
		this.ballIsticSound=arg.ballIsticSound ;
		this.action=arg.action ;
		this.InitHitFury=arg.InitHitFury ;
		this.skillAttackFury=arg.skillAttackFury ;
		this.WeakenTargetFuryReward=arg.WeakenTargetFuryReward ;
		this.killFury=arg.killFury ;
		this.VibrationScreen=arg.VibrationScreen ;
		this.skillReleaseType=arg.skillReleaseType ;
		this.hitFrame=arg.hitFrame ;
		this.skillLogicID=arg.skillLogicID ;
		this.Param=arg.Param ;
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
	public String skillName  = null  ;
	
	public String getSkillName(){
		return this.skillName;
	}
	
	public void setSkillName(String v){
		this.skillName=v;
	}
	
	/**
	 * 
	 */
	public String skillNameRes  = null  ;
	
	public String getSkillNameRes(){
		return this.skillNameRes;
	}
	
	public void setSkillNameRes(String v){
		this.skillNameRes=v;
	}
	
	/**
	 * 
	 */
	public String skillIcon  = null  ;
	
	public String getSkillIcon(){
		return this.skillIcon;
	}
	
	public void setSkillIcon(String v){
		this.skillIcon=v;
	}
	
	/**
	 * 
	 */
	public String skillDes  = null  ;
	
	public String getSkillDes(){
		return this.skillDes;
	}
	
	public void setSkillDes(String v){
		this.skillDes=v;
	}
	
	/**
	 * 
	 */
	public int skillLevel  = 0  ;
	
	public int getSkillLevel(){
		return this.skillLevel;
	}
	
	public void setSkillLevel(int v){
		this.skillLevel=v;
	}
	
	/**
	 * 
	 */
	public int skillType  = 0  ;
	
	public int getSkillType(){
		return this.skillType;
	}
	
	public void setSkillType(int v){
		this.skillType=v;
	}
	
	/**
	 * 
	 */
	public int skillNo  = 0  ;
	
	public int getSkillNo(){
		return this.skillNo;
	}
	
	public void setSkillNo(int v){
		this.skillNo=v;
	}
	
	/**
	 * 
	 */
	public int skillCostType1  = 0  ;
	
	public int getSkillCostType1(){
		return this.skillCostType1;
	}
	
	public void setSkillCostType1(int v){
		this.skillCostType1=v;
	}
	
	/**
	 * 
	 */
	public int skillCostId1  = 0  ;
	
	public int getSkillCostId1(){
		return this.skillCostId1;
	}
	
	public void setSkillCostId1(int v){
		this.skillCostId1=v;
	}
	
	/**
	 * 
	 */
	public int skillCostNum1  = 0  ;
	
	public int getSkillCostNum1(){
		return this.skillCostNum1;
	}
	
	public void setSkillCostNum1(int v){
		this.skillCostNum1=v;
	}
	
	/**
	 * 
	 */
	public int skillCostType2  = 0  ;
	
	public int getSkillCostType2(){
		return this.skillCostType2;
	}
	
	public void setSkillCostType2(int v){
		this.skillCostType2=v;
	}
	
	/**
	 * 
	 */
	public int skillCostId2  = 0  ;
	
	public int getSkillCostId2(){
		return this.skillCostId2;
	}
	
	public void setSkillCostId2(int v){
		this.skillCostId2=v;
	}
	
	/**
	 * 
	 */
	public int skillCostNum2  = 0  ;
	
	public int getSkillCostNum2(){
		return this.skillCostNum2;
	}
	
	public void setSkillCostNum2(int v){
		this.skillCostNum2=v;
	}
	
	/**
	 * 
	 */
	public int skillCostType3  = 0  ;
	
	public int getSkillCostType3(){
		return this.skillCostType3;
	}
	
	public void setSkillCostType3(int v){
		this.skillCostType3=v;
	}
	
	/**
	 * 
	 */
	public int skillCostId3  = 0  ;
	
	public int getSkillCostId3(){
		return this.skillCostId3;
	}
	
	public void setSkillCostId3(int v){
		this.skillCostId3=v;
	}
	
	/**
	 * 
	 */
	public int skillCostNum3  = 0  ;
	
	public int getSkillCostNum3(){
		return this.skillCostNum3;
	}
	
	public void setSkillCostNum3(int v){
		this.skillCostNum3=v;
	}
	
	/**
	 * 
	 */
	public int hpConditionType  = 0  ;
	
	public int getHpConditionType(){
		return this.hpConditionType;
	}
	
	public void setHpConditionType(int v){
		this.hpConditionType=v;
	}
	
	/**
	 * 
	 */
	public int hpConditionNum  = 0  ;
	
	public int getHpConditionNum(){
		return this.hpConditionNum;
	}
	
	public void setHpConditionNum(int v){
		this.hpConditionNum=v;
	}
	
	/**
	 * 
	 */
	public int rpConditionType  = 0  ;
	
	public int getRpConditionType(){
		return this.rpConditionType;
	}
	
	public void setRpConditionType(int v){
		this.rpConditionType=v;
	}
	
	/**
	 * 
	 */
	public int rpConditionNum  = 0  ;
	
	public int getRpConditionNum(){
		return this.rpConditionNum;
	}
	
	public void setRpConditionNum(int v){
		this.rpConditionNum=v;
	}
	
	/**
	 * 
	 */
	public String normalTemplate  = null  ;
	
	public String getNormalTemplate(){
		return this.normalTemplate;
	}
	
	public void setNormalTemplate(String v){
		this.normalTemplate=v;
	}
	
	/**
	 * 
	 */
	public String normalpriority  = null  ;
	
	public String getNormalpriority(){
		return this.normalpriority;
	}
	
	public void setNormalpriority(String v){
		this.normalpriority=v;
	}
	
	/**
	 * 
	 */
	public String attFirstTemplate  = null  ;
	
	public String getAttFirstTemplate(){
		return this.attFirstTemplate;
	}
	
	public void setAttFirstTemplate(String v){
		this.attFirstTemplate=v;
	}
	
	/**
	 * 
	 */
	public String attFirstpriority  = null  ;
	
	public String getAttFirstpriority(){
		return this.attFirstpriority;
	}
	
	public void setAttFirstpriority(String v){
		this.attFirstpriority=v;
	}
	
	/**
	 * 
	 */
	public String defFirstTemplate  = null  ;
	
	public String getDefFirstTemplate(){
		return this.defFirstTemplate;
	}
	
	public void setDefFirstTemplate(String v){
		this.defFirstTemplate=v;
	}
	
	/**
	 * 
	 */
	public String defFirstpriority  = null  ;
	
	public String getDefFirstpriority(){
		return this.defFirstpriority;
	}
	
	public void setDefFirstpriority(String v){
		this.defFirstpriority=v;
	}
	
	/**
	 * 
	 */
	public int cooldown  = 0  ;
	
	public int getCooldown(){
		return this.cooldown;
	}
	
	public void setCooldown(int v){
		this.cooldown=v;
	}
	
	/**
	 * 
	 */
	public int cooldownAlike  = 0  ;
	
	public int getCooldownAlike(){
		return this.cooldownAlike;
	}
	
	public void setCooldownAlike(int v){
		this.cooldownAlike=v;
	}
	
	/**
	 * 
	 */
	public int damageInterrupt  = 0  ;
	
	public int getDamageInterrupt(){
		return this.damageInterrupt;
	}
	
	public void setDamageInterrupt(int v){
		this.damageInterrupt=v;
	}
	
	/**
	 * 
	 */
	public int damageInterruptType  = 0  ;
	
	public int getDamageInterruptType(){
		return this.damageInterruptType;
	}
	
	public void setDamageInterruptType(int v){
		this.damageInterruptType=v;
	}
	
	/**
	 * 
	 */
	public int interruptSkill  = 0  ;
	
	public int getInterruptSkill(){
		return this.interruptSkill;
	}
	
	public void setInterruptSkill(int v){
		this.interruptSkill=v;
	}
	
	/**
	 * 
	 */
	public int target  = 0  ;
	
	public int getTarget(){
		return this.target;
	}
	
	public void setTarget(int v){
		this.target=v;
	}
	
	/**
	 * 
	 */
	public String attDistance  = null  ;
	
	public String getAttDistance(){
		return this.attDistance;
	}
	
	public void setAttDistance(String v){
		this.attDistance=v;
	}
	
	/**
	 * 
	 */
	public String coverage  = null  ;
	
	public String getCoverage(){
		return this.coverage;
	}
	
	public void setCoverage(String v){
		this.coverage=v;
	}
	
	/**
	 * 
	 */
	public int targetNum  = 0  ;
	
	public int getTargetNum(){
		return this.targetNum;
	}
	
	public void setTargetNum(int v){
		this.targetNum=v;
	}
	
	/**
	 * 
	 */
	public String temporarySelfBuff  = null  ;
	
	public String getTemporarySelfBuff(){
		return this.temporarySelfBuff;
	}
	
	public void setTemporarySelfBuff(String v){
		this.temporarySelfBuff=v;
	}
	
	/**
	 * 
	 */
	public String temporaryTargetBuff  = null  ;
	
	public String getTemporaryTargetBuff(){
		return this.temporaryTargetBuff;
	}
	
	public void setTemporaryTargetBuff(String v){
		this.temporaryTargetBuff=v;
	}
	
	/**
	 * 
	 */
	public String buffList  = null  ;
	
	public String getBuffList(){
		return this.buffList;
	}
	
	public void setBuffList(String v){
		this.buffList=v;
	}
	
	/**
	 * 
	 */
	public String attackSound  = null  ;
	
	public String getAttackSound(){
		return this.attackSound;
	}
	
	public void setAttackSound(String v){
		this.attackSound=v;
	}
	
	/**
	 * 
	 */
	public String underAttackEffID  = null  ;
	
	public String getUnderAttackEffID(){
		return this.underAttackEffID;
	}
	
	public void setUnderAttackEffID(String v){
		this.underAttackEffID=v;
	}
	
	/**
	 * 
	 */
	public int underAttackEffLink  = 0  ;
	
	public int getUnderAttackEffLink(){
		return this.underAttackEffLink;
	}
	
	public void setUnderAttackEffLink(int v){
		this.underAttackEffLink=v;
	}
	
	/**
	 * 
	 */
	public String underAttackSound  = null  ;
	
	public String getUnderAttackSound(){
		return this.underAttackSound;
	}
	
	public void setUnderAttackSound(String v){
		this.underAttackSound=v;
	}
	
	/**
	 * 
	 */
	public String ballIsticEffID  = null  ;
	
	public String getBallIsticEffID(){
		return this.ballIsticEffID;
	}
	
	public void setBallIsticEffID(String v){
		this.ballIsticEffID=v;
	}
	
	/**
	 * 
	 */
	public String bullEffectPoint  = null  ;
	
	public String getBullEffectPoint(){
		return this.bullEffectPoint;
	}
	
	public void setBullEffectPoint(String v){
		this.bullEffectPoint=v;
	}
	
	/**
	 * 
	 */
	public String effectHitPoint  = null  ;
	
	public String getEffectHitPoint(){
		return this.effectHitPoint;
	}
	
	public void setEffectHitPoint(String v){
		this.effectHitPoint=v;
	}
	
	/**
	 * 
	 */
	public String bulletsFiredSound  = null  ;
	
	public String getBulletsFiredSound(){
		return this.bulletsFiredSound;
	}
	
	public void setBulletsFiredSound(String v){
		this.bulletsFiredSound=v;
	}
	
	/**
	 * 
	 */
	public int ballIsticSpeed  = 0  ;
	
	public int getBallIsticSpeed(){
		return this.ballIsticSpeed;
	}
	
	public void setBallIsticSpeed(int v){
		this.ballIsticSpeed=v;
	}
	
	/**
	 * 
	 */
	public String ballIsticSound  = null  ;
	
	public String getBallIsticSound(){
		return this.ballIsticSound;
	}
	
	public void setBallIsticSound(String v){
		this.ballIsticSound=v;
	}
	
	/**
	 * 
	 */
	public String action  = null  ;
	
	public String getAction(){
		return this.action;
	}
	
	public void setAction(String v){
		this.action=v;
	}
	
	/**
	 * 
	 */
	public int InitHitFury  = 0  ;
	
	public int getInitHitFury(){
		return this.InitHitFury;
	}
	
	public void setInitHitFury(int v){
		this.InitHitFury=v;
	}
	
	/**
	 * 
	 */
	public int skillAttackFury  = 0  ;
	
	public int getSkillAttackFury(){
		return this.skillAttackFury;
	}
	
	public void setSkillAttackFury(int v){
		this.skillAttackFury=v;
	}
	
	/**
	 * 
	 */
	public int WeakenTargetFuryReward  = 0  ;
	
	public int getWeakenTargetFuryReward(){
		return this.WeakenTargetFuryReward;
	}
	
	public void setWeakenTargetFuryReward(int v){
		this.WeakenTargetFuryReward=v;
	}
	
	/**
	 * 
	 */
	public int killFury  = 0  ;
	
	public int getKillFury(){
		return this.killFury;
	}
	
	public void setKillFury(int v){
		this.killFury=v;
	}
	
	/**
	 * 
	 */
	public int VibrationScreen  = 0  ;
	
	public int getVibrationScreen(){
		return this.VibrationScreen;
	}
	
	public void setVibrationScreen(int v){
		this.VibrationScreen=v;
	}
	
	/**
	 * 
	 */
	public int skillReleaseType  = 0  ;
	
	public int getSkillReleaseType(){
		return this.skillReleaseType;
	}
	
	public void setSkillReleaseType(int v){
		this.skillReleaseType=v;
	}
	
	/**
	 * 
	 */
	public int hitFrame  = 0  ;
	
	public int getHitFrame(){
		return this.hitFrame;
	}
	
	public void setHitFrame(int v){
		this.hitFrame=v;
	}
	
	/**
	 * 
	 */
	public int skillLogicID  = 0  ;
	
	public int getSkillLogicID(){
		return this.skillLogicID;
	}
	
	public void setSkillLogicID(int v){
		this.skillLogicID=v;
	}
	
	/**
	 * 
	 */
	public String Param  = null  ;
	
	public String getParam(){
		return this.Param;
	}
	
	public void setParam(String v){
		this.Param=v;
	}
	
	
};