package chuhan.gsp.item;


public class cequipconfig implements mytools.ConvMain.Checkable ,Comparable<cequipconfig>{

	public int compareTo(cequipconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cequipconfig(){
		super();
	}
	public cequipconfig(cequipconfig arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.color=arg.color ;
		this.position=arg.position ;
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
		this.armyremakegrow=arg.armyremakegrow ;
		this.attaremakegrow=arg.attaremakegrow ;
		this.denfremakegrow=arg.denfremakegrow ;
		this.wiseremakegrow=arg.wiseremakegrow ;
		this.speedremakegrow=arg.speedremakegrow ;
		this.costtimes=arg.costtimes ;
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
	public int position  = 0  ;
	
	public int getPosition(){
		return this.position;
	}
	
	public void setPosition(int v){
		this.position=v;
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
	public double armyremakegrow  = 0.0  ;
	
	public double getArmyremakegrow(){
		return this.armyremakegrow;
	}
	
	public void setArmyremakegrow(double v){
		this.armyremakegrow=v;
	}
	
	/**
	 * 
	 */
	public double attaremakegrow  = 0.0  ;
	
	public double getAttaremakegrow(){
		return this.attaremakegrow;
	}
	
	public void setAttaremakegrow(double v){
		this.attaremakegrow=v;
	}
	
	/**
	 * 
	 */
	public double denfremakegrow  = 0.0  ;
	
	public double getDenfremakegrow(){
		return this.denfremakegrow;
	}
	
	public void setDenfremakegrow(double v){
		this.denfremakegrow=v;
	}
	
	/**
	 * 
	 */
	public double wiseremakegrow  = 0.0  ;
	
	public double getWiseremakegrow(){
		return this.wiseremakegrow;
	}
	
	public void setWiseremakegrow(double v){
		this.wiseremakegrow=v;
	}
	
	/**
	 * 
	 */
	public double speedremakegrow  = 0.0  ;
	
	public double getSpeedremakegrow(){
		return this.speedremakegrow;
	}
	
	public void setSpeedremakegrow(double v){
		this.speedremakegrow=v;
	}
	
	/**
	 * 
	 */
	public double costtimes  = 0.0  ;
	
	public double getCosttimes(){
		return this.costtimes;
	}
	
	public void setCosttimes(double v){
		this.costtimes=v;
	}
	
	
};