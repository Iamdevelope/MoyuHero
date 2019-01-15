package chuhan.gsp.task;


public class cmeirenconfig implements mytools.ConvMain.Checkable ,Comparable<cmeirenconfig>{

	public int compareTo(cmeirenconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cmeirenconfig(){
		super();
	}
	public cmeirenconfig(cmeirenconfig arg){
		this.id=arg.id ;
		this.heroid=arg.heroid ;
		this.heroid1=arg.heroid1 ;
		this.name1=arg.name1 ;
		this.pic1=arg.pic1 ;
		this.pic2=arg.pic2 ;
		this.yiban1=arg.yiban1 ;
		this.music1=arg.music1 ;
		this.yiban2=arg.yiban2 ;
		this.music2=arg.music2 ;
		this.keqiu=arg.keqiu ;
		this.music3=arg.music3 ;
		this.chaping=arg.chaping ;
		this.music4=arg.music4 ;
		this.haoping=arg.haoping ;
		this.music5=arg.music5 ;
		this.linjin=arg.linjin ;
		this.music6=arg.music6 ;
		this.date=arg.date ;
		this.bonus=arg.bonus ;
		this.skin=arg.skin ;
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
	public int heroid  = 0  ;
	
	public int getHeroid(){
		return this.heroid;
	}
	
	public void setHeroid(int v){
		this.heroid=v;
	}
	
	/**
	 * 
	 */
	public int heroid1  = 0  ;
	
	public int getHeroid1(){
		return this.heroid1;
	}
	
	public void setHeroid1(int v){
		this.heroid1=v;
	}
	
	/**
	 * 
	 */
	public String name1  = null  ;
	
	public String getName1(){
		return this.name1;
	}
	
	public void setName1(String v){
		this.name1=v;
	}
	
	/**
	 * 
	 */
	public String pic1  = null  ;
	
	public String getPic1(){
		return this.pic1;
	}
	
	public void setPic1(String v){
		this.pic1=v;
	}
	
	/**
	 * 
	 */
	public String pic2  = null  ;
	
	public String getPic2(){
		return this.pic2;
	}
	
	public void setPic2(String v){
		this.pic2=v;
	}
	
	/**
	 * 
	 */
	public String yiban1  = null  ;
	
	public String getYiban1(){
		return this.yiban1;
	}
	
	public void setYiban1(String v){
		this.yiban1=v;
	}
	
	/**
	 * 
	 */
	public String music1  = null  ;
	
	public String getMusic1(){
		return this.music1;
	}
	
	public void setMusic1(String v){
		this.music1=v;
	}
	
	/**
	 * 
	 */
	public String yiban2  = null  ;
	
	public String getYiban2(){
		return this.yiban2;
	}
	
	public void setYiban2(String v){
		this.yiban2=v;
	}
	
	/**
	 * 
	 */
	public String music2  = null  ;
	
	public String getMusic2(){
		return this.music2;
	}
	
	public void setMusic2(String v){
		this.music2=v;
	}
	
	/**
	 * 
	 */
	public String keqiu  = null  ;
	
	public String getKeqiu(){
		return this.keqiu;
	}
	
	public void setKeqiu(String v){
		this.keqiu=v;
	}
	
	/**
	 * 
	 */
	public String music3  = null  ;
	
	public String getMusic3(){
		return this.music3;
	}
	
	public void setMusic3(String v){
		this.music3=v;
	}
	
	/**
	 * 
	 */
	public String chaping  = null  ;
	
	public String getChaping(){
		return this.chaping;
	}
	
	public void setChaping(String v){
		this.chaping=v;
	}
	
	/**
	 * 
	 */
	public String music4  = null  ;
	
	public String getMusic4(){
		return this.music4;
	}
	
	public void setMusic4(String v){
		this.music4=v;
	}
	
	/**
	 * 
	 */
	public String haoping  = null  ;
	
	public String getHaoping(){
		return this.haoping;
	}
	
	public void setHaoping(String v){
		this.haoping=v;
	}
	
	/**
	 * 
	 */
	public String music5  = null  ;
	
	public String getMusic5(){
		return this.music5;
	}
	
	public void setMusic5(String v){
		this.music5=v;
	}
	
	/**
	 * 
	 */
	public String linjin  = null  ;
	
	public String getLinjin(){
		return this.linjin;
	}
	
	public void setLinjin(String v){
		this.linjin=v;
	}
	
	/**
	 * 
	 */
	public String music6  = null  ;
	
	public String getMusic6(){
		return this.music6;
	}
	
	public void setMusic6(String v){
		this.music6=v;
	}
	
	/**
	 * 
	 */
	public int date  = 0  ;
	
	public int getDate(){
		return this.date;
	}
	
	public void setDate(int v){
		this.date=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<Integer> bonus  ;
	
	public java.util.ArrayList<Integer> getBonus(){
		return this.bonus;
	}
	
	public void setBonus(java.util.ArrayList<Integer> v){
		this.bonus=v;
	}
	
	/**
	 * 
	 */
	public java.util.ArrayList<Integer> skin  ;
	
	public java.util.ArrayList<Integer> getSkin(){
		return this.skin;
	}
	
	public void setSkin(java.util.ArrayList<Integer> v){
		this.skin=v;
	}
	
	
};