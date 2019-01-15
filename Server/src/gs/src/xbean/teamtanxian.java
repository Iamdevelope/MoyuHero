
package xbean;

public interface teamtanxian extends xdb.Bean {
	public teamtanxian copy(); // deep clone
	public teamtanxian toData(); // a Data instance
	public teamtanxian toBean(); // a Bean instance
	public teamtanxian toDataIf(); // a Data instance If need. else return this
	public teamtanxian toBeanIf(); // a Bean instance If need. else return this

	public int getTanxianid(); // 探险id
	public java.util.List<Integer> getTeam(); // 小队英雄key列表
	public java.util.List<Integer> getTeamAsData(); // 小队英雄key列表

	public void setTanxianid(int _v_); // 探险id
}
