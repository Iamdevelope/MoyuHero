
package xbean;

public interface FirstFeedActivity extends xdb.Bean {
	public FirstFeedActivity copy(); // deep clone
	public FirstFeedActivity toData(); // a Data instance
	public FirstFeedActivity toBean(); // a Bean instance
	public FirstFeedActivity toDataIf(); // a Data instance If need. else return this
	public FirstFeedActivity toBeanIf(); // a Bean instance If need. else return this

	public long getChargetime(); // 首次充值时间
	public long getRebatetime(); // 领取时间
	public boolean getIsgainaward(); // 是否已经参与过

	public void setChargetime(long _v_); // 首次充值时间
	public void setRebatetime(long _v_); // 领取时间
	public void setIsgainaward(boolean _v_); // 是否已经参与过
}
