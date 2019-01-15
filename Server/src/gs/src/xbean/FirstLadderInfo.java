
package xbean;

public interface FirstLadderInfo extends xdb.Bean {
	public FirstLadderInfo copy(); // deep clone
	public FirstLadderInfo toData(); // a Data instance
	public FirstLadderInfo toBean(); // a Bean instance
	public FirstLadderInfo toDataIf(); // a Data instance If need. else return this
	public FirstLadderInfo toBeanIf(); // a Bean instance If need. else return this

	public long getStarttime(); // 上一次登上天梯第一名的时间
	public int getZaiweimilsec(); // 本周在天梯第一名的总时间 单位：毫秒

	public void setStarttime(long _v_); // 上一次登上天梯第一名的时间
	public void setZaiweimilsec(int _v_); // 本周在天梯第一名的总时间 单位：毫秒
}
