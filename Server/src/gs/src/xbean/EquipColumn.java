
package xbean;

public interface EquipColumn extends xdb.Bean {
	public EquipColumn copy(); // deep clone
	public EquipColumn toData(); // a Data instance
	public EquipColumn toBean(); // a Bean instance
	public EquipColumn toDataIf(); // a Data instance If need. else return this
	public EquipColumn toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.Equip> getEquips(); // 
	public java.util.List<xbean.Equip> getEquipsAsData(); // 
	public int getNextkey(); // 

	public void setNextkey(int _v_); // 
}
