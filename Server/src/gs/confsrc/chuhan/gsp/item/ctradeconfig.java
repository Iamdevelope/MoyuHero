package chuhan.gsp.item;


public class ctradeconfig implements mytools.ConvMain.Checkable ,Comparable<ctradeconfig>{

	public int compareTo(ctradeconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ctradeconfig(){
		super();
	}
	public ctradeconfig(ctradeconfig arg){
		this.id=arg.id ;
		this.zoneid=arg.zoneid ;
		this.starttime=arg.starttime ;
		this.endtime=arg.endtime ;
		this.times=arg.times ;
		this.viplevel=arg.viplevel ;
		this.describe=arg.describe ;
		this.yinliang=arg.yinliang ;
		this.yuanbao=arg.yuanbao ;
		this.itemlist=arg.itemlist ;
		this.itemrank=arg.itemrank ;
		this.itemnum=arg.itemnum ;
		this.convert=arg.convert ;
		this.item=arg.item ;
		this.num=arg.num ;
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
	public String zoneid  = null  ;
	
	public String getZoneid(){
		return this.zoneid;
	}
	
	public void setZoneid(String v){
		this.zoneid=v;
	}
	
	/**
	 * 
	 */
	public String starttime  = null  ;
	
	public String getStarttime(){
		return this.starttime;
	}
	
	public void setStarttime(String v){
		this.starttime=v;
	}
	
	/**
	 * 
	 */
	public String endtime  = null  ;
	
	public String getEndtime(){
		return this.endtime;
	}
	
	public void setEndtime(String v){
		this.endtime=v;
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
	public int viplevel  = 0  ;
	
	public int getViplevel(){
		return this.viplevel;
	}
	
	public void setViplevel(int v){
		this.viplevel=v;
	}
	
	/**
	 * 
	 */
	public String describe  = null  ;
	
	public String getDescribe(){
		return this.describe;
	}
	
	public void setDescribe(String v){
		this.describe=v;
	}
	
	/**
	 * 
	 */
	public int yinliang  = 0  ;
	
	public int getYinliang(){
		return this.yinliang;
	}
	
	public void setYinliang(int v){
		this.yinliang=v;
	}
	
	/**
	 * 
	 */
	public int yuanbao  = 0  ;
	
	public int getYuanbao(){
		return this.yuanbao;
	}
	
	public void setYuanbao(int v){
		this.yuanbao=v;
	}
	
	/**
	 * 
	 */
	public int itemlist  = 0  ;
	
	public int getItemlist(){
		return this.itemlist;
	}
	
	public void setItemlist(int v){
		this.itemlist=v;
	}
	
	/**
	 * 
	 */
	public int itemrank  = 0  ;
	
	public int getItemrank(){
		return this.itemrank;
	}
	
	public void setItemrank(int v){
		this.itemrank=v;
	}
	
	/**
	 * 
	 */
	public int itemnum  = 0  ;
	
	public int getItemnum(){
		return this.itemnum;
	}
	
	public void setItemnum(int v){
		this.itemnum=v;
	}
	
	/**
	 * 
	 */
	public int convert  = 0  ;
	
	public int getConvert(){
		return this.convert;
	}
	
	public void setConvert(int v){
		this.convert=v;
	}
	
	/**
	 * 
	 */
	public int item  = 0  ;
	
	public int getItem(){
		return this.item;
	}
	
	public void setItem(int v){
		this.item=v;
	}
	
	/**
	 * 
	 */
	public int num  = 0  ;
	
	public int getNum(){
		return this.num;
	}
	
	public void setNum(int v){
		this.num=v;
	}
	
	
};