
package xbean;

public interface FriendInfo extends xdb.Bean {
	public FriendInfo copy(); // deep clone
	public FriendInfo toData(); // a Data instance
	public FriendInfo toBean(); // a Bean instance
	public FriendInfo toDataIf(); // a Data instance If need. else return this
	public FriendInfo toBeanIf(); // a Bean instance If need. else return this

	public int getTotilinum(); // 今日赠送给他体力次数
	public int getGivetilinum(); // 今日给我体力次数
	public long getLastdaychangetime(); // 上次数据变动时间，为跨天清除用

	public void setTotilinum(int _v_); // 今日赠送给他体力次数
	public void setGivetilinum(int _v_); // 今日给我体力次数
	public void setLastdaychangetime(long _v_); // 上次数据变动时间，为跨天清除用
}
