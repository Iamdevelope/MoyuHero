
package xbean;

public interface ServerInfo extends xdb.Bean {
	public ServerInfo copy(); // deep clone
	public ServerInfo toData(); // a Data instance
	public ServerInfo toBean(); // a Bean instance
	public ServerInfo toDataIf(); // a Data instance If need. else return this
	public ServerInfo toBeanIf(); // a Bean instance If need. else return this

	public long getFirsttime(); // 第一次起服时间
	public long getStarttime(); // 本次起服时间

	public void setFirsttime(long _v_); // 第一次起服时间
	public void setStarttime(long _v_); // 本次起服时间
}
