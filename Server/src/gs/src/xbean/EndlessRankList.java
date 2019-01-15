
package xbean;

public interface EndlessRankList extends xdb.Bean {
	public EndlessRankList copy(); // deep clone
	public EndlessRankList toData(); // a Data instance
	public EndlessRankList toBean(); // a Bean instance
	public EndlessRankList toDataIf(); // a Data instance If need. else return this
	public EndlessRankList toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.EndlessRankInfo> getRanklist(); // 排名列表
	public java.util.List<xbean.EndlessRankInfo> getRanklistAsData(); // 排名列表
	public long getRanktime(); // 排名时间

	public void setRanktime(long _v_); // 排名时间
}
