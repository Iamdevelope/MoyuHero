package chuhan.gsp.item;


public class SEquip implements mytools.ConvMain.Checkable ,Comparable<SEquip>{

	public int compareTo(SEquip o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SEquip(){
		super();
	}
	public SEquip(SEquip arg){
		this.id=arg.id ;
		this.color=arg.color ;
		this.position=arg.position ;
		this.army=arg.army ;
		this.atta=arg.atta ;
		this.denf=arg.denf ;
		this.wise=arg.wise ;
		this.speed=arg.speed ;
		this.army_grow=arg.army_grow ;
		this.atta_grow=arg.atta_grow ;
		this.denf_grow=arg.denf_grow ;
		this.wise_grow=arg.wise_grow ;
		this.speed_grow=arg.speed_grow ;
		this.army_remake_grow=arg.army_remake_grow ;
		this.atta_remake_grow=arg.atta_remake_grow ;
		this.denf_remake_grow=arg.denf_remake_grow ;
		this.wise_remake_grow=arg.wise_remake_grow ;
		this.speed_remake_grow=arg.speed_remake_grow ;
		this.cost_times=arg.cost_times ;
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
	public double army_grow  = 0.0  ;
	
	public double getArmy_grow(){
		return this.army_grow;
	}
	
	public void setArmy_grow(double v){
		this.army_grow=v;
	}
	
	/**
	 * 
	 */
	public double atta_grow  = 0.0  ;
	
	public double getAtta_grow(){
		return this.atta_grow;
	}
	
	public void setAtta_grow(double v){
		this.atta_grow=v;
	}
	
	/**
	 * 
	 */
	public double denf_grow  = 0.0  ;
	
	public double getDenf_grow(){
		return this.denf_grow;
	}
	
	public void setDenf_grow(double v){
		this.denf_grow=v;
	}
	
	/**
	 * 
	 */
	public double wise_grow  = 0.0  ;
	
	public double getWise_grow(){
		return this.wise_grow;
	}
	
	public void setWise_grow(double v){
		this.wise_grow=v;
	}
	
	/**
	 * 
	 */
	public double speed_grow  = 0.0  ;
	
	public double getSpeed_grow(){
		return this.speed_grow;
	}
	
	public void setSpeed_grow(double v){
		this.speed_grow=v;
	}
	
	/**
	 * 
	 */
	public double army_remake_grow  = 0.0  ;
	
	public double getArmy_remake_grow(){
		return this.army_remake_grow;
	}
	
	public void setArmy_remake_grow(double v){
		this.army_remake_grow=v;
	}
	
	/**
	 * 
	 */
	public double atta_remake_grow  = 0.0  ;
	
	public double getAtta_remake_grow(){
		return this.atta_remake_grow;
	}
	
	public void setAtta_remake_grow(double v){
		this.atta_remake_grow=v;
	}
	
	/**
	 * 
	 */
	public double denf_remake_grow  = 0.0  ;
	
	public double getDenf_remake_grow(){
		return this.denf_remake_grow;
	}
	
	public void setDenf_remake_grow(double v){
		this.denf_remake_grow=v;
	}
	
	/**
	 * 
	 */
	public double wise_remake_grow  = 0.0  ;
	
	public double getWise_remake_grow(){
		return this.wise_remake_grow;
	}
	
	public void setWise_remake_grow(double v){
		this.wise_remake_grow=v;
	}
	
	/**
	 * 
	 */
	public double speed_remake_grow  = 0.0  ;
	
	public double getSpeed_remake_grow(){
		return this.speed_remake_grow;
	}
	
	public void setSpeed_remake_grow(double v){
		this.speed_remake_grow=v;
	}
	
	/**
	 * 
	 */
	public double cost_times  = 0.0  ;
	
	public double getCost_times(){
		return this.cost_times;
	}
	
	public void setCost_times(double v){
		this.cost_times=v;
	}
	
	
};