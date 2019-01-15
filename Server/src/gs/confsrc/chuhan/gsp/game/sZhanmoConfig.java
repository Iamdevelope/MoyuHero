package chuhan.gsp.game;


public class sZhanmoConfig implements mytools.ConvMain.Checkable ,Comparable<sZhanmoConfig>{

	public int compareTo(sZhanmoConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sZhanmoConfig(){
		super();
	}
	public sZhanmoConfig(sZhanmoConfig arg){
		this.id=arg.id ;
		this.BOSS1_ID=arg.BOSS1_ID ;
		this.BOSS2_ID=arg.BOSS2_ID ;
		this.BOSS3_ID=arg.BOSS3_ID ;
		this.BOSS4_ID=arg.BOSS4_ID ;
		this.StartTime=arg.StartTime ;
		this.EndTime=arg.EndTime ;
		this.Gailv=arg.Gailv ;
		this.ShuaxinFeiyong=arg.ShuaxinFeiyong ;
		this.XiaohaoShiqi=arg.XiaohaoShiqi ;
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
	public int BOSS1_ID  = 0  ;
	
	public int getBOSS1_ID(){
		return this.BOSS1_ID;
	}
	
	public void setBOSS1_ID(int v){
		this.BOSS1_ID=v;
	}
	
	/**
	 * 
	 */
	public int BOSS2_ID  = 0  ;
	
	public int getBOSS2_ID(){
		return this.BOSS2_ID;
	}
	
	public void setBOSS2_ID(int v){
		this.BOSS2_ID=v;
	}
	
	/**
	 * 
	 */
	public int BOSS3_ID  = 0  ;
	
	public int getBOSS3_ID(){
		return this.BOSS3_ID;
	}
	
	public void setBOSS3_ID(int v){
		this.BOSS3_ID=v;
	}
	
	/**
	 * 
	 */
	public int BOSS4_ID  = 0  ;
	
	public int getBOSS4_ID(){
		return this.BOSS4_ID;
	}
	
	public void setBOSS4_ID(int v){
		this.BOSS4_ID=v;
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
	public double Gailv  = 0.0  ;
	
	public double getGailv(){
		return this.Gailv;
	}
	
	public void setGailv(double v){
		this.Gailv=v;
	}
	
	/**
	 * 
	 */
	public int ShuaxinFeiyong  = 0  ;
	
	public int getShuaxinFeiyong(){
		return this.ShuaxinFeiyong;
	}
	
	public void setShuaxinFeiyong(int v){
		this.ShuaxinFeiyong=v;
	}
	
	/**
	 * 
	 */
	public int XiaohaoShiqi  = 0  ;
	
	public int getXiaohaoShiqi(){
		return this.XiaohaoShiqi;
	}
	
	public void setXiaohaoShiqi(int v){
		this.XiaohaoShiqi=v;
	}
	
	
};