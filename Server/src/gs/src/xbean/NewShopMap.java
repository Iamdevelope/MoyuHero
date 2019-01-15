
package xbean;

public interface NewShopMap extends xdb.Bean {
	public NewShopMap copy(); // deep clone
	public NewShopMap toData(); // a Data instance
	public NewShopMap toBean(); // a Bean instance
	public NewShopMap toDataIf(); // a Data instance If need. else return this
	public NewShopMap toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.NewShopList> getShopmap(); // 整个商城map，key为76表的序列号
	public java.util.Map<Integer, xbean.NewShopList> getShopmapAsData(); // 整个商城map，key为76表的序列号

}
