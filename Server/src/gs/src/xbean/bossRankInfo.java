
package xbean;

public interface bossRankInfo extends xdb.Bean {
	public bossRankInfo copy(); // deep clone
	public bossRankInfo toData(); // a Data instance
	public bossRankInfo toBean(); // a Bean instance
	public bossRankInfo toDataIf(); // a Data instance If need. else return this
	public bossRankInfo toBeanIf(); // a Bean instance If need. else return this

	public long getRoleid(); // 玩家guid
	public String getRolename(); // 玩家名称
	public com.goldhuman.Common.Octets getRolenameOctets(); // 玩家名称
	public long getNum(); // 伤害
	public int getRankid(); // 名次

	public void setRoleid(long _v_); // 玩家guid
	public void setRolename(String _v_); // 玩家名称
	public void setRolenameOctets(com.goldhuman.Common.Octets _v_); // 玩家名称
	public void setNum(long _v_); // 伤害
	public void setRankid(int _v_); // 名次
}
