
package xbean;

public interface GameLevel extends xdb.Bean {
	public GameLevel copy(); // deep clone
	public GameLevel toData(); // a Data instance
	public GameLevel toBean(); // a Bean instance
	public GameLevel toDataIf(); // a Data instance If need. else return this
	public GameLevel toBeanIf(); // a Bean instance If need. else return this

	public int getBattleid(); // 副本ID
	public java.util.Map<Integer, Integer> getUseherokeylist(); // 关卡用到的英雄
	public java.util.Map<Integer, Integer> getUseherokeylistAsData(); // 关卡用到的英雄
	public int getDropgold(); // 掉落金币
	public int getDropcrystal(); // 掉落宝石
	public java.util.List<Integer> getEquipidlist(); // 掉落物品列表
	public java.util.List<Integer> getEquipidlistAsData(); // 掉落物品列表
	public int getTrooptype(); // 战队类型

	public void setBattleid(int _v_); // 副本ID
	public void setDropgold(int _v_); // 掉落金币
	public void setDropcrystal(int _v_); // 掉落宝石
	public void setTrooptype(int _v_); // 战队类型
}
