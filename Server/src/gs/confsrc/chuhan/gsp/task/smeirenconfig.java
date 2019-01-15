package chuhan.gsp.task;


public class smeirenconfig implements mytools.ConvMain.Checkable ,Comparable<smeirenconfig>{

	public int compareTo(smeirenconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public smeirenconfig(){
		super();
	}
	public smeirenconfig(smeirenconfig arg){
		this.id=arg.id ;
		this.heroid=arg.heroid ;
		this.heroid1=arg.heroid1 ;
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