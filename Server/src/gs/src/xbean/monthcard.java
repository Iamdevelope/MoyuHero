
package xbean;

public interface monthcard extends xdb.Bean {
	public monthcard copy(); // deep clone
	public monthcard toData(); // a Data instance
	public monthcard toBean(); // a Bean instance
	public monthcard toDataIf(); // a Data instance If need. else return this
	public monthcard toBeanIf(); // a Bean instance If need. else return this

	public int getMonthcardid(); // 月卡id
	public long getOvertime(); // 到期时间
	public long getGetboxlasttime(); // 领取奖励最后一次时间

	public void setMonthcardid(int _v_); // 月卡id
	public void setOvertime(long _v_); // 到期时间
	public void setGetboxlasttime(long _v_); // 领取奖励最后一次时间
}
