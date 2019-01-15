
package xbean;

public interface stagetxall extends xdb.Bean {
	public stagetxall copy(); // deep clone
	public stagetxall toData(); // a Data instance
	public stagetxall toBean(); // a Bean instance
	public stagetxall toDataIf(); // a Data instance If need. else return this
	public stagetxall toBeanIf(); // a Bean instance If need. else return this

	public long getTxtime(); // 探险每日刷新时间
	public java.util.Map<Integer, xbean.teamtanxian> getTeamallmap(); // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
	public java.util.Map<Integer, xbean.teamtanxian> getTeamallmapAsData(); // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
	public java.util.Map<Integer, xbean.stagetanxian> getStagetxallmap(); // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表
	public java.util.Map<Integer, xbean.stagetanxian> getStagetxallmapAsData(); // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表

	public void setTxtime(long _v_); // 探险每日刷新时间
}
