package chuhan.gsp.attr;


public class clevelconfig implements mytools.ConvMain.Checkable ,Comparable<clevelconfig>{

	public int compareTo(clevelconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public clevelconfig(){
		super();
	}
	public clevelconfig(clevelconfig arg){
		this.id=arg.id ;
		this.levelexp=arg.levelexp ;
		this.herolevelexp=arg.herolevelexp ;
		this.strengthencost=arg.strengthencost ;
		this.canzhan=arg.canzhan ;
		this.tili=arg.tili ;
		this.huoli=arg.huoli ;
		this.invitetimes=arg.invitetimes ;
		this.skillexp1=arg.skillexp1 ;
		this.skillexp2=arg.skillexp2 ;
		this.skillexp3=arg.skillexp3 ;
		this.skillexp4=arg.skillexp4 ;
		this.skillexp5=arg.skillexp5 ;
		this.bonusworth=arg.bonusworth ;
		this.offergrow=arg.offergrow ;
		this.equipexp=arg.equipexp ;
		this.lvtext=arg.lvtext ;
		this.buttontext=arg.buttontext ;
		this.layout=arg.layout ;
		this.fujiang=arg.fujiang ;
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
	public int levelexp  = 0  ;
	
	public int getLevelexp(){
		return this.levelexp;
	}
	
	public void setLevelexp(int v){
		this.levelexp=v;
	}
	
	/**
	 * 
	 */
	public int herolevelexp  = 0  ;
	
	public int getHerolevelexp(){
		return this.herolevelexp;
	}
	
	public void setHerolevelexp(int v){
		this.herolevelexp=v;
	}
	
	/**
	 * 
	 */
	public int strengthencost  = 0  ;
	
	public int getStrengthencost(){
		return this.strengthencost;
	}
	
	public void setStrengthencost(int v){
		this.strengthencost=v;
	}
	
	/**
	 * 
	 */
	public int canzhan  = 0  ;
	
	public int getCanzhan(){
		return this.canzhan;
	}
	
	public void setCanzhan(int v){
		this.canzhan=v;
	}
	
	/**
	 * 
	 */
	public int tili  = 0  ;
	
	public int getTili(){
		return this.tili;
	}
	
	public void setTili(int v){
		this.tili=v;
	}
	
	/**
	 * 
	 */
	public int huoli  = 0  ;
	
	public int getHuoli(){
		return this.huoli;
	}
	
	public void setHuoli(int v){
		this.huoli=v;
	}
	
	/**
	 * 
	 */
	public int invitetimes  = 0  ;
	
	public int getInvitetimes(){
		return this.invitetimes;
	}
	
	public void setInvitetimes(int v){
		this.invitetimes=v;
	}
	
	/**
	 * 
	 */
	public int skillexp1  = 0  ;
	
	public int getSkillexp1(){
		return this.skillexp1;
	}
	
	public void setSkillexp1(int v){
		this.skillexp1=v;
	}
	
	/**
	 * 
	 */
	public int skillexp2  = 0  ;
	
	public int getSkillexp2(){
		return this.skillexp2;
	}
	
	public void setSkillexp2(int v){
		this.skillexp2=v;
	}
	
	/**
	 * 
	 */
	public int skillexp3  = 0  ;
	
	public int getSkillexp3(){
		return this.skillexp3;
	}
	
	public void setSkillexp3(int v){
		this.skillexp3=v;
	}
	
	/**
	 * 
	 */
	public int skillexp4  = 0  ;
	
	public int getSkillexp4(){
		return this.skillexp4;
	}
	
	public void setSkillexp4(int v){
		this.skillexp4=v;
	}
	
	/**
	 * 
	 */
	public int skillexp5  = 0  ;
	
	public int getSkillexp5(){
		return this.skillexp5;
	}
	
	public void setSkillexp5(int v){
		this.skillexp5=v;
	}
	
	/**
	 * 
	 */
	public int bonusworth  = 0  ;
	
	public int getBonusworth(){
		return this.bonusworth;
	}
	
	public void setBonusworth(int v){
		this.bonusworth=v;
	}
	
	/**
	 * 
	 */
	public int offergrow  = 0  ;
	
	public int getOffergrow(){
		return this.offergrow;
	}
	
	public void setOffergrow(int v){
		this.offergrow=v;
	}
	
	/**
	 * 
	 */
	public int equipexp  = 0  ;
	
	public int getEquipexp(){
		return this.equipexp;
	}
	
	public void setEquipexp(int v){
		this.equipexp=v;
	}
	
	/**
	 * 
	 */
	public String lvtext  = null  ;
	
	public String getLvtext(){
		return this.lvtext;
	}
	
	public void setLvtext(String v){
		this.lvtext=v;
	}
	
	/**
	 * 
	 */
	public String buttontext  = null  ;
	
	public String getButtontext(){
		return this.buttontext;
	}
	
	public void setButtontext(String v){
		this.buttontext=v;
	}
	
	/**
	 * 
	 */
	public String layout  = null  ;
	
	public String getLayout(){
		return this.layout;
	}
	
	public void setLayout(String v){
		this.layout=v;
	}
	
	/**
	 * 
	 */
	public int fujiang  = 0  ;
	
	public int getFujiang(){
		return this.fujiang;
	}
	
	public void setFujiang(int v){
		this.fujiang=v;
	}
	
	
};