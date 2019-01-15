
package xbean;

public interface Troop extends xdb.Bean {
	public Troop copy(); // deep clone
	public Troop toData(); // a Data instance
	public Troop toBean(); // a Bean instance
	public Troop toDataIf(); // a Data instance If need. else return this
	public Troop toBeanIf(); // a Bean instance If need. else return this

	public int getTroopnum(); // 战队编号
	public int getTrooptype(); // 战队类型，1为前2后3，2为前3后2
	public int getLocation1(); // 0没装
	public int getLocation2(); // 0没装
	public int getLocation3(); // 0没装
	public int getLocation4(); // 0没装
	public int getLocation5(); // 0没装
	public int getSh1(); // 神魂1号，0没装
	public int getSh2(); // 神魂2号，0没装
	public int getSh3(); // 神魂3号，0没装
	public int getSh4(); // 神魂4号，0没装

	public void setTroopnum(int _v_); // 战队编号
	public void setTrooptype(int _v_); // 战队类型，1为前2后3，2为前3后2
	public void setLocation1(int _v_); // 0没装
	public void setLocation2(int _v_); // 0没装
	public void setLocation3(int _v_); // 0没装
	public void setLocation4(int _v_); // 0没装
	public void setLocation5(int _v_); // 0没装
	public void setSh1(int _v_); // 神魂1号，0没装
	public void setSh2(int _v_); // 神魂2号，0没装
	public void setSh3(int _v_); // 神魂3号，0没装
	public void setSh4(int _v_); // 神魂4号，0没装
}
