package chuhan.gsp.item;


public class cheroconfig implements mytools.ConvMain.Checkable ,Comparable<cheroconfig>{

	public int compareTo(cheroconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cheroconfig(){
		super();
	}
	public cheroconfig(cheroconfig arg){
		this.id=arg.id ;
		this.chuhan=arg.chuhan ;
		this.name=arg.name ;
		this.color=arg.color ;
		this.deify=arg.deify ;
		this.icon=arg.icon ;
		this.pic=arg.pic ;
		this.describe=arg.describe ;
		this.army=arg.army ;
		this.atta=arg.atta ;
		this.denf=arg.denf ;
		this.wise=arg.wise ;
		this.speed=arg.speed ;
		this.armygrow=arg.armygrow ;
		this.attagrow=arg.attagrow ;
		this.denfgrow=arg.denfgrow ;
		this.wisegrow=arg.wisegrow ;
		this.speedgrow=arg.speedgrow ;
		this.evoarmygrow=arg.evoarmygrow ;
		this.evoattagrow=arg.evoattagrow ;
		this.evodenfgrow=arg.evodenfgrow ;
		this.evowisegrow=arg.evowisegrow ;
		this.evospeedgrow=arg.evospeedgrow ;
		this.soulid=arg.soulid ;
		this.exchangenum=arg.exchangenum ;
		this.bindskill=arg.bindskill ;
		this.bindsweapon=arg.bindsweapon ;
		this.bindscloth=arg.bindscloth ;
		this.bindshorse=arg.bindshorse ;
		this.sort=arg.sort ;
		this.qiyuan=arg.qiyuan ;
		this.qiyuanxianshi=arg.qiyuanxianshi ;
		this.qiyuanattr=arg.qiyuanattr ;
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
	public int deify  = 0  ;
	
	public int getDeify(){
		return this.deify;
	}
	
	public void setDeify(int v){
		this.deify=v;
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
	public String describe  = null  ;
	
	public String getDescribe(){
		return this.describe;
	}
	
	public void setDescribe(String v){
		this.describe=v;
	}
	
	/**
	 * 
	 */
	public int army  = 0  ;
	
	public int getArmy(){
		return this.army;
	}
	
	public void setArmy(int v){
		this.army=v;
	}
	
	/**
	 * 
	 */
	public int atta  = 0  ;
	
	public int getAtta(){
		return this.atta;
	}
	
	public void setAtta(int v){
		this.atta=v;
	}
	
	/**
	 * 
	 */
	public int denf  = 0  ;
	
	public int getDenf(){
		return this.denf;
	}
	
	public void setDenf(int v){
		this.denf=v;
	}
	
	/**
	 * 
	 */
	public int wise  = 0  ;
	
	public int getWise(){
		return this.wise;
	}
	
	public void setWise(int v){
		this.wise=v;
	}
	
	/**
	 * 
	 */
	public int speed  = 0  ;
	
	public int getSpeed(){
		return this.speed;
	}
	
	public void setSpeed(int v){
		this.speed=v;
	}
	
	/**
	 * 
	 */
	public double armygrow  = 0.0  ;
	
	public double getArmygrow(){
		return this.armygrow;
	}
	
	public void setArmygrow(double v){
		this.armygrow=v;
	}
	
	/**
	 * 
	 */
	public double attagrow  = 0.0  ;
	
	public double getAttagrow(){
		return this.attagrow;
	}
	
	public void setAttagrow(double v){
		this.attagrow=v;
	}
	
	/**
	 * 
	 */
	public double denfgrow  = 0.0  ;
	
	public double getDenfgrow(){
		return this.denfgrow;
	}
	
	public void setDenfgrow(double v){
		this.denfgrow=v;
	}
	
	/**
	 * 
	 */
	public double wisegrow  = 0.0  ;
	
	public double getWisegrow(){
		return this.wisegrow;
	}
	
	public void setWisegrow(double v){
		this.wisegrow=v;
	}
	
	/**
	 * 
	 */
	public double speedgrow  = 0.0  ;
	
	public double getSpeedgrow(){
		return this.speedgrow;
	}
	
	public void setSpeedgrow(double v){
		this.speedgrow=v;
	}
	
	/**
	 * 
	 */
	public double evoarmygrow  = 0.0  ;
	
	public double getEvoarmygrow(){
		return this.evoarmygrow;
	}
	
	public void setEvoarmygrow(double v){
		this.evoarmygrow=v;
	}
	
	/**
	 * 
	 */
	public double evoattagrow  = 0.0  ;
	
	public double getEvoattagrow(){
		return this.evoattagrow;
	}
	
	public void setEvoattagrow(double v){
		this.evoattagrow=v;
	}
	
	/**
	 * 
	 */
	public double evodenfgrow  = 0.0  ;
	
	public double getEvodenfgrow(){
		return this.evodenfgrow;
	}
	
	public void setEvodenfgrow(double v){
		this.evodenfgrow=v;
	}
	
	/**
	 * 
	 */
	public double evowisegrow  = 0.0  ;
	
	public double getEvowisegrow(){
		return this.evowisegrow;
	}
	
	public void setEvowisegrow(double v){
		this.evowisegrow=v;
	}
	
	/**
	 * 
	 */
	public double evospeedgrow  = 0.0  ;
	
	public double getEvospeedgrow(){
		return this.evospeedgrow;
	}
	
	public void setEvospeedgrow(double v){
		this.evospeedgrow=v;
	}
	
	/**
	 * 
	 */
	public int soulid  = 0  ;
	
	public int getSoulid(){
		return this.soulid;
	}
	
	public void setSoulid(int v){
		this.soulid=v;
	}
	
	/**
	 * 
	 */
	public int exchangenum  = 0  ;
	
	public int getExchangenum(){
		return this.exchangenum;
	}
	
	public void setExchangenum(int v){
		this.exchangenum=v;
	}
	
	/**
	 * 
	 */
	public int bindskill  = 0  ;
	
	public int getBindskill(){
		return this.bindskill;
	}
	
	public void setBindskill(int v){
		this.bindskill=v;
	}
	
	/**
	 * 
	 */
	public int bindsweapon  = 0  ;
	
	public int getBindsweapon(){
		return this.bindsweapon;
	}
	
	public void setBindsweapon(int v){
		this.bindsweapon=v;
	}
	
	/**
	 * 
	 */
	public int bindscloth  = 0  ;
	
	public int getBindscloth(){
		return this.bindscloth;
	}
	
	public void setBindscloth(int v){
		this.bindscloth=v;
	}
	
	/**
	 * 
	 */
	public int bindshorse  = 0  ;
	
	public int getBindshorse(){
		return this.bindshorse;
	}
	
	public void setBindshorse(int v){
		this.bindshorse=v;
	}
	
	/**
	 * 
	 */
	public int sort  = 0  ;
	
	public int getSort(){
		return this.sort;
	}
	
	public void setSort(int v){
		this.sort=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<String> qiyuan  ;
	
	public java.util.ArrayList<String> getQiyuan(){
		return this.qiyuan;
	}
	
	public void setQiyuan(java.util.ArrayList<String> v){
		this.qiyuan=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<String> qiyuanxianshi  ;
	
	public java.util.ArrayList<String> getQiyuanxianshi(){
		return this.qiyuanxianshi;
	}
	
	public void setQiyuanxianshi(java.util.ArrayList<String> v){
		this.qiyuanxianshi=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<String> qiyuanattr  ;
	
	public java.util.ArrayList<String> getQiyuanattr(){
		return this.qiyuanattr;
	}
	
	public void setQiyuanattr(java.util.ArrayList<String> v){
		this.qiyuanattr=v;
	}
	
	
};