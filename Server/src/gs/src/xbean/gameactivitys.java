
package xbean;

public interface gameactivitys extends xdb.Bean {
	public gameactivitys copy(); // deep clone
	public gameactivitys toData(); // a Data instance
	public gameactivitys toBean(); // a Bean instance
	public gameactivitys toDataIf(); // a Data instance If need. else return this
	public gameactivitys toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.gameactivity> getGameactivitymap(); // 
	public java.util.Map<Integer, xbean.gameactivity> getGameactivitymapAsData(); // 

}
