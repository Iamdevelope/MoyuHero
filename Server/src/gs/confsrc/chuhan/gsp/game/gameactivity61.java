package chuhan.gsp.game;


public class gameactivity61 implements mytools.ConvMain.Checkable ,Comparable<gameactivity61>{

	public int compareTo(gameactivity61 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public gameactivity61(){
		super();
	}
	public gameactivity61(gameactivity61 arg){
		this.id=arg.id ;
		this.type=arg.type ;
		this.team=arg.team ;
		this.beginday=arg.beginday ;
		this.deadline=arg.deadline ;
		this.titledes=arg.titledes ;
		this.contentdes=arg.contentdes ;
		this.daymax=arg.daymax ;
		this.periodmax=arg.periodmax ;
		this.parameter1=arg.parameter1 ;
		this.parameter2=arg.parameter2 ;
		this.parameter3=arg.parameter3 ;
		this.drop=arg.drop ;
		this.dropdestype=arg.dropdestype ;
		this.dropdes=arg.dropdes ;
		this.numdes=arg.numdes ;
		this.textdes=arg.textdes ;
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
	public int team  = 0  ;
	
	public int getTeam(){
		return this.team;
	}
	
	public void setTeam(int v){
		this.team=v;
	}
	
	/**
	 * 
	 */
	public String beginday  = null  ;
	
	public String getBeginday(){
		return this.beginday;
	}
	
	public void setBeginday(String v){
		this.beginday=v;
	}
	
	/**
	 * 
	 */
	public String deadline  = null  ;
	
	public String getDeadline(){
		return this.deadline;
	}
	
	public void setDeadline(String v){
		this.deadline=v;
	}
	
	/**
	 * 
	 */
	public String titledes  = null  ;
	
	public String getTitledes(){
		return this.titledes;
	}
	
	public void setTitledes(String v){
		this.titledes=v;
	}
	
	/**
	 * 
	 */
	public String contentdes  = null  ;
	
	public String getContentdes(){
		return this.contentdes;
	}
	
	public void setContentdes(String v){
		this.contentdes=v;
	}
	
	/**
	 * 
	 */
	public int daymax  = 0  ;
	
	public int getDaymax(){
		return this.daymax;
	}
	
	public void setDaymax(int v){
		this.daymax=v;
	}
	
	/**
	 * 
	 */
	public int periodmax  = 0  ;
	
	public int getPeriodmax(){
		return this.periodmax;
	}
	
	public void setPeriodmax(int v){
		this.periodmax=v;
	}
	
	/**
	 * 
	 */
	public int parameter1  = 0  ;
	
	public int getParameter1(){
		return this.parameter1;
	}
	
	public void setParameter1(int v){
		this.parameter1=v;
	}
	
	/**
	 * 
	 */
	public int parameter2  = 0  ;
	
	public int getParameter2(){
		return this.parameter2;
	}
	
	public void setParameter2(int v){
		this.parameter2=v;
	}
	
	/**
	 * 
	 */
	public int parameter3  = 0  ;
	
	public int getParameter3(){
		return this.parameter3;
	}
	
	public void setParameter3(int v){
		this.parameter3=v;
	}
	
	/**
	 * 
	 */
	public String drop  = null  ;
	
	public String getDrop(){
		return this.drop;
	}
	
	public void setDrop(String v){
		this.drop=v;
	}
	
	/**
	 * 
	 */
	public String dropdestype  = null  ;
	
	public String getDropdestype(){
		return this.dropdestype;
	}
	
	public void setDropdestype(String v){
		this.dropdestype=v;
	}
	
	/**
	 * 
	 */
	public String dropdes  = null  ;
	
	public String getDropdes(){
		return this.dropdes;
	}
	
	public void setDropdes(String v){
		this.dropdes=v;
	}
	
	/**
	 * 
	 */
	public String numdes  = null  ;
	
	public String getNumdes(){
		return this.numdes;
	}
	
	public void setNumdes(String v){
		this.numdes=v;
	}
	
	/**
	 * 
	 */
	public String textdes  = null  ;
	
	public String getTextdes(){
		return this.textdes;
	}
	
	public void setTextdes(String v){
		this.textdes=v;
	}
	
	
};