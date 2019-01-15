
package xbean;

public interface huoyues extends xdb.Bean {
	public huoyues copy(); // deep clone
	public huoyues toData(); // a Data instance
	public huoyues toBean(); // a Bean instance
	public huoyues toDataIf(); // a Data instance If need. else return this
	public huoyues toBeanIf(); // a Bean instance If need. else return this

	public int getHuoyuenum(); // 活跃度
	public int getGetnum(); // 领取记录，个位第一个，十位第二个~~
	public long getHuoyuetime(); // 刷新时间，跨天用
	public java.util.Map<Integer, xbean.huoyue> getHuoyuemap(); // 活跃任务列表，key为选择类型
	public java.util.Map<Integer, xbean.huoyue> getHuoyuemapAsData(); // 活跃任务列表，key为选择类型

	public void setHuoyuenum(int _v_); // 活跃度
	public void setGetnum(int _v_); // 领取记录，个位第一个，十位第二个~~
	public void setHuoyuetime(long _v_); // 刷新时间，跨天用
}
