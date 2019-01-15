package chuhan.gsp.item;


public class ccolorconfig implements mytools.ConvMain.Checkable ,Comparable<ccolorconfig>{

	public int compareTo(ccolorconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ccolorconfig(){
		super();
	}
	public ccolorconfig(ccolorconfig arg){
		this.id=arg.id ;
		this.heroexptimes=arg.heroexptimes ;
		this.skilltimes=arg.skilltimes ;
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
	public double heroexptimes  = 0.0  ;
	
	public double getHeroexptimes(){
		return this.heroexptimes;
	}
	
	public void setHeroexptimes(double v){
		this.heroexptimes=v;
	}
	
	/**
	 * 
	 */
	public double skilltimes  = 0.0  ;
	
	public double getSkilltimes(){
		return this.skilltimes;
	}
	
	public void setSkilltimes(double v){
		this.skilltimes=v;
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