package chuhan.gsp.game;


public class SAddCashConfig implements mytools.ConvMain.Checkable ,Comparable<SAddCashConfig>{

	public int compareTo(SAddCashConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SAddCashConfig(){
		super();
	}
	public SAddCashConfig(SAddCashConfig arg){
		this.id=arg.id ;
		this.serverids=arg.serverids ;
		this.platform=arg.platform ;
		this.price=arg.price ;
		this.yuanbao=arg.yuanbao ;
		this.present=arg.present ;
		this.name=arg.name ;
		this.monthcardID=arg.monthcardID ;
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
	public String serverids  = null  ;
	
	public String getServerids(){
		return this.serverids;
	}
	
	public void setServerids(String v){
		this.serverids=v;
	}
	
	/**
	 * 
	 */
	public String platform  = null  ;
	
	public String getPlatform(){
		return this.platform;
	}
	
	public void setPlatform(String v){
		this.platform=v;
	}
	
	/**
	 * 
	 */
	public int price  = 0  ;
	
	public int getPrice(){
		return this.price;
	}
	
	public void setPrice(int v){
		this.price=v;
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
	public int present  = 0  ;
	
	public int getPresent(){
		return this.present;
	}
	
	public void setPresent(int v){
		this.present=v;
	}
	
	/**
	 * 
	 */
	public String name  = null  ;
	
	public String getName(){
		return this.name;
	}
	
	public void setName(String v){
		this.name=v;
	}
	
	/**
	 * 
	 */
	public int monthcardID  = 0  ;
	
	public int getMonthcardID(){
		return this.monthcardID;
	}
	
	public void setMonthcardID(int v){
		this.monthcardID=v;
	}
	
	
};