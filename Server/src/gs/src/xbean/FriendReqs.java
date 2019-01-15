
package xbean;

public interface FriendReqs extends xdb.Bean {
	public FriendReqs copy(); // deep clone
	public FriendReqs toData(); // a Data instance
	public FriendReqs toBean(); // a Bean instance
	public FriendReqs toDataIf(); // a Data instance If need. else return this
	public FriendReqs toBeanIf(); // a Bean instance If need. else return this

	public java.util.Set<Long> getByme(); // 我邀请的人
	public java.util.Set<Long> getBymeAsData(); // 我邀请的人
	public java.util.Set<Long> getImby(); // 邀请我的人
	public java.util.Set<Long> getImbyAsData(); // 邀请我的人

}
