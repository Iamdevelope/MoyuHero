package chuhan.gsp.item;


public class strade implements mytools.ConvMain.Checkable ,Comparable<strade>{

	public int compareTo(strade o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public strade(){
		super();
	}
	public strade(strade arg){
		this.id=arg.id ;
		this.sequenceid=arg.sequenceid ;
		this.zoneid=arg.zoneid ;
		this.starttime=arg.starttime ;
		this.endtime=arg.endtime ;
		this.times=arg.times ;
		this.viplevel=arg.viplevel ;
		this.yinliang=arg.yinliang ;
		this.yuanbao=arg.yuanbao ;
		this.itemlist=arg.itemlist ;
		this.itemrank=arg.itemrank ;
		this.itemnum=arg.itemnum ;
		this.convert=arg.convert ;
		this.reward=arg.reward ;
		this.post=arg.post ;
		this.postname=arg.postname ;
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
	public int sequenceid  = 0  ;
	
	public int getSequenceid(){
		return this.sequenceid;
	}
	
	public void setSequenceid(int v){
		this.sequenceid=v;
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
	public int reward  = 0  ;
	
	public int getReward(){
		return this.reward;
	}
	
	public void setReward(int v){
		this.reward=v;
	}
	
	/**
	 * 
	 */
	public int post  = 0  ;
	
	public int getPost(){
		return this.post;
	}
	
	public void setPost(int v){
		this.post=v;
	}
	
	/**
	 * 
	 */
	public String postname  = null  ;
	
	public String getPostname(){
		return this.postname;
	}
	
	public void setPostname(String v){
		this.postname=v;
	}
	
	
};