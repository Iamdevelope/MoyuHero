
package xbean;

public interface Friends extends xdb.Bean {
	public Friends copy(); // deep clone
	public Friends toData(); // a Data instance
	public Friends toBean(); // a Bean instance
	public Friends toDataIf(); // a Data instance If need. else return this
	public Friends toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Long, xbean.FriendInfo> getMine(); // key=好友roldId
	public java.util.Map<Long, xbean.FriendInfo> getMineAsData(); // key=好友roldId

}
