package chuhan.gsp.game;


public class sTurntableRull implements mytools.ConvMain.Checkable ,Comparable<sTurntableRull>{

	public int compareTo(sTurntableRull o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sTurntableRull(){
		super();
	}
	public sTurntableRull(sTurntableRull arg){
		this.id=arg.id ;
		this.tableid=arg.tableid ;
		this.type=arg.type ;
		this.times=arg.times ;
		this.quanzhong1=arg.quanzhong1 ;
		this.quanzhong2=arg.quanzhong2 ;
		this.quanzhong3=arg.quanzhong3 ;
		this.quanzhong4=arg.quanzhong4 ;
		this.quanzhong5=arg.quanzhong5 ;
		this.quanzhong6=arg.quanzhong6 ;
		this.quanzhong7=arg.quanzhong7 ;
		this.quanzhong8=arg.quanzhong8 ;
		this.cost=arg.cost ;
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
	public int tableid  = 0  ;
	
	public int getTableid(){
		return this.tableid;
	}
	
	public void setTableid(int v){
		this.tableid=v;
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
	public int times  = 0  ;
	
	public int getTimes(){
		return this.times;
	}
	
	public void setTimes(int v){
		this.times=v;
	}
	
	/**
	 * 
	 */
	public double quanzhong1  = 0.0  ;
	
	public double getQuanzhong1(){
		return this.quanzhong1;
	}
	
	public void setQuanzhong1(double v){
		this.quanzhong1=v;
	}
	
	/**
	 * 
	 */
	public double quanzhong2  = 0.0  ;
	
	public double getQuanzhong2(){
		return this.quanzhong2;
	}
	
	public void setQuanzhong2(double v){
		this.quanzhong2=v;
	}
	
	/**
	 * 
	 */
	public double quanzhong3  = 0.0  ;
	
	public double getQuanzhong3(){
		return this.quanzhong3;
	}
	
	public void setQuanzhong3(double v){
		this.quanzhong3=v;
	}
	
	/**
	 * 
	 */
	public double quanzhong4  = 0.0  ;
	
	public double getQuanzhong4(){
		return this.quanzhong4;
	}
	
	public void setQuanzhong4(double v){
		this.quanzhong4=v;
	}
	
	/**
	 * 
	 */
	public double quanzhong5  = 0.0  ;
	
	public double getQuanzhong5(){
		return this.quanzhong5;
	}
	
	public void setQuanzhong5(double v){
		this.quanzhong5=v;
	}
	
	/**
	 * 
	 */
	public double quanzhong6  = 0.0  ;
	
	public double getQuanzhong6(){
		return this.quanzhong6;
	}
	
	public void setQuanzhong6(double v){
		this.quanzhong6=v;
	}
	
	/**
	 * 
	 */
	public double quanzhong7  = 0.0  ;
	
	public double getQuanzhong7(){
		return this.quanzhong7;
	}
	
	public void setQuanzhong7(double v){
		this.quanzhong7=v;
	}
	
	/**
	 * 
	 */
	public double quanzhong8  = 0.0  ;
	
	public double getQuanzhong8(){
		return this.quanzhong8;
	}
	
	public void setQuanzhong8(double v){
		this.quanzhong8=v;
	}
	
	/**
	 * 
	 */
	public int cost  = 0  ;
	
	public int getCost(){
		return this.cost;
	}
	
	public void setCost(int v){
		this.cost=v;
	}
	
	
};