
package xbean;

public interface StageBattleInfo extends xdb.Bean {
	public StageBattleInfo copy(); // deep clone
	public StageBattleInfo toData(); // a Data instance
	public StageBattleInfo toBean(); // a Bean instance
	public StageBattleInfo toDataIf(); // a Data instance If need. else return this
	public StageBattleInfo toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // 
	public int getMaxstar(); // 
	public int getFightnum(); // 
	public long getLastfighttime(); // 
	public int getAllfightnum(); // 
	public int getBuybattlenum(); // 购买关卡次数
	public long getBuybattlelasttime(); // 最后购买关卡次数时间
	public int getResetnum(); // 已重置次数
	public int getSweepnum(); // 已扫荡次数

	public void setId(int _v_); // 
	public void setMaxstar(int _v_); // 
	public void setFightnum(int _v_); // 
	public void setLastfighttime(long _v_); // 
	public void setAllfightnum(int _v_); // 
	public void setBuybattlenum(int _v_); // 购买关卡次数
	public void setBuybattlelasttime(long _v_); // 最后购买关卡次数时间
	public void setResetnum(int _v_); // 已重置次数
	public void setSweepnum(int _v_); // 已扫荡次数
}
