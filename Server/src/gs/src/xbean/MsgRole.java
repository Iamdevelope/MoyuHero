
package xbean;

public interface MsgRole extends xdb.Bean {
	public MsgRole copy(); // deep clone
	public MsgRole toData(); // a Data instance
	public MsgRole toBean(); // a Bean instance
	public MsgRole toDataIf(); // a Data instance If need. else return this
	public MsgRole toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.SysMsg> getSysmsgs(); // 
	public java.util.List<xbean.SysMsg> getSysmsgsAsData(); // 

}
