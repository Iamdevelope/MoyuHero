
package xbean;

public interface AUUserInfo extends xdb.Bean {
	public AUUserInfo copy(); // deep clone
	public AUUserInfo toData(); // a Data instance
	public AUUserInfo toBean(); // a Bean instance
	public AUUserInfo toDataIf(); // a Data instance If need. else return this
	public AUUserInfo toBeanIf(); // a Bean instance If need. else return this

	public int getRetcode(); // 
	public int getLoginip(); // 
	public int getBlisgm(); // 
	public String getNickname(); // 
	public com.goldhuman.Common.Octets getNicknameOctets(); // 
	public String getUsername(); // 
	public com.goldhuman.Common.Octets getUsernameOctets(); // 

	public void setRetcode(int _v_); // 
	public void setLoginip(int _v_); // 
	public void setBlisgm(int _v_); // 
	public void setNickname(String _v_); // 
	public void setNicknameOctets(com.goldhuman.Common.Octets _v_); // 
	public void setUsername(String _v_); // 
	public void setUsernameOctets(com.goldhuman.Common.Octets _v_); // 
}
