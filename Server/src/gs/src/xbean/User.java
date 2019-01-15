
package xbean;

public interface User extends xdb.Bean {
	public User copy(); // deep clone
	public User toData(); // a Data instance
	public User toBean(); // a Bean instance
	public User toDataIf(); // a Data instance If need. else return this
	public User toBeanIf(); // a Bean instance If need. else return this

	public String getUsername(); // 帐号名称
	public com.goldhuman.Common.Octets getUsernameOctets(); // 帐号名称
	public java.util.List<Long> getIdlist(); // 用户的角色列表 value是roleid
	public java.util.List<Long> getIdlistAsData(); // 用户的角色列表 value是roleid
	public long getCreatetime(); // 帐号第一次进入游戏的时间

	public void setUsername(String _v_); // 帐号名称
	public void setUsernameOctets(com.goldhuman.Common.Octets _v_); // 帐号名称
	public void setCreatetime(long _v_); // 帐号第一次进入游戏的时间
}
