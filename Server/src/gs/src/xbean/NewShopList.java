
package xbean;

public interface NewShopList extends xdb.Bean {
	public NewShopList copy(); // deep clone
	public NewShopList toData(); // a Data instance
	public NewShopList toBean(); // a Bean instance
	public NewShopList toDataIf(); // a Data instance If need. else return this
	public NewShopList toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.NewShop> getShoplist(); // 单个商城列表
	public java.util.List<xbean.NewShop> getShoplistAsData(); // 单个商城列表
	public long getLasttime(); // 正常刷新时间
	public long getRefreshtime(); // 手动刷新时间
	public int getRefreshnum(); // 手动刷新次数

	public void setLasttime(long _v_); // 正常刷新时间
	public void setRefreshtime(long _v_); // 手动刷新时间
	public void setRefreshnum(int _v_); // 手动刷新次数
}
