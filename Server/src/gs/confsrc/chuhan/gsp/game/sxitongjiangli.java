package chuhan.gsp.game;


public class sxitongjiangli implements mytools.ConvMain.Checkable ,Comparable<sxitongjiangli>{

	public int compareTo(sxitongjiangli o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public sxitongjiangli(){
		super();
	}
	public sxitongjiangli(sxitongjiangli arg){
		this.id=arg.id ;
		this.enable=arg.enable ;
		this.starttime=arg.starttime ;
		this.endtime=arg.endtime ;
		this.msgtitle=arg.msgtitle ;
		this.msgid=arg.msgid ;
		this.msgtext=arg.msgtext ;
		this.awardid=arg.awardid ;
		this.roleids=arg.roleids ;
		this.userid=arg.userid ;
		this.zoneid=arg.zoneid ;
		this.plattype=arg.plattype ;
		this.levelmin=arg.levelmin ;
		this.levelmax=arg.levelmax ;
		this.vipmin=arg.vipmin ;
		this.vipmax=arg.vipmax ;
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
	public int enable  = 0  ;
	
	public int getEnable(){
		return this.enable;
	}
	
	public void setEnable(int v){
		this.enable=v;
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
	public String msgtitle  = null  ;
	
	public String getMsgtitle(){
		return this.msgtitle;
	}
	
	public void setMsgtitle(String v){
		this.msgtitle=v;
	}
	
	/**
	 * 
	 */
	public int msgid  = 0  ;
	
	public int getMsgid(){
		return this.msgid;
	}
	
	public void setMsgid(int v){
		this.msgid=v;
	}
	
	/**
	 * 
	 */
	public String msgtext  = null  ;
	
	public String getMsgtext(){
		return this.msgtext;
	}
	
	public void setMsgtext(String v){
		this.msgtext=v;
	}
	
	/**
	 * 
	 */
	public int awardid  = 0  ;
	
	public int getAwardid(){
		return this.awardid;
	}
	
	public void setAwardid(int v){
		this.awardid=v;
	}
	
	/**
	 * 
	 */
	public String roleids  = null  ;
	
	public String getRoleids(){
		return this.roleids;
	}
	
	public void setRoleids(String v){
		this.roleids=v;
	}
	
	/**
	 * 
	 */
	public String userid  = null  ;
	
	public String getUserid(){
		return this.userid;
	}
	
	public void setUserid(String v){
		this.userid=v;
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
	public String plattype  = null  ;
	
	public String getPlattype(){
		return this.plattype;
	}
	
	public void setPlattype(String v){
		this.plattype=v;
	}
	
	/**
	 * 
	 */
	public int levelmin  = 0  ;
	
	public int getLevelmin(){
		return this.levelmin;
	}
	
	public void setLevelmin(int v){
		this.levelmin=v;
	}
	
	/**
	 * 
	 */
	public int levelmax  = 0  ;
	
	public int getLevelmax(){
		return this.levelmax;
	}
	
	public void setLevelmax(int v){
		this.levelmax=v;
	}
	
	/**
	 * 
	 */
	public int vipmin  = 0  ;
	
	public int getVipmin(){
		return this.vipmin;
	}
	
	public void setVipmin(int v){
		this.vipmin=v;
	}
	
	/**
	 * 
	 */
	public int vipmax  = 0  ;
	
	public int getVipmax(){
		return this.vipmax;
	}
	
	public void setVipmax(int v){
		this.vipmax=v;
	}
	
	
};