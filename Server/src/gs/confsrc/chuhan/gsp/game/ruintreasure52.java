package chuhan.gsp.game;


public class ruintreasure52 implements mytools.ConvMain.Checkable ,Comparable<ruintreasure52>{

	public int compareTo(ruintreasure52 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ruintreasure52(){
		super();
	}
	public ruintreasure52(ruintreasure52 arg){
		this.id=arg.id ;
		this.type=arg.type ;
		this.parameter1=arg.parameter1 ;
		this.parameter2=arg.parameter2 ;
		this.weight1=arg.weight1 ;
		this.monthcard_refresh1=arg.monthcard_refresh1 ;
		this.weight2=arg.weight2 ;
		this.monthcard_refresh2=arg.monthcard_refresh2 ;
		this.weight3=arg.weight3 ;
		this.monthcard_refresh3=arg.monthcard_refresh3 ;
		this.weight4=arg.weight4 ;
		this.monthcard_refresh4=arg.monthcard_refresh4 ;
		this.get_weight=arg.get_weight ;
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
	public String parameter2  = null  ;
	
	public String getParameter2(){
		return this.parameter2;
	}
	
	public void setParameter2(String v){
		this.parameter2=v;
	}
	
	/**
	 * 
	 */
	public int weight1  = 0  ;
	
	public int getWeight1(){
		return this.weight1;
	}
	
	public void setWeight1(int v){
		this.weight1=v;
	}
	
	/**
	 * 
	 */
	public int monthcard_refresh1  = 0  ;
	
	public int getMonthcard_refresh1(){
		return this.monthcard_refresh1;
	}
	
	public void setMonthcard_refresh1(int v){
		this.monthcard_refresh1=v;
	}
	
	/**
	 * 
	 */
	public int weight2  = 0  ;
	
	public int getWeight2(){
		return this.weight2;
	}
	
	public void setWeight2(int v){
		this.weight2=v;
	}
	
	/**
	 * 
	 */
	public int monthcard_refresh2  = 0  ;
	
	public int getMonthcard_refresh2(){
		return this.monthcard_refresh2;
	}
	
	public void setMonthcard_refresh2(int v){
		this.monthcard_refresh2=v;
	}
	
	/**
	 * 
	 */
	public int weight3  = 0  ;
	
	public int getWeight3(){
		return this.weight3;
	}
	
	public void setWeight3(int v){
		this.weight3=v;
	}
	
	/**
	 * 
	 */
	public int monthcard_refresh3  = 0  ;
	
	public int getMonthcard_refresh3(){
		return this.monthcard_refresh3;
	}
	
	public void setMonthcard_refresh3(int v){
		this.monthcard_refresh3=v;
	}
	
	/**
	 * 
	 */
	public int weight4  = 0  ;
	
	public int getWeight4(){
		return this.weight4;
	}
	
	public void setWeight4(int v){
		this.weight4=v;
	}
	
	/**
	 * 
	 */
	public int monthcard_refresh4  = 0  ;
	
	public int getMonthcard_refresh4(){
		return this.monthcard_refresh4;
	}
	
	public void setMonthcard_refresh4(int v){
		this.monthcard_refresh4=v;
	}
	
	/**
	 * 
	 */
	public int get_weight  = 0  ;
	
	public int getGet_weight(){
		return this.get_weight;
	}
	
	public void setGet_weight(int v){
		this.get_weight=v;
	}
	
	
};