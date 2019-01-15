
package xbean;

public interface bossrole extends xdb.Bean {
	public bossrole copy(); // deep clone
	public bossrole toData(); // a Data instance
	public bossrole toBean(); // a Bean instance
	public bossrole toDataIf(); // a Data instance If need. else return this
	public bossrole toBeanIf(); // a Bean instance If need. else return this

	public long getKillhpall(); // 击杀总血量
	public int getKillboss(); // 攻击boss类型，值为1234，代表4个boss
	public long getBossnowhp(); // 本次攻击前boss血量
	public long getTime(); // 上次攻击时间
	public int getZhufunum(); // 祝福次数
	public int getShouwangzl(); // 守望之灵
	public int getChuanshuozs(); // 传说之石
	public xbean.bossshop getBshop(); // 猎人集市

	public void setKillhpall(long _v_); // 击杀总血量
	public void setKillboss(int _v_); // 攻击boss类型，值为1234，代表4个boss
	public void setBossnowhp(long _v_); // 本次攻击前boss血量
	public void setTime(long _v_); // 上次攻击时间
	public void setZhufunum(int _v_); // 祝福次数
	public void setShouwangzl(int _v_); // 守望之灵
	public void setChuanshuozs(int _v_); // 传说之石
}
