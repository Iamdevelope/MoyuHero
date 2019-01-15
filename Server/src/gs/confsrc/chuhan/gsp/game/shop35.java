package chuhan.gsp.game;


public class shop35 implements mytools.ConvMain.Checkable ,Comparable<shop35>{

	public int compareTo(shop35 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public shop35(){
		super();
	}
	public shop35(shop35 arg){
		this.id=arg.id ;
		this.commodityName=arg.commodityName ;
		this.commodityDes=arg.commodityDes ;
		this.tabID=arg.tabID ;
		this.sorting=arg.sorting ;
		this.type=arg.type ;
		this.para=arg.para ;
		this.dailyMaxBuy=arg.dailyMaxBuy ;
		this.shelveMaxBuy=arg.shelveMaxBuy ;
		this.costType=arg.costType ;
		this.cost=arg.cost ;
		this.onShelve=arg.onShelve ;
		this.offShelve=arg.offShelve ;
		this.discountCost=arg.discountCost ;
		this.discountOn=arg.discountOn ;
		this.discountOff=arg.discountOff ;
		this.resourceName=arg.resourceName ;
		this.vipLimit=arg.vipLimit ;
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
	public String commodityName  = null  ;
	
	public String getCommodityName(){
		return this.commodityName;
	}
	
	public void setCommodityName(String v){
		this.commodityName=v;
	}
	
	/**
	 * 
	 */
	public String commodityDes  = null  ;
	
	public String getCommodityDes(){
		return this.commodityDes;
	}
	
	public void setCommodityDes(String v){
		this.commodityDes=v;
	}
	
	/**
	 * 
	 */
	public int tabID  = 0  ;
	
	public int getTabID(){
		return this.tabID;
	}
	
	public void setTabID(int v){
		this.tabID=v;
	}
	
	/**
	 * 
	 */
	public int sorting  = 0  ;
	
	public int getSorting(){
		return this.sorting;
	}
	
	public void setSorting(int v){
		this.sorting=v;
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
	public String para  = null  ;
	
	public String getPara(){
		return this.para;
	}
	
	public void setPara(String v){
		this.para=v;
	}
	
	/**
	 * 
	 */
	public int dailyMaxBuy  = 0  ;
	
	public int getDailyMaxBuy(){
		return this.dailyMaxBuy;
	}
	
	public void setDailyMaxBuy(int v){
		this.dailyMaxBuy=v;
	}
	
	/**
	 * 
	 */
	public int shelveMaxBuy  = 0  ;
	
	public int getShelveMaxBuy(){
		return this.shelveMaxBuy;
	}
	
	public void setShelveMaxBuy(int v){
		this.shelveMaxBuy=v;
	}
	
	/**
	 * 
	 */
	public int costType  = 0  ;
	
	public int getCostType(){
		return this.costType;
	}
	
	public void setCostType(int v){
		this.costType=v;
	}
	
	/**
	 * 
	 */
	public String cost  = null  ;
	
	public String getCost(){
		return this.cost;
	}
	
	public void setCost(String v){
		this.cost=v;
	}
	
	/**
	 * 
	 */
	public String onShelve  = null  ;
	
	public String getOnShelve(){
		return this.onShelve;
	}
	
	public void setOnShelve(String v){
		this.onShelve=v;
	}
	
	/**
	 * 
	 */
	public String offShelve  = null  ;
	
	public String getOffShelve(){
		return this.offShelve;
	}
	
	public void setOffShelve(String v){
		this.offShelve=v;
	}
	
	/**
	 * 
	 */
	public String discountCost  = null  ;
	
	public String getDiscountCost(){
		return this.discountCost;
	}
	
	public void setDiscountCost(String v){
		this.discountCost=v;
	}
	
	/**
	 * 
	 */
	public String discountOn  = null  ;
	
	public String getDiscountOn(){
		return this.discountOn;
	}
	
	public void setDiscountOn(String v){
		this.discountOn=v;
	}
	
	/**
	 * 
	 */
	public String discountOff  = null  ;
	
	public String getDiscountOff(){
		return this.discountOff;
	}
	
	public void setDiscountOff(String v){
		this.discountOff=v;
	}
	
	/**
	 * 
	 */
	public String resourceName  = null  ;
	
	public String getResourceName(){
		return this.resourceName;
	}
	
	public void setResourceName(String v){
		this.resourceName=v;
	}
	
	/**
	 * 
	 */
	public int vipLimit  = 0  ;
	
	public int getVipLimit(){
		return this.vipLimit;
	}
	
	public void setVipLimit(int v){
		this.vipLimit=v;
	}
	
	
};