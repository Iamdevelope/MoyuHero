package chuhan.gsp.game;


public class vip39 implements mytools.ConvMain.Checkable ,Comparable<vip39>{

	public int compareTo(vip39 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public vip39(){
		super();
	}
	public vip39(vip39 arg){
		this.id=arg.id ;
		this.vipExp=arg.vipExp ;
		this.extraAp=arg.extraAp ;
		this.maxBuyAp=arg.maxBuyAp ;
		this.maxUseApPotion=arg.maxUseApPotion ;
		this.extraPVPAp=arg.extraPVPAp ;
		this.maxBuyPVPAp=arg.maxBuyPVPAp ;
		this.maxUsePVPApPotion=arg.maxUsePVPApPotion ;
		this.maxFriends=arg.maxFriends ;
		this.reSkillTime=arg.reSkillTime ;
		this.skillconlimit=arg.skillconlimit ;
		this.extraEp=arg.extraEp ;
		this.maxBuyEp=arg.maxBuyEp ;
		this.maxUseEpPotion=arg.maxUseEpPotion ;
		this.ifCanAccelerate=arg.ifCanAccelerate ;
		this.ifCanBuyStageReset=arg.ifCanBuyStageReset ;
		this.stageResetBuyTimes=arg.stageResetBuyTimes ;
		this.ifCanBuyStageReset1=arg.ifCanBuyStageReset1 ;
		this.stageResetBuyTimes1=arg.stageResetBuyTimes1 ;
		this.ifCanRapidClear=arg.ifCanRapidClear ;
		this.ifCanBuyLimiteStageReset=arg.ifCanBuyLimiteStageReset ;
		this.stageResetBuyLimiteTimes=arg.stageResetBuyLimiteTimes ;
		this.rapidClearNums=arg.rapidClearNums ;
		this.rapidClearBuyTimes=arg.rapidClearBuyTimes ;
		this.privilegedDes=arg.privilegedDes ;
		this.isNew=arg.isNew ;
		this.isClearten=arg.isClearten ;
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
	public int vipExp  = 0  ;
	
	public int getVipExp(){
		return this.vipExp;
	}
	
	public void setVipExp(int v){
		this.vipExp=v;
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
	public int maxBuyAp  = 0  ;
	
	public int getMaxBuyAp(){
		return this.maxBuyAp;
	}
	
	public void setMaxBuyAp(int v){
		this.maxBuyAp=v;
	}
	
	/**
	 * 
	 */
	public int maxUseApPotion  = 0  ;
	
	public int getMaxUseApPotion(){
		return this.maxUseApPotion;
	}
	
	public void setMaxUseApPotion(int v){
		this.maxUseApPotion=v;
	}
	
	/**
	 * 
	 */
	public int extraPVPAp  = 0  ;
	
	public int getExtraPVPAp(){
		return this.extraPVPAp;
	}
	
	public void setExtraPVPAp(int v){
		this.extraPVPAp=v;
	}
	
	/**
	 * 
	 */
	public int maxBuyPVPAp  = 0  ;
	
	public int getMaxBuyPVPAp(){
		return this.maxBuyPVPAp;
	}
	
	public void setMaxBuyPVPAp(int v){
		this.maxBuyPVPAp=v;
	}
	
	/**
	 * 
	 */
	public int maxUsePVPApPotion  = 0  ;
	
	public int getMaxUsePVPApPotion(){
		return this.maxUsePVPApPotion;
	}
	
	public void setMaxUsePVPApPotion(int v){
		this.maxUsePVPApPotion=v;
	}
	
	/**
	 * 
	 */
	public int maxFriends  = 0  ;
	
	public int getMaxFriends(){
		return this.maxFriends;
	}
	
	public void setMaxFriends(int v){
		this.maxFriends=v;
	}
	
	/**
	 * 
	 */
	public int reSkillTime  = 0  ;
	
	public int getReSkillTime(){
		return this.reSkillTime;
	}
	
	public void setReSkillTime(int v){
		this.reSkillTime=v;
	}
	
	/**
	 * 
	 */
	public int skillconlimit  = 0  ;
	
	public int getSkillconlimit(){
		return this.skillconlimit;
	}
	
	public void setSkillconlimit(int v){
		this.skillconlimit=v;
	}
	
	/**
	 * 
	 */
	public int extraEp  = 0  ;
	
	public int getExtraEp(){
		return this.extraEp;
	}
	
	public void setExtraEp(int v){
		this.extraEp=v;
	}
	
	/**
	 * 
	 */
	public int maxBuyEp  = 0  ;
	
	public int getMaxBuyEp(){
		return this.maxBuyEp;
	}
	
	public void setMaxBuyEp(int v){
		this.maxBuyEp=v;
	}
	
	/**
	 * 
	 */
	public int maxUseEpPotion  = 0  ;
	
	public int getMaxUseEpPotion(){
		return this.maxUseEpPotion;
	}
	
	public void setMaxUseEpPotion(int v){
		this.maxUseEpPotion=v;
	}
	
	/**
	 * 
	 */
	public int ifCanAccelerate  = 0  ;
	
	public int getIfCanAccelerate(){
		return this.ifCanAccelerate;
	}
	
	public void setIfCanAccelerate(int v){
		this.ifCanAccelerate=v;
	}
	
	/**
	 * 
	 */
	public int ifCanBuyStageReset  = 0  ;
	
	public int getIfCanBuyStageReset(){
		return this.ifCanBuyStageReset;
	}
	
	public void setIfCanBuyStageReset(int v){
		this.ifCanBuyStageReset=v;
	}
	
	/**
	 * 
	 */
	public int stageResetBuyTimes  = 0  ;
	
	public int getStageResetBuyTimes(){
		return this.stageResetBuyTimes;
	}
	
	public void setStageResetBuyTimes(int v){
		this.stageResetBuyTimes=v;
	}
	
	/**
	 * 
	 */
	public int ifCanBuyStageReset1  = 0  ;
	
	public int getIfCanBuyStageReset1(){
		return this.ifCanBuyStageReset1;
	}
	
	public void setIfCanBuyStageReset1(int v){
		this.ifCanBuyStageReset1=v;
	}
	
	/**
	 * 
	 */
	public int stageResetBuyTimes1  = 0  ;
	
	public int getStageResetBuyTimes1(){
		return this.stageResetBuyTimes1;
	}
	
	public void setStageResetBuyTimes1(int v){
		this.stageResetBuyTimes1=v;
	}
	
	/**
	 * 
	 */
	public int ifCanRapidClear  = 0  ;
	
	public int getIfCanRapidClear(){
		return this.ifCanRapidClear;
	}
	
	public void setIfCanRapidClear(int v){
		this.ifCanRapidClear=v;
	}
	
	/**
	 * 
	 */
	public int ifCanBuyLimiteStageReset  = 0  ;
	
	public int getIfCanBuyLimiteStageReset(){
		return this.ifCanBuyLimiteStageReset;
	}
	
	public void setIfCanBuyLimiteStageReset(int v){
		this.ifCanBuyLimiteStageReset=v;
	}
	
	/**
	 * 
	 */
	public int stageResetBuyLimiteTimes  = 0  ;
	
	public int getStageResetBuyLimiteTimes(){
		return this.stageResetBuyLimiteTimes;
	}
	
	public void setStageResetBuyLimiteTimes(int v){
		this.stageResetBuyLimiteTimes=v;
	}
	
	/**
	 * 
	 */
	public int rapidClearNums  = 0  ;
	
	public int getRapidClearNums(){
		return this.rapidClearNums;
	}
	
	public void setRapidClearNums(int v){
		this.rapidClearNums=v;
	}
	
	/**
	 * 
	 */
	public int rapidClearBuyTimes  = 0  ;
	
	public int getRapidClearBuyTimes(){
		return this.rapidClearBuyTimes;
	}
	
	public void setRapidClearBuyTimes(int v){
		this.rapidClearBuyTimes=v;
	}
	
	/**
	 * 
	 */
	public String privilegedDes  = null  ;
	
	public String getPrivilegedDes(){
		return this.privilegedDes;
	}
	
	public void setPrivilegedDes(String v){
		this.privilegedDes=v;
	}
	
	/**
	 * 
	 */
	public String isNew  = null  ;
	
	public String getIsNew(){
		return this.isNew;
	}
	
	public void setIsNew(String v){
		this.isNew=v;
	}
	
	/**
	 * 
	 */
	public int isClearten  = 0  ;
	
	public int getIsClearten(){
		return this.isClearten;
	}
	
	public void setIsClearten(int v){
		this.isClearten=v;
	}
	
	
};