package chuhan.gsp.game;


public class monthcard45 implements mytools.ConvMain.Checkable ,Comparable<monthcard45>{

	public int compareTo(monthcard45 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public monthcard45(){
		super();
	}
	public monthcard45(monthcard45 arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.des=arg.des ;
		this.dailygold=arg.dailygold ;
		this.dailydiamond=arg.dailydiamond ;
		this.expBonus=arg.expBonus ;
		this.refresh5Star=arg.refresh5Star ;
		this.duration=arg.duration ;
		this.vipexperience=arg.vipexperience ;
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
	public int dailygold  = 0  ;
	
	public int getDailygold(){
		return this.dailygold;
	}
	
	public void setDailygold(int v){
		this.dailygold=v;
	}
	
	/**
	 * 
	 */
	public int dailydiamond  = 0  ;
	
	public int getDailydiamond(){
		return this.dailydiamond;
	}
	
	public void setDailydiamond(int v){
		this.dailydiamond=v;
	}
	
	/**
	 * 
	 */
	public String expBonus  = null  ;
	
	public String getExpBonus(){
		return this.expBonus;
	}
	
	public void setExpBonus(String v){
		this.expBonus=v;
	}
	
	/**
	 * 
	 */
	public int refresh5Star  = 0  ;
	
	public int getRefresh5Star(){
		return this.refresh5Star;
	}
	
	public void setRefresh5Star(int v){
		this.refresh5Star=v;
	}
	
	/**
	 * 
	 */
	public int duration  = 0  ;
	
	public int getDuration(){
		return this.duration;
	}
	
	public void setDuration(int v){
		this.duration=v;
	}
	
	/**
	 * 
	 */
	public int vipexperience  = 0  ;
	
	public int getVipexperience(){
		return this.vipexperience;
	}
	
	public void setVipexperience(int v){
		this.vipexperience=v;
	}
	
	
};