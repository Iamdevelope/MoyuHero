package chuhan.gsp.item;


public class cskillconfig implements mytools.ConvMain.Checkable ,Comparable<cskillconfig>{

	public int compareTo(cskillconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cskillconfig(){
		super();
	}
	public cskillconfig(cskillconfig arg){
		this.id=arg.id ;
		this.name=arg.name ;
		this.color=arg.color ;
		this.icon=arg.icon ;
		this.type=arg.type ;
		this.effecttype=arg.effecttype ;
		this.skillstrength=arg.skillstrength ;
		this.skillstrengthgrow=arg.skillstrengthgrow ;
		this.jingattr1=arg.jingattr1 ;
		this.jingnum1=arg.jingnum1 ;
		this.jingattr2=arg.jingattr2 ;
		this.jingnum2=arg.jingnum2 ;
		this.jingattr3=arg.jingattr3 ;
		this.jingnum3=arg.jingnum3 ;
		this.namexap=arg.namexap ;
		this.xap1=arg.xap1 ;
		this.xap2=arg.xap2 ;
		this.lizi=arg.lizi ;
		this.action1=arg.action1 ;
		this.action2=arg.action2 ;
		this.waittime=arg.waittime ;
		this.attrup=arg.attrup ;
		this.battlesound=arg.battlesound ;
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
	public int effecttype  = 0  ;
	
	public int getEffecttype(){
		return this.effecttype;
	}
	
	public void setEffecttype(int v){
		this.effecttype=v;
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
	public double skillstrengthgrow  = 0.0  ;
	
	public double getSkillstrengthgrow(){
		return this.skillstrengthgrow;
	}
	
	public void setSkillstrengthgrow(double v){
		this.skillstrengthgrow=v;
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
	public String namexap  = null  ;
	
	public String getNamexap(){
		return this.namexap;
	}
	
	public void setNamexap(String v){
		this.namexap=v;
	}
	
	/**
	 * 
	 */
	public String xap1  = null  ;
	
	public String getXap1(){
		return this.xap1;
	}
	
	public void setXap1(String v){
		this.xap1=v;
	}
	
	/**
	 * 
	 */
	public String xap2  = null  ;
	
	public String getXap2(){
		return this.xap2;
	}
	
	public void setXap2(String v){
		this.xap2=v;
	}
	
	/**
	 * 
	 */
	public String lizi  = null  ;
	
	public String getLizi(){
		return this.lizi;
	}
	
	public void setLizi(String v){
		this.lizi=v;
	}
	
	/**
	 * 
	 */
	public int action1  = 0  ;
	
	public int getAction1(){
		return this.action1;
	}
	
	public void setAction1(int v){
		this.action1=v;
	}
	
	/**
	 * 
	 */
	public int action2  = 0  ;
	
	public int getAction2(){
		return this.action2;
	}
	
	public void setAction2(int v){
		this.action2=v;
	}
	
	/**
	 * 
	 */
	public int waittime  = 0  ;
	
	public int getWaittime(){
		return this.waittime;
	}
	
	public void setWaittime(int v){
		this.waittime=v;
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
	
	/**
	 * 
	 */
	public String battlesound  = null  ;
	
	public String getBattlesound(){
		return this.battlesound;
	}
	
	public void setBattlesound(String v){
		this.battlesound=v;
	}
	
	
};