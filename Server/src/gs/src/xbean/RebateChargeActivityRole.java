
package xbean;

public interface RebateChargeActivityRole extends xdb.Bean {
	public RebateChargeActivityRole copy(); // deep clone
	public RebateChargeActivityRole toData(); // a Data instance
	public RebateChargeActivityRole toBean(); // a Bean instance
	public RebateChargeActivityRole toDataIf(); // a Data instance If need. else return this
	public RebateChargeActivityRole toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.RebateChargeActivity> getActivities(); // key=活动id
	public java.util.Map<Integer, xbean.RebateChargeActivity> getActivitiesAsData(); // key=活动id

}
