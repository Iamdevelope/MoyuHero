
package xbean;

public interface MacInfo extends xdb.Bean {
	public MacInfo copy(); // deep clone
	public MacInfo toData(); // a Data instance
	public MacInfo toBean(); // a Bean instance
	public MacInfo toDataIf(); // a Data instance If need. else return this
	public MacInfo toBeanIf(); // a Bean instance If need. else return this

	public long getOnlinetime(); // 
	public long getOfflinetime(); // 

	public void setOnlinetime(long _v_); // 
	public void setOfflinetime(long _v_); // 
}
