
package xbean;

public interface ShopbuyColumn extends xdb.Bean {
	public ShopbuyColumn copy(); // deep clone
	public ShopbuyColumn toData(); // a Data instance
	public ShopbuyColumn toBean(); // a Bean instance
	public ShopbuyColumn toDataIf(); // a Data instance If need. else return this
	public ShopbuyColumn toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.Shopbuy> getShopbuys(); // 
	public java.util.Map<Integer, xbean.Shopbuy> getShopbuysAsData(); // 

}
