
package xbean;

public interface EndlessRankInfo extends xdb.Bean {
	public EndlessRankInfo copy(); // deep clone
	public EndlessRankInfo toData(); // a Data instance
	public EndlessRankInfo toBean(); // a Bean instance
	public EndlessRankInfo toDataIf(); // a Data instance If need. else return this
	public EndlessRankInfo toBeanIf(); // a Bean instance If need. else return this

	public long getRoleid(); // 玩家guid
	public String getRolename(); // 玩家名称
	public com.goldhuman.Common.Octets getRolenameOctets(); // 玩家名称
	public int getLevel(); // 玩家等级
	public int getGroupnum(); // 第几轮
	public int getTrooptype(); // 战队类型
	public int getAlldropnum(); // 勇者证明总数量
	public java.util.Map<Integer, xbean.OtherHero> getHeroattribute(); // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
	public java.util.Map<Integer, xbean.OtherHero> getHeroattributeAsData(); // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
	public int getOnranknum(); // 连续在榜次数

	public void setRoleid(long _v_); // 玩家guid
	public void setRolename(String _v_); // 玩家名称
	public void setRolenameOctets(com.goldhuman.Common.Octets _v_); // 玩家名称
	public void setLevel(int _v_); // 玩家等级
	public void setGroupnum(int _v_); // 第几轮
	public void setTrooptype(int _v_); // 战队类型
	public void setAlldropnum(int _v_); // 勇者证明总数量
	public void setOnranknum(int _v_); // 连续在榜次数
}
