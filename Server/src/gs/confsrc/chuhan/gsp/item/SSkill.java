package chuhan.gsp.item;


public class SSkill implements mytools.ConvMain.Checkable ,Comparable<SSkill>{

	public int compareTo(SSkill o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SSkill(){
		super();
	}
	public SSkill(SSkill arg){
		this.id=arg.id ;
		this.color=arg.color ;
		this.type=arg.type ;
		this.skillstrength=arg.skillstrength ;
		this.skillstrength_grow=arg.skillstrength_grow ;
		this.skillshifang=arg.skillshifang ;
		this.hurt=arg.hurt ;
		this.jingattr1=arg.jingattr1 ;
		this.jingnum1=arg.jingnum1 ;
		this.jingattr2=arg.jingattr2 ;
		this.jingnum2=arg.jingnum2 ;
		this.jingattr3=arg.jingattr3 ;
		this.jingnum3=arg.jingnum3 ;
		this.effectNum=arg.effectNum ;
		this.reduce1=arg.reduce1 ;
		this.reduce2=arg.reduce2 ;
		this.reduce3=arg.reduce3 ;
		this.reduce4=arg.reduce4 ;
		this.attrup=arg.attrup ;
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
	public int skillstrength  = 0  ;
	
	public int getSkillstrength(){
		return this.skillstrength;
	}
	
	public void setSkillstrength(int v){
		this.skillstrength=v;
	}
	
	/**
	 * 
	 */
	public double skillstrength_grow  = 0.0  ;
	
	public double getSkillstrength_grow(){
		return this.skillstrength_grow;
	}
	
	public void setSkillstrength_grow(double v){
		this.skillstrength_grow=v;
	}
	
	/**
	 * 
	 */
	public double skillshifang  = 0.0  ;
	
	public double getSkillshifang(){
		return this.skillshifang;
	}
	
	public void setSkillshifang(double v){
		this.skillshifang=v;
	}
	
	/**
	 * 
	 */
	public String hurt  = null  ;
	
	public String getHurt(){
		return this.hurt;
	}
	
	public void setHurt(String v){
		this.hurt=v;
	}
	
	/**
	 * 
	 */
	public int jingattr1  = 0  ;
	
	public int getJingattr1(){
		return this.jingattr1;
	}
	
	public void setJingattr1(int v){
		this.jingattr1=v;
	}
	
	/**
	 * 
	 */
	public double jingnum1  = 0.0  ;
	
	public double getJingnum1(){
		return this.jingnum1;
	}
	
	public void setJingnum1(double v){
		this.jingnum1=v;
	}
	
	/**
	 * 
	 */
	public int jingattr2  = 0  ;
	
	public int getJingattr2(){
		return this.jingattr2;
	}
	
	public void setJingattr2(int v){
		this.jingattr2=v;
	}
	
	/**
	 * 
	 */
	public double jingnum2  = 0.0  ;
	
	public double getJingnum2(){
		return this.jingnum2;
	}
	
	public void setJingnum2(double v){
		this.jingnum2=v;
	}
	
	/**
	 * 
	 */
	public int jingattr3  = 0  ;
	
	public int getJingattr3(){
		return this.jingattr3;
	}
	
	public void setJingattr3(int v){
		this.jingattr3=v;
	}
	
	/**
	 * 
	 */
	public double jingnum3  = 0.0  ;
	
	public double getJingnum3(){
		return this.jingnum3;
	}
	
	public void setJingnum3(double v){
		this.jingnum3=v;
	}
	
	/**
	 * 
	 */
	public int effectNum  = 0  ;
	
	public int getEffectNum(){
		return this.effectNum;
	}
	
	public void setEffectNum(int v){
		this.effectNum=v;
	}
	
	/**
	 * 
	 */
	public double reduce1  = 0.0  ;
	
	public double getReduce1(){
		return this.reduce1;
	}
	
	public void setReduce1(double v){
		this.reduce1=v;
	}
	
	/**
	 * 
	 */
	public double reduce2  = 0.0  ;
	
	public double getReduce2(){
		return this.reduce2;
	}
	
	public void setReduce2(double v){
		this.reduce2=v;
	}
	
	/**
	 * 
	 */
	public double reduce3  = 0.0  ;
	
	public double getReduce3(){
		return this.reduce3;
	}
	
	public void setReduce3(double v){
		this.reduce3=v;
	}
	
	/**
	 * 
	 */
	public double reduce4  = 0.0  ;
	
	public double getReduce4(){
		return this.reduce4;
	}
	
	public void setReduce4(double v){
		this.reduce4=v;
	}
	
	/**
	 * 
	 */
	public int attrup  = 0  ;
	
	public int getAttrup(){
		return this.attrup;
	}
	
	public void setAttrup(int v){
		this.attrup=v;
	}
	
	
};