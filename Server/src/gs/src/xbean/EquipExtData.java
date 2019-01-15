
package xbean;

public interface EquipExtData extends xdb.Bean {
	public EquipExtData copy(); // deep clone
	public EquipExtData toData(); // a Data instance
	public EquipExtData toBean(); // a Bean instance
	public EquipExtData toDataIf(); // a Data instance If need. else return this
	public EquipExtData toBeanIf(); // a Bean instance If need. else return this

	public int getLevel(); // 强化等级
	public int getInit1(); // 基础属性1，默认-1
	public int getInit2(); // 基础属性2，默认-1
	public int getInit3(); // 基础属性3，默认-1
	public int getAttr1(); // 附属属性1，默认-1
	public int getAttr2(); // 附属属性2，默认-1
	public int getAttr3(); // 附属属性3，默认-1
	public int getAttr4(); // 附属属性4，默认-1

	public void setLevel(int _v_); // 强化等级
	public void setInit1(int _v_); // 基础属性1，默认-1
	public void setInit2(int _v_); // 基础属性2，默认-1
	public void setInit3(int _v_); // 基础属性3，默认-1
	public void setAttr1(int _v_); // 附属属性1，默认-1
	public void setAttr2(int _v_); // 附属属性2，默认-1
	public void setAttr3(int _v_); // 附属属性3，默认-1
	public void setAttr4(int _v_); // 附属属性4，默认-1
}
