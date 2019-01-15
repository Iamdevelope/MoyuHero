
package xbean;

public interface Bag extends xdb.Bean {
	public Bag copy(); // deep clone
	public Bag toData(); // a Data instance
	public Bag toBean(); // a Bean instance
	public Bag toDataIf(); // a Data instance If need. else return this
	public Bag toBeanIf(); // a Bean instance If need. else return this

	public long getMoney(); // 
	public int getCapacity(); // 
	public int getNextid(); // 
	public java.util.Map<Integer, xbean.Item> getItems(); // 
	public java.util.Map<Integer, xbean.Item> getItemsAsData(); // 
	public java.util.List<Integer> getRemovedkeys(); // 
	public java.util.List<Integer> getRemovedkeysAsData(); // 

	public void setMoney(long _v_); // 
	public void setCapacity(int _v_); // 
	public void setNextid(int _v_); // 
}
