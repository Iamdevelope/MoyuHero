package chuhan.gsp.item;


public class ms73 implements mytools.ConvMain.Checkable ,Comparable<ms73>{

	public int compareTo(ms73 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public ms73(){
		super();
	}
	public ms73(ms73 arg){
		this.id=arg.id ;
		this.mstype=arg.mstype ;
		this.msname=arg.msname ;
		this.ddes=arg.ddes ;
		this.icon=arg.icon ;
		this.stardemand=arg.stardemand ;
		this.stagedemand=arg.stagedemand ;
		this.level=arg.level ;
		this.value=arg.value ;
		this.consumexpevalue=arg.consumexpevalue ;
		this.consumzyid=arg.consumzyid ;
		this.consumnb=arg.consumnb ;
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
	public int mstype  = 0  ;
	
	public int getMstype(){
		return this.mstype;
	}
	
	public void setMstype(int v){
		this.mstype=v;
	}
	
	/**
	 * 
	 */
	public String msname  = null  ;
	
	public String getMsname(){
		return this.msname;
	}
	
	public void setMsname(String v){
		this.msname=v;
	}
	
	/**
	 * 
	 */
	public String ddes  = null  ;
	
	public String getDdes(){
		return this.ddes;
	}
	
	public void setDdes(String v){
		this.ddes=v;
	}
	
	/**
	 * 
	 */
	public String icon  = null  ;
	
	public String getIcon(){
		return this.icon;
	}
	
	public void setIcon(String v){
		this.icon=v;
	}
	
	/**
	 * 
	 */
	public int stardemand  = 0  ;
	
	public int getStardemand(){
		return this.stardemand;
	}
	
	public void setStardemand(int v){
		this.stardemand=v;
	}
	
	/**
	 * 
	 */
	public int stagedemand  = 0  ;
	
	public int getStagedemand(){
		return this.stagedemand;
	}
	
	public void setStagedemand(int v){
		this.stagedemand=v;
	}
	
	/**
	 * 
	 */
	public String level  = null  ;
	
	public String getLevel(){
		return this.level;
	}
	
	public void setLevel(String v){
		this.level=v;
	}
	
	/**
	 * 
	 */
	public String value  = null  ;
	
	public String getValue(){
		return this.value;
	}
	
	public void setValue(String v){
		this.value=v;
	}
	
	/**
	 * 
	 */
	public String consumexpevalue  = null  ;
	
	public String getConsumexpevalue(){
		return this.consumexpevalue;
	}
	
	public void setConsumexpevalue(String v){
		this.consumexpevalue=v;
	}
	
	/**
	 * 
	 */
	public String consumzyid  = null  ;
	
	public String getConsumzyid(){
		return this.consumzyid;
	}
	
	public void setConsumzyid(String v){
		this.consumzyid=v;
	}
	
	/**
	 * 
	 */
	public String consumnb  = null  ;
	
	public String getConsumnb(){
		return this.consumnb;
	}
	
	public void setConsumnb(String v){
		this.consumnb=v;
	}
	
	
};