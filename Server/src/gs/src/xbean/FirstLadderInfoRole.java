
package xbean;

public interface FirstLadderInfoRole extends xdb.Bean {
	public FirstLadderInfoRole copy(); // deep clone
	public FirstLadderInfoRole toData(); // a Data instance
	public FirstLadderInfoRole toBean(); // a Bean instance
	public FirstLadderInfoRole toDataIf(); // a Data instance If need. else return this
	public FirstLadderInfoRole toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Long, xbean.FirstLadderInfo> getRoleinfos(); // key=roleId
	public java.util.Map<Long, xbean.FirstLadderInfo> getRoleinfosAsData(); // key=roleId

}
