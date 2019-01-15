
package xbean;

public interface BuffAgent extends xdb.Bean {
	public BuffAgent copy(); // deep clone
	public BuffAgent toData(); // a Data instance
	public BuffAgent toBean(); // a Bean instance
	public BuffAgent toDataIf(); // a Data instance If need. else return this
	public BuffAgent toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.Buff> getBuffs(); // key为buffId
	public java.util.Map<Integer, xbean.Buff> getBuffsAsData(); // key为buffId

}
