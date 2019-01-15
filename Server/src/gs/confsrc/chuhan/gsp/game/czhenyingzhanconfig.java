package chuhan.gsp.game;


public class czhenyingzhanconfig implements mytools.ConvMain.Checkable ,Comparable<czhenyingzhanconfig>{

	public int compareTo(czhenyingzhanconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public czhenyingzhanconfig(){
		super();
	}
	public czhenyingzhanconfig(czhenyingzhanconfig arg){
		this.id=arg.id ;
		this.StartTime=arg.StartTime ;
		this.EndTime=arg.EndTime ;
		this.meirijiangli=arg.meirijiangli ;
		this.tuijianjiangli=arg.tuijianjiangli ;
		this.changecampcost=arg.changecampcost ;
		this.xiaohaoshiqi=arg.xiaohaoshiqi ;
		this.shuaxinfeiyong=arg.shuaxinfeiyong ;
		this.chushijifen=arg.chushijifen ;
		this.starttime=arg.starttime ;
		this.nengli1=arg.nengli1 ;
		this.nengli2=arg.nengli2 ;
		this.nengli3=arg.nengli3 ;
		this.jifen1=arg.jifen1 ;
		this.jifen2=arg.jifen2 ;
		this.jifen3=arg.jifen3 ;
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
	public String StartTime  = null  ;
	
	public String getStartTime(){
		return this.StartTime;
	}
	
	public void setStartTime(String v){
		this.StartTime=v;
	}
	
	/**
	 * 
	 */
	public String EndTime  = null  ;
	
	public String getEndTime(){
		return this.EndTime;
	}
	
	public void setEndTime(String v){
		this.EndTime=v;
	}
	
	/**
	 * 
	 */
	public int meirijiangli  = 0  ;
	
	public int getMeirijiangli(){
		return this.meirijiangli;
	}
	
	public void setMeirijiangli(int v){
		this.meirijiangli=v;
	}
	
	/**
	 * 
	 */
	public int tuijianjiangli  = 0  ;
	
	public int getTuijianjiangli(){
		return this.tuijianjiangli;
	}
	
	public void setTuijianjiangli(int v){
		this.tuijianjiangli=v;
	}
	
	/**
	 * 
	 */
	public int changecampcost  = 0  ;
	
	public int getChangecampcost(){
		return this.changecampcost;
	}
	
	public void setChangecampcost(int v){
		this.changecampcost=v;
	}
	
	/**
	 * 
	 */
	public int xiaohaoshiqi  = 0  ;
	
	public int getXiaohaoshiqi(){
		return this.xiaohaoshiqi;
	}
	
	public void setXiaohaoshiqi(int v){
		this.xiaohaoshiqi=v;
	}
	
	/**
	 * 
	 */
	public int shuaxinfeiyong  = 0  ;
	
	public int getShuaxinfeiyong(){
		return this.shuaxinfeiyong;
	}
	
	public void setShuaxinfeiyong(int v){
		this.shuaxinfeiyong=v;
	}
	
	/**
	 * 
	 */
	public int chushijifen  = 0  ;
	
	public int getChushijifen(){
		return this.chushijifen;
	}
	
	public void setChushijifen(int v){
		this.chushijifen=v;
	}
	
	/**
	 * 
	 */
	public int starttime  = 0  ;
	
	public int getStarttime(){
		return this.starttime;
	}
	
	public void setStarttime(int v){
		this.starttime=v;
	}
	
	/**
	 * 
	 */
	public int nengli1  = 0  ;
	
	public int getNengli1(){
		return this.nengli1;
	}
	
	public void setNengli1(int v){
		this.nengli1=v;
	}
	
	/**
	 * 
	 */
	public int nengli2  = 0  ;
	
	public int getNengli2(){
		return this.nengli2;
	}
	
	public void setNengli2(int v){
		this.nengli2=v;
	}
	
	/**
	 * 
	 */
	public int nengli3  = 0  ;
	
	public int getNengli3(){
		return this.nengli3;
	}
	
	public void setNengli3(int v){
		this.nengli3=v;
	}
	
	/**
	 * 
	 */
	public int jifen1  = 0  ;
	
	public int getJifen1(){
		return this.jifen1;
	}
	
	public void setJifen1(int v){
		this.jifen1=v;
	}
	
	/**
	 * 
	 */
	public int jifen2  = 0  ;
	
	public int getJifen2(){
		return this.jifen2;
	}
	
	public void setJifen2(int v){
		this.jifen2=v;
	}
	
	/**
	 * 
	 */
	public int jifen3  = 0  ;
	
	public int getJifen3(){
		return this.jifen3;
	}
	
	public void setJifen3(int v){
		this.jifen3=v;
	}
	
	
};