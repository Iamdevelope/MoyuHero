
package xbean;

public interface Shopbuy extends xdb.Bean {
	public Shopbuy copy(); // deep clone
	public Shopbuy toData(); // a Data instance
	public Shopbuy toBean(); // a Bean instance
	public Shopbuy toDataIf(); // a Data instance If need. else return this
	public Shopbuy toBeanIf(); // a Bean instance If need. else return this

	public int getShopid(); // 商城ID（key）
	public int getTodaynum(); // 今日已购买次数
	public long getLasttime(); // 最后一次购买时间
	public int getBuyallnum(); // 总共购买次数

	public void setShopid(int _v_); // 商城ID（key）
	public void setTodaynum(int _v_); // 今日已购买次数
	public void setLasttime(long _v_); // 最后一次购买时间
	public void setBuyallnum(int _v_); // 总共购买次数
}
