
package xbean;

public interface ChargeActivity extends xdb.Bean {
	public ChargeActivity copy(); // deep clone
	public ChargeActivity toData(); // a Data instance
	public ChargeActivity toBean(); // a Bean instance
	public ChargeActivity toDataIf(); // a Data instance If need. else return this
	public ChargeActivity toBeanIf(); // a Bean instance If need. else return this

	public int getActivityid(); // 在该id活动中
	public int getTotalcharge(); // 充值的总数
	public java.util.Map<Integer, Boolean> getIsgainaward(); // 是否已经领取奖励 key=元宝数量
	public java.util.Map<Integer, Boolean> getIsgainawardAsData(); // 是否已经领取奖励 key=元宝数量

	public void setActivityid(int _v_); // 在该id活动中
	public void setTotalcharge(int _v_); // 充值的总数
}
