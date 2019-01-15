
package xbean;

public interface LadderInfo extends xdb.Bean {
	public LadderInfo copy(); // deep clone
	public LadderInfo toData(); // a Data instance
	public LadderInfo toBean(); // a Bean instance
	public LadderInfo toDataIf(); // a Data instance If need. else return this
	public LadderInfo toBeanIf(); // a Bean instance If need. else return this

	public long getRoleid(); // 

	public void setRoleid(long _v_); // 
}
