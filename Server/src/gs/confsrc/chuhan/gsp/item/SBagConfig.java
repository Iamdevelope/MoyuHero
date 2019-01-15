package chuhan.gsp.item;


public class SBagConfig implements mytools.ConvMain.Checkable ,Comparable<SBagConfig>{

	public int compareTo(SBagConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SBagConfig(){
		super();
	}
	public SBagConfig(SBagConfig arg){
		this.id=arg.id ;
		this.initcap=arg.initcap ;
		this.tablename=arg.tablename ;
		this.canpile=arg.canpile ;
		this.sendlogin=arg.sendlogin ;
		this.maxmoney=arg.maxmoney ;
	}
	public void checkValid(java.util.Map<String,java.util.Map<Integer,? extends Object> > objs){
			do{
				int tmprefvalue=initcap;
				
				if(tmprefvalue < 0) throw new RuntimeException("SBagConfig.initcap="+tmprefvalue+",所以不满足条件 SBagConfig.initcap < 0");
			}while(false);
			do{
				int tmprefvalue=canpile;
				
				if(tmprefvalue < 0) throw new RuntimeException("SBagConfig.canpile="+tmprefvalue+",所以不满足条件 SBagConfig.canpile < 0");
				if(tmprefvalue > 1) throw new RuntimeException("SBagConfig.canpile="+tmprefvalue+",所以不满足条件 SBagConfig.canpile > 1");
			}while(false);
			do{
				int tmprefvalue=sendlogin;
				
				if(tmprefvalue < 0) throw new RuntimeException("SBagConfig.sendlogin="+tmprefvalue+",所以不满足条件 SBagConfig.sendlogin < 0");
				if(tmprefvalue > 1) throw new RuntimeException("SBagConfig.sendlogin="+tmprefvalue+",所以不满足条件 SBagConfig.sendlogin > 1");
			}while(false);
			do{
				long tmprefvalue=maxmoney;
				
				if(tmprefvalue < 0L) throw new RuntimeException("SBagConfig.maxmoney="+tmprefvalue+",所以不满足条件 SBagConfig.maxmoney < 0L");
			}while(false);
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
	public int initcap  = 0  ;
	
	public int getInitcap(){
		return this.initcap;
	}
	
	public void setInitcap(int v){
		this.initcap=v;
	}
	
	/**
	 * 
	 */
	public String tablename  = null  ;
	
	public String getTablename(){
		return this.tablename;
	}
	
	public void setTablename(String v){
		this.tablename=v;
	}
	
	/**
	 * 
	 */
	public int canpile  = 0  ;
	
	public int getCanpile(){
		return this.canpile;
	}
	
	public void setCanpile(int v){
		this.canpile=v;
	}
	
	/**
	 * 
	 */
	public int sendlogin  = 0  ;
	
	public int getSendlogin(){
		return this.sendlogin;
	}
	
	public void setSendlogin(int v){
		this.sendlogin=v;
	}
	
	/**
	 * 
	 */
	public long maxmoney  = 0L  ;
	
	public long getMaxmoney(){
		return this.maxmoney;
	}
	
	public void setMaxmoney(long v){
		this.maxmoney=v;
	}
	
	
};