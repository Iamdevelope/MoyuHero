package chuhan.gsp.game;


public class shangdian76 implements mytools.ConvMain.Checkable ,Comparable<shangdian76>{

	public int compareTo(shangdian76 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public shangdian76(){
		super();
	}
	public shangdian76(shangdian76 arg){
		this.id=arg.id ;
		this.StoreName=arg.StoreName ;
		this.StoreIocn=arg.StoreIocn ;
		this.StoreOpen=arg.StoreOpen ;
		this.ConditionalData=arg.ConditionalData ;
		this.ConditionDescription=arg.ConditionDescription ;
		this.WhetherDisplay=arg.WhetherDisplay ;
		this.RefreshTime=arg.RefreshTime ;
		this.CurrencyType=arg.CurrencyType ;
		this.Consumption=arg.Consumption ;
		this.StoreCurrencyType=arg.StoreCurrencyType ;
		this.StoreCurrencyIcon=arg.StoreCurrencyIcon ;
		this.MoneyStoreDescription=arg.MoneyStoreDescription ;
		this.RepeatPurchase=arg.RepeatPurchase ;
		this.Commodity=arg.Commodity ;
		this.Recommend=arg.Recommend ;
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
	public String StoreName  = null  ;
	
	public String getStoreName(){
		return this.StoreName;
	}
	
	public void setStoreName(String v){
		this.StoreName=v;
	}
	
	/**
	 * 
	 */
	public String StoreIocn  = null  ;
	
	public String getStoreIocn(){
		return this.StoreIocn;
	}
	
	public void setStoreIocn(String v){
		this.StoreIocn=v;
	}
	
	/**
	 * 
	 */
	public int StoreOpen  = 0  ;
	
	public int getStoreOpen(){
		return this.StoreOpen;
	}
	
	public void setStoreOpen(int v){
		this.StoreOpen=v;
	}
	
	/**
	 * 
	 */
	public int ConditionalData  = 0  ;
	
	public int getConditionalData(){
		return this.ConditionalData;
	}
	
	public void setConditionalData(int v){
		this.ConditionalData=v;
	}
	
	/**
	 * 
	 */
	public String ConditionDescription  = null  ;
	
	public String getConditionDescription(){
		return this.ConditionDescription;
	}
	
	public void setConditionDescription(String v){
		this.ConditionDescription=v;
	}
	
	/**
	 * 
	 */
	public int WhetherDisplay  = 0  ;
	
	public int getWhetherDisplay(){
		return this.WhetherDisplay;
	}
	
	public void setWhetherDisplay(int v){
		this.WhetherDisplay=v;
	}
	
	/**
	 * 
	 */
	public String RefreshTime  = null  ;
	
	public String getRefreshTime(){
		return this.RefreshTime;
	}
	
	public void setRefreshTime(String v){
		this.RefreshTime=v;
	}
	
	/**
	 * 
	 */
	public int CurrencyType  = 0  ;
	
	public int getCurrencyType(){
		return this.CurrencyType;
	}
	
	public void setCurrencyType(int v){
		this.CurrencyType=v;
	}
	
	/**
	 * 
	 */
	public String Consumption  = null  ;
	
	public String getConsumption(){
		return this.Consumption;
	}
	
	public void setConsumption(String v){
		this.Consumption=v;
	}
	
	/**
	 * 
	 */
	public int StoreCurrencyType  = 0  ;
	
	public int getStoreCurrencyType(){
		return this.StoreCurrencyType;
	}
	
	public void setStoreCurrencyType(int v){
		this.StoreCurrencyType=v;
	}
	
	/**
	 * 
	 */
	public String StoreCurrencyIcon  = null  ;
	
	public String getStoreCurrencyIcon(){
		return this.StoreCurrencyIcon;
	}
	
	public void setStoreCurrencyIcon(String v){
		this.StoreCurrencyIcon=v;
	}
	
	/**
	 * 
	 */
	public String MoneyStoreDescription  = null  ;
	
	public String getMoneyStoreDescription(){
		return this.MoneyStoreDescription;
	}
	
	public void setMoneyStoreDescription(String v){
		this.MoneyStoreDescription=v;
	}
	
	/**
	 * 
	 */
	public int RepeatPurchase  = 0  ;
	
	public int getRepeatPurchase(){
		return this.RepeatPurchase;
	}
	
	public void setRepeatPurchase(int v){
		this.RepeatPurchase=v;
	}
	
	/**
	 * 
	 */
	public String Commodity  = null  ;
	
	public String getCommodity(){
		return this.Commodity;
	}
	
	public void setCommodity(String v){
		this.Commodity=v;
	}
	
	/**
	 * 
	 */
	public String Recommend  = null  ;
	
	public String getRecommend(){
		return this.Recommend;
	}
	
	public void setRecommend(String v){
		this.Recommend=v;
	}
	
	
};