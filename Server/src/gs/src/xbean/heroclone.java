
package xbean;

public interface heroclone extends xdb.Bean {
	public heroclone copy(); // deep clone
	public heroclone toData(); // a Data instance
	public heroclone toBean(); // a Bean instance
	public heroclone toDataIf(); // a Data instance If need. else return this
	public heroclone toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<Integer> getClonelist(); // 英雄克隆信息列表
	public java.util.List<Integer> getClonelistAsData(); // 英雄克隆信息列表

}
