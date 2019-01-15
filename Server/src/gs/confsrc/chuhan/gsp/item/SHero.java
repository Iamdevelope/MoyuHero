package chuhan.gsp.item;


public class SHero implements mytools.ConvMain.Checkable ,Comparable<SHero>{

	public int compareTo(SHero o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SHero(){
		super();
	}
	public SHero(SHero arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.chuhan=arg.chuhan ;
		this.color=arg.color ;
		this.deify=arg.deify ;
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
		this.evo_army_grow=arg.evo_army_grow ;
		this.evo_atta_grow=arg.evo_atta_grow ;
		this.evo_denf_grow=arg.evo_denf_grow ;
		this.evo_wise_grow=arg.evo_wise_grow ;
		this.evo_speed_grow=arg.evo_speed_grow ;
		this.soulid=arg.soulid ;
		this.exchangenum=arg.exchangenum ;
		this.bindskill=arg.bindskill ;
		this.bindsweapon=arg.bindsweapon ;
		this.bindscloth=arg.bindscloth ;
		this.bindshorse=arg.bindshorse ;
		this.qiyuan=arg.qiyuan ;
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
	public double evo_army_grow  = 0.0  ;
	
	public double getEvo_army_grow(){
		return this.evo_army_grow;
	}
	
	public void setEvo_army_grow(double v){
		this.evo_army_grow=v;
	}
	
	/**
	 * 
	 */
	public double evo_atta_grow  = 0.0  ;
	
	public double getEvo_atta_grow(){
		return this.evo_atta_grow;
	}
	
	public void setEvo_atta_grow(double v){
		this.evo_atta_grow=v;
	}
	
	/**
	 * 
	 */
	public double evo_denf_grow  = 0.0  ;
	
	public double getEvo_denf_grow(){
		return this.evo_denf_grow;
	}
	
	public void setEvo_denf_grow(double v){
		this.evo_denf_grow=v;
	}
	
	/**
	 * 
	 */
	public double evo_wise_grow  = 0.0  ;
	
	public double getEvo_wise_grow(){
		return this.evo_wise_grow;
	}
	
	public void setEvo_wise_grow(double v){
		this.evo_wise_grow=v;
	}
	
	/**
	 * 
	 */
	public double evo_speed_grow  = 0.0  ;
	
	public double getEvo_speed_grow(){
		return this.evo_speed_grow;
	}
	
	public void setEvo_speed_grow(double v){
		this.evo_speed_grow=v;
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
	public java.util.ArrayList<String> qiyuanattr  ;
	
	public java.util.ArrayList<String> getQiyuanattr(){
		return this.qiyuanattr;
	}
	
	public void setQiyuanattr(java.util.ArrayList<String> v){
		this.qiyuanattr=v;
	}
	
	
};