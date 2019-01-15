
package xbean;

public interface EndlessInfo extends xdb.Bean {
	public EndlessInfo copy(); // deep clone
	public EndlessInfo toData(); // a Data instance
	public EndlessInfo toBean(); // a Bean instance
	public EndlessInfo toDataIf(); // a Data instance If need. else return this
	public EndlessInfo toBeanIf(); // a Bean instance If need. else return this

	public int getBattleid(); // 关卡id
	public int getGroupnum(); // 第几轮
	public java.util.Map<Integer, Integer> getUseherokeylist(); // 使用英雄id和位置（key为位置，value为herokey）
	public java.util.Map<Integer, Integer> getUseherokeylistAsData(); // 使用英雄id和位置（key为位置，value为herokey）
	public java.util.List<Integer> getMonstergroup(); // 怪物组
	public java.util.List<Integer> getMonstergroupAsData(); // 怪物组
	public int getTrooptype(); // 战队类型
	public int getMonstertrooptype(); // 怪物战队类型
	public int getPact(); // 今日战斗强者之约（没有则为-1）
	public int getDropnum(); // 剩余勇者证明数量
	public int getAlldropnum(); // 勇者证明总数量
	public int getAdd1(); // 属性1购买次数
	public int getAdd2(); // 属性2购买次数
	public int getAdd3(); // 属性3购买次数
	public int getAdd4(); // 属性4购买次数（仅计数）
	public java.util.Map<Integer, Integer> getHerobloodlist(); // 使用英雄的血量（key为位置，value为血量）
	public java.util.Map<Integer, Integer> getHerobloodlistAsData(); // 使用英雄的血量（key为位置，value为血量）
	public int getIsend(); // 0未开始，1进行中，2结束
	public long getTime(); // 此记录时间
	public int getIshalfcostpact(); // 上次购买的强者之约是否达成（0是达成，1是未达成）
	public long getEndtime(); // 结束时间
	public int getExpectedrank(); // 预期排名
	public java.util.Map<Integer, xbean.OtherHero> getHeroattribute(); // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
	public java.util.Map<Integer, xbean.OtherHero> getHeroattributeAsData(); // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
	public int getOnranknum(); // 连续在榜次数
	public long getOnranklasttime(); // 最后在榜时间
	public int getIsnotfirst(); // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）

	public void setBattleid(int _v_); // 关卡id
	public void setGroupnum(int _v_); // 第几轮
	public void setTrooptype(int _v_); // 战队类型
	public void setMonstertrooptype(int _v_); // 怪物战队类型
	public void setPact(int _v_); // 今日战斗强者之约（没有则为-1）
	public void setDropnum(int _v_); // 剩余勇者证明数量
	public void setAlldropnum(int _v_); // 勇者证明总数量
	public void setAdd1(int _v_); // 属性1购买次数
	public void setAdd2(int _v_); // 属性2购买次数
	public void setAdd3(int _v_); // 属性3购买次数
	public void setAdd4(int _v_); // 属性4购买次数（仅计数）
	public void setIsend(int _v_); // 0未开始，1进行中，2结束
	public void setTime(long _v_); // 此记录时间
	public void setIshalfcostpact(int _v_); // 上次购买的强者之约是否达成（0是达成，1是未达成）
	public void setEndtime(long _v_); // 结束时间
	public void setExpectedrank(int _v_); // 预期排名
	public void setOnranknum(int _v_); // 连续在榜次数
	public void setOnranklasttime(long _v_); // 最后在榜时间
	public void setIsnotfirst(int _v_); // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）
}
