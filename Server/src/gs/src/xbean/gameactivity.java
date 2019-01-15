
package xbean;

public interface gameactivity extends xdb.Bean {
	public gameactivity copy(); // deep clone
	public gameactivity toData(); // a Data instance
	public gameactivity toBean(); // a Bean instance
	public gameactivity toDataIf(); // a Data instance If need. else return this
	public gameactivity toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // 活动id
	public long getTime(); // 最近一次时间
	public int getTodaynum(); // 今日次数
	public int getAllnum(); // 累计次数
	public int getCangetnum(); // 可以领取次数（）
	public int getActivitynum(); // 活动计数
	public int getAllactivitynum(); // 累计计数
	public int getIssee(); // 是否看过（提示用，0未看，1已看）

	public void setId(int _v_); // 活动id
	public void setTime(long _v_); // 最近一次时间
	public void setTodaynum(int _v_); // 今日次数
	public void setAllnum(int _v_); // 累计次数
	public void setCangetnum(int _v_); // 可以领取次数（）
	public void setActivitynum(int _v_); // 活动计数
	public void setAllactivitynum(int _v_); // 累计计数
	public void setIssee(int _v_); // 是否看过（提示用，0未看，1已看）
}
