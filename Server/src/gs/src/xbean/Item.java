
package xbean;

public interface Item extends xdb.Bean {
	public Item copy(); // deep clone
	public Item toData(); // a Data instance
	public Item toBean(); // a Bean instance
	public Item toDataIf(); // a Data instance If need. else return this
	public Item toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // 物品编号
	public int getFlags(); // 标志，叠加的时候，flags 也 OR 叠加
	public int getPosition(); // 包裹属性，位置。从0开始编号
	public int getNumber(); // 数量
	public java.util.Map<Integer, Integer> getNumbermap(); // 数量
	public java.util.Map<Integer, Integer> getNumbermapAsData(); // 数量
	public long getUniqueid(); // 物品的唯一id

	public void setId(int _v_); // 物品编号
	public void setFlags(int _v_); // 标志，叠加的时候，flags 也 OR 叠加
	public void setPosition(int _v_); // 包裹属性，位置。从0开始编号
	public void setNumber(int _v_); // 数量
	public void setUniqueid(long _v_); // 物品的唯一id
}
