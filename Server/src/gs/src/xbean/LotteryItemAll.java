
package xbean;

public interface LotteryItemAll extends xdb.Bean {
	public LotteryItemAll copy(); // deep clone
	public LotteryItemAll toData(); // a Data instance
	public LotteryItemAll toBean(); // a Bean instance
	public LotteryItemAll toDataIf(); // a Data instance If need. else return this
	public LotteryItemAll toBeanIf(); // a Bean instance If need. else return this

	public int getMapkey(); // 第几层
	public int getMapvalue(); // 第几个
	public java.util.List<Integer> getSuperlist(); // 遗迹宝藏特殊list
	public java.util.List<Integer> getSuperlistAsData(); // 遗迹宝藏特殊list
	public long getMonthfirsttime(); // 月卡首刷时间
	public long getFreelotterytime(); // 免费单抽到期时间
	public long getLastrefreshtime(); // 上次刷新时间（每日刷新）
	public java.util.Map<Integer, xbean.LotteryItemlayer> getLotteryitemmap(); // 遗迹宝藏总信息
	public java.util.Map<Integer, xbean.LotteryItemlayer> getLotteryitemmapAsData(); // 遗迹宝藏总信息

	public void setMapkey(int _v_); // 第几层
	public void setMapvalue(int _v_); // 第几个
	public void setMonthfirsttime(long _v_); // 月卡首刷时间
	public void setFreelotterytime(long _v_); // 免费单抽到期时间
	public void setLastrefreshtime(long _v_); // 上次刷新时间（每日刷新）
}
