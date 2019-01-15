
package xbean;

public interface Troops extends xdb.Bean {
	public Troops copy(); // deep clone
	public Troops toData(); // a Data instance
	public Troops toBean(); // a Bean instance
	public Troops toDataIf(); // a Data instance If need. else return this
	public Troops toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.Troop> getTroops(); // 
	public java.util.List<xbean.Troop> getTroopsAsData(); // 

}
