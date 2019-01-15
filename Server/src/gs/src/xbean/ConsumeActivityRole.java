
package xbean;

public interface ConsumeActivityRole extends xdb.Bean {
	public ConsumeActivityRole copy(); // deep clone
	public ConsumeActivityRole toData(); // a Data instance
	public ConsumeActivityRole toBean(); // a Bean instance
	public ConsumeActivityRole toDataIf(); // a Data instance If need. else return this
	public ConsumeActivityRole toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.ConsumeActivity> getActivities(); // key=活动id
	public java.util.Map<Integer, xbean.ConsumeActivity> getActivitiesAsData(); // key=活动id

}
