
package xbean;

public interface ItemNumLimit extends xdb.Bean {
	public ItemNumLimit copy(); // deep clone
	public ItemNumLimit toData(); // a Data instance
	public ItemNumLimit toBean(); // a Bean instance
	public ItemNumLimit toDataIf(); // a Data instance If need. else return this
	public ItemNumLimit toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, Integer> getItemnums(); // 每天使用道具次数s
	public java.util.Map<Integer, Integer> getItemnumsAsData(); // 每天使用道具次数s
	public long getTime(); // 最后更新时间，清除用

	public void setTime(long _v_); // 最后更新时间，清除用
}
