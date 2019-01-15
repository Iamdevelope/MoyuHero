
package xbean;

public interface LadderRole extends xdb.Bean {
	public LadderRole copy(); // deep clone
	public LadderRole toData(); // a Data instance
	public LadderRole toBean(); // a Bean instance
	public LadderRole toDataIf(); // a Data instance If need. else return this
	public LadderRole toBeanIf(); // a Bean instance If need. else return this

	public int getLadderrank(); // 天梯排名
	public int getLaddersoul(); // 天梯元魂
	public long getLastsoulchangetime(); // 上次天梯元魂变动时间
	public java.util.List<Long> getEnermies(); // 最近的4个仇敌
	public java.util.List<Long> getEnermiesAsData(); // 最近的4个仇敌
	public int getFighttimes(); // 今天战斗次数
	public long getLastfighttime(); // 上次战斗时间

	public void setLadderrank(int _v_); // 天梯排名
	public void setLaddersoul(int _v_); // 天梯元魂
	public void setLastsoulchangetime(long _v_); // 上次天梯元魂变动时间
	public void setFighttimes(int _v_); // 今天战斗次数
	public void setLastfighttime(long _v_); // 上次战斗时间
}
