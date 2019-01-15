package chuhan.gsp.game;


public class mysteriousshop43 implements mytools.ConvMain.Checkable ,Comparable<mysteriousshop43>{

	public int compareTo(mysteriousshop43 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public mysteriousshop43(){
		super();
	}
	public mysteriousshop43(mysteriousshop43 arg){
		this.id=arg.id ;
		this.shopID=arg.shopID ;
		this.onShelve=arg.onShelve ;
		this.offShelve=arg.offShelve ;
		this.costType=arg.costType ;
		this.commoditytype=arg.commoditytype ;
		this.sellweight=arg.sellweight ;
		this.commodityid=arg.commodityid ;
		this.commoditynum=arg.commoditynum ;
		this.mincost=arg.mincost ;
		this.maxcost=arg.maxcost ;
		this.unitcost=arg.unitcost ;
		this.commodityName=arg.commodityName ;
		this.commodityDes=arg.commodityDes ;
		this.commodityresource=arg.commodityresource ;
		this.sorting=arg.sorting ;
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
	public int shopID  = 0  ;
	
	public int getShopID(){
		return this.shopID;
	}
	
	public void setShopID(int v){
		this.shopID=v;
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
	public int commoditytype  = 0  ;
	
	public int getCommoditytype(){
		return this.commoditytype;
	}
	
	public void setCommoditytype(int v){
		this.commoditytype=v;
	}
	
	/**
	 * 
	 */
	public int sellweight  = 0  ;
	
	public int getSellweight(){
		return this.sellweight;
	}
	
	public void setSellweight(int v){
		this.sellweight=v;
	}
	
	/**
	 * 
	 */
	public int commodityid  = 0  ;
	
	public int getCommodityid(){
		return this.commodityid;
	}
	
	public void setCommodityid(int v){
		this.commodityid=v;
	}
	
	/**
	 * 
	 */
	public int commoditynum  = 0  ;
	
	public int getCommoditynum(){
		return this.commoditynum;
	}
	
	public void setCommoditynum(int v){
		this.commoditynum=v;
	}
	
	/**
	 * 
	 */
	public int mincost  = 0  ;
	
	public int getMincost(){
		return this.mincost;
	}
	
	public void setMincost(int v){
		this.mincost=v;
	}
	
	/**
	 * 
	 */
	public int maxcost  = 0  ;
	
	public int getMaxcost(){
		return this.maxcost;
	}
	
	public void setMaxcost(int v){
		this.maxcost=v;
	}
	
	/**
	 * 
	 */
	public int unitcost  = 0  ;
	
	public int getUnitcost(){
		return this.unitcost;
	}
	
	public void setUnitcost(int v){
		this.unitcost=v;
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
	public String commodityresource  = null  ;
	
	public String getCommodityresource(){
		return this.commodityresource;
	}
	
	public void setCommodityresource(String v){
		this.commodityresource=v;
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
	
	
};