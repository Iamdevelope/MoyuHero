package chuhan.gsp.item;


public class skillupcost17 implements mytools.ConvMain.Checkable ,Comparable<skillupcost17>{

	public int compareTo(skillupcost17 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public skillupcost17(){
		super();
	}
	public skillupcost17(skillupcost17 arg){
		this.id=arg.id ;
		this.skillLevel=arg.skillLevel ;
		this.upgradeCostId=arg.upgradeCostId ;
		this.upgradeCostNum=arg.upgradeCostNum ;
		this.upgradeSkillID=arg.upgradeSkillID ;
		this.upgradeStarCondition=arg.upgradeStarCondition ;
		this.upgradeDes=arg.upgradeDes ;
		this.upgradeCostId2=arg.upgradeCostId2 ;
		this.upgradeCostNum2=arg.upgradeCostNum2 ;
		this.upgradeCostId3=arg.upgradeCostId3 ;
		this.upgradeCostNum3=arg.upgradeCostNum3 ;
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
	public String upgradeCostId  = null  ;
	
	public String getUpgradeCostId(){
		return this.upgradeCostId;
	}
	
	public void setUpgradeCostId(String v){
		this.upgradeCostId=v;
	}
	
	/**
	 * 
	 */
	public String upgradeCostNum  = null  ;
	
	public String getUpgradeCostNum(){
		return this.upgradeCostNum;
	}
	
	public void setUpgradeCostNum(String v){
		this.upgradeCostNum=v;
	}
	
	/**
	 * 
	 */
	public int upgradeSkillID  = 0  ;
	
	public int getUpgradeSkillID(){
		return this.upgradeSkillID;
	}
	
	public void setUpgradeSkillID(int v){
		this.upgradeSkillID=v;
	}
	
	/**
	 * 
	 */
	public int upgradeStarCondition  = 0  ;
	
	public int getUpgradeStarCondition(){
		return this.upgradeStarCondition;
	}
	
	public void setUpgradeStarCondition(int v){
		this.upgradeStarCondition=v;
	}
	
	/**
	 * 
	 */
	public String upgradeDes  = null  ;
	
	public String getUpgradeDes(){
		return this.upgradeDes;
	}
	
	public void setUpgradeDes(String v){
		this.upgradeDes=v;
	}
	
	/**
	 * 
	 */
	public String upgradeCostId2  = null  ;
	
	public String getUpgradeCostId2(){
		return this.upgradeCostId2;
	}
	
	public void setUpgradeCostId2(String v){
		this.upgradeCostId2=v;
	}
	
	/**
	 * 
	 */
	public String upgradeCostNum2  = null  ;
	
	public String getUpgradeCostNum2(){
		return this.upgradeCostNum2;
	}
	
	public void setUpgradeCostNum2(String v){
		this.upgradeCostNum2=v;
	}
	
	/**
	 * 
	 */
	public String upgradeCostId3  = null  ;
	
	public String getUpgradeCostId3(){
		return this.upgradeCostId3;
	}
	
	public void setUpgradeCostId3(String v){
		this.upgradeCostId3=v;
	}
	
	/**
	 * 
	 */
	public String upgradeCostNum3  = null  ;
	
	public String getUpgradeCostNum3(){
		return this.upgradeCostNum3;
	}
	
	public void setUpgradeCostNum3(String v){
		this.upgradeCostNum3=v;
	}
	
	
};