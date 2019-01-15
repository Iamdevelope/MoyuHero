
package xbean;

public interface StageInfo extends xdb.Bean {
	public StageInfo copy(); // deep clone
	public StageInfo toData(); // a Data instance
	public StageInfo toBean(); // a Bean instance
	public StageInfo toDataIf(); // a Data instance If need. else return this
	public StageInfo toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // 
	public int getRewardgot(); // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
	public java.util.Map<Integer, xbean.StageBattleInfo> getStagebattles(); // 
	public java.util.Map<Integer, xbean.StageBattleInfo> getStagebattlesAsData(); // 

	public void setId(int _v_); // 
	public void setRewardgot(int _v_); // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
}
