
package xbean;

public interface tanxian extends xdb.Bean {
	public tanxian copy(); // deep clone
	public tanxian toData(); // a Data instance
	public tanxian toBean(); // a Bean instance
	public tanxian toDataIf(); // a Data instance If need. else return this
	public tanxian toBeanIf(); // a Bean instance If need. else return this

	public int getTanxianid(); // 探险id
	public int getTanxiantype(); // 状态，0未开启，1进行中，2已完成
	public long getEndtime(); // 结束时间
	public int getTeamnum(); // 队伍号

	public void setTanxianid(int _v_); // 探险id
	public void setTanxiantype(int _v_); // 状态，0未开启，1进行中，2已完成
	public void setEndtime(long _v_); // 结束时间
	public void setTeamnum(int _v_); // 队伍号
}
