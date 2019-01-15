
package xbean;

public interface StageRole extends xdb.Bean {
	public StageRole copy(); // deep clone
	public StageRole toData(); // a Data instance
	public StageRole toBean(); // a Bean instance
	public StageRole toDataIf(); // a Data instance If need. else return this
	public StageRole toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.StageInfo> getStages(); // 
	public java.util.Map<Integer, xbean.StageInfo> getStagesAsData(); // 

}
