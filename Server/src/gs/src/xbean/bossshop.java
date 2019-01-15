
package xbean;

public interface bossshop extends xdb.Bean {
	public bossshop copy(); // deep clone
	public bossshop toData(); // a Data instance
	public bossshop toBean(); // a Bean instance
	public bossshop toDataIf(); // a Data instance If need. else return this
	public bossshop toBeanIf(); // a Bean instance If need. else return this

	public long getTime(); // 刷新时间
	public java.util.List<Integer> getShoplist(); // 今天可买的物品表
	public java.util.List<Integer> getShoplistAsData(); // 今天可买的物品表
	public int getHunternum(); // 今日猎人集市累计兑换次数

	public void setTime(long _v_); // 刷新时间
	public void setHunternum(int _v_); // 今日猎人集市累计兑换次数
}
