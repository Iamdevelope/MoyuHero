
package xbean;

public interface roledhmap extends xdb.Bean {
	public roledhmap copy(); // deep clone
	public roledhmap toData(); // a Data instance
	public roledhmap toBean(); // a Bean instance
	public roledhmap toDataIf(); // a Data instance If need. else return this
	public roledhmap toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.roleduihuanlq> getDhmap(); // 兑换礼券计数列表
	public java.util.Map<Integer, xbean.roleduihuanlq> getDhmapAsData(); // 兑换礼券计数列表

}
