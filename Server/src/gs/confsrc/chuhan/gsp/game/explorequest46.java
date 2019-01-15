package chuhan.gsp.game;


public class explorequest46 implements mytools.ConvMain.Checkable ,Comparable<explorequest46>{

	public int compareTo(explorequest46 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public explorequest46(){
		super();
	}
	public explorequest46(explorequest46 arg){
		this.id=arg.id ;
		this.chapterID=arg.chapterID ;
		this.name=arg.name ;
		this.des=arg.des ;
		this.needHeroDes=arg.needHeroDes ;
		this.bonusDes=arg.bonusDes ;
		this.quality=arg.quality ;
		this.type=arg.type ;
		this.time=arg.time ;
		this.cost=arg.cost ;
		this.bonus=arg.bonus ;
		this.weight=arg.weight ;
		this.coordinate=arg.coordinate ;
		this.needHeroType=arg.needHeroType ;
		this.needHeroLevel=arg.needHeroLevel ;
		this.needHeroCamp=arg.needHeroCamp ;
		this.needHeroStar=arg.needHeroStar ;
		this.needNum=arg.needNum ;
		this.needHeroID1=arg.needHeroID1 ;
		this.needHeroID2=arg.needHeroID2 ;
		this.needHeroID3=arg.needHeroID3 ;
		this.needHeroID4=arg.needHeroID4 ;
		this.needHeroID5=arg.needHeroID5 ;
		this.forceReturnCost=arg.forceReturnCost ;
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
	public int chapterID  = 0  ;
	
	public int getChapterID(){
		return this.chapterID;
	}
	
	public void setChapterID(int v){
		this.chapterID=v;
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
	public String des  = null  ;
	
	public String getDes(){
		return this.des;
	}
	
	public void setDes(String v){
		this.des=v;
	}
	
	/**
	 * 
	 */
	public String needHeroDes  = null  ;
	
	public String getNeedHeroDes(){
		return this.needHeroDes;
	}
	
	public void setNeedHeroDes(String v){
		this.needHeroDes=v;
	}
	
	/**
	 * 
	 */
	public String bonusDes  = null  ;
	
	public String getBonusDes(){
		return this.bonusDes;
	}
	
	public void setBonusDes(String v){
		this.bonusDes=v;
	}
	
	/**
	 * 
	 */
	public int quality  = 0  ;
	
	public int getQuality(){
		return this.quality;
	}
	
	public void setQuality(int v){
		this.quality=v;
	}
	
	/**
	 * 
	 */
	public int type  = 0  ;
	
	public int getType(){
		return this.type;
	}
	
	public void setType(int v){
		this.type=v;
	}
	
	/**
	 * 
	 */
	public int time  = 0  ;
	
	public int getTime(){
		return this.time;
	}
	
	public void setTime(int v){
		this.time=v;
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
	public int bonus  = 0  ;
	
	public int getBonus(){
		return this.bonus;
	}
	
	public void setBonus(int v){
		this.bonus=v;
	}
	
	/**
	 * 
	 */
	public int weight  = 0  ;
	
	public int getWeight(){
		return this.weight;
	}
	
	public void setWeight(int v){
		this.weight=v;
	}
	
	/**
	 * 
	 */
	public String coordinate  = null  ;
	
	public String getCoordinate(){
		return this.coordinate;
	}
	
	public void setCoordinate(String v){
		this.coordinate=v;
	}
	
	/**
	 * 
	 */
	public int needHeroType  = 0  ;
	
	public int getNeedHeroType(){
		return this.needHeroType;
	}
	
	public void setNeedHeroType(int v){
		this.needHeroType=v;
	}
	
	/**
	 * 
	 */
	public int needHeroLevel  = 0  ;
	
	public int getNeedHeroLevel(){
		return this.needHeroLevel;
	}
	
	public void setNeedHeroLevel(int v){
		this.needHeroLevel=v;
	}
	
	/**
	 * 
	 */
	public String needHeroCamp  = null  ;
	
	public String getNeedHeroCamp(){
		return this.needHeroCamp;
	}
	
	public void setNeedHeroCamp(String v){
		this.needHeroCamp=v;
	}
	
	/**
	 * 
	 */
	public int needHeroStar  = 0  ;
	
	public int getNeedHeroStar(){
		return this.needHeroStar;
	}
	
	public void setNeedHeroStar(int v){
		this.needHeroStar=v;
	}
	
	/**
	 * 
	 */
	public int needNum  = 0  ;
	
	public int getNeedNum(){
		return this.needNum;
	}
	
	public void setNeedNum(int v){
		this.needNum=v;
	}
	
	/**
	 * 
	 */
	public String needHeroID1  = null  ;
	
	public String getNeedHeroID1(){
		return this.needHeroID1;
	}
	
	public void setNeedHeroID1(String v){
		this.needHeroID1=v;
	}
	
	/**
	 * 
	 */
	public String needHeroID2  = null  ;
	
	public String getNeedHeroID2(){
		return this.needHeroID2;
	}
	
	public void setNeedHeroID2(String v){
		this.needHeroID2=v;
	}
	
	/**
	 * 
	 */
	public String needHeroID3  = null  ;
	
	public String getNeedHeroID3(){
		return this.needHeroID3;
	}
	
	public void setNeedHeroID3(String v){
		this.needHeroID3=v;
	}
	
	/**
	 * 
	 */
	public String needHeroID4  = null  ;
	
	public String getNeedHeroID4(){
		return this.needHeroID4;
	}
	
	public void setNeedHeroID4(String v){
		this.needHeroID4=v;
	}
	
	/**
	 * 
	 */
	public String needHeroID5  = null  ;
	
	public String getNeedHeroID5(){
		return this.needHeroID5;
	}
	
	public void setNeedHeroID5(String v){
		this.needHeroID5=v;
	}
	
	/**
	 * 
	 */
	public int forceReturnCost  = 0  ;
	
	public int getForceReturnCost(){
		return this.forceReturnCost;
	}
	
	public void setForceReturnCost(int v){
		this.forceReturnCost=v;
	}
	
	
};