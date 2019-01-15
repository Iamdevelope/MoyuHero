
package xbean;

public interface Equip extends xdb.Bean {
	public Equip copy(); // deep clone
	public Equip toData(); // a Data instance
	public Equip toBean(); // a Bean instance
	public Equip toDataIf(); // a Data instance If need. else return this
	public Equip toBeanIf(); // a Bean instance If need. else return this

	public int getKey(); // 物品唯一ID
	public int getEquipid(); // 物品ID
	public int getQianghualevel(); // 强化等级
	public int getAttr1odds(); // 属性1几率
	public int getAttr2odds(); // 属性2几率
	public int getQhadd(); // 强化增加几率

	public void setKey(int _v_); // 物品唯一ID
	public void setEquipid(int _v_); // 物品ID
	public void setQianghualevel(int _v_); // 强化等级
	public void setAttr1odds(int _v_); // 属性1几率
	public void setAttr2odds(int _v_); // 属性2几率
	public void setQhadd(int _v_); // 强化增加几率
}
