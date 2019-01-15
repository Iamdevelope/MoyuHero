
package xbean;

public interface BloodRankRole extends xdb.Bean {
	public BloodRankRole copy(); // deep clone
	public BloodRankRole toData(); // a Data instance
	public BloodRankRole toBean(); // a Bean instance
	public BloodRankRole toDataIf(); // a Data instance If need. else return this
	public BloodRankRole toBeanIf(); // a Bean instance If need. else return this

	public long getRoleid(); // 
	public int getMaxlevel(); // 

	public void setRoleid(long _v_); // 
	public void setMaxlevel(int _v_); // 
}
