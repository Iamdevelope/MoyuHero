
package xbean;

public interface BillRole extends xdb.Bean {
	public BillRole copy(); // deep clone
	public BillRole toData(); // a Data instance
	public BillRole toBean(); // a Bean instance
	public BillRole toDataIf(); // a Data instance If need. else return this
	public BillRole toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Long, xbean.BillData> getBills(); // 
	public java.util.Map<Long, xbean.BillData> getBillsAsData(); // 
	public int getFirstcharge(); // 是否已首充

	public void setFirstcharge(int _v_); // 是否已首充
}
