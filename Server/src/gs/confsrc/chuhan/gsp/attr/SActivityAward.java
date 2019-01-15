package chuhan.gsp.attr;


public class SActivityAward implements mytools.ConvMain.Checkable ,Comparable<SActivityAward>{

	public int compareTo(SActivityAward o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SActivityAward(){
		super();
	}
	public SActivityAward(SActivityAward arg){
		this.id=arg.id ;
		this.yuanbao=arg.yuanbao ;
		this.money=arg.money ;
		this.exp=arg.exp ;
		this.HeroExp=arg.HeroExp ;
		this.firstClassAward=arg.firstClassAward ;
		this.secondClassAward=arg.secondClassAward ;
		this.secondClassAwardProb=arg.secondClassAwardProb ;
		this.secondClassDenominator=arg.secondClassDenominator ;
		this.randomType=arg.randomType ;
		this.totalProb=arg.totalProb ;
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
	public String yuanbao  = null  ;
	
	public String getYuanbao(){
		return this.yuanbao;
	}
	
	public void setYuanbao(String v){
		this.yuanbao=v;
	}
	
	/**
	 * 
	 */
	public String money  = null  ;
	
	public String getMoney(){
		return this.money;
	}
	
	public void setMoney(String v){
		this.money=v;
	}
	
	/**
	 * 
	 */
	public String exp  = null  ;
	
	public String getExp(){
		return this.exp;
	}
	
	public void setExp(String v){
		this.exp=v;
	}
	
	/**
	 * 
	 */
	public String HeroExp  = null  ;
	
	public String getHeroExp(){
		return this.HeroExp;
	}
	
	public void setHeroExp(String v){
		this.HeroExp=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<Integer> firstClassAward  ;
	
	public java.util.ArrayList<Integer> getFirstClassAward(){
		return this.firstClassAward;
	}
	
	public void setFirstClassAward(java.util.ArrayList<Integer> v){
		this.firstClassAward=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<Integer> secondClassAward  ;
	
	public java.util.ArrayList<Integer> getSecondClassAward(){
		return this.secondClassAward;
	}
	
	public void setSecondClassAward(java.util.ArrayList<Integer> v){
		this.secondClassAward=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<String> secondClassAwardProb  ;
	
	public java.util.ArrayList<String> getSecondClassAwardProb(){
		return this.secondClassAwardProb;
	}
	
	public void setSecondClassAwardProb(java.util.ArrayList<String> v){
		this.secondClassAwardProb=v;
	}
	
	/**
	 * 
	 */
	public int secondClassDenominator  = 0  ;
	
	public int getSecondClassDenominator(){
		return this.secondClassDenominator;
	}
	
	public void setSecondClassDenominator(int v){
		this.secondClassDenominator=v;
	}
	
	/**
	 * 
	 */
	public int randomType  = 0  ;
	
	public int getRandomType(){
		return this.randomType;
	}
	
	public void setRandomType(int v){
		this.randomType=v;
	}
	
	/**
	 * 
	 */
	public String totalProb  = null  ;
	
	public String getTotalProb(){
		return this.totalProb;
	}
	
	public void setTotalProb(String v){
		this.totalProb=v;
	}
	
	
};