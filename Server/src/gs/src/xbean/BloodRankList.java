
package xbean;

public interface BloodRankList extends xdb.Bean {
	public BloodRankList copy(); // deep clone
	public BloodRankList toData(); // a Data instance
	public BloodRankList toBean(); // a Bean instance
	public BloodRankList toDataIf(); // a Data instance If need. else return this
	public BloodRankList toBeanIf(); // a Bean instance If need. else return this

	public int getCurweek(); // 
	public java.util.List<xbean.BloodRankRole> getRankers(); // 以前已加成的效果
	public java.util.List<xbean.BloodRankRole> getRankersAsData(); // 以前已加成的效果

	public void setCurweek(int _v_); // 
}
