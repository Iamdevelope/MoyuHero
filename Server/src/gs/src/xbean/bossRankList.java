
package xbean;

public interface bossRankList extends xdb.Bean {
	public bossRankList copy(); // deep clone
	public bossRankList toData(); // a Data instance
	public bossRankList toBean(); // a Bean instance
	public bossRankList toDataIf(); // a Data instance If need. else return this
	public bossRankList toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.bossRankInfo> getRanklist(); // 排名列表
	public java.util.List<xbean.bossRankInfo> getRanklistAsData(); // 排名列表
	public long getRanktime(); // 排名时间
	public int getBossid(); // bossid：1234

	public void setRanktime(long _v_); // 排名时间
	public void setBossid(int _v_); // bossid：1234
}
