
package xbean;

public interface FirstFeedActivityRole extends xdb.Bean {
	public FirstFeedActivityRole copy(); // deep clone
	public FirstFeedActivityRole toData(); // a Data instance
	public FirstFeedActivityRole toBean(); // a Bean instance
	public FirstFeedActivityRole toDataIf(); // a Data instance If need. else return this
	public FirstFeedActivityRole toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.FirstFeedActivity> getActivities(); // key=活动id
	public java.util.Map<Integer, xbean.FirstFeedActivity> getActivitiesAsData(); // key=活动id

}
