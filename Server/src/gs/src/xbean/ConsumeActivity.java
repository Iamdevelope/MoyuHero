
package xbean;

public interface ConsumeActivity extends xdb.Bean {
	public ConsumeActivity copy(); // deep clone
	public ConsumeActivity toData(); // a Data instance
	public ConsumeActivity toBean(); // a Bean instance
	public ConsumeActivity toDataIf(); // a Data instance If need. else return this
	public ConsumeActivity toBeanIf(); // a Bean instance If need. else return this

	public int getActivityid(); // 在该id活动中
	public int getTotalconsume(); // 消费的总数
	public java.util.Map<Integer, Boolean> getIsgainaward(); // 是否已经领取奖励 key=元宝数量
	public java.util.Map<Integer, Boolean> getIsgainawardAsData(); // 是否已经领取奖励 key=元宝数量

	public void setActivityid(int _v_); // 在该id活动中
	public void setTotalconsume(int _v_); // 消费的总数
}
