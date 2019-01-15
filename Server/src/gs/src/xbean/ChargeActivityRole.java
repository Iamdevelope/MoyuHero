
package xbean;

public interface ChargeActivityRole extends xdb.Bean {
	public ChargeActivityRole copy(); // deep clone
	public ChargeActivityRole toData(); // a Data instance
	public ChargeActivityRole toBean(); // a Bean instance
	public ChargeActivityRole toDataIf(); // a Data instance If need. else return this
	public ChargeActivityRole toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.ChargeActivity> getActivities(); // key=活动id
	public java.util.Map<Integer, xbean.ChargeActivity> getActivitiesAsData(); // key=活动id

}
