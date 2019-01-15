
package xbean;

public interface monthcards extends xdb.Bean {
	public monthcards copy(); // deep clone
	public monthcards toData(); // a Data instance
	public monthcards toBean(); // a Bean instance
	public monthcards toDataIf(); // a Data instance If need. else return this
	public monthcards toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.monthcard> getRolemonthcards(); // 
	public java.util.Map<Integer, xbean.monthcard> getRolemonthcardsAsData(); // 

}
