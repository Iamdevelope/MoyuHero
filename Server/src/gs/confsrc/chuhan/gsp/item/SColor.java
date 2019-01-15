package chuhan.gsp.item;


public class SColor implements mytools.ConvMain.Checkable ,Comparable<SColor>{

	public int compareTo(SColor o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SColor(){
		super();
	}
	public SColor(SColor arg){
		this.id=arg.id ;
		this.heroexp_times=arg.heroexp_times ;
		this.skill_times=arg.skill_times ;
		this.HeroOfferGrow=arg.HeroOfferGrow ;
		this.SoulOfferGrow=arg.SoulOfferGrow ;
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
	public double heroexp_times  = 0.0  ;
	
	public double getHeroexp_times(){
		return this.heroexp_times;
	}
	
	public void setHeroexp_times(double v){
		this.heroexp_times=v;
	}
	
	/**
	 * 
	 */
	public double skill_times  = 0.0  ;
	
	public double getSkill_times(){
		return this.skill_times;
	}
	
	public void setSkill_times(double v){
		this.skill_times=v;
	}
	
	/**
	 * 
	 */
	public int HeroOfferGrow  = 0  ;
	
	public int getHeroOfferGrow(){
		return this.HeroOfferGrow;
	}
	
	public void setHeroOfferGrow(int v){
		this.HeroOfferGrow=v;
	}
	
	/**
	 * 
	 */
	public int SoulOfferGrow  = 0  ;
	
	public int getSoulOfferGrow(){
		return this.SoulOfferGrow;
	}
	
	public void setSoulOfferGrow(int v){
		this.SoulOfferGrow=v;
	}
	
	
};